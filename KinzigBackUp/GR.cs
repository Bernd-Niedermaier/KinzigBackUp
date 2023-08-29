using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KinzigBackUp
{
    //Globale Resourcen
    public static class GR
    {        
        //Wenn ein neues Element in ArtDerSicherung eingefügt wird muss es auch in den Methode
        //ArtDerSicherungToString
        //angepasst werden
       public enum ArtDerSicherung { Echtzeit_Spiegeln , Terminiert }
       public enum BackUpVerhalten { Indexiert, Datum }
        //Eine Liste die alle aktiven Instanzen von Backup enthält
        public static List <Backup> BackupListe { get; } = new List<Backup>();
        //Eine Liste die alle aktiven Instanzen der Backup Aufgaben enthält
        public static List<Backup_Aufgabe> AufgabenListe { get; } = new List<Backup_Aufgabe>();
        public static string AufgabenOrdner { get; } = Application.StartupPath + "\\Aufgaben";
        public static string BackupToDoOrdner { get; } = Application.StartupPath + "\\BackupToDo";
        public static string BackupOrdner = "\\Backup\\";                
        private static volatile string[] AvailableDrives;
        //Dieses Event wird beim Beenden des Programmes geöffnet
        public static ManualResetEvent GateProgramShutDown = new ManualResetEvent(false);

        public static string[] ArtDerSicherungToString()
        {
            string[] Arten = new string[] 
            { 
                ArtDerSicherung.Echtzeit_Spiegeln.ToString(), 
                ArtDerSicherung.Terminiert.ToString() 
            };
            
            return Arten;
        }
        //Sollte nur 1x verwiesen sein und zwar an einen eigenen Thread in der Methode Main
        //Frägt regelmäßig die Verfügbaren Laufwerke ab und weist sie AvailableDrives zu
        //Soll Leistung einsparen damit nicht jede einzelne Backup Instanz nach den Laufwerken abfragt
        public static void AvailableDrivesWatcher()
        {
            List<string> AvailableDrivesListe = new List<string>();
            while (!GateProgramShutDown.WaitOne(0)) 
            {
                AvailableDrivesListe.Clear();
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach(DriveInfo drive in drives)
                {
                    if(drive.IsReady)
                        AvailableDrivesListe.Add(drive.Name);
                }
                GR.AvailableDrives = new string[AvailableDrivesListe.Count];
                GR.AvailableDrives = AvailableDrivesListe.ToArray();
                Thread.Sleep(100);
            }
        }

        public static string[] GetAvailableDrives()
        {
            return AvailableDrives;
        }
    }
}
