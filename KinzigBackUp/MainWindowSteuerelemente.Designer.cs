namespace KinzigBackUp
{
    partial class MainWindow
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.imageList_Icons = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1_Backup = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox_Aufgabeanlegen = new System.Windows.Forms.GroupBox();
            this.button_AufgabeAnlegen = new System.Windows.Forms.Button();
            this.groupBox_BackupUeberschreiben = new System.Windows.Forms.GroupBox();
            this.checkBox_BackupUeberschreiben = new System.Windows.Forms.CheckBox();
            this.groupBox_VerhaltenNeuesBackup = new System.Windows.Forms.GroupBox();
            this.comboBox_VerhaltenBackup = new System.Windows.Forms.ComboBox();
            this.groupBox_Terminierung = new System.Windows.Forms.GroupBox();
            this.button_Status3 = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.checkBox_Mo = new System.Windows.Forms.CheckBox();
            this.checkBox_Di = new System.Windows.Forms.CheckBox();
            this.checkBox_Mi = new System.Windows.Forms.CheckBox();
            this.checkBox_Do = new System.Windows.Forms.CheckBox();
            this.checkBox_Fr = new System.Windows.Forms.CheckBox();
            this.checkBox_Sa = new System.Windows.Forms.CheckBox();
            this.checkBox_So = new System.Windows.Forms.CheckBox();
            this.checkBox_JedenTag = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.checkBox_KeinEnddatum = new System.Windows.Forms.CheckBox();
            this.dateTimePicker_Uhr = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_Bis = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_Von = new System.Windows.Forms.DateTimePicker();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.comboBox_ArtDerSicherung = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button_Status2 = new System.Windows.Forms.Button();
            this.textBox_ZielDerSicherung = new System.Windows.Forms.TextBox();
            this.button_ZielDerSicherungPfadDialog = new System.Windows.Forms.Button();
            this.groupBox_ZuSichernderPfad = new System.Windows.Forms.GroupBox();
            this.button_Status1 = new System.Windows.Forms.Button();
            this.textBox_ZuSichernderPfad = new System.Windows.Forms.TextBox();
            this.button_ZuSichernPfadDialog = new System.Windows.Forms.Button();
            this.groupBox_Aufgabenliste = new System.Windows.Forms.GroupBox();
            this.Button_AufgabeLoeschen = new System.Windows.Forms.Button();
            this.listView_Aufgaben = new System.Windows.Forms.ListView();
            this.columnHeaderIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_ZuSichernderPfad = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_ZielDerSicherung = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_ArtDerSicherung = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Wiederholungen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Terminierung = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_BackupVerhalten = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_Bearbeiten = new System.Windows.Forms.Button();
            this.listView_DrivesToSave = new System.Windows.Forms.ListView();
            this.button_BackupStarten = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_Bearbeiten_BackUp = new System.Windows.Forms.Button();
            this.listView_Drives_Backup = new System.Windows.Forms.ListView();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip_ButtonStatus = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1_Backup.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox_Aufgabeanlegen.SuspendLayout();
            this.groupBox_BackupUeberschreiben.SuspendLayout();
            this.groupBox_VerhaltenNeuesBackup.SuspendLayout();
            this.groupBox_Terminierung.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox_ZuSichernderPfad.SuspendLayout();
            this.groupBox_Aufgabenliste.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList_Icons
            // 
            this.imageList_Icons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_Icons.ImageStream")));
            this.imageList_Icons.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_Icons.Images.SetKeyName(0, "drive-harddisk-5.ico");
            this.imageList_Icons.Images.SetKeyName(1, "dialog-apply.png");
            this.imageList_Icons.Images.SetKeyName(2, "application-exit-4.png");
            this.imageList_Icons.Images.SetKeyName(3, "dialog-more.png");
            this.imageList_Icons.Images.SetKeyName(4, "edit-clear-list.png");
            this.imageList_Icons.Images.SetKeyName(5, "clock.png");
            this.imageList_Icons.Images.SetKeyName(6, "edit-delete-7.png");
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1_Backup);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList_Icons;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(984, 461);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1_Backup
            // 
            this.tabPage1_Backup.Controls.Add(this.panel2);
            this.tabPage1_Backup.ImageIndex = 0;
            this.tabPage1_Backup.Location = new System.Drawing.Point(4, 23);
            this.tabPage1_Backup.Name = "tabPage1_Backup";
            this.tabPage1_Backup.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1_Backup.Size = new System.Drawing.Size(976, 434);
            this.tabPage1_Backup.TabIndex = 0;
            this.tabPage1_Backup.Text = "Backup";
            this.tabPage1_Backup.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.groupBox_Aufgabeanlegen);
            this.panel2.Controls.Add(this.groupBox_Aufgabenliste);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(970, 428);
            this.panel2.TabIndex = 7;
            // 
            // groupBox_Aufgabeanlegen
            // 
            this.groupBox_Aufgabeanlegen.Controls.Add(this.button_AufgabeAnlegen);
            this.groupBox_Aufgabeanlegen.Controls.Add(this.groupBox_BackupUeberschreiben);
            this.groupBox_Aufgabeanlegen.Controls.Add(this.groupBox_Terminierung);
            this.groupBox_Aufgabeanlegen.Controls.Add(this.groupBox4);
            this.groupBox_Aufgabeanlegen.Controls.Add(this.groupBox3);
            this.groupBox_Aufgabeanlegen.Controls.Add(this.groupBox_ZuSichernderPfad);
            this.groupBox_Aufgabeanlegen.Location = new System.Drawing.Point(24, 16);
            this.groupBox_Aufgabeanlegen.Name = "groupBox_Aufgabeanlegen";
            this.groupBox_Aufgabeanlegen.Size = new System.Drawing.Size(920, 200);
            this.groupBox_Aufgabeanlegen.TabIndex = 6;
            this.groupBox_Aufgabeanlegen.TabStop = false;
            this.groupBox_Aufgabeanlegen.Text = "Aufgabe anlegen";
            // 
            // button_AufgabeAnlegen
            // 
            this.button_AufgabeAnlegen.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_AufgabeAnlegen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_AufgabeAnlegen.ImageIndex = 3;
            this.button_AufgabeAnlegen.ImageList = this.imageList_Icons;
            this.button_AufgabeAnlegen.Location = new System.Drawing.Point(610, 169);
            this.button_AufgabeAnlegen.Name = "button_AufgabeAnlegen";
            this.button_AufgabeAnlegen.Size = new System.Drawing.Size(110, 23);
            this.button_AufgabeAnlegen.TabIndex = 7;
            this.button_AufgabeAnlegen.Text = "Aufgabe anlegen";
            this.button_AufgabeAnlegen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_AufgabeAnlegen.UseVisualStyleBackColor = true;
            this.button_AufgabeAnlegen.Click += new System.EventHandler(this.Button_Aufgabeanlegen_Click);
            // 
            // groupBox_BackupUeberschreiben
            // 
            this.groupBox_BackupUeberschreiben.Controls.Add(this.checkBox_BackupUeberschreiben);
            this.groupBox_BackupUeberschreiben.Controls.Add(this.groupBox_VerhaltenNeuesBackup);
            this.groupBox_BackupUeberschreiben.Enabled = false;
            this.groupBox_BackupUeberschreiben.Location = new System.Drawing.Point(610, 77);
            this.groupBox_BackupUeberschreiben.Name = "groupBox_BackupUeberschreiben";
            this.groupBox_BackupUeberschreiben.Size = new System.Drawing.Size(183, 87);
            this.groupBox_BackupUeberschreiben.TabIndex = 6;
            this.groupBox_BackupUeberschreiben.TabStop = false;
            // 
            // checkBox_BackupUeberschreiben
            // 
            this.checkBox_BackupUeberschreiben.AutoSize = true;
            this.checkBox_BackupUeberschreiben.Checked = true;
            this.checkBox_BackupUeberschreiben.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_BackupUeberschreiben.Location = new System.Drawing.Point(10, -1);
            this.checkBox_BackupUeberschreiben.Name = "checkBox_BackupUeberschreiben";
            this.checkBox_BackupUeberschreiben.Size = new System.Drawing.Size(165, 17);
            this.checkBox_BackupUeberschreiben.TabIndex = 1;
            this.checkBox_BackupUeberschreiben.Text = "Altes Backup überschreiben?";
            this.checkBox_BackupUeberschreiben.UseVisualStyleBackColor = true;
            this.checkBox_BackupUeberschreiben.CheckedChanged += new System.EventHandler(this.CheckBox_BackupUeberschreiben_CheckedChanged);
            // 
            // groupBox_VerhaltenNeuesBackup
            // 
            this.groupBox_VerhaltenNeuesBackup.Controls.Add(this.comboBox_VerhaltenBackup);
            this.groupBox_VerhaltenNeuesBackup.Enabled = false;
            this.groupBox_VerhaltenNeuesBackup.Location = new System.Drawing.Point(23, 22);
            this.groupBox_VerhaltenNeuesBackup.Name = "groupBox_VerhaltenNeuesBackup";
            this.groupBox_VerhaltenNeuesBackup.Size = new System.Drawing.Size(138, 51);
            this.groupBox_VerhaltenNeuesBackup.TabIndex = 0;
            this.groupBox_VerhaltenNeuesBackup.TabStop = false;
            this.groupBox_VerhaltenNeuesBackup.Text = "Verhalten neues Backup";
            // 
            // comboBox_VerhaltenBackup
            // 
            this.comboBox_VerhaltenBackup.FormattingEnabled = true;
            this.comboBox_VerhaltenBackup.Items.AddRange(new object[] {
            "Indexiert",
            "Datum"});
            this.comboBox_VerhaltenBackup.Location = new System.Drawing.Point(9, 19);
            this.comboBox_VerhaltenBackup.Name = "comboBox_VerhaltenBackup";
            this.comboBox_VerhaltenBackup.Size = new System.Drawing.Size(121, 21);
            this.comboBox_VerhaltenBackup.TabIndex = 0;
            // 
            // groupBox_Terminierung
            // 
            this.groupBox_Terminierung.Controls.Add(this.button_Status3);
            this.groupBox_Terminierung.Controls.Add(this.groupBox6);
            this.groupBox_Terminierung.Controls.Add(this.label11);
            this.groupBox_Terminierung.Controls.Add(this.label10);
            this.groupBox_Terminierung.Controls.Add(this.label9);
            this.groupBox_Terminierung.Controls.Add(this.checkBox_KeinEnddatum);
            this.groupBox_Terminierung.Controls.Add(this.dateTimePicker_Uhr);
            this.groupBox_Terminierung.Controls.Add(this.dateTimePicker_Bis);
            this.groupBox_Terminierung.Controls.Add(this.dateTimePicker_Von);
            this.groupBox_Terminierung.Enabled = false;
            this.groupBox_Terminierung.Location = new System.Drawing.Point(6, 77);
            this.groupBox_Terminierung.Name = "groupBox_Terminierung";
            this.groupBox_Terminierung.Size = new System.Drawing.Size(598, 116);
            this.groupBox_Terminierung.TabIndex = 5;
            this.groupBox_Terminierung.TabStop = false;
            this.groupBox_Terminierung.Text = "Terminierung";
            // 
            // button_Status3
            // 
            this.button_Status3.ImageIndex = 1;
            this.button_Status3.ImageList = this.imageList_Icons;
            this.button_Status3.Location = new System.Drawing.Point(155, 19);
            this.button_Status3.Name = "button_Status3";
            this.button_Status3.Size = new System.Drawing.Size(23, 23);
            this.button_Status3.TabIndex = 8;
            this.toolTip_ButtonStatus.SetToolTip(this.button_Status3, "Status");
            this.button_Status3.UseVisualStyleBackColor = true;
            this.button_Status3.MouseHover += new System.EventHandler(this.Button_Status3_MouseHover);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.checkBox_Mo);
            this.groupBox6.Controls.Add(this.checkBox_Di);
            this.groupBox6.Controls.Add(this.checkBox_Mi);
            this.groupBox6.Controls.Add(this.checkBox_Do);
            this.groupBox6.Controls.Add(this.checkBox_Fr);
            this.groupBox6.Controls.Add(this.checkBox_Sa);
            this.groupBox6.Controls.Add(this.checkBox_So);
            this.groupBox6.Controls.Add(this.checkBox_JedenTag);
            this.groupBox6.Location = new System.Drawing.Point(233, 17);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(358, 54);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "An Wochentag:";
            // 
            // checkBox_Mo
            // 
            this.checkBox_Mo.AutoSize = true;
            this.checkBox_Mo.Location = new System.Drawing.Point(12, 23);
            this.checkBox_Mo.Name = "checkBox_Mo";
            this.checkBox_Mo.Size = new System.Drawing.Size(41, 17);
            this.checkBox_Mo.TabIndex = 7;
            this.checkBox_Mo.Text = "Mo";
            this.checkBox_Mo.UseVisualStyleBackColor = true;
            // 
            // checkBox_Di
            // 
            this.checkBox_Di.AutoSize = true;
            this.checkBox_Di.Location = new System.Drawing.Point(53, 23);
            this.checkBox_Di.Name = "checkBox_Di";
            this.checkBox_Di.Size = new System.Drawing.Size(36, 17);
            this.checkBox_Di.TabIndex = 6;
            this.checkBox_Di.Text = "Di";
            this.checkBox_Di.UseVisualStyleBackColor = true;
            // 
            // checkBox_Mi
            // 
            this.checkBox_Mi.AutoSize = true;
            this.checkBox_Mi.Location = new System.Drawing.Point(89, 23);
            this.checkBox_Mi.Name = "checkBox_Mi";
            this.checkBox_Mi.Size = new System.Drawing.Size(37, 17);
            this.checkBox_Mi.TabIndex = 5;
            this.checkBox_Mi.Text = "Mi";
            this.checkBox_Mi.UseVisualStyleBackColor = true;
            // 
            // checkBox_Do
            // 
            this.checkBox_Do.AutoSize = true;
            this.checkBox_Do.Location = new System.Drawing.Point(126, 23);
            this.checkBox_Do.Name = "checkBox_Do";
            this.checkBox_Do.Size = new System.Drawing.Size(40, 17);
            this.checkBox_Do.TabIndex = 4;
            this.checkBox_Do.Text = "Do";
            this.checkBox_Do.UseVisualStyleBackColor = true;
            // 
            // checkBox_Fr
            // 
            this.checkBox_Fr.AutoSize = true;
            this.checkBox_Fr.Location = new System.Drawing.Point(166, 23);
            this.checkBox_Fr.Name = "checkBox_Fr";
            this.checkBox_Fr.Size = new System.Drawing.Size(35, 17);
            this.checkBox_Fr.TabIndex = 3;
            this.checkBox_Fr.Text = "Fr";
            this.checkBox_Fr.UseVisualStyleBackColor = true;
            // 
            // checkBox_Sa
            // 
            this.checkBox_Sa.AutoSize = true;
            this.checkBox_Sa.Location = new System.Drawing.Point(201, 23);
            this.checkBox_Sa.Name = "checkBox_Sa";
            this.checkBox_Sa.Size = new System.Drawing.Size(39, 17);
            this.checkBox_Sa.TabIndex = 2;
            this.checkBox_Sa.Text = "Sa";
            this.checkBox_Sa.UseVisualStyleBackColor = true;
            // 
            // checkBox_So
            // 
            this.checkBox_So.AutoSize = true;
            this.checkBox_So.Location = new System.Drawing.Point(240, 23);
            this.checkBox_So.Name = "checkBox_So";
            this.checkBox_So.Size = new System.Drawing.Size(39, 17);
            this.checkBox_So.TabIndex = 1;
            this.checkBox_So.Text = "So";
            this.checkBox_So.UseVisualStyleBackColor = true;
            // 
            // checkBox_JedenTag
            // 
            this.checkBox_JedenTag.AutoSize = true;
            this.checkBox_JedenTag.Location = new System.Drawing.Point(279, 23);
            this.checkBox_JedenTag.Name = "checkBox_JedenTag";
            this.checkBox_JedenTag.Size = new System.Drawing.Size(77, 17);
            this.checkBox_JedenTag.TabIndex = 0;
            this.checkBox_JedenTag.Text = "Jeden Tag";
            this.checkBox_JedenTag.UseVisualStyleBackColor = true;
            this.checkBox_JedenTag.CheckedChanged += new System.EventHandler(this.CheckBox_JedenTag_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 75);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Uhrzeit:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(26, 49);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(24, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Bis:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Ab:";
            // 
            // checkBox_KeinEnddatum
            // 
            this.checkBox_KeinEnddatum.AutoSize = true;
            this.checkBox_KeinEnddatum.Location = new System.Drawing.Point(156, 47);
            this.checkBox_KeinEnddatum.Name = "checkBox_KeinEnddatum";
            this.checkBox_KeinEnddatum.Size = new System.Drawing.Size(71, 17);
            this.checkBox_KeinEnddatum.TabIndex = 3;
            this.checkBox_KeinEnddatum.Text = "Für immer";
            this.checkBox_KeinEnddatum.UseVisualStyleBackColor = true;
            this.checkBox_KeinEnddatum.CheckedChanged += new System.EventHandler(this.CheckBox_KeinEnddatum_CheckedChanged);
            // 
            // dateTimePicker_Uhr
            // 
            this.dateTimePicker_Uhr.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker_Uhr.Location = new System.Drawing.Point(56, 71);
            this.dateTimePicker_Uhr.Name = "dateTimePicker_Uhr";
            this.dateTimePicker_Uhr.ShowUpDown = true;
            this.dateTimePicker_Uhr.Size = new System.Drawing.Size(93, 20);
            this.dateTimePicker_Uhr.TabIndex = 2;
            this.dateTimePicker_Uhr.Value = new System.DateTime(2023, 8, 14, 0, 0, 0, 0);
            // 
            // dateTimePicker_Bis
            // 
            this.dateTimePicker_Bis.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker_Bis.Location = new System.Drawing.Point(56, 45);
            this.dateTimePicker_Bis.Name = "dateTimePicker_Bis";
            this.dateTimePicker_Bis.Size = new System.Drawing.Size(93, 20);
            this.dateTimePicker_Bis.TabIndex = 1;
            this.dateTimePicker_Bis.Value = new System.DateTime(2023, 8, 14, 18, 3, 8, 0);
            this.dateTimePicker_Bis.ValueChanged += new System.EventHandler(this.DateTimePicker_Bis_ValueChanged);
            // 
            // dateTimePicker_Von
            // 
            this.dateTimePicker_Von.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker_Von.Location = new System.Drawing.Point(56, 19);
            this.dateTimePicker_Von.Name = "dateTimePicker_Von";
            this.dateTimePicker_Von.Size = new System.Drawing.Size(93, 20);
            this.dateTimePicker_Von.TabIndex = 0;
            this.dateTimePicker_Von.Value = new System.DateTime(2023, 8, 14, 18, 3, 8, 0);
            this.dateTimePicker_Von.ValueChanged += new System.EventHandler(this.DateTimePicker_Von_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.comboBox_ArtDerSicherung);
            this.groupBox4.Location = new System.Drawing.Point(340, 19);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(137, 52);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Art der Sicherung";
            // 
            // comboBox_ArtDerSicherung
            // 
            this.comboBox_ArtDerSicherung.FormattingEnabled = true;
            this.comboBox_ArtDerSicherung.Location = new System.Drawing.Point(7, 19);
            this.comboBox_ArtDerSicherung.Name = "comboBox_ArtDerSicherung";
            this.comboBox_ArtDerSicherung.Size = new System.Drawing.Size(121, 21);
            this.comboBox_ArtDerSicherung.TabIndex = 3;
            this.comboBox_ArtDerSicherung.SelectedIndexChanged += new System.EventHandler(this.ComboBox_ArtDerSicherung_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button_Status2);
            this.groupBox3.Controls.Add(this.textBox_ZielDerSicherung);
            this.groupBox3.Controls.Add(this.button_ZielDerSicherungPfadDialog);
            this.groupBox3.Location = new System.Drawing.Point(173, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(161, 52);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Pfad/Ziel der Sicherung";
            // 
            // button_Status2
            // 
            this.button_Status2.ImageIndex = 2;
            this.button_Status2.ImageList = this.imageList_Icons;
            this.button_Status2.Location = new System.Drawing.Point(133, 17);
            this.button_Status2.Name = "button_Status2";
            this.button_Status2.Size = new System.Drawing.Size(23, 23);
            this.button_Status2.TabIndex = 2;
            this.toolTip_ButtonStatus.SetToolTip(this.button_Status2, "Status");
            this.button_Status2.UseVisualStyleBackColor = true;
            this.button_Status2.MouseHover += new System.EventHandler(this.Button_Status2_MouseHover);
            // 
            // textBox_ZielDerSicherung
            // 
            this.textBox_ZielDerSicherung.Location = new System.Drawing.Point(6, 19);
            this.textBox_ZielDerSicherung.Name = "textBox_ZielDerSicherung";
            this.textBox_ZielDerSicherung.Size = new System.Drawing.Size(100, 20);
            this.textBox_ZielDerSicherung.TabIndex = 0;
            this.textBox_ZielDerSicherung.TextChanged += new System.EventHandler(this.TextBox_ZielDerSicherung_TextChanged);
            // 
            // button_ZielDerSicherungPfadDialog
            // 
            this.button_ZielDerSicherungPfadDialog.AutoSize = true;
            this.button_ZielDerSicherungPfadDialog.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_ZielDerSicherungPfadDialog.Location = new System.Drawing.Point(106, 17);
            this.button_ZielDerSicherungPfadDialog.Name = "button_ZielDerSicherungPfadDialog";
            this.button_ZielDerSicherungPfadDialog.Size = new System.Drawing.Size(26, 23);
            this.button_ZielDerSicherungPfadDialog.TabIndex = 1;
            this.button_ZielDerSicherungPfadDialog.Text = "...";
            this.button_ZielDerSicherungPfadDialog.UseVisualStyleBackColor = true;
            this.button_ZielDerSicherungPfadDialog.Click += new System.EventHandler(this.Button_ZielDerSicherungPfadDialog_Click);
            // 
            // groupBox_ZuSichernderPfad
            // 
            this.groupBox_ZuSichernderPfad.Controls.Add(this.button_Status1);
            this.groupBox_ZuSichernderPfad.Controls.Add(this.textBox_ZuSichernderPfad);
            this.groupBox_ZuSichernderPfad.Controls.Add(this.button_ZuSichernPfadDialog);
            this.groupBox_ZuSichernderPfad.Location = new System.Drawing.Point(6, 19);
            this.groupBox_ZuSichernderPfad.Name = "groupBox_ZuSichernderPfad";
            this.groupBox_ZuSichernderPfad.Size = new System.Drawing.Size(161, 52);
            this.groupBox_ZuSichernderPfad.TabIndex = 2;
            this.groupBox_ZuSichernderPfad.TabStop = false;
            this.groupBox_ZuSichernderPfad.Text = "Zu sichernder Pfad";
            // 
            // button_Status1
            // 
            this.button_Status1.ImageIndex = 2;
            this.button_Status1.ImageList = this.imageList_Icons;
            this.button_Status1.Location = new System.Drawing.Point(133, 17);
            this.button_Status1.Name = "button_Status1";
            this.button_Status1.Size = new System.Drawing.Size(23, 23);
            this.button_Status1.TabIndex = 2;
            this.toolTip_ButtonStatus.SetToolTip(this.button_Status1, "Status");
            this.button_Status1.UseVisualStyleBackColor = true;
            this.button_Status1.MouseHover += new System.EventHandler(this.Button_Status_MouseHover);
            // 
            // textBox_ZuSichernderPfad
            // 
            this.textBox_ZuSichernderPfad.Location = new System.Drawing.Point(6, 19);
            this.textBox_ZuSichernderPfad.Name = "textBox_ZuSichernderPfad";
            this.textBox_ZuSichernderPfad.Size = new System.Drawing.Size(100, 20);
            this.textBox_ZuSichernderPfad.TabIndex = 0;
            this.textBox_ZuSichernderPfad.TextChanged += new System.EventHandler(this.TextBox_ZuSichernderPfad_TextChanged);
            // 
            // button_ZuSichernPfadDialog
            // 
            this.button_ZuSichernPfadDialog.AutoSize = true;
            this.button_ZuSichernPfadDialog.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_ZuSichernPfadDialog.Location = new System.Drawing.Point(106, 17);
            this.button_ZuSichernPfadDialog.Name = "button_ZuSichernPfadDialog";
            this.button_ZuSichernPfadDialog.Size = new System.Drawing.Size(26, 23);
            this.button_ZuSichernPfadDialog.TabIndex = 1;
            this.button_ZuSichernPfadDialog.Text = "...";
            this.button_ZuSichernPfadDialog.UseVisualStyleBackColor = true;
            this.button_ZuSichernPfadDialog.Click += new System.EventHandler(this.Button_ZuSichernPfadDialog_Click);
            // 
            // groupBox_Aufgabenliste
            // 
            this.groupBox_Aufgabenliste.Controls.Add(this.Button_AufgabeLoeschen);
            this.groupBox_Aufgabenliste.Controls.Add(this.listView_Aufgaben);
            this.groupBox_Aufgabenliste.Location = new System.Drawing.Point(24, 222);
            this.groupBox_Aufgabenliste.Name = "groupBox_Aufgabenliste";
            this.groupBox_Aufgabenliste.Size = new System.Drawing.Size(920, 187);
            this.groupBox_Aufgabenliste.TabIndex = 5;
            this.groupBox_Aufgabenliste.TabStop = false;
            this.groupBox_Aufgabenliste.Text = "Aufgaben";
            // 
            // Button_AufgabeLoeschen
            // 
            this.Button_AufgabeLoeschen.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Button_AufgabeLoeschen.Enabled = false;
            this.Button_AufgabeLoeschen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button_AufgabeLoeschen.ImageIndex = 6;
            this.Button_AufgabeLoeschen.ImageList = this.imageList_Icons;
            this.Button_AufgabeLoeschen.Location = new System.Drawing.Point(3, 158);
            this.Button_AufgabeLoeschen.Name = "Button_AufgabeLoeschen";
            this.Button_AufgabeLoeschen.Size = new System.Drawing.Size(110, 23);
            this.Button_AufgabeLoeschen.TabIndex = 8;
            this.Button_AufgabeLoeschen.Text = "Aufgabe löschen";
            this.Button_AufgabeLoeschen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip_ButtonStatus.SetToolTip(this.Button_AufgabeLoeschen, "Löscht einen markierten Eintrag aus der Liste der angelegten Aufgaben. \r\nTaste: E" +
        "ntf");
            this.Button_AufgabeLoeschen.UseVisualStyleBackColor = true;
            this.Button_AufgabeLoeschen.Click += new System.EventHandler(this.Button_AufgabeLoeschen_Click);
            // 
            // listView_Aufgaben
            // 
            this.listView_Aufgaben.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView_Aufgaben.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderIndex,
            this.columnHeader_ZuSichernderPfad,
            this.columnHeader_ZielDerSicherung,
            this.columnHeader_ArtDerSicherung,
            this.columnHeader_Wiederholungen,
            this.columnHeader_Terminierung,
            this.columnHeader_BackupVerhalten});
            this.listView_Aufgaben.Dock = System.Windows.Forms.DockStyle.Top;
            this.listView_Aufgaben.FullRowSelect = true;
            this.listView_Aufgaben.GridLines = true;
            this.listView_Aufgaben.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView_Aufgaben.HideSelection = false;
            this.listView_Aufgaben.Location = new System.Drawing.Point(3, 16);
            this.listView_Aufgaben.MultiSelect = false;
            this.listView_Aufgaben.Name = "listView_Aufgaben";
            this.listView_Aufgaben.ShowItemToolTips = true;
            this.listView_Aufgaben.Size = new System.Drawing.Size(914, 141);
            this.listView_Aufgaben.SmallImageList = this.imageList_Icons;
            this.listView_Aufgaben.TabIndex = 0;
            this.listView_Aufgaben.UseCompatibleStateImageBehavior = false;
            this.listView_Aufgaben.View = System.Windows.Forms.View.Details;
            this.listView_Aufgaben.SelectedIndexChanged += new System.EventHandler(this.ListView_Aufgaben_SelectedIndexChanged);
            this.listView_Aufgaben.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListView_Aufgaben_KeyDown);
            // 
            // columnHeaderIndex
            // 
            this.columnHeaderIndex.Text = "Index";
            this.columnHeaderIndex.Width = 40;
            // 
            // columnHeader_ZuSichernderPfad
            // 
            this.columnHeader_ZuSichernderPfad.Text = "Zu sichernder Pfad";
            this.columnHeader_ZuSichernderPfad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader_ZuSichernderPfad.Width = 140;
            // 
            // columnHeader_ZielDerSicherung
            // 
            this.columnHeader_ZielDerSicherung.Text = "Ziel der Sicherung";
            this.columnHeader_ZielDerSicherung.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader_ZielDerSicherung.Width = 140;
            // 
            // columnHeader_ArtDerSicherung
            // 
            this.columnHeader_ArtDerSicherung.Text = "Art der Sicherung";
            this.columnHeader_ArtDerSicherung.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader_ArtDerSicherung.Width = 100;
            // 
            // columnHeader_Wiederholungen
            // 
            this.columnHeader_Wiederholungen.Text = "Wiederholungen";
            this.columnHeader_Wiederholungen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader_Wiederholungen.Width = 90;
            // 
            // columnHeader_Terminierung
            // 
            this.columnHeader_Terminierung.Text = "Terminierung";
            this.columnHeader_Terminierung.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader_Terminierung.Width = 260;
            // 
            // columnHeader_BackupVerhalten
            // 
            this.columnHeader_BackupVerhalten.Text = "Verhalten neues Backup";
            this.columnHeader_BackupVerhalten.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader_BackupVerhalten.Width = 140;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(976, 434);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.button_BackupStarten);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(976, 434);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_Bearbeiten);
            this.groupBox1.Controls.Add(this.listView_DrivesToSave);
            this.groupBox1.Location = new System.Drawing.Point(186, 128);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(153, 167);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Zu sichernde Festplatten";
            // 
            // button_Bearbeiten
            // 
            this.button_Bearbeiten.Location = new System.Drawing.Point(40, 137);
            this.button_Bearbeiten.Name = "button_Bearbeiten";
            this.button_Bearbeiten.Size = new System.Drawing.Size(75, 23);
            this.button_Bearbeiten.TabIndex = 1;
            this.button_Bearbeiten.Text = "Bearbeiten";
            this.button_Bearbeiten.UseVisualStyleBackColor = true;
            // 
            // listView_DrivesToSave
            // 
            this.listView_DrivesToSave.HideSelection = false;
            this.listView_DrivesToSave.Location = new System.Drawing.Point(6, 19);
            this.listView_DrivesToSave.Name = "listView_DrivesToSave";
            this.listView_DrivesToSave.Size = new System.Drawing.Size(141, 112);
            this.listView_DrivesToSave.SmallImageList = this.imageList_Icons;
            this.listView_DrivesToSave.TabIndex = 0;
            this.listView_DrivesToSave.UseCompatibleStateImageBehavior = false;
            this.listView_DrivesToSave.View = System.Windows.Forms.View.SmallIcon;
            // 
            // button_BackupStarten
            // 
            this.button_BackupStarten.Location = new System.Drawing.Point(504, 138);
            this.button_BackupStarten.Name = "button_BackupStarten";
            this.button_BackupStarten.Size = new System.Drawing.Size(102, 23);
            this.button_BackupStarten.TabIndex = 6;
            this.button_BackupStarten.Text = "Backup Starten";
            this.button_BackupStarten.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_Bearbeiten_BackUp);
            this.groupBox2.Controls.Add(this.listView_Drives_Backup);
            this.groupBox2.Location = new System.Drawing.Point(345, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(153, 167);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Backup Festplatten";
            // 
            // button_Bearbeiten_BackUp
            // 
            this.button_Bearbeiten_BackUp.Location = new System.Drawing.Point(40, 137);
            this.button_Bearbeiten_BackUp.Name = "button_Bearbeiten_BackUp";
            this.button_Bearbeiten_BackUp.Size = new System.Drawing.Size(75, 23);
            this.button_Bearbeiten_BackUp.TabIndex = 1;
            this.button_Bearbeiten_BackUp.Text = "Bearbeiten";
            this.button_Bearbeiten_BackUp.UseVisualStyleBackColor = true;
            // 
            // listView_Drives_Backup
            // 
            this.listView_Drives_Backup.HideSelection = false;
            this.listView_Drives_Backup.Location = new System.Drawing.Point(6, 19);
            this.listView_Drives_Backup.Name = "listView_Drives_Backup";
            this.listView_Drives_Backup.Size = new System.Drawing.Size(141, 112);
            this.listView_Drives_Backup.SmallImageList = this.imageList_Icons;
            this.listView_Drives_Backup.TabIndex = 0;
            this.listView_Drives_Backup.UseCompatibleStateImageBehavior = false;
            this.listView_Drives_Backup.View = System.Windows.Forms.View.SmallIcon;
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kinzig Backup";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1_Backup.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox_Aufgabeanlegen.ResumeLayout(false);
            this.groupBox_BackupUeberschreiben.ResumeLayout(false);
            this.groupBox_BackupUeberschreiben.PerformLayout();
            this.groupBox_VerhaltenNeuesBackup.ResumeLayout(false);
            this.groupBox_Terminierung.ResumeLayout(false);
            this.groupBox_Terminierung.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox_ZuSichernderPfad.ResumeLayout(false);
            this.groupBox_ZuSichernderPfad.PerformLayout();
            this.groupBox_Aufgabenliste.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imageList_Icons;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1_Backup;
        private System.Windows.Forms.GroupBox groupBox_Aufgabenliste;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_Bearbeiten;
        private System.Windows.Forms.ListView listView_DrivesToSave;
        private System.Windows.Forms.Button button_BackupStarten;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_Bearbeiten_BackUp;
        private System.Windows.Forms.ListView listView_Drives_Backup;
        private System.Windows.Forms.GroupBox groupBox_Aufgabeanlegen;
        private System.Windows.Forms.GroupBox groupBox_ZuSichernderPfad;
        private System.Windows.Forms.TextBox textBox_ZuSichernderPfad;
        private System.Windows.Forms.Button button_ZuSichernPfadDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button button_Status1;
        private System.Windows.Forms.ToolTip toolTip_ButtonStatus;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button_Status2;
        private System.Windows.Forms.TextBox textBox_ZielDerSicherung;
        private System.Windows.Forms.Button button_ZielDerSicherungPfadDialog;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox comboBox_ArtDerSicherung;
        private System.Windows.Forms.GroupBox groupBox_Terminierung;
        private System.Windows.Forms.DateTimePicker dateTimePicker_Von;
        private System.Windows.Forms.DateTimePicker dateTimePicker_Uhr;
        private System.Windows.Forms.DateTimePicker dateTimePicker_Bis;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox checkBox_KeinEnddatum;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox checkBox_Mo;
        private System.Windows.Forms.CheckBox checkBox_Di;
        private System.Windows.Forms.CheckBox checkBox_Mi;
        private System.Windows.Forms.CheckBox checkBox_Do;
        private System.Windows.Forms.CheckBox checkBox_Fr;
        private System.Windows.Forms.CheckBox checkBox_Sa;
        private System.Windows.Forms.CheckBox checkBox_So;
        private System.Windows.Forms.CheckBox checkBox_JedenTag;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox_BackupUeberschreiben;
        private System.Windows.Forms.CheckBox checkBox_BackupUeberschreiben;
        private System.Windows.Forms.GroupBox groupBox_VerhaltenNeuesBackup;
        private System.Windows.Forms.ComboBox comboBox_VerhaltenBackup;
        private System.Windows.Forms.Button button_AufgabeAnlegen;
        private System.Windows.Forms.Button button_Status3;
        private System.Windows.Forms.ListView listView_Aufgaben;
        private System.Windows.Forms.ColumnHeader columnHeaderIndex;
        private System.Windows.Forms.ColumnHeader columnHeader_ZuSichernderPfad;
        private System.Windows.Forms.ColumnHeader columnHeader_ZielDerSicherung;
        private System.Windows.Forms.ColumnHeader columnHeader_ArtDerSicherung;
        private System.Windows.Forms.ColumnHeader columnHeader_Wiederholungen;
        private System.Windows.Forms.ColumnHeader columnHeader_Terminierung;
        private System.Windows.Forms.ColumnHeader columnHeader_BackupVerhalten;
        private System.Windows.Forms.Button Button_AufgabeLoeschen;
    }
}

