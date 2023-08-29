using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Text;

namespace KinzigBackUp
{
    public partial class MainWindow : Form
    {
        //Backup Objekt anlegen
        private void BackupAnlegen(Backup_Aufgabe aufgabe)
        {
            switch (aufgabe.ArtDerSicherung)
            {
                case (int)GR.ArtDerSicherung.Echtzeit_Spiegeln:
                    {
                        BUSpiegeln bUSpiegeln = new BUSpiegeln(aufgabe);
                        GR.BackupListe.Add(bUSpiegeln);
                    }
                    break;
                case (int)GR.ArtDerSicherung.Terminiert:
                    {
                        BUTerminiert bUTerminiert = new BUTerminiert(aufgabe);
                        GR.BackupListe.Add(bUTerminiert);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
