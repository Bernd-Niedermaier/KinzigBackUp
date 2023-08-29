using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Forms;

namespace KinzigBackUp
{
    //Abstrakte Basisklasse für alle Backuptypen
    public abstract class Backup
    {
        protected string ZuSichern;
        protected string BackupZiel;
        protected string BackupOrdner = GR.BackupOrdner;
        protected string BackupTemp;
        protected string BackupPathComplete;    
        protected string BackupID;
        protected string LastDoneFilePath;
        protected string ToDoFilePath;
        protected static string DoneFlag = "Complete";
        protected List<string> ToDoOrdnerLeafs = new List<string>();   //Die zu kopierenden Ordner
        protected List<string> ToDoFiles = new List<string>();           //Die zu kopierenden Dateien (Alle Dateien im zu sichernden Pfad)
        protected volatile string LastDone = string.Empty;               //Beeinhaltet die zuletzt ausgeführte Sicherung
        protected List<string> AllFoldersInToSave = new List<string>();  //Alle Ordner in dem zu sichernden Pfad

        protected ManualResetEvent GateDataInfrastructure = new ManualResetEvent(false);    //Gate das offen ist wenn die Infrastruktur für die Daten in Ordnung ist
        protected ManualResetEvent GateCallToClose = new ManualResetEvent(false);           //Gate das offen ist wenn das Backup geschlossen werden soll
        protected ManualResetEvent GateBackupDone = new ManualResetEvent(false);            //Gate das offen ist wenn das Backup erfolgreich angelegt wurde

        public Backup(Backup_Aufgabe aufgabe)
        {
            this.ZuSichern = aufgabe.ZuSichern;
            this.BackupZiel = aufgabe.BackupZiel;
            BackupPathComplete = this.BackupZiel + BackupOrdner;
            BackupID = aufgabe.ID;
            LastDoneFilePath = GR.BackupToDoOrdner + "\\" + this.BackupID + "LastDone.bin";
            ToDoFilePath = GR.BackupToDoOrdner + "\\" + this.BackupID + "ToDo.bin";
            BackupTemp = BackupPathComplete + "Temp";
            Thread PfadeErreichbarThread = new Thread(DataInfrastructure);
            PfadeErreichbarThread.Start();           
        }


        /// <summary>
        /// Die Start Methode (wird vererbt und muss überschrieben werden)<br/>
        /// Wird im Konstruktor einer untergeordneten Klasse an einen Thread übergeben und <br/>
        /// führt die standard Backup aktionen der untergeordneten Klasse aus.
        /// </summary>
        protected abstract void BUStart();

