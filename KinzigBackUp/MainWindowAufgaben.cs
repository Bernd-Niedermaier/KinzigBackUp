using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KinzigBackUp
{
    public partial class MainWindow : Form
    {
        //Fügt der ListView einträge hinzu für die angelegten Aufgaben
        private void AufgabenListViewItemAdd(Backup_Aufgabe neueAufgabe)
        {
            string[] itemtexts = new string[7];

            //Spalte 1 (Index)
            itemtexts[0] = (GR.AufgabenListe.IndexOf(neueAufgabe) + 1).ToString();
            //Spalte 2 (Zu sichernder Pfad)
            itemtexts[1] = neueAufgabe.ZuSichern;
            //Spalte 3 (Backup Ziel)
            itemtexts[2] = neueAufgabe.BackupZiel;
            //Spalte 4 (Art der Sicherung)
            switch (neueAufgabe.ArtDerSicherung)
            {
                case (int)GR.ArtDerSicherung.Echtzeit_Spiegeln:
                    itemtexts[3] = "Spiegeln";
                    break;
                case (int)GR.ArtDerSicherung.Terminiert:
                    itemtexts[3] = "Terminiert";
                    break;
                default:
                    itemtexts[3] = "Fehler";
                    break;
            }
            //Spalte 5 (Wiederholungen)
            if (neueAufgabe.Forever == true || neueAufgabe.ArtDerSicherung == (int)GR.ArtDerSicherung.Echtzeit_Spiegeln)
            {
                itemtexts[4] = "∞";
            }
            else
            {
                int wiederholungen = 0;
                for (DateTime Ab = neueAufgabe.Ab; Ab < neueAufgabe.Bis; Ab = Ab.AddDays(1))
                {
                    if (neueAufgabe.Wochentage[(int)Ab.DayOfWeek] == true)
                        wiederholungen++;
                }
                itemtexts[4] = wiederholungen.ToString();
            }
            //Spalte 6 (Terminierung)
            if (neueAufgabe.ArtDerSicherung == (int)GR.ArtDerSicherung.Echtzeit_Spiegeln)
                itemtexts[5] = "Echtzeit";
            else
            {
                itemtexts[5] = neueAufgabe.Ab.ToShortDateString() + " - " + neueAufgabe.Bis.ToShortDateString() + " - " + neueAufgabe.Uhr.ToShortTimeString() + " ";
                string[] tagkurz = { "So ", "Mo ", "Di ", "Mi ", "Do ", "Fr ", "Sa " };
                for (int i = 0; i < neueAufgabe.Wochentage.Length; i++)
                {
                    if (neueAufgabe.Wochentage[i] == true)
                        itemtexts[5] = itemtexts[5] + tagkurz[i];
                }
            }
            //Spalte 7 (Backup Verhalten)
            if (neueAufgabe.BackupOverride == true)
                itemtexts[6] = "Überschreiben";
            else if (neueAufgabe.BackupVerhalten == (int)GR.BackUpVerhalten.Indexiert)
                itemtexts[6] = "Indexiert";
            else if (neueAufgabe.BackupVerhalten == (int)GR.BackUpVerhalten.Datum)
                itemtexts[6] = "Datum";



            //Das Item erzeugen und der Liste anfügen
            ListViewItem item = new ListViewItem(itemtexts);
            listView_Aufgaben.Items.Add(item);
        }

        //Legt eine neue Datei für eine Aufgabe an, der Dateiname wird aus dem Listenindex erstellt!
        private void AufgabenListeSpeichern(Backup_Aufgabe aufgabe)
        {
            if (GR.AufgabenListe.IndexOf(aufgabe) != -1)
            {
                using (FileStream fileStream = new FileStream(GR.AufgabenOrdner + "\\" + GR.AufgabenListe.IndexOf(aufgabe) + ".bin", FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fileStream))
                    {
                        bw.Write(aufgabe.ID);
                        bw.Write(aufgabe.ZuSichern);
                        bw.Write(aufgabe.BackupZiel);
                        bw.Write(aufgabe.ArtDerSicherung);
                        bw.Write(aufgabe.Ab.Ticks);
                        bw.Write(aufgabe.Bis.Ticks);
                        bw.Write(aufgabe.Forever);
                        bw.Write(aufgabe.Uhr.Ticks);
                        for (int i = 0; i < aufgabe.Wochentage.Length; i++)
                            bw.Write(aufgabe.Wochentage[i]);
                        bw.Write(aufgabe.BackupOverride);
                        bw.Write(aufgabe.BackupVerhalten);
                    }
                }
            }
            else
            {
                MessageBox.Show("Fehler beim speichern der Aufgabe! Die Aufgabe wurde nicht korrekt indexiert.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        //Zeichnet die Aufgabenliste neu und ändert die gespeicherten Daten nachdem ein Eintrag aus der Aufgabenliste entfernt wurde
        private void AufgabenListeEintragEntfernt(int RemovedIndex)
        {
            GR.AufgabenListe.RemoveAt(RemovedIndex);
            //Die betroffene Datei entfernen
            if (File.Exists(GR.AufgabenOrdner + "\\" + Convert.ToString(RemovedIndex) + ".bin"))
            File.Delete(GR.AufgabenOrdner + "\\" + Convert.ToString(RemovedIndex)+ ".bin"); 

            //Die Daten umbenennen ab dort wo in die Reihe eingegriffen wurde
            for(int i = RemovedIndex; i < Directory.GetFiles(GR.AufgabenOrdner).Length; i++)
            {
                string AlterName = GR.AufgabenOrdner + "\\" + (i+1).ToString() + ".bin";
                string NeuerName = GR.AufgabenOrdner + "\\" + (i).ToString() + ".bin";
                File.Move(AlterName, NeuerName);
            }
            
            //Die Liste neu Zeichnen
            listView_Aufgaben.Items.Clear();
            for(int i =0; i < GR.AufgabenListe.Count; i++)
            {
                AufgabenListViewItemAdd(GR.AufgabenListe[i]);
            }
        }
    }
}
