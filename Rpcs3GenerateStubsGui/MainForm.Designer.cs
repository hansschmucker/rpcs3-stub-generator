namespace Rpcs3GenerateStubsGui
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Rpcs3Searcher = new System.Windows.Forms.OpenFileDialog();
            this.OutputSearcher = new System.Windows.Forms.FolderBrowserDialog();
            this.RPCS3PathText = new System.Windows.Forms.TextBox();
            this.ShortcutsPathText = new System.Windows.Forms.TextBox();
            this.RPCS3BrowserLabel = new System.Windows.Forms.Label();
            this.ShortcutsBrowserLabel = new System.Windows.Forms.Label();
            this.DiscoveredGamesList = new System.Windows.Forms.ListBox();
            this.Rpcs3PathBrowseButton = new System.Windows.Forms.Button();
            this.ShortcutsFolderBrowseButton = new System.Windows.Forms.Button();
            this.GenerateStubsButton = new System.Windows.Forms.Button();
            this.ListGamesDiscovered = new System.Windows.Forms.Label();
            this.NoGuiOption = new System.Windows.Forms.CheckBox();
            this.CbAddAditionalElfStubs = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // Rpcs3Searcher
            // 
            this.Rpcs3Searcher.FileName = "rpcs3.exe";
            // 
            // RPCS3PathText
            // 
            this.RPCS3PathText.Location = new System.Drawing.Point(176, 12);
            this.RPCS3PathText.Name = "RPCS3PathText";
            this.RPCS3PathText.Size = new System.Drawing.Size(1194, 26);
            this.RPCS3PathText.TabIndex = 0;
            this.RPCS3PathText.TextChanged += new System.EventHandler(this.RPCS3PathText_TextChanged);
            // 
            // ShortcutsPathText
            // 
            this.ShortcutsPathText.Location = new System.Drawing.Point(176, 56);
            this.ShortcutsPathText.Name = "ShortcutsPathText";
            this.ShortcutsPathText.Size = new System.Drawing.Size(1194, 26);
            this.ShortcutsPathText.TabIndex = 1;
            this.ShortcutsPathText.TextChanged += new System.EventHandler(this.ShortcutsPathText_TextChanged);
            // 
            // RPCS3BrowserLabel
            // 
            this.RPCS3BrowserLabel.AutoSize = true;
            this.RPCS3BrowserLabel.Location = new System.Drawing.Point(12, 18);
            this.RPCS3BrowserLabel.Name = "RPCS3BrowserLabel";
            this.RPCS3BrowserLabel.Size = new System.Drawing.Size(99, 20);
            this.RPCS3BrowserLabel.TabIndex = 2;
            this.RPCS3BrowserLabel.Text = "RPCS3 Path";
            // 
            // ShortcutsBrowserLabel
            // 
            this.ShortcutsBrowserLabel.AutoSize = true;
            this.ShortcutsBrowserLabel.Location = new System.Drawing.Point(12, 56);
            this.ShortcutsBrowserLabel.Name = "ShortcutsBrowserLabel";
            this.ShortcutsBrowserLabel.Size = new System.Drawing.Size(145, 20);
            this.ShortcutsBrowserLabel.TabIndex = 3;
            this.ShortcutsBrowserLabel.Text = "Shortcuts Directory";
            // 
            // DiscoveredGamesList
            // 
            this.DiscoveredGamesList.FormattingEnabled = true;
            this.DiscoveredGamesList.ItemHeight = 20;
            this.DiscoveredGamesList.Location = new System.Drawing.Point(16, 124);
            this.DiscoveredGamesList.Name = "DiscoveredGamesList";
            this.DiscoveredGamesList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.DiscoveredGamesList.Size = new System.Drawing.Size(1435, 264);
            this.DiscoveredGamesList.TabIndex = 4;
            // 
            // Rpcs3PathBrowseButton
            // 
            this.Rpcs3PathBrowseButton.Location = new System.Drawing.Point(1376, 12);
            this.Rpcs3PathBrowseButton.Name = "Rpcs3PathBrowseButton";
            this.Rpcs3PathBrowseButton.Size = new System.Drawing.Size(75, 38);
            this.Rpcs3PathBrowseButton.TabIndex = 5;
            this.Rpcs3PathBrowseButton.Text = "Browse";
            this.Rpcs3PathBrowseButton.UseVisualStyleBackColor = true;
            this.Rpcs3PathBrowseButton.Click += new System.EventHandler(this.Rpcs3PathBrowseButton_Click);
            // 
            // ShortcutsFolderBrowseButton
            // 
            this.ShortcutsFolderBrowseButton.Location = new System.Drawing.Point(1376, 56);
            this.ShortcutsFolderBrowseButton.Name = "ShortcutsFolderBrowseButton";
            this.ShortcutsFolderBrowseButton.Size = new System.Drawing.Size(75, 38);
            this.ShortcutsFolderBrowseButton.TabIndex = 6;
            this.ShortcutsFolderBrowseButton.Text = "Browse";
            this.ShortcutsFolderBrowseButton.UseVisualStyleBackColor = true;
            this.ShortcutsFolderBrowseButton.Click += new System.EventHandler(this.ShortcutsFolderBrowseButton_Click);
            // 
            // GenerateStubsButton
            // 
            this.GenerateStubsButton.Location = new System.Drawing.Point(1224, 405);
            this.GenerateStubsButton.Name = "GenerateStubsButton";
            this.GenerateStubsButton.Size = new System.Drawing.Size(227, 44);
            this.GenerateStubsButton.TabIndex = 7;
            this.GenerateStubsButton.Text = "Generate Shortcuts";
            this.GenerateStubsButton.UseVisualStyleBackColor = true;
            this.GenerateStubsButton.Click += new System.EventHandler(this.GenerateStubsButton_Click);
            // 
            // ListGamesDiscovered
            // 
            this.ListGamesDiscovered.AutoSize = true;
            this.ListGamesDiscovered.Location = new System.Drawing.Point(12, 101);
            this.ListGamesDiscovered.Name = "ListGamesDiscovered";
            this.ListGamesDiscovered.Size = new System.Drawing.Size(386, 20);
            this.ListGamesDiscovered.TabIndex = 8;
            this.ListGamesDiscovered.Text = "Games found (select items to only generate selected)";
            // 
            // NoGuiOption
            // 
            this.NoGuiOption.AutoSize = true;
            this.NoGuiOption.Location = new System.Drawing.Point(16, 394);
            this.NoGuiOption.Name = "NoGuiOption";
            this.NoGuiOption.Size = new System.Drawing.Size(276, 24);
            this.NoGuiOption.TabIndex = 9;
            this.NoGuiOption.Text = "Hide RPCS3 GUI on game launch";
            this.NoGuiOption.UseVisualStyleBackColor = true;
            this.NoGuiOption.CheckedChanged += new System.EventHandler(this.NoGuiOption_CheckedChanged);
            // 
            // CbAddAditionalElfStubs
            // 
            this.CbAddAditionalElfStubs.AutoSize = true;
            this.CbAddAditionalElfStubs.Location = new System.Drawing.Point(16, 425);
            this.CbAddAditionalElfStubs.Name = "CbAddAditionalElfStubs";
            this.CbAddAditionalElfStubs.Size = new System.Drawing.Size(291, 24);
            this.CbAddAditionalElfStubs.TabIndex = 10;
            this.CbAddAditionalElfStubs.Text = "Add Shortcuts for additional binaries";
            this.CbAddAditionalElfStubs.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1463, 460);
            this.Controls.Add(this.CbAddAditionalElfStubs);
            this.Controls.Add(this.NoGuiOption);
            this.Controls.Add(this.ListGamesDiscovered);
            this.Controls.Add(this.GenerateStubsButton);
            this.Controls.Add(this.ShortcutsFolderBrowseButton);
            this.Controls.Add(this.Rpcs3PathBrowseButton);
            this.Controls.Add(this.DiscoveredGamesList);
            this.Controls.Add(this.ShortcutsBrowserLabel);
            this.Controls.Add(this.RPCS3BrowserLabel);
            this.Controls.Add(this.ShortcutsPathText);
            this.Controls.Add(this.RPCS3PathText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rpcs3 Shortcut Generator";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog Rpcs3Searcher;
        private System.Windows.Forms.FolderBrowserDialog OutputSearcher;
        private System.Windows.Forms.TextBox RPCS3PathText;
        private System.Windows.Forms.TextBox ShortcutsPathText;
        private System.Windows.Forms.Label RPCS3BrowserLabel;
        private System.Windows.Forms.Label ShortcutsBrowserLabel;
        private System.Windows.Forms.ListBox DiscoveredGamesList;
        private System.Windows.Forms.Button Rpcs3PathBrowseButton;
        private System.Windows.Forms.Button ShortcutsFolderBrowseButton;
        private System.Windows.Forms.Button GenerateStubsButton;
        private System.Windows.Forms.Label ListGamesDiscovered;
        private System.Windows.Forms.CheckBox NoGuiOption;
        private System.Windows.Forms.CheckBox CbAddAditionalElfStubs;
    }
}