        /// <summary>
        /// Lädt den letzten Standpunkt des Backups, die Dateien hierfür existieren nur wenn das Bakup abgebrochen wurde
        /// </summary>
        protected void LoadLastToDo()
        {
            try
            {
                if (File.Exists(LastDoneFilePath) && File.Exists(ToDoFilePath))
                {
                    using (FileStream fileStream = new FileStream(LastDoneFilePath, FileMode.Open, FileAccess.Read))
                    {
                        using (BinaryReader reader = new BinaryReader(fileStream))
                        {
                            LastDone = reader.ReadString();
                        }
                    }

                    using (FileStream fileStream = new FileStream(ToDoFilePath, FileMode.Open, FileAccess.Read))
                    {
                        using (BinaryReader reader = new BinaryReader(fileStream))
                        {
                            int FolderCount = reader.ReadInt32();
                            bool NoFolders = false;
                            //Ist LastDone ein Verweis auf eine Datei können die Ordner aus der ToDoListe übersprungen werden
                            if (File.Exists(LastDone))
                            {
                                NoFolders = true;
                                for (int i = 0; (i < FolderCount) && (fileStream.Position < fileStream.Length); i++)
                                {
                                    reader.ReadString();
                                }
                            }
                            if (fileStream.Position >= fileStream.Length)//Ist überhaupt noch Platz zum lesen in der Datei?
                                return;
                            //Den Anschluss finden an LastDone
                            string step;
                            do
                            {
                                step = reader.ReadString();
                                if (NoFolders == false)
                                    FolderCount--;
                            } while ((step != LastDone) && (fileStream.Position < fileStream.Length));
                            if (fileStream.Position >= fileStream.Length)//Ist überhaupt noch Platz zum lesen in der Datei?
                                return;
                            //Die ToDoOrdner Liste befüllen
                            ToDoOrdnerLeafs.Clear();
                            if (NoFolders == false)
                                for (int i = 0; (i < FolderCount) && (fileStream.Position < fileStream.Length); i++)
                                    ToDoOrdnerLeafs.Add(reader.ReadString());
                            //Die ToDoFile Liste befüllen
                            ToDoFiles.Clear();
                            while (fileStream.Position < fileStream.Length)
                            {
                                ToDoFiles.Add(reader.ReadString());
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show($"Ein Fehler ist in der Methode LoadLastToDo aufgetreten.\nBackupID: {BackupID}");
                System.Windows.MessageBox.Show(e.Message);
                return;
            }
        }

        /// <summary>
        /// Füllt die ToDoListen mit Ordnerpfaden und Dateipfaden und legt eine Sicherung für die ToDoListen an<br/>
        /// <br/>
        /// Die Methode nutzt <seealso cref="DataInfrastructureCheck"/> und sollte daher nur in eigenständigen Threads aufgerufen werden!
        /// </summary>
        protected virtual void ToDoAnlegen()
        {
            try
            {
                AllFoldersInToSave.Add(ZuSichern);
                GetToDoOrdnerLeafsAndAllFoldersInToSave(ZuSichern);
                using (FileStream fileStream = new FileStream(ToDoFilePath, FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fileStream))
                    {
                        DataInfrastructureCheck();
                        bw.Write(ToDoOrdnerLeafs.Count);  //Die Anzahl der Elemente wird für die Ordner eingetragen um den Wechsel zu den Dateipfaden zu ermitteln
                        foreach (string ordner in ToDoOrdnerLeafs)
                        {
                            DataInfrastructureCheck();
                            bw.Write(ordner);
                        }
                    }
                }
                GetToDoFiles();
                using (FileStream fileStream = new FileStream(ToDoFilePath, FileMode.Append))
                {
                    using (BinaryWriter bw = new BinaryWriter(fileStream))
                    {
                        foreach (string Datei in ToDoFiles)
                        {
                            DataInfrastructureCheck();
                            bw.Write(Datei);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ein Fehler ist in der Methode ToDoAnlegen aufgetreten.\nBackupID: {BackupID}");
                System.Windows.MessageBox.Show(ex.Message);
                return;
            }
        }
        /// <summary>
        /// Organisiert die ToDoOrdnerLeafs Liste und AllFoldersInToSave Liste<br/>
        /// <br/>
        /// In die ToDoOrdnerLeafs Liste werden alle Ordnerpfade eingefügt die ausgehend von einer Pfadroot<br/>
        /// an der Spitze liegen<br/>
        /// <br/>
        /// In die AllFoldersInToSave Liste werden alle Unterordnerpfade eingefügt welche sich im Pfad des Parameters:<paramref name="Sichern"/><br/>
        /// befinden<br/>
        /// <br/>
        /// Die Methode nutzt <seealso cref="DataInfrastructureCheck"/> und sollte daher nur in eigenständigen Threads aufgerufen werden!
        /// </summary>
        /// <param name="Sichern">Der zu sichernde Ordnerpfad</param>
        protected void GetToDoOrdnerLeafsAndAllFoldersInToSave(string Sichern)
        {
            DataInfrastructureCheck();
            string[] SubFolders = Directory.GetDirectories(Sichern);
            if (SubFolders.Length == 0)
                ToDoOrdnerLeafs.Add(Sichern);
            foreach (string Unterordner in SubFolders)
            {
                AllFoldersInToSave.Add(Unterordner);
                GetToDoOrdnerLeafsAndAllFoldersInToSave(Unterordner);
            }
        }

        /// <summary>
        /// Organisiert die ToDoFiles Liste<br/>
        /// In die Liste werden Dateipfade eingefügt, sofern die Datei im Backuppfad bereits existiert aber ihr Ursprung neuer ist oder<br/>
        /// wenn die Datei im Backup noch nicht existiert<br/>
        /// <br/>
        /// Die Methode sollte wenn benötigt nach der Methode <seealso cref="GetToDoOrdnerLeafsAndAllFoldersInToSave(string)"/> aufgerufen werden<br/>
        /// Die Methode nutzt <seealso cref="DataInfrastructureCheck"/> und sollte daher nur in eigenständigen Threads aufgerufen werden!
        /// </summary>
        protected void GetToDoFiles()
        {
            foreach (string Ordner in AllFoldersInToSave)
            {
                DataInfrastructureCheck();
                foreach (string Datei in Directory.GetFiles(Ordner))
                {
                    string BackupDateiVergleich = BUPathBuilder(Datei);
                    DataInfrastructureCheck();
                    if ((File.Exists(BackupDateiVergleich) && (File.GetCreationTime(Datei) > File.GetCreationTime(BackupDateiVergleich))) || !File.Exists(BackupDateiVergleich))
                        ToDoFiles.Add(Datei);
                }
            }
        }

        /// <summary>
        /// Legt die Dateien und Ordner im BackupZiel an<br/>
        /// <br/>
        /// Die Methode nutzt <seealso cref="DataInfrastructureCheck"/> und sollte daher nur in eigenständigen Threads aufgerufen werden!
        /// </summary>
        protected void BackupAnlegen()
        {
            try
            {
                //Zuerst die Ordnersturktur anlegen
                DataInfrastructureCheck();
                Directory.CreateDirectory(BUPathBuilder(ZuSichern));
                Directory.CreateDirectory(BackupTemp);
                for (int i = 0; i < ToDoOrdnerLeafs.Count; i++)
                {
                    string Unterordner = ToDoOrdnerLeafs[i];
                    DataInfrastructureCheck();
                    Directory.CreateDirectory(BUPathBuilder(Unterordner));
                    if (Directory.Exists(BUPathBuilder(Unterordner)))
                        SetAndSaveLastDone(Unterordner);
                    else
                        i--;
                }

                //Anschließend alle Dateien in den Ordner kopieren
                for (int i = 0; i < ToDoFiles.Count; i++)
                {
                    DataInfrastructureCheck();
                    try
                    {
                        string Datei = ToDoFiles[i];
                        bool match = false;
                        while (IsFileReady(Datei)) ;
                        File.Copy(Datei, BackupTemp + "\\" + BackupID + Path.GetExtension(Datei), true);    //Temporäre Kopie erstellen
                        //Die Daten  teilweise Binär abgleichen (Original und Temporäre Datei)
                        using(FileStream OriginalDatei = new FileStream(Datei,FileMode.Open,FileAccess.Read,FileShare.ReadWrite), BackupDatei = new FileStream(BackupTemp + "\\" + BackupID + Path.GetExtension(Datei), FileMode.Open, FileAccess.Read, FileShare.None))
                        {
                            using(BinaryReader ReaderOriginal = new BinaryReader(OriginalDatei), ReaderBackup = new BinaryReader(BackupDatei))
                            {
                                long onepercent = OriginalDatei.Length / 100;
                                if (onepercent == 0)
                                    onepercent = 4;                                
                              for(int j = 1; j < 100 ; j++)
                                {
                                        DataInfrastructureCheck();
                                        int original = OriginalDatei.ReadByte();
                                        int backup = BackupDatei.ReadByte();

                                        if (!(original == backup) || (OriginalDatei.Length != BackupDatei.Length))
                                        {
                                            match = false;
                                            break;
                                        }
                                        else
                                            match = true;
                                        if (original == -1 && backup == -1)
                                            break;

                                        OriginalDatei.Position = onepercent * j;
                                        BackupDatei.Position = OriginalDatei.Position;
                                }
                            }
                        }
                        if (match == false)
                            throw new IOException("Temp und Original stimmen nicht überein!");
                        //Falls bereits eine Kopie vorhanden, diese löschen
                        if (File.Exists(BUPathBuilder(Datei)))
                        {
                            while (IsFileReady(BUPathBuilder(Datei))) ;
                            File.Delete(BUPathBuilder(Datei));
                        }
                        //Die temporäre Datei an ihre Position verschieben
                        File.Move(BackupTemp + "\\" + BackupID + Path.GetExtension(Datei), BUPathBuilder(Datei));
                        if (!File.Exists(BUPathBuilder(Datei)))
                            throw new FileNotFoundException();
                        else
                            SetAndSaveLastDone(Datei);
                    }
                    catch (IOException e)
                    {
                        System.Windows.MessageBox.Show(e.Message);
                        i--;
                    }
                }
                //Wenn der Block bis hier ausgeführt wurde ist das Backup erfolgreich abgeschlossen und LastDone wird auf "Complete" gesetzt
                SetAndSaveLastDone(DoneFlag);
                while(Delete(LastDoneFilePath));
                while(Delete(ToDoFilePath));
                ToDoOrdnerLeafs.Clear();
                ToDoFiles.Clear();
                AllFoldersInToSave.Clear();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Es ist ein Fehler in der Methode BackupAnlegen() geschehen.\nBackupID: {BackupID}");
                System.Windows.MessageBox.Show(ex.Message);
                return;
            }
        }

        /// <summary>
        /// Überprüft ob eine Datei bereit ist oder gerade von einem anderen Prozess beansprucht wird<br/>
        /// <br/>
        /// Beeinhaltet <see cref="DataInfrastructureCheck"/> sollte daher nur in Methoden angewandt werden die in eigenen Threads eingesetzt werden.
        /// </summary>
        /// <param name="Path">Der Pfad zur Datei</param>
        /// <returns>false wenn die Datei bereit ist<br/>true wenn die Datei nicht bereit ist</returns>
        protected bool IsFileReady(string Path)
        {
            DataInfrastructureCheck();
            try
            {
                using(FileStream stream = new FileStream(Path,FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {                    
                    return false;
                }
            }
            catch (IOException)
            {
                return true;
            }
        }

        /// <summary>
        /// Überprüft ob ein Ordner oder eine Datei existieren
        /// </summary>
        /// <param name="Path">Der Pfad zum Ordner oder der Datei</param>
        /// <returns>true wenn der Ordner oder die Datei gefunden wurden<br/>false wenn der Ordner oder die Datei nicht gefunden wurden</returns>
        protected bool PathExists(string Path)
        {
            if (System.IO.Path.HasExtension(Path))
            {
                if(File.Exists(Path))
                    return true;
                else
                    return false;
            }
            else if(Directory.Exists(Path))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Erstellt oder überschreibt Dateien oder legt eine Ordnerstruktur an wenn diese noch nicht existiert.<br/>
        /// Beachte das immer der Ursprungspfad angewendet wird. Die Kopie erfolgt automatisch im Backup Ordner<br/>
        /// <br/>
        /// Beeinhaltet <see cref="DataInfrastructureCheck"/> sollte daher nur in Methoden angewandt werden die in eigenen Threads eingesetzt werden.
        /// </summary>
        /// <param name="Path">Der zu erstellende Pfad</param>
        /// <returns>Gibt true zurück wenn der Vorgang fehlgeschlagen ist<br/>Gibt false zurück wenn der Vorgang erfolgreich war.</returns>
        protected bool CreateInBackup(string Path)
        {
            DataInfrastructureCheck();
            try
            {
                if(System.IO.Path.HasExtension(Path))
                {
                    File.Copy(Path,BUPathBuilder(Path),true);
                    return false;
                }
                else
                {
                    Directory.CreateDirectory(BUPathBuilder(Path));
                    return false;
                }
            }
            catch (IOException)
            {
                return true;
            }
        }

        /// <summary>
        /// Verschiebt Dateien und Ordner von einem Pfad(OldPath) zu einem neuen Pfad(NewPath)<br/>
        /// Mögliche Unterordner und in den Ordnern befindlichen Dateien werden ebenfalls verschoben.<br/>
        /// <br/>
        /// Beeinhaltet <see cref="DataInfrastructureCheck"/> sollte daher nur in Methoden angewandt werden die in eigenen Threads eingesetzt werden.
        /// </summary>
        /// <param name="OldPath">Alter Pfadname</param>
        /// <param name="NewPath">Neuer Pfadname</param>
        /// <returns>Gibt true zurück wenn das verschieben fehlgeschlagen ist<br/>false wenn es erfolgreich abgeschlossen werden konnte</returns>
        protected bool Move(string OldPath, string NewPath)
        {
            DataInfrastructureCheck();
            try
            {
                if(Path.HasExtension(OldPath) && Path.HasExtension(NewPath))
                {
                    File.Move(OldPath, NewPath);
                    return false;
                }
                else
                {
                    Directory.Move(OldPath, NewPath);
                    return false;
                }
            }
            catch(IOException)
            {
                return true;
            }
        }

        /// <summary>
        /// Löscht Dateien oder Ordner(Sofern diese keine Dateien oder Ordner beinhalten)<br/>
        /// <br/>
        /// Beeinhaltet <see cref="DataInfrastructureCheck"/> sollte daher nur in Methoden angewandt werden die in eigenen Threads eingesetzt werden.
        /// </summary>
        /// <param name="Path">Zielpfad des Ordners oder der Datei</param>
        /// <returns>false wenn der Vorgang erfolgreich war<br/>true wenn der Vorgang fehlgeschlagen ist</returns>
        protected bool Delete(string Path)
        {
            DataInfrastructureCheck();
            try
            {
                if (System.IO.Path.HasExtension(Path))
                {
                    File.Delete(Path);
                    return false;
                }
                else
                {
                    Directory.Delete(Path);
                    return false;
                }
            }
            catch
            {
                return true;
            }
        }

        /// <summary>
        /// Erstellt eine Sicherungsdatei für die zuletzt ausgeführte Sicherung und setzt die Variable LastDone <br/>
        /// auf den zuletzt ausgeführten Sicherungspfad<br/>
        /// <br/>
        /// Wenn das Backup erfolgreich angelegt wurde sollte diese Methode mit dem Parameter DoneFlag aufgerufen werden<br/>
        /// Die Methode nutzt <seealso cref="DataInfrastructureCheck"/> und sollte daher nur in eigenständigen Threads aufgerufen werden!
        /// </summary>
        /// <param name="Last">Der zuletzt ausgeführte Pfad</param>       
        protected void SetAndSaveLastDone(string Last)
        {
            DataInfrastructureCheck();
            LastDone = Last;
            using (FileStream fileStream = new FileStream(LastDoneFilePath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fileStream))
                {
                    bw.Write(LastDone);
                }
            }
        }

        /// <summary>
        /// Baut den vollständigen Backuppfad aus dem Backupzielpfad(BackupPathComplete) <br/>
        /// und dem zu sichernden Ordner oder Datei
        /// </summary>
        /// <param name="PfadOrdnerOderDatei">Der Vollständige Pfad des aktuell zu sichernden Ordners oder Datei</param>
        /// <returns>Der vollständige Zielpfad im Backup</returns>
        protected string BUPathBuilder(string PfadOrdnerOderDatei)
        {
            return BackupPathComplete + PfadOrdnerOderDatei.Substring(Path.GetPathRoot(PfadOrdnerOderDatei).Length);
        }

        /// <summary>
        ///Wird an einen Thread übergeben und setzt das Gate für die Dateninfrastruktur auf offen wenn alles in Ordnung ist.<br/>
        ///Kann erweitert werden wenn weitere Faktoren betreffend der Pfade oder Drives überwacht werden müssen
        /// </summary>
        protected void DataInfrastructure()
        {
            string DriveLetterFrom = Path.GetPathRoot(ZuSichern);
            string DriveLetterTo = Path.GetPathRoot(BackupZiel);

            while (!GateCallToClose.WaitOne(0))
            {
                if (GR.GetAvailableDrives().Contains<string>(DriveLetterFrom) && GR.GetAvailableDrives().Contains<string>(DriveLetterTo) && Directory.Exists(ZuSichern))
                {
                    GateDataInfrastructure.Set();
                }
                else
                {
                    GateDataInfrastructure.Reset();
                }
                Thread.Sleep(100);
            }
            GateDataInfrastructure.Reset();
        }

        /// <summary>
        ///Kann innerhalb von Methoden als Gate eingefügt werden das überprüft ob die notwendigen Pfade erreichbar sind und<br/>
        ///ob kein Aufruf zum schließen vorhanden ist, der Aufruf zum schließen (CallToClose) setzt die Pfade auch auf <br/>
        ///unerreichbar und führt dazu das der Thread beim erreichen dieser Methode geschlossen werden kann.<br/>
        ///Die Methode sollte nur in Methoden aufgerufen werden welche an eigene Threads übergeben werden!
        /// </summary>
        protected void DataInfrastructureCheck()
        {
            while (!GateDataInfrastructure.WaitOne(0))//Wenn das Gate geschlossen ist wird diese Schleife ausgeführt
            {
                if (GateCallToClose.WaitOne(100))
                    Thread.CurrentThread.Abort();

                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Fordert alle laufenden Threads die innerhalb einer Instanz, von den untergeordneten Klassen von Backup,<br/>
        /// gestartet wurden auf, sich zu schließen sofern diese auf das öffnen des GateCallToClose warten.<br/>
        /// <br/>
        /// Alle Threads die von einer Backup Klasse gestartet werden sollten daher auf die Antwort des GateCallToClose warten,<br/>
        /// um alle Programmbezogenen Threads korrekt zu beenden
        /// </summary>        
        public void CallToClose()
        {
            GateCallToClose.Set();
        }
       
    }





    public class BUSpiegeln : Backup 
    {
        private List<string> AllFilesInBackup;                      //Alle Dateien im Zielordner/Backupordner um den Vergleich auszuführen
        private List<string> AllFoldersInBackup;                    //Alle Ordner im Zielordner/Backupordner
        private volatile List<string> SpiegelnToDoFolderPaths;            //Eine Liste die vom Filewatcher organisiert wird
        private volatile List<string> SpiegelnToDoFolderPathsRenamed;     //Eine Liste die vom Filewatcher organisiert wird und die umbenannten Dateien enthält
        private volatile List<int> SpiegelnToDoFolderActions;             //Eine Liste die vom Filewatcher organisiert wird und die Änderungsaktionen(ToDoActions) enthält
        private volatile List<string> SpiegelnToDoFilePaths;            //Eine Liste die vom Filewatcher organisiert wird
        private volatile List<string> SpiegelnToDoFilePathsRenamed;     //Eine Liste die vom Filewatcher organisiert wird und die umbenannten Dateien enthält
        private volatile List<int> SpiegelnToDoFileActions;             //Eine Liste die vom Filewatcher organisiert wird und die Änderungsaktionen(ToDoActions) enthält
        FileSystemWatcher Spiegel;
        private enum ToDoActions  { Erstellt,Geändert,Gelöscht,Umbenannt };
        private ManualResetEvent FolderSpiegelHatArbeit = new ManualResetEvent(false);    //Gate das geöffnet wird sobald der Filewatcher Änderungen gefunden hat
        private ManualResetEvent FileSpiegelHatArbeit = new ManualResetEvent(false);
        private ManualResetEvent FolderSpiegelArbeitet = new ManualResetEvent(false);
        private ManualResetEvent FileSpiegelArbeitet = new ManualResetEvent(false);




        public BUSpiegeln(Backup_Aufgabe aufgabe) : base(aufgabe) 
        {
            AllFilesInBackup = new List<string>();
            AllFoldersInBackup = new List<string>();
            SpiegelnToDoFolderPaths = new List<string>();
            SpiegelnToDoFolderPathsRenamed = new List<string>();
            SpiegelnToDoFolderActions = new List<int>();
            SpiegelnToDoFilePaths = new List<string>();
            SpiegelnToDoFilePathsRenamed = new List<string>();
            SpiegelnToDoFileActions = new List<int>();
            
            Thread BackupAnlegen = new Thread(BUStart);
            BackupAnlegen.Start();

            Spiegel = new FileSystemWatcher()
            {
                Path = ZuSichern,
                Filter = "*.*",
                EnableRaisingEvents = true,
                IncludeSubdirectories = true,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Attributes | NotifyFilters.Size | NotifyFilters.LastWrite | NotifyFilters.CreationTime | NotifyFilters.LastAccess | NotifyFilters.Security
            };
            Spiegel.Created += OnCreated;
            Spiegel.Changed += OnChanged;
            Spiegel.Deleted += OnDeleted;
            Spiegel.Renamed += OnRenamed;

            Thread SpiegelManagerThread = new Thread(SpiegelManager);
            SpiegelManagerThread.Start();
            Thread SpiegelnFolderThread = new Thread(SpiegelnFolder);
            SpiegelnFolderThread.Start();
            Thread SpiegelnFileThread = new Thread(SpiegelnFile);
            SpiegelnFileThread.Start();
        }

        private void OnCreated (object sender, FileSystemEventArgs e)
        {
            if (Path.HasExtension(e.FullPath))
            {
                SpiegelnToDoFilePaths.Add(e.FullPath);
                SpiegelnToDoFileActions.Add((int)ToDoActions.Erstellt);
            }
            else
            {
                SpiegelnToDoFolderPaths.Add(e.FullPath);
                SpiegelnToDoFolderActions.Add((int)ToDoActions.Erstellt);
            }
        }
        private void OnChanged (object sender, FileSystemEventArgs e)
        {
            e.ChangeType
            if (Path.HasExtension(e.FullPath))
            {
                SpiegelnToDoFilePaths.Add(e.FullPath);
                SpiegelnToDoFileActions.Add((int)ToDoActions.Geändert);
            }
            else
            {
                SpiegelnToDoFolderPaths.Add(e.FullPath);
                SpiegelnToDoFolderActions.Add((int)ToDoActions.Geändert);
            }
        }
        private void OnDeleted (object sender, FileSystemEventArgs e)
        {
            if(Path.HasExtension(e.FullPath))
            {
                SpiegelnToDoFilePaths.Add(e.FullPath);
                SpiegelnToDoFileActions.Add((int)ToDoActions.Gelöscht);
            }
            else
            {
                SpiegelnToDoFolderPaths.Add(e.FullPath);
                SpiegelnToDoFolderActions.Add((int)ToDoActions.Gelöscht);
            }
        }
        private void OnRenamed (object sender, RenamedEventArgs e)
        {
            if (Path.HasExtension(e.OldFullPath))
            {
                SpiegelnToDoFilePaths.Add(e.OldFullPath);
                SpiegelnToDoFilePathsRenamed.Add(e.FullPath);
                SpiegelnToDoFileActions.Add((int)ToDoActions.Umbenannt);
            }
            else
            {
                SpiegelnToDoFolderPaths.Add(e.OldFullPath);
                SpiegelnToDoFolderPathsRenamed.Add(e.FullPath);
                SpiegelnToDoFolderActions.Add((int)ToDoActions.Umbenannt);
            }
        }
        private void SpiegelManager()
        {
            while(!GateCallToClose.WaitOne(0))
            {
                while(!GateBackupDone.WaitOne(1000))
                {
                    if(GateCallToClose.WaitOne(500))
                        Thread.CurrentThread.Abort();
                }
                if(SpiegelnToDoFolderPaths.Count > 0)
                {
                    while(FileSpiegelArbeitet.WaitOne(10))
                    {
                        if (GateCallToClose.WaitOne(0))
                            Thread.CurrentThread.Abort();
                    }
                    FileSpiegelHatArbeit.Reset();
                    FolderSpiegelHatArbeit.Set();
                }
                if(SpiegelnToDoFilePaths.Count > 0 && SpiegelnToDoFolderPaths.Count == 0) 
                {
                    while (FolderSpiegelArbeitet.WaitOne(10))
                    {
                        if (GateCallToClose.WaitOne(0))
                            Thread.CurrentThread.Abort();
                    }
                    FolderSpiegelHatArbeit.Reset();
                    FileSpiegelHatArbeit.Set();
                }
            }
            Thread.CurrentThread.Abort();
        }
        private void SpiegelnFile()
        {
            int SkipIndex = 0;
            int SkipIndexRename = 0;
            while (!GateCallToClose.WaitOne(0))
            {
                if(SkipIndex >= (SpiegelnToDoFilePaths.Count - 1))
                {
                    SkipIndex = 0;
                    SkipIndexRename = 0;
                }

                
                DataInfrastructureCheck();
                if (GateBackupDone.WaitOne(0) && FileSpiegelHatArbeit.WaitOne(1000) && SpiegelnToDoFilePaths.Count > 0)    //Folderspiegel wird priorisiert!
                {
                    FileSpiegelArbeitet.Set();
                    string BackupPfad = BUPathBuilder(SpiegelnToDoFilePaths[SkipIndex]);
                    switch (SpiegelnToDoFileActions[SkipIndex])
                    {
                        case (int)ToDoActions.Erstellt:
                            {
                                for(int i = 0; i < 6; i++)
                                {
                                    DataInfrastructureCheck();
                                    try
                                    {
                                        string Datei = SpiegelnToDoFilePaths[SkipIndex];
                                        while (IsFileReady(Datei)) ;
                                        File.Copy(Datei, BackupTemp + "\\" + BackupID + Path.GetExtension(Datei), true);    //Temporäre Kopie erstellen
                                                                                                                            //Die Daten  teilweise Binär abgleichen (Original und Temporäre Datei)
                                        using (FileStream OriginalDatei = new FileStream(Datei, FileMode.Open, FileAccess.Read, FileShare.None), BackupDatei = new FileStream(BackupTemp + "\\" + BackupID + Path.GetExtension(Datei), FileMode.Open, FileAccess.Read, FileShare.None))
                                        {
                                            using (BinaryReader ReaderOriginal = new BinaryReader(OriginalDatei), ReaderBackup = new BinaryReader(BackupDatei))
                                            {
                                                long onepercent = OriginalDatei.Length / 100;
                                                for (int j = 0; OriginalDatei.Position < OriginalDatei.Length; j++)
                                                {
                                                    if (!(OriginalDatei.ReadByte() == BackupDatei.ReadByte()) || (OriginalDatei.Length != BackupDatei.Length))
                                                        throw new FileNotFoundException();
                                                    if (j % 10 == 0)
                                                    {
                                                        OriginalDatei.Position = onepercent * (j / 10);
                                                        BackupDatei.Position = OriginalDatei.Position;
                                                    }
                                                }
                                            }
                                        }
                                        //Falls bereits eine Kopie vorhanden, diese löschen
                                        if (File.Exists(BackupPfad))
                                        {
                                            while (IsFileReady(BackupPfad)) ;
                                            File.Delete(BackupPfad);
                                        }
                                        //Die temporäre Datei an ihre Position verschieben
                                        File.Move(BackupTemp + "\\" + BackupID + Path.GetExtension(Datei), BackupPfad);
                                        if (!File.Exists(BackupPfad))
                                            throw new FileNotFoundException();
                                        else
                                        {
                                            SpiegelnToDoFilePaths.RemoveAt(SkipIndex);
                                            SpiegelnToDoFileActions.RemoveAt(SkipIndex);
                                            break;
                                        }
                                    }
                                    catch (IOException e)
                                    {
                                        System.Windows.MessageBox.Show(e.Message);
                                    }
                                    if (i >= 5)
                                    {
                                        SkipIndex++;
                                        break;
                                    }
                                }
                            }
                            break;
                        case (int)ToDoActions.Geändert:
                            {

                            }
                            break;
                        case (int)ToDoActions.Gelöscht:
                            {
                                for (int i = 0; i < 6; i++)
                                {
                                    try
                                    {
                                        DataInfrastructureCheck();
                                        if (!IsFileReady(BackupPfad))
                                        {
                                            File.Delete(BackupPfad);
                                            if (!File.Exists(BackupPfad))
                                            {
                                                SpiegelnToDoFileActions.RemoveAt(SkipIndex);
                                                SpiegelnToDoFilePaths.RemoveAt(SkipIndex);
                                                break;
                                            }
                                            else
                                                throw new FileNotFoundException();
                                        }
                                    }
                                    catch (IOException e)
                                    {
                                        System.Windows.MessageBox.Show(e.Message);
                                    }
                                    if (i >= 5)
                                    {
                                        SkipIndex++;
                                        break;
                                    }
                                }
                            }
                            break;
                        case (int)ToDoActions.Umbenannt:
                            {
                                string Renamed = BUPathBuilder(SpiegelnToDoFilePathsRenamed[SkipIndexRename]);  //Alter Pfad
                                for (int i = 0; i < 6; i++)
                                {
                                    try
                                    {
                                        DataInfrastructureCheck();
                                        if (File.Exists(BackupPfad) && !File.Exists(Renamed))
                                        {
                                            if (!IsFileReady(BackupPfad))                                            
                                                File.Move(BackupPfad, Renamed);                                                                                            
                                        }
                                        if (!File.Exists(Renamed))
                                            throw new FileNotFoundException();
                                        else
                                        {
                                            SpiegelnToDoFilePaths.RemoveAt(SkipIndex);
                                            SpiegelnToDoFilePathsRenamed.RemoveAt(SkipIndexRename);
                                            SpiegelnToDoFileActions.RemoveAt(SkipIndex);
                                            break;
                                        }
                                    }
                                    catch(IOException e)
                                    {
                                        System.Windows.MessageBox.Show(e.Message);
                                    }
                                    if (i >= 5)
                                    {
                                        SkipIndex++;
                                        SkipIndexRename++;
                                        break;
                                    }
                                }
                            }
                            break;
                        default:
                            {

                            }
                            break;
                    } 
                    FileSpiegelArbeitet.Reset();                    
                }
                if (SpiegelnToDoFileActions.Count == 0 && SpiegelnToDoFilePaths.Count == 0)
                    FileSpiegelHatArbeit.Reset();
            }
            Thread.CurrentThread.Abort();
        }
        private void SpiegelnFolder()
        {
            int SkipIndex = 0;
            int SkipIndexRename = 0;
            while (!GateCallToClose.WaitOne(0))
            {
                if (SkipIndex >= (SpiegelnToDoFolderPaths.Count - 1))
                {
                    SkipIndex = 0;
                    SkipIndexRename = 0;
                }


                DataInfrastructureCheck();
                if (GateBackupDone.WaitOne(0) && FolderSpiegelHatArbeit.WaitOne(1000) && SpiegelnToDoFolderPaths.Count > 0)    //Folderspiegel wird priorisiert!
                {
                    FolderSpiegelArbeitet.Set();
                    string BackupPfad = BUPathBuilder(SpiegelnToDoFolderPaths[SkipIndex]);
                    switch (SpiegelnToDoFolderActions[SkipIndex])
                    {
                        case (int)ToDoActions.Erstellt:
                            {
                                for (int i = 0; i < 6; i++)
                                {
                                    DataInfrastructureCheck();
                                    try
                                    {
                                        Directory.CreateDirectory(BackupPfad);
                                        if (!Directory.Exists(BackupPfad))
                                            throw new DirectoryNotFoundException();
                                        else
                                        {
                                            SpiegelnToDoFolderPaths.RemoveAt(SkipIndex);
                                            SpiegelnToDoFolderActions.RemoveAt(SkipIndex);
                                            break;
                                        }
                                    }
                                    catch (IOException e)
                                    {
                                        System.Windows.MessageBox.Show(e.Message);
                                    }
                                    if (i >= 5)
                                    {
                                        SkipIndex++;
                                        break;
                                    }
                                }
                            }
                            break;
                        case (int)ToDoActions.Geändert:
                            {
                                for (int i = 0; i < 6; i++)
                                {                                    
                                    try
                                    {
                                        ToDoOrdnerLeafs.Clear();
                                        ToDoFiles.Clear();
                                        AllFoldersInToSave.Clear();
                                        DataInfrastructureCheck();
                                        if(Directory.Exists(BackupPfad))                                        
                                            OrdnerLöschen(BackupPfad);

                                        GetToDoOrdnerLeafsAndAllFoldersInToSave(SpiegelnToDoFolderPaths[SkipIndex]);
                                        GetToDoFiles();

                                        for (int j = 0; j < ToDoOrdnerLeafs.Count; j++)
                                        {
                                            string Unterordner = ToDoOrdnerLeafs[j];
                                            DataInfrastructureCheck();
                                            Directory.CreateDirectory(BUPathBuilder(Unterordner));
                                            if (Directory.Exists(BUPathBuilder(Unterordner)))
                                                continue;
                                            else
                                                j--;
                                        }

                                        for (int j = 0; j < ToDoFiles.Count; j++)
                                        {
                                            DataInfrastructureCheck();
                                            string Datei = ToDoFiles[j];
                                            while (IsFileReady(Datei)) ;
                                            File.Copy(Datei, BackupTemp + "\\" + BackupID + Path.GetExtension(Datei), true);    //Temporäre Kopie erstellen
                                            //Die Daten  teilweise Binär abgleichen (Original und Temporäre Datei)
                                            using (FileStream OriginalDatei = new FileStream(Datei, FileMode.Open, FileAccess.Read, FileShare.None), BackupDatei = new FileStream(BackupTemp + "\\" + BackupID + Path.GetExtension(Datei), FileMode.Open, FileAccess.Read, FileShare.None))
                                            {
                                                using (BinaryReader ReaderOriginal = new BinaryReader(OriginalDatei), ReaderBackup = new BinaryReader(BackupDatei))
                                                {
                                                    long onepercent = OriginalDatei.Length / 100;
                                                    for (int k = 0; OriginalDatei.Position < OriginalDatei.Length; k++)
                                                    {
                                                        if (!(OriginalDatei.ReadByte() == BackupDatei.ReadByte()) || (OriginalDatei.Length != BackupDatei.Length))
                                                            throw new FileNotFoundException();
                                                        if (k % 10 == 0)
                                                        {
                                                            OriginalDatei.Position = onepercent * (k / 10);
                                                            BackupDatei.Position = OriginalDatei.Position;
                                                        }
                                                    }
                                                }
                                            }
                                            //Die temporäre Datei an ihre Position verschieben
                                            File.Move(BackupTemp + "\\" + BackupID + Path.GetExtension(Datei), BUPathBuilder(Datei));
                                            if (!File.Exists(BUPathBuilder(Datei)))
                                                throw new FileNotFoundException();
                                        }  


                                        if (!Directory.Exists(BackupPfad))
                                            throw new DirectoryNotFoundException();
                                        else
                                        {
                                            ToDoOrdnerLeafs.Clear();
                                            ToDoFiles.Clear();
                                            AllFoldersInToSave.Clear();
                                            SpiegelnToDoFolderPaths.RemoveAt(SkipIndex);
                                            SpiegelnToDoFolderActions.RemoveAt(SkipIndex);
                                            break;
                                        }
                                    }
                                    catch (IOException e)
                                    {
                                        System.Windows.MessageBox.Show(e.Message);
                                    }
                                    if (i >= 5)
                                    {
                                        SkipIndex++;
                                        break;
                                    }
                                }
                            }
                            break;
                        case (int)ToDoActions.Gelöscht:
                            {
                                for (int i = 0; i < 6; i++)
                                {
                                    try
                                    {
                                        DataInfrastructureCheck();
                                        if (Directory.Exists(BackupPfad))
                                            OrdnerLöschen(BackupPfad);

                                        if (!Directory.Exists(BackupPfad))
                                        {
                                            SpiegelnToDoFolderActions.RemoveAt(SkipIndex);
                                            SpiegelnToDoFolderPaths.RemoveAt(SkipIndex);
                                            break;
                                        }
                                        else
                                            throw new DirectoryNotFoundException();
                                        
                                    }
                                    catch (IOException e)
                                    {
                                        System.Windows.MessageBox.Show(e.Message);
                                    }
                                    if (i >= 5)
                                    {
                                        SkipIndex++;
                                        break;
                                    }
                                }
                            }
                            break;
                        case (int)ToDoActions.Umbenannt:
                            {
                                string Renamed = BUPathBuilder(SpiegelnToDoFolderPathsRenamed[SkipIndexRename]);
                                for (int i = 0; i < 6; i++)
                                {
                                    try
                                    {
                                        DataInfrastructureCheck();
                                        if (Directory.Exists(BackupPfad) && !Directory.Exists(Renamed))
                                        {
                                            ToDoOrdnerLeafs.Clear();
                                            ToDoFiles.Clear();
                                            AllFoldersInToSave.Clear();
                                            
                                            AllFoldersInToSave.Add(SpiegelnToDoFolderPathsRenamed[SkipIndexRename]);
                                            GetToDoOrdnerLeafsAndAllFoldersInToSave(SpiegelnToDoFolderPathsRenamed[SkipIndexRename]);
                                            GetToDoFiles();

                                            for (int j = 0; j < ToDoOrdnerLeafs.Count; j++)
                                            {
                                                string Unterordner = ToDoOrdnerLeafs[j];
                                                DataInfrastructureCheck();
                                                Directory.CreateDirectory(BUPathBuilder(Unterordner));
                                                if (Directory.Exists(BUPathBuilder(Unterordner)))
                                                    continue;
                                                else
                                                    j--;
                                            }

                                            for (int j = 0; j < ToDoFiles.Count; j++)
                                            {
                                                DataInfrastructureCheck();
                                                string Datei = ToDoFiles[j];
                                                while (IsFileReady(Datei)) ;
                                                File.Copy(Datei, BackupTemp + "\\" + BackupID + Path.GetExtension(Datei), true);    //Temporäre Kopie erstellen
                                                                                                                                    //Die Daten  teilweise Binär abgleichen (Original und Temporäre Datei)
                                                using (FileStream OriginalDatei = new FileStream(Datei, FileMode.Open, FileAccess.Read, FileShare.None), BackupDatei = new FileStream(BackupTemp + "\\" + BackupID + Path.GetExtension(Datei), FileMode.Open, FileAccess.Read, FileShare.None))
                                                {
                                                    using (BinaryReader ReaderOriginal = new BinaryReader(OriginalDatei), ReaderBackup = new BinaryReader(BackupDatei))
                                                    {
                                                        long onepercent = OriginalDatei.Length / 100;
                                                        for (int k = 0; OriginalDatei.Position < OriginalDatei.Length; k++)
                                                        {
                                                            if (!(OriginalDatei.ReadByte() == BackupDatei.ReadByte()) || (OriginalDatei.Length != BackupDatei.Length))
                                                                throw new FileNotFoundException();
                                                            if (k % 10 == 0)
                                                            {
                                                                OriginalDatei.Position = onepercent * (k / 10);
                                                                BackupDatei.Position = OriginalDatei.Position;
                                                            }
                                                        }
                                                    }
                                                }
                                                //Die temporäre Datei an ihre Position verschieben
                                                File.Move(BackupTemp + "\\" + BackupID + Path.GetExtension(Datei), BUPathBuilder(Datei));
                                                if (!File.Exists(BUPathBuilder(Datei)))
                                                    throw new FileNotFoundException();
                                            }
                                            OrdnerLöschen(BackupPfad);
                                        }
                                        if (!Directory.Exists(Renamed) || Directory.Exists(BackupPfad))
                                            throw new DirectoryNotFoundException();
                                        else
                                        {
                                            ToDoOrdnerLeafs.Clear();
                                            ToDoFiles.Clear();
                                            AllFoldersInToSave.Clear();
                                            SpiegelnToDoFolderPaths.RemoveAt(SkipIndex);
                                            SpiegelnToDoFolderPathsRenamed.RemoveAt(SkipIndexRename);
                                            SpiegelnToDoFolderActions.RemoveAt(SkipIndex);
                                            break;
                                        }
                                    }
                                    catch (IOException e)
                                    {
                                        System.Windows.MessageBox.Show(e.Message);
                                    }
                                    if (i >= 5)
                                    {
                                        SkipIndex++;
                                        SkipIndexRename++;
                                        break;
                                    }
                                }
                            }
                            break;
                        default:
                            {

                            }
                            break;
                    }
                    FolderSpiegelArbeitet.Reset();                    
                }
                if (SpiegelnToDoFolderActions.Count == 0 && SpiegelnToDoFolderPaths.Count == 0)
                    FolderSpiegelHatArbeit.Reset();
            }
            Thread.CurrentThread.Abort();
        }

        //Die Methode muss an einen Thread übergeben werden, sie startet und kontrolliert den gesammten Backupablauf
        protected override void BUStart()
        {
            try
            {
                DataInfrastructureCheck();                          //Zunächst die Pfade überprüfen
                LoadLastToDo();                                     //Überprüfen ob es ein abgebrochenes Backup gibt
                if (LastDone == string.Empty)
                    ToDoAnlegen();                                  //Wenn kein altes Backup vorhanden ist wird eine komplette ToDoListe angelegt
                if (LastDone != DoneFlag)
                    BackupAnlegen();
                GetAlleBackupOrdnerUndDateien(BackupPathComplete);
                BackupVergleichen();
                AllFilesInBackup.Clear();
                AllFoldersInBackup.Clear();
                GateBackupDone.Set();
                return;
            }
            catch
            {

            }
            
        }
        //Vergleicht den Inhalt des Backuppfades mit dem Inhalt des zu sichernden Pfades und entfernt Dateien und Ordner die nur im Backuppfad vorhanden sind.
        private void BackupVergleichen()
        {
            try
            {
                foreach (string Datei in AllFilesInBackup)
                {
                    string ZuSichernVergleich = Path.GetPathRoot(ZuSichern) +  Datei.Substring(BackupPathComplete.Length-1);
                    DataInfrastructureCheck();
                    if (!File.Exists(ZuSichernVergleich))
                        File.Delete(Datei);
                }

                foreach (string Unterordner in AllFoldersInBackup)
                {
                    string ZuSichernVergleich = Path.GetPathRoot(ZuSichern) +  Unterordner.Substring(BackupPathComplete.Length - 1);
                    DataInfrastructureCheck();
                    if (!Directory.Exists(ZuSichernVergleich) && Directory.Exists(Unterordner) && Unterordner != BackupTemp)
                        OrdnerLöschen(Unterordner);   
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Es ist ein Fehler in der Methode BackupVergleichen() aufgetreten");
                System.Windows.MessageBox.Show(ex.Message);
                return;
            }
        }
        //Eine Methode die Ordner und ihre Inhalte korrekt löscht, wenn ein Ordner mit Inhalt gelöscht wird, wird standardmäßig eine
        //Ausnahme vom System ausgelöst. Erst wird der Inhalt der Ordner gelöscht und dann der Ordner selbst und dies Recursiv
        protected void OrdnerLöschen(string ordner)
        {
            DataInfrastructureCheck();
            while (Directory.GetDirectories(ordner).Length > 0 || Directory.GetFiles(ordner).Length > 0)
            {
                DataInfrastructureCheck();
                foreach (string Datei in Directory.GetFiles(ordner))
                {
                    DataInfrastructureCheck();
                    if (File.Exists(Datei))
                    {
                        try
                        {
                            using(FileStream fileStream = new FileStream(Datei, FileMode.Open, FileAccess.ReadWrite,FileShare.None))
                            {

                            }
                            File.Delete(Datei);
                        }
                        catch(IOException) 
                        { 

                        }
                    }
                }
                foreach (string Unterordner in Directory.GetDirectories(ordner))
                {
                        OrdnerLöschen(Unterordner);
                }
            }
            if(Directory.Exists(ordner))
                Directory.Delete(ordner);
        }
        //Organisiert die Liste für Alle Backupordner und alle Backupdateien
        private void GetAlleBackupOrdnerUndDateien(string Backup, bool recursiv = true)
        {
            if(recursiv)
            {
                AllFoldersInBackup.Clear();
                AllFilesInBackup.Clear();
                AllFoldersInBackup.Add(BackupPathComplete);
                foreach (string Datei in Directory.GetFiles(BackupPathComplete))
                    AllFilesInBackup.Add(Datei);
            }
            DataInfrastructureCheck();
            foreach (string Unterordner in Directory.GetDirectories(Backup))
            {
                AllFoldersInBackup.Add(Unterordner);
                DataInfrastructureCheck();
                foreach (string Datei in Directory.GetFiles(Unterordner))
                    AllFilesInBackup.Add(Datei);
                GetAlleBackupOrdnerUndDateien(Unterordner, recursiv = false);
            }
        }
    }






    public class BUTerminiert : Backup
    {
        public BUTerminiert(Backup_Aufgabe aufgabe) : base(aufgabe)
        {

        }

        protected override void BUStart()
        {
            throw new NotImplementedException();
        }
    }
}
