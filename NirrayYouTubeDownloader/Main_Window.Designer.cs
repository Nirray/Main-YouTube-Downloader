namespace NirrayYouTubeDownloader
{
    partial class NirrayYouTubeParser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NirrayYouTubeParser));
            this.DownloadListButton = new System.Windows.Forms.Button();
            this.Information_container = new System.Windows.Forms.RichTextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.counter = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Progress_text = new System.Windows.Forms.ToolStripStatusLabel();
            this.DirectUrlContainer = new System.Windows.Forms.TextBox();
            this.DownloadFromDirectLink = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.plikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolBox_ConvertToMp3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mp3_always_best = new System.Windows.Forms.ToolStripMenuItem();
            this.mp3_32kb = new System.Windows.Forms.ToolStripMenuItem();
            this.mp3_96kb = new System.Windows.Forms.ToolStripMenuItem();
            this.mp3_128kb = new System.Windows.Forms.ToolStripMenuItem();
            this.mp3_160kb = new System.Windows.Forms.ToolStripMenuItem();
            this.mp3_192kb = new System.Windows.Forms.ToolStripMenuItem();
            this.mp3_224kb = new System.Windows.Forms.ToolStripMenuItem();
            this.mp3_256kb = new System.Windows.Forms.ToolStripMenuItem();
            this.mp3_272kb = new System.Windows.Forms.ToolStripMenuItem();
            this.usuwajPlikiŹródłoweToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolBox_NewLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.pobieranieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolBox_DoNotDownloadAlready = new System.Windows.Forms.ToolStripMenuItem();
            this.countPlaylistDiscSpaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alternativeSquadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pomocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informacjeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GitHubError = new System.Windows.Forms.ToolStripMenuItem();
            this.NewSavePath = new System.Windows.Forms.FolderBrowserDialog();
            this.SelectListToDownloadFrom = new System.Windows.Forms.OpenFileDialog();
            this.Cannot_download_text = new System.Windows.Forms.Label();
            this.Can_download_text = new System.Windows.Forms.Label();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.PauseButton = new System.Windows.Forms.Button();
            this.UserPlaylist = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Checkbox_UseCombine = new System.Windows.Forms.CheckBox();
            this.Checkbox_UseUrl = new System.Windows.Forms.CheckBox();
            this.maxsizeof = new System.Windows.Forms.Label();
            this.backgroundthreads = new System.Windows.Forms.Label();
            this.whatsize = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.speedtext = new System.Windows.Forms.Label();
            this.speedimage = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.speedDownloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.turboNoLimitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.twenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.własneUstawieniaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.translateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Lang_Polish = new System.Windows.Forms.ToolStripMenuItem();
            this.Lang_English = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speedimage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // DownloadListButton
            // 
            this.DownloadListButton.Enabled = false;
            this.DownloadListButton.Location = new System.Drawing.Point(12, 179);
            this.DownloadListButton.Name = "DownloadListButton";
            this.DownloadListButton.Size = new System.Drawing.Size(183, 23);
            this.DownloadListButton.TabIndex = 3;
            this.DownloadListButton.Text = "Pobierz z pliku tekstowego";
            this.ToolTip.SetToolTip(this.DownloadListButton, "Odczytuje listę adresów z pliku tekstowego i automatycznie je sprawdza");
            this.DownloadListButton.UseVisualStyleBackColor = true;
            this.DownloadListButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // Information_container
            // 
            this.Information_container.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Information_container.BackColor = System.Drawing.SystemColors.Menu;
            this.Information_container.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Information_container.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Information_container.DetectUrls = false;
            this.Information_container.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Information_container.Location = new System.Drawing.Point(48, 15);
            this.Information_container.Name = "Information_container";
            this.Information_container.ReadOnly = true;
            this.Information_container.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.Information_container.Size = new System.Drawing.Size(760, 56);
            this.Information_container.TabIndex = 8;
            this.Information_container.Text = "";
            this.Information_container.TextChanged += new System.EventHandler(this.Information_container_TextChanged);
            this.Information_container.DoubleClick += new System.EventHandler(this.Information_container_DoubleClick);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 321);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(784, 17);
            this.progressBar1.Step = 100;
            this.progressBar1.TabIndex = 3;
            // 
            // counter
            // 
            this.counter.AutoSize = true;
            this.counter.BackColor = System.Drawing.SystemColors.Control;
            this.counter.ForeColor = System.Drawing.SystemColors.WindowText;
            this.counter.Location = new System.Drawing.Point(28, 23);
            this.counter.Name = "counter";
            this.counter.Size = new System.Drawing.Size(53, 13);
            this.counter.TabIndex = 5;
            this.counter.Text = "Aktualny:";
            this.ToolTip.SetToolTip(this.counter, "Pozostało do końca");
            // 
            // statusStrip1
            // 
            this.statusStrip1.AllowMerge = false;
            this.statusStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Progress_text});
            this.statusStrip1.Location = new System.Drawing.Point(0, 338);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "Statusbar";
            // 
            // Progress_text
            // 
            this.Progress_text.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Progress_text.Name = "Progress_text";
            this.Progress_text.Size = new System.Drawing.Size(0, 17);
            // 
            // DirectUrlContainer
            // 
            this.DirectUrlContainer.Location = new System.Drawing.Point(9, 43);
            this.DirectUrlContainer.Name = "DirectUrlContainer";
            this.DirectUrlContainer.Size = new System.Drawing.Size(540, 21);
            this.DirectUrlContainer.TabIndex = 5;
            this.DirectUrlContainer.Text = "https://www.youtube.com/watch?v=pxikqLzKZDY&list=PLy_wKxVmWb4b1efRCOFzax3JQrRj2VW" +
    "fH&index=1";
            this.ToolTip.SetToolTip(this.DirectUrlContainer, "Wklej lub przepisz adres, który chcesz pobrać na swój dysk\r\n(jeżeli chcesz pobrać" +
        " całą playlistę pamiętaj, że musi być to bezpośredni odnośnik)");
            this.DirectUrlContainer.DoubleClick += new System.EventHandler(this.DirectUrlContainer_DoubleClick);
            // 
            // DownloadFromDirectLink
            // 
            this.DownloadFromDirectLink.Location = new System.Drawing.Point(12, 121);
            this.DownloadFromDirectLink.Name = "DownloadFromDirectLink";
            this.DownloadFromDirectLink.Size = new System.Drawing.Size(183, 23);
            this.DownloadFromDirectLink.TabIndex = 1;
            this.DownloadFromDirectLink.Text = "Pobierz";
            this.ToolTip.SetToolTip(this.DownloadFromDirectLink, "Pozwala na sprawdzenie i ściągnięcie audio z adresu odnośnika (jeden plik lub cał" +
        "a playlista)");
            this.DownloadFromDirectLink.UseVisualStyleBackColor = true;
            this.DownloadFromDirectLink.Click += new System.EventHandler(this.DownloadFromDirectLink_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plikToolStripMenuItem,
            this.pobieranieToolStripMenuItem,
            this.translateToolStripMenuItem,
            this.pomocToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // plikToolStripMenuItem
            // 
            this.plikToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolBox_ConvertToMp3,
            this.ToolBox_NewLocation});
            this.plikToolStripMenuItem.Name = "plikToolStripMenuItem";
            this.plikToolStripMenuItem.Size = new System.Drawing.Size(34, 20);
            this.plikToolStripMenuItem.Text = "Plik";
            // 
            // ToolBox_ConvertToMp3
            // 
            this.ToolBox_ConvertToMp3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mp3_always_best,
            this.mp3_32kb,
            this.mp3_96kb,
            this.mp3_128kb,
            this.mp3_160kb,
            this.mp3_192kb,
            this.mp3_224kb,
            this.mp3_256kb,
            this.mp3_272kb,
            this.usuwajPlikiŹródłoweToolStripMenuItem});
            this.ToolBox_ConvertToMp3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ToolBox_ConvertToMp3.Name = "ToolBox_ConvertToMp3";
            this.ToolBox_ConvertToMp3.Size = new System.Drawing.Size(234, 22);
            this.ToolBox_ConvertToMp3.Text = "Automatycznie konwertuj do MP3";
            // 
            // mp3_always_best
            // 
            this.mp3_always_best.CheckOnClick = true;
            this.mp3_always_best.Name = "mp3_always_best";
            this.mp3_always_best.Size = new System.Drawing.Size(290, 22);
            this.mp3_always_best.Text = "Zawsze najlepsza jakość";
            this.mp3_always_best.Click += new System.EventHandler(this.Mp3_always_best_Click);
            // 
            // mp3_32kb
            // 
            this.mp3_32kb.CheckOnClick = true;
            this.mp3_32kb.Name = "mp3_32kb";
            this.mp3_32kb.Size = new System.Drawing.Size(290, 22);
            this.mp3_32kb.Text = "32 kbit/s";
            this.mp3_32kb.Click += new System.EventHandler(this.mp3_32kb_Click);
            // 
            // mp3_96kb
            // 
            this.mp3_96kb.CheckOnClick = true;
            this.mp3_96kb.Name = "mp3_96kb";
            this.mp3_96kb.Size = new System.Drawing.Size(290, 22);
            this.mp3_96kb.Text = "96 kbit/s";
            this.mp3_96kb.Click += new System.EventHandler(this.mp3_96kb_Click);
            // 
            // mp3_128kb
            // 
            this.mp3_128kb.CheckOnClick = true;
            this.mp3_128kb.Name = "mp3_128kb";
            this.mp3_128kb.Size = new System.Drawing.Size(290, 22);
            this.mp3_128kb.Text = "128 kbit/s";
            this.mp3_128kb.Click += new System.EventHandler(this.mp3_128kb_Click);
            // 
            // mp3_160kb
            // 
            this.mp3_160kb.CheckOnClick = true;
            this.mp3_160kb.Name = "mp3_160kb";
            this.mp3_160kb.Size = new System.Drawing.Size(290, 22);
            this.mp3_160kb.Text = "160 kbit/s";
            this.mp3_160kb.Click += new System.EventHandler(this.mp3_160kb_Click);
            // 
            // mp3_192kb
            // 
            this.mp3_192kb.CheckOnClick = true;
            this.mp3_192kb.Name = "mp3_192kb";
            this.mp3_192kb.Size = new System.Drawing.Size(290, 22);
            this.mp3_192kb.Text = "192 kbit/s";
            this.mp3_192kb.Click += new System.EventHandler(this.mp3_192kb_Click);
            // 
            // mp3_224kb
            // 
            this.mp3_224kb.CheckOnClick = true;
            this.mp3_224kb.Name = "mp3_224kb";
            this.mp3_224kb.Size = new System.Drawing.Size(290, 22);
            this.mp3_224kb.Text = "224 kbit/s";
            this.mp3_224kb.Click += new System.EventHandler(this.mp3_224kb_Click);
            // 
            // mp3_256kb
            // 
            this.mp3_256kb.CheckOnClick = true;
            this.mp3_256kb.Name = "mp3_256kb";
            this.mp3_256kb.Size = new System.Drawing.Size(290, 22);
            this.mp3_256kb.Text = "256 kbit/s";
            this.mp3_256kb.Click += new System.EventHandler(this.mp3_256kb_Click);
            // 
            // mp3_272kb
            // 
            this.mp3_272kb.CheckOnClick = true;
            this.mp3_272kb.Name = "mp3_272kb";
            this.mp3_272kb.Size = new System.Drawing.Size(290, 22);
            this.mp3_272kb.Text = "272 kbit/s";
            this.mp3_272kb.Click += new System.EventHandler(this.mp3_272kb_Click);
            // 
            // usuwajPlikiŹródłoweToolStripMenuItem
            // 
            this.usuwajPlikiŹródłoweToolStripMenuItem.CheckOnClick = true;
            this.usuwajPlikiŹródłoweToolStripMenuItem.Enabled = false;
            this.usuwajPlikiŹródłoweToolStripMenuItem.Name = "usuwajPlikiŹródłoweToolStripMenuItem";
            this.usuwajPlikiŹródłoweToolStripMenuItem.Size = new System.Drawing.Size(290, 22);
            this.usuwajPlikiŹródłoweToolStripMenuItem.Text = "Usuwaj pliki źródłowe po pomyślnej konwersji";
            // 
            // ToolBox_NewLocation
            // 
            this.ToolBox_NewLocation.Name = "ToolBox_NewLocation";
            this.ToolBox_NewLocation.Size = new System.Drawing.Size(234, 22);
            this.ToolBox_NewLocation.Text = "Wybierz inną lokalizacje zapisu";
            this.ToolBox_NewLocation.Click += new System.EventHandler(this.ToolBox_NewLocation_Click);
            // 
            // pobieranieToolStripMenuItem
            // 
            this.pobieranieToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolBox_DoNotDownloadAlready,
            this.countPlaylistDiscSpaceToolStripMenuItem,
            this.speedDownloadToolStripMenuItem,
            this.alternativeSquadToolStripMenuItem});
            this.pobieranieToolStripMenuItem.Name = "pobieranieToolStripMenuItem";
            this.pobieranieToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.pobieranieToolStripMenuItem.Text = "Pobieranie";
            // 
            // ToolBox_DoNotDownloadAlready
            // 
            this.ToolBox_DoNotDownloadAlready.Checked = true;
            this.ToolBox_DoNotDownloadAlready.CheckOnClick = true;
            this.ToolBox_DoNotDownloadAlready.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolBox_DoNotDownloadAlready.Name = "ToolBox_DoNotDownloadAlready";
            this.ToolBox_DoNotDownloadAlready.Size = new System.Drawing.Size(339, 22);
            this.ToolBox_DoNotDownloadAlready.Text = "Nie pobieraj istniejących już plików";
            // 
            // countPlaylistDiscSpaceToolStripMenuItem
            // 
            this.countPlaylistDiscSpaceToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.countPlaylistDiscSpaceToolStripMenuItem.CheckOnClick = true;
            this.countPlaylistDiscSpaceToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.countPlaylistDiscSpaceToolStripMenuItem.Name = "countPlaylistDiscSpaceToolStripMenuItem";
            this.countPlaylistDiscSpaceToolStripMenuItem.Size = new System.Drawing.Size(339, 22);
            this.countPlaylistDiscSpaceToolStripMenuItem.Text = "Przelicz rozmiar przed pobraniem";
            this.countPlaylistDiscSpaceToolStripMenuItem.Click += new System.EventHandler(this.countPlaylistDiscSpaceToolStripMenuItem_Click);
            // 
            // alternativeSquadToolStripMenuItem
            // 
            this.alternativeSquadToolStripMenuItem.CheckOnClick = true;
            this.alternativeSquadToolStripMenuItem.Name = "alternativeSquadToolStripMenuItem";
            this.alternativeSquadToolStripMenuItem.Size = new System.Drawing.Size(339, 22);
            this.alternativeSquadToolStripMenuItem.Text = "Alternatywna składnia dla parsowania playlist (videoId)";
            this.alternativeSquadToolStripMenuItem.Click += new System.EventHandler(this.alternativeSquadToolStripMenuItem_Click);
            // 
            // pomocToolStripMenuItem
            // 
            this.pomocToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.informacjeToolStripMenuItem,
            this.GitHubError});
            this.pomocToolStripMenuItem.Name = "pomocToolStripMenuItem";
            this.pomocToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.pomocToolStripMenuItem.Text = "Pomoc";
            // 
            // informacjeToolStripMenuItem
            // 
            this.informacjeToolStripMenuItem.Name = "informacjeToolStripMenuItem";
            this.informacjeToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.informacjeToolStripMenuItem.Text = "Informacje";
            this.informacjeToolStripMenuItem.Click += new System.EventHandler(this.informacjeToolStripMenuItem_Click);
            // 
            // GitHubError
            // 
            this.GitHubError.Name = "GitHubError";
            this.GitHubError.Size = new System.Drawing.Size(126, 22);
            this.GitHubError.Text = "Zgłoś błąd";
            this.GitHubError.Click += new System.EventHandler(this.GitHubError_Click);
            // 
            // NewSavePath
            // 
            this.NewSavePath.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // SelectListToDownloadFrom
            // 
            this.SelectListToDownloadFrom.Filter = "Plik tekstowy|*.txt";
            // 
            // Cannot_download_text
            // 
            this.Cannot_download_text.AutoSize = true;
            this.Cannot_download_text.BackColor = System.Drawing.SystemColors.Control;
            this.Cannot_download_text.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Cannot_download_text.Location = new System.Drawing.Point(28, 62);
            this.Cannot_download_text.Name = "Cannot_download_text";
            this.Cannot_download_text.Size = new System.Drawing.Size(73, 13);
            this.Cannot_download_text.TabIndex = 11;
            this.Cannot_download_text.Text = "Niepomyślnie:";
            // 
            // Can_download_text
            // 
            this.Can_download_text.AutoSize = true;
            this.Can_download_text.BackColor = System.Drawing.SystemColors.Control;
            this.Can_download_text.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Can_download_text.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Can_download_text.Location = new System.Drawing.Point(28, 42);
            this.Can_download_text.Name = "Can_download_text";
            this.Can_download_text.Size = new System.Drawing.Size(58, 13);
            this.Can_download_text.TabIndex = 12;
            this.Can_download_text.Text = "Pomyślnie:";
            // 
            // ToolTip
            // 
            this.ToolTip.AutoPopDelay = 10000;
            this.ToolTip.InitialDelay = 100;
            this.ToolTip.ReshowDelay = 100;
            this.ToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.ToolTip.ToolTipTitle = "Informacja";
            this.ToolTip.UseAnimation = false;
            this.ToolTip.UseFading = false;
            // 
            // PauseButton
            // 
            this.PauseButton.Location = new System.Drawing.Point(12, 150);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(183, 23);
            this.PauseButton.TabIndex = 2;
            this.PauseButton.Text = "Wstrzymaj";
            this.ToolTip.SetToolTip(this.PauseButton, "Wstrzymuje lub kontynuuje aktualną kolejkę (od kolejnego pliku)");
            this.PauseButton.UseVisualStyleBackColor = true;
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // UserPlaylist
            // 
            this.UserPlaylist.BackColor = System.Drawing.SystemColors.Window;
            this.UserPlaylist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserPlaylist.Enabled = false;
            this.UserPlaylist.ForeColor = System.Drawing.SystemColors.WindowText;
            this.UserPlaylist.Location = new System.Drawing.Point(9, 90);
            this.UserPlaylist.Name = "UserPlaylist";
            this.UserPlaylist.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.UserPlaylist.Size = new System.Drawing.Size(540, 90);
            this.UserPlaylist.TabIndex = 7;
            this.UserPlaylist.Text = "https://www.youtube.com/playlist?list=PL46644416B2D205DA\nhttps://www.youtube.com/" +
    "playlist?list=PLFBHIu8sTMFKLxamcXnWmRmRhxWxmcsu6";
            this.UserPlaylist.WordWrap = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox3);
            this.groupBox1.Controls.Add(this.counter);
            this.groupBox1.Controls.Add(this.Cannot_download_text);
            this.groupBox1.Controls.Add(this.Can_download_text);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox1.Location = new System.Drawing.Point(12, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(183, 86);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stan";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Checkbox_UseCombine);
            this.groupBox2.Controls.Add(this.Checkbox_UseUrl);
            this.groupBox2.Controls.Add(this.DirectUrlContainer);
            this.groupBox2.Controls.Add(this.UserPlaylist);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox2.Location = new System.Drawing.Point(201, 31);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(571, 190);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Adresy";
            // 
            // Checkbox_UseCombine
            // 
            this.Checkbox_UseCombine.AutoSize = true;
            this.Checkbox_UseCombine.Cursor = System.Windows.Forms.Cursors.Default;
            this.Checkbox_UseCombine.Enabled = false;
            this.Checkbox_UseCombine.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Checkbox_UseCombine.Location = new System.Drawing.Point(9, 70);
            this.Checkbox_UseCombine.Name = "Checkbox_UseCombine";
            this.Checkbox_UseCombine.Size = new System.Drawing.Size(103, 17);
            this.Checkbox_UseCombine.TabIndex = 6;
            this.Checkbox_UseCombine.Text = "Połącz playlisty:";
            this.Checkbox_UseCombine.UseVisualStyleBackColor = true;
            this.Checkbox_UseCombine.CheckedChanged += new System.EventHandler(this.Checkbox_UseCombine_CheckedChanged);
            // 
            // Checkbox_UseUrl
            // 
            this.Checkbox_UseUrl.AutoSize = true;
            this.Checkbox_UseUrl.Checked = true;
            this.Checkbox_UseUrl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Checkbox_UseUrl.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Checkbox_UseUrl.Location = new System.Drawing.Point(9, 20);
            this.Checkbox_UseUrl.Name = "Checkbox_UseUrl";
            this.Checkbox_UseUrl.Size = new System.Drawing.Size(118, 17);
            this.Checkbox_UseUrl.TabIndex = 4;
            this.Checkbox_UseUrl.Text = "Adres do pobrania:";
            this.Checkbox_UseUrl.UseVisualStyleBackColor = true;
            this.Checkbox_UseUrl.CheckedChanged += new System.EventHandler(this.Checkbox_UseUrl_CheckedChanged);
            // 
            // maxsizeof
            // 
            this.maxsizeof.AutoSize = true;
            this.maxsizeof.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.maxsizeof.Location = new System.Drawing.Point(329, 11);
            this.maxsizeof.Name = "maxsizeof";
            this.maxsizeof.Size = new System.Drawing.Size(49, 13);
            this.maxsizeof.TabIndex = 10;
            this.maxsizeof.Text = "Rozmiar:";
            this.maxsizeof.Visible = false;
            // 
            // backgroundthreads
            // 
            this.backgroundthreads.AutoSize = true;
            this.backgroundthreads.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.backgroundthreads.Location = new System.Drawing.Point(6, 11);
            this.backgroundthreads.Name = "backgroundthreads";
            this.backgroundthreads.Size = new System.Drawing.Size(81, 13);
            this.backgroundthreads.TabIndex = 9;
            this.backgroundthreads.Text = "Aktualnie w tle:";
            // 
            // whatsize
            // 
            this.whatsize.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.whatsize.Location = new System.Drawing.Point(672, 11);
            this.whatsize.Name = "whatsize";
            this.whatsize.Size = new System.Drawing.Size(60, 13);
            this.whatsize.TabIndex = 8;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.pictureBox4);
            this.groupBox3.Controls.Add(this.Information_container);
            this.groupBox3.Location = new System.Drawing.Point(-36, 220);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(866, 148);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.backgroundthreads);
            this.groupBox4.Controls.Add(this.maxsizeof);
            this.groupBox4.Controls.Add(this.whatsize);
            this.groupBox4.Location = new System.Drawing.Point(48, 74);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(738, 54);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            // 
            // speedtext
            // 
            this.speedtext.AutoSize = true;
            this.speedtext.Location = new System.Drawing.Point(40, 207);
            this.speedtext.Name = "speedtext";
            this.speedtext.Size = new System.Drawing.Size(147, 13);
            this.speedtext.TabIndex = 8;
            this.speedtext.Text = "Szybkie pobieranie załączone";
            this.speedtext.Visible = false;
            // 
            // speedimage
            // 
            this.speedimage.Image = global::NirrayYouTubeDownloader.Properties.Resources.runer_silhouette_running_fast;
            this.speedimage.Location = new System.Drawing.Point(18, 208);
            this.speedimage.Name = "speedimage";
            this.speedimage.Size = new System.Drawing.Size(16, 14);
            this.speedimage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.speedimage.TabIndex = 16;
            this.speedimage.TabStop = false;
            this.ToolTip.SetToolTip(this.speedimage, "Szybkie pobieranie pozwala na ściągnięcie\r\ndużej ilości plików w jak najkrótszym " +
        "czasie.\r\nNiedostępne są wtedy opcje takie jak:\r\nKonwersja na MP3\r\nWstrzymaj/Kont" +
        "ynuuj");
            this.speedimage.Visible = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox3.Image = global::NirrayYouTubeDownloader.Properties.Resources.folder;
            this.pictureBox3.Location = new System.Drawing.Point(6, 22);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(18, 14);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 15;
            this.pictureBox3.TabStop = false;
            this.ToolTip.SetToolTip(this.pictureBox3, "Otwórz folder zawierający");
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::NirrayYouTubeDownloader.Properties.Resources.correct;
            this.pictureBox1.Location = new System.Drawing.Point(6, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 14);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            this.ToolTip.SetToolTip(this.pictureBox1, "Ilość pomyślnie skopiowanych plików");
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::NirrayYouTubeDownloader.Properties.Resources.wrong;
            this.pictureBox2.Location = new System.Drawing.Point(6, 61);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 14);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            this.ToolTip.SetToolTip(this.pictureBox2, "Wszystkie możliwe błędy");
            // 
            // speedDownloadToolStripMenuItem
            // 
            this.speedDownloadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.turboNoLimitToolStripMenuItem,
            this.toolStripMenuItem2,
            this.tenToolStripMenuItem,
            this.twenToolStripMenuItem,
            this.thirToolStripMenuItem,
            this.własneUstawieniaToolStripMenuItem});
            this.speedDownloadToolStripMenuItem.Image = global::NirrayYouTubeDownloader.Properties.Resources.runer_silhouette_running_fast;
            this.speedDownloadToolStripMenuItem.Name = "speedDownloadToolStripMenuItem";
            this.speedDownloadToolStripMenuItem.Size = new System.Drawing.Size(339, 22);
            this.speedDownloadToolStripMenuItem.Text = "Szybkie pobieranie";
            // 
            // turboNoLimitToolStripMenuItem
            // 
            this.turboNoLimitToolStripMenuItem.CheckOnClick = true;
            this.turboNoLimitToolStripMenuItem.Name = "turboNoLimitToolStripMenuItem";
            this.turboNoLimitToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.turboNoLimitToolStripMenuItem.Text = "Bez limitu";
            this.turboNoLimitToolStripMenuItem.Click += new System.EventHandler(this.turboNoLimitToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.CheckOnClick = true;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(164, 22);
            this.toolStripMenuItem2.Text = "5 jednocześnie";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // tenToolStripMenuItem
            // 
            this.tenToolStripMenuItem.CheckOnClick = true;
            this.tenToolStripMenuItem.Name = "tenToolStripMenuItem";
            this.tenToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.tenToolStripMenuItem.Text = "10 jednocześnie";
            this.tenToolStripMenuItem.Click += new System.EventHandler(this.tenToolStripMenuItem_Click);
            // 
            // twenToolStripMenuItem
            // 
            this.twenToolStripMenuItem.CheckOnClick = true;
            this.twenToolStripMenuItem.Name = "twenToolStripMenuItem";
            this.twenToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.twenToolStripMenuItem.Text = "20 jednocześnie";
            this.twenToolStripMenuItem.Click += new System.EventHandler(this.twenToolStripMenuItem_Click);
            // 
            // thirToolStripMenuItem
            // 
            this.thirToolStripMenuItem.CheckOnClick = true;
            this.thirToolStripMenuItem.Name = "thirToolStripMenuItem";
            this.thirToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.thirToolStripMenuItem.Text = "30 jednocześnie";
            this.thirToolStripMenuItem.Click += new System.EventHandler(this.thirToolStripMenuItem_Click);
            // 
            // własneUstawieniaToolStripMenuItem
            // 
            this.własneUstawieniaToolStripMenuItem.Enabled = false;
            this.własneUstawieniaToolStripMenuItem.Name = "własneUstawieniaToolStripMenuItem";
            this.własneUstawieniaToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.własneUstawieniaToolStripMenuItem.Text = "Własne ustawienia";
            // 
            // translateToolStripMenuItem
            // 
            this.translateToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.translateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Lang_Polish,
            this.Lang_English});
            this.translateToolStripMenuItem.Image = global::NirrayYouTubeDownloader.Properties.Resources.translate;
            this.translateToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.translateToolStripMenuItem.Name = "translateToolStripMenuItem";
            this.translateToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.translateToolStripMenuItem.Text = "Język";
            // 
            // Lang_Polish
            // 
            this.Lang_Polish.Checked = true;
            this.Lang_Polish.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Lang_Polish.Image = global::NirrayYouTubeDownloader.Properties.Resources.polish_flag_small;
            this.Lang_Polish.Name = "Lang_Polish";
            this.Lang_Polish.Size = new System.Drawing.Size(180, 22);
            this.Lang_Polish.Text = "Polski";
            this.Lang_Polish.Click += new System.EventHandler(this.Lang_Polish_Click);
            // 
            // Lang_English
            // 
            this.Lang_English.Image = global::NirrayYouTubeDownloader.Properties.Resources.english_language_clip_art_14749;
            this.Lang_English.Name = "Lang_English";
            this.Lang_English.Size = new System.Drawing.Size(180, 22);
            this.Lang_English.Text = "English";
            this.Lang_English.Click += new System.EventHandler(this.Lang_English_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox4.Image = global::NirrayYouTubeDownloader.Properties.Resources.icons8_double_down_48;
            this.pictureBox4.Location = new System.Drawing.Point(792, 85);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(16, 14);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 16;
            this.pictureBox4.TabStop = false;
            this.ToolTip.SetToolTip(this.pictureBox4, "Przejdź na sam dół");
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Image = global::NirrayYouTubeDownloader.Properties.Resources.correct;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(23, 23);
            // 
            // NirrayYouTubeParser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 360);
            this.Controls.Add(this.speedimage);
            this.Controls.Add(this.speedtext);
            this.Controls.Add(this.DownloadFromDirectLink);
            this.Controls.Add(this.DownloadListButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.PauseButton);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox3);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 699);
            this.MinimumSize = new System.Drawing.Size(800, 399);
            this.Name = "NirrayYouTubeParser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nirray YouTube Downloader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NirrayYouTubeParser_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speedimage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DownloadListButton;
        private System.Windows.Forms.RichTextBox Information_container;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label counter;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel Progress_text;
        private System.Windows.Forms.TextBox DirectUrlContainer;
        private System.Windows.Forms.Button DownloadFromDirectLink;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem plikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolBox_ConvertToMp3;
        private System.Windows.Forms.ToolStripMenuItem pomocToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informacjeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GitHubError;
        private System.Windows.Forms.ToolStripMenuItem ToolBox_NewLocation;
        public System.Windows.Forms.FolderBrowserDialog NewSavePath;
        private System.Windows.Forms.OpenFileDialog SelectListToDownloadFrom;
        private System.Windows.Forms.Label Cannot_download_text;
        private System.Windows.Forms.Label Can_download_text;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.Button PauseButton;
        private System.Windows.Forms.ToolStripMenuItem translateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Lang_English;
        private System.Windows.Forms.ToolStripMenuItem Lang_Polish;
        private System.Windows.Forms.RichTextBox UserPlaylist;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox Checkbox_UseCombine;
        private System.Windows.Forms.CheckBox Checkbox_UseUrl;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label speedtext;
        private System.Windows.Forms.PictureBox speedimage;
        private System.Windows.Forms.Label whatsize;
        private System.Windows.Forms.Label backgroundthreads;
        private System.Windows.Forms.Label maxsizeof;
        private System.Windows.Forms.ToolStripMenuItem mp3_always_best;
        private System.Windows.Forms.ToolStripMenuItem mp3_32kb;
        private System.Windows.Forms.ToolStripMenuItem mp3_96kb;
        private System.Windows.Forms.ToolStripMenuItem mp3_128kb;
        private System.Windows.Forms.ToolStripMenuItem mp3_160kb;
        private System.Windows.Forms.ToolStripMenuItem mp3_192kb;
        private System.Windows.Forms.ToolStripMenuItem mp3_256kb;
        private System.Windows.Forms.ToolStripMenuItem mp3_272kb;
        private System.Windows.Forms.ToolStripMenuItem usuwajPlikiŹródłoweToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pobieranieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolBox_DoNotDownloadAlready;
        private System.Windows.Forms.ToolStripMenuItem countPlaylistDiscSpaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem speedDownloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem turboNoLimitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem twenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem własneUstawieniaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mp3_224kb;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.ToolStripMenuItem alternativeSquadToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}

