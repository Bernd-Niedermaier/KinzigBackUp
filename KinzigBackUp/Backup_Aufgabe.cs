using System;
namespace KinzigBackUp
{
    public readonly struct Backup_Aufgabe
    {
        //Diese Struktur soll die Daten empfangen und verfügbar machen zum initialisieren von Backups
       public string ZuSichern { get; }
       public string BackupZiel { get; }
       public int ArtDerSicherung { get; }
       public DateTime Ab { get; }
       public DateTime Bis { get; }
       public bool Forever { get; }
       public DateTime Uhr { get; }
       public bool[] Wochentage { get; }
       public bool BackupOverride { get; }
       public int BackupVerhalten { get; }
       public string ID { get; }

        public Backup_Aufgabe(string ZuSichern, string BackupZiel, int ArtDerSicherung, DateTime Ab, DateTime Bis, bool Forever, DateTime Uhr, bool[] Wochentage, bool BackupOverride, int BackupVerhalten, string ID)
        {
            this.ZuSichern = ZuSichern;
            this.BackupZiel = BackupZiel;
            this.ArtDerSicherung = ArtDerSicherung;
            this.Ab = Ab;
            this.Bis = Bis;
            this.Forever = Forever;
            this.Uhr = Uhr;
            this.Wochentage = new bool[7];
            this.Wochentage = Wochentage;
            this.BackupOverride = BackupOverride;
            this.BackupVerhalten = BackupVerhalten;
            this.ID = ID;
        }
    }
}
