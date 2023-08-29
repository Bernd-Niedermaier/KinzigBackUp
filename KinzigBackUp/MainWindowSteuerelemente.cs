using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Text;
using static System.Windows.Forms.ListView;
using System.Security.RightsManagement;

namespace KinzigBackUp
{
    public partial class MainWindow : Form
    {
        readonly ImageList SmallIcons;
       
        
        //Konstruktor
        public MainWindow()
        {
            InitializeComponent();
            //Felder initialisieren
           
            
            //Steuerelemente initialisieren
            for(int i = 0; i < GR.ArtDerSicherungToString().Length; i++)
            {
                comboBox_ArtDerSicherung.Items.Add(GR.ArtDerSicherungToString()[i]);
            }
            comboBox_ArtDerSicherung.SelectedIndex = 0;
            comboBox_VerhaltenBackup.SelectedIndex = 0;
            dateTimePicker_Von.Value = DateTime.Today;
            dateTimePicker_Bis.Value = DateTime.Today.AddDays(7);
            dateTimePicker_Uhr.Value = DateTime.Now;
            SmallIcons = imageList_Icons;

            //Laden und prüfen von Daten                         
            Directory.CreateDirectory(GR.AufgabenOrdner); //Existiert der Aufgabenordner?
            Directory.CreateDirectory(GR.BackupToDoOrdner); //Existiert der BackupToDoOrdner?
            
            //Vorhandene Aufgaben laden und der Aufgabenliste hinzufügen
            foreach (string datei in Directory.GetFiles(GR.AufgabenOrdner))        
            {
                using (FileStream fileStream = new FileStream(datei, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader reader = new BinaryReader(fileStream))
                    {
                        string ID = reader.ReadString();
                        string ZuSichern = reader.ReadString();
                        string BackupZiel = reader.ReadString();
                        int ArtDerSicherung = reader.ReadInt32();
                        DateTime Ab = new DateTime(reader.ReadInt64());
                        DateTime Bis = new DateTime(reader.ReadInt64());
                        bool Forever = reader.ReadBoolean();
                        DateTime Uhr = new DateTime(reader.ReadInt64());
                        bool[] Wochentage = new bool[7];
                        for (int i = 0; i < Wochentage.Length; i++)
                            Wochentage[i] = reader.ReadBoolean();
                        bool BackupOverride = reader.ReadBoolean();
                        int BackupVerhalten = reader.ReadInt32();
                        Backup_Aufgabe backup_Aufgabe = new Backup_Aufgabe(ZuSichern, BackupZiel, ArtDerSicherung, Ab, Bis, Forever, Uhr, Wochentage, BackupOverride, BackupVerhalten,ID);                        
                        GR.AufgabenListe.Insert(Convert.ToInt32(Path.GetFileNameWithoutExtension(datei)), backup_Aufgabe); //Um die Aufgaben an den korrekten Index zu setzen                       
                        AufgabenListViewItemAdd(backup_Aufgabe);                        
                    }
                }
            }

            //Backups Anlegen und gegebenenfalls Daten bereinigen
            foreach(string datei in Directory.GetFiles(GR.BackupToDoOrdner))
            {
                bool AufgabeZuToDoGefunden = false;
                string id = datei;
                while (!int.TryParse(id, out _) && id.Length > 0)
                    id = id.Remove(id.Length - 1, 1);
                foreach(Backup_Aufgabe aufgabe in GR.AufgabenListe)
                {
                    if (aufgabe.ID == id)
                    {
                        AufgabeZuToDoGefunden = true;
                        break;
                    }
                }
                if (AufgabeZuToDoGefunden)
                    continue;
                else                
                    File.Delete(datei);                
            }
            foreach (Backup_Aufgabe aufgabe in GR.AufgabenListe)
            {
                BackupAnlegen(aufgabe);
            }
        }

        //===========Steuerelement Methoden============
        private void PfadDialog(object sender)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK && folderBrowserDialog.SelectedPath != string.Empty)
            {
                Control element;
                element = (Control)sender;
                foreach (Control control in element.Parent.Controls)
                {
                    if (control is TextBox textbox)
                    {
                        textbox.Text = folderBrowserDialog.SelectedPath;
                        folderBrowserDialog.SelectedPath = string.Empty;
                        break;
                    }
                }

            }
        }
        private void Button_ZuSichernPfadDialog_Click(object sender, EventArgs e)
        {
            PfadDialog(sender);
        }
        private void Button_ZielDerSicherungPfadDialog_Click(object sender, EventArgs e)
        {
            PfadDialog(sender);
        }




        private void PfadValidierung(object sender)
        {
            if (sender is TextBox textBox)
            {
                if (Directory.Exists(textBox.Text))
                {

                    textBox.BackColor = Color.White;
                    textBox.ForeColor = Color.Black;
                    foreach (Control element in textBox.Parent.Controls)
                    {
                        if (((element is Button button) && (button.Text == string.Empty)))
                        {
                            button.ImageIndex = 1;
                            break;
                        }
                    }
                    return;
                }
                else
                {
                    textBox.BackColor = Color.Red;
                    textBox.ForeColor = Color.White;
                    foreach (Control element in textBox.Parent.Controls)
                    {
                        if (((element is Button button) && (button.Text == string.Empty)))
                        {
                            button.ImageIndex = 2;
                            break;
                        }
                    }
                    return;
                }
            }
        }
        private void TextBox_ZuSichernderPfad_TextChanged(object sender, EventArgs e)
        {
            PfadValidierung(sender);
        }
        private void TextBox_ZielDerSicherung_TextChanged(object sender, EventArgs e)
        {
            PfadValidierung(sender);
        }



        private void SetButtonStatusToolTip(object sender)
        {
            if (sender is Button button)
            {
                switch (button.ImageIndex)
                {
                    case 1:
                        {
                            toolTip_ButtonStatus.SetToolTip(button, "Der angegebene Pfad wurde akzeptiert.");
                        }
                        break;
                    case 2:
                        {
                            toolTip_ButtonStatus.SetToolTip(button, "Der angegebene Pfad wurde nicht akzeptiert.");
                        }
                        break;
                    default:
                        {
                            toolTip_ButtonStatus.SetToolTip(button, "");
                        }
                        break;
                }
            }
        }
        private void Button_Status2_MouseHover(object sender, EventArgs e)
        {
            SetButtonStatusToolTip(sender);
        }
        private void Button_Status_MouseHover(object sender, EventArgs e)
        {
            SetButtonStatusToolTip(sender);
        }
        private void Button_Status3_MouseHover(object sender, EventArgs e)
        {
            switch (button_Status3.ImageIndex)
            {
                case 1:
                    toolTip_ButtonStatus.SetToolTip(button_Status3, "Die Zeitangabe wurde akzeptiert.");
                    break;
                case 2:
                    toolTip_ButtonStatus.SetToolTip(button_Status3, "Die Zeitangabe ist nicht plausibel!");
                    break;
                default:
                    break;

            }
        }

        private void ComboBox_ArtDerSicherung_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_ArtDerSicherung.SelectedIndex == (int)GR.ArtDerSicherung.Terminiert)
            {
                groupBox_Terminierung.Enabled = true;
                groupBox_BackupUeberschreiben.Enabled = true;
            }
            else
            {
                groupBox_Terminierung.Enabled = false;
                groupBox_BackupUeberschreiben.Enabled = false;
            }
        }

        private void CheckBox_JedenTag_CheckedChanged(object sender, EventArgs e)
        {
            Control sendercontrol = (Control)sender;
            if (checkBox_JedenTag.Checked == true)
            {
                foreach (Control element in sendercontrol.Parent.Controls)
                {
                    if (element is CheckBox checkBox)
                        checkBox.Checked = true;
                }
            }
        }


        private void DateChecker()
        {
            if (dateTimePicker_Von.Value <= dateTimePicker_Bis.Value || checkBox_KeinEnddatum.Checked == true)
                button_Status3.ImageIndex = 1;
            else
                button_Status3.ImageIndex = 2;
        }
        private void CheckBox_KeinEnddatum_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_KeinEnddatum.Checked == true)
                dateTimePicker_Bis.Enabled = false;
            else
                dateTimePicker_Bis.Enabled = true;
            DateChecker();
        }
        private void CheckBox_BackupUeberschreiben_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_BackupUeberschreiben.Checked)
                groupBox_VerhaltenNeuesBackup.Enabled = false;
            else
                groupBox_VerhaltenNeuesBackup.Enabled = true;
        }
        private void DateTimePicker_Von_ValueChanged(object sender, EventArgs e)
        {
            DateChecker();
        }
        private void DateTimePicker_Bis_ValueChanged(object sender, EventArgs e)
        {
            DateChecker();
        }

        private void Button_Aufgabeanlegen_Click(object sender, EventArgs e)
        {
            //Eingaben prüfen Anfang
            if (!Directory.Exists(textBox_ZuSichernderPfad.Text))       //Existiert der zu sichernde Pfad?
            {
                MessageBox.Show("Der zu sichernde Pfad ist fehlerhaft!", "Fehler beim Anlegen der Aufgabe...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Directory.Exists(textBox_ZielDerSicherung.Text))        //Existiert der Zielordner der Sicherung?
            {
                if (textBox_ZielDerSicherung.Text != string.Empty)      //Sofern Zeichen eingegeben wurden wird die Methode fortgesetzt da später geprüft wird beim ausführen ob der Pfad erreichbar ist. Ist das Textfeld jedoch leer wird abgebrochen.
                    MessageBox.Show("Pfad der Sicherung ist derzeit nicht erreichbar!\nFalls es sich um ein externes Speichergerät handelt \nmüssen Sie sicherstellen das zum Zeitpunkt der Sicherung das Gerät angeschlossen ist. \nAnsonsten wird die Sicherung nicht ausgeführt.", "Fehler beim Anlegen der Aufgabe...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    MessageBox.Show("Pfad der Sicherung ist ungültig!", "Fehler beim Anlegen der Aufgabe...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (comboBox_ArtDerSicherung.SelectedIndex != 0)    //Ist die Zeitangabe bei Terminierung logisch?
            {
                if (checkBox_KeinEnddatum.Checked == false && dateTimePicker_Von.Value > dateTimePicker_Bis.Value)
                {
                    MessageBox.Show("Überprüfen Sie den ausgewählten Zeitraum!", "Fehler beim Anlegen der Aufgabe...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bool tagausgewählt = false;
                foreach (bool tage in Wochentage())
                {
                    if (tage == true)
                    {
                        tagausgewählt = true;
                        break;
                    }
                }
                if (tagausgewählt == false)
                {
                    MessageBox.Show("Überprüfen Sie die ausgewählten Wochentage! Es muss mindestens ein Tag ausgewählt sein!", "Fehler beim Anlegen der Aufgabe...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            //Eingaben prüfen Ende

            // Aufgabe und Backup anlegen
            button_AufgabeAnlegen.Enabled = false;  //Der Button wird gesperrt
            button_AufgabeAnlegen.ImageIndex = 5;

            Backup_Aufgabe neueAufgabe = new Backup_Aufgabe(textBox_ZuSichernderPfad.Text, textBox_ZielDerSicherung.Text, comboBox_ArtDerSicherung.SelectedIndex, dateTimePicker_Von.Value, dateTimePicker_Bis.Value, checkBox_KeinEnddatum.Checked, dateTimePicker_Uhr.Value, Wochentage(), checkBox_BackupUeberschreiben.Checked, comboBox_VerhaltenBackup.SelectedIndex, Convert.ToString(DateTime.Now.GetHashCode()));
            GR.AufgabenListe.Add(neueAufgabe);
            AufgabenListViewItemAdd(neueAufgabe);
            AufgabenListeSpeichern(neueAufgabe);
            BackupAnlegen(neueAufgabe);

            //Vorgang abschließen
            button_AufgabeAnlegen.Enabled = true;   //Der Button wird entsperrt
            button_AufgabeAnlegen.ImageIndex = 3;
        }
         private bool[] Wochentage()
        {
            bool[] wochentage = new bool[7];
            int i = 0;
            if (checkBox_So.Checked == false)
                wochentage[i] = false;
            else
                wochentage[i] = true;
            i++;

            if (checkBox_Mo.Checked == false)
                wochentage[i] = false;
            else
                wochentage[i] = true;
            i++;

            if (checkBox_Di.Checked == false)
                wochentage[i] = false;
            else
                wochentage[i] = true;
            i++;

            if (checkBox_Mi.Checked == false)
                wochentage[i] = false;
            else
                wochentage[i] = true;
            i++;

            if (checkBox_Do.Checked == false)
                wochentage[i] = false;
            else
                wochentage[i] = true;
            i++;

            if (checkBox_Fr.Checked == false)
                wochentage[i] = false;
            else
                wochentage[i] = true;
            i++;

            if (checkBox_Sa.Checked == false)
                wochentage[i] = false;
            else
                wochentage[i] = true;

            return wochentage;
        }
        private void Button_AufgabeLoeschen_Click(object sender, EventArgs e)
        {
            SelectedListViewItemCollection collection = listView_Aufgaben.SelectedItems;
            if (collection.Count != 0)
            {
                Button_AufgabeLoeschen.Enabled = false;
                int RemovedAt = Convert.ToInt32(listView_Aufgaben.SelectedItems[0].Text) - 1;
                AufgabenListeEintragEntfernt(RemovedAt);
            }
        }
        private void ListView_Aufgaben_KeyDown(object sender, KeyEventArgs e)
        {
            SelectedListViewItemCollection collection = listView_Aufgaben.SelectedItems;
            if (collection.Count != 0)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    Button_AufgabeLoeschen.Enabled = false;
                    int RemovedAt = Convert.ToInt32(listView_Aufgaben.SelectedItems[0].Text) - 1;
                    AufgabenListeEintragEntfernt(RemovedAt);                    
                }
            }
        }

        private void ListView_Aufgaben_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedListViewItemCollection collection = listView_Aufgaben.SelectedItems;
            if (collection.Count != 0)
                Button_AufgabeLoeschen.Enabled = true;
            else
                Button_AufgabeLoeschen.Enabled=false;
        }
        //Alles was vor dem schließen noch erledigt werden sollte
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            GR.GateProgramShutDown.Set();
            foreach (Backup backup in GR.BackupListe)
            {
                backup.CallToClose();
            }
        }
    }
}
