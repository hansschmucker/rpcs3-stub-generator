using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rpcs3GenerateStubsGui
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void RPCS3PathText_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(RPCS3PathText.Text))
                SaveRpcs3Path(RPCS3PathText.Text);
        }
        private void ShortcutsPathText_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(ShortcutsPathText.Text))
                SaveShortcutsPath(ShortcutsPathText.Text);
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            string rpcs3Path = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Rpcs3GenerateStubs", "Rpcs3Path", null);
            string shortcutsFolder = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Rpcs3GenerateStubs", "ShortcutsPath", null);
            string hideGui = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Rpcs3GenerateStubs", "HideGui", null);
            if (!String.IsNullOrWhiteSpace(rpcs3Path) && File.Exists(rpcs3Path))
                SaveRpcs3Path(rpcs3Path);
            if (!String.IsNullOrWhiteSpace(shortcutsFolder) && Directory.Exists(shortcutsFolder))
                SaveShortcutsPath(shortcutsFolder);
            
            SaveHideGuiSetting(!String.IsNullOrWhiteSpace(hideGui) && hideGui=="TRUE");
        }

        private void SaveRpcs3Path(string path)
        {
            try
            {
                Rpcs3 = new PS3Utils.RPCS3(Path.GetDirectoryName(path));
                var games = Rpcs3.GetAllGames();
                DiscoveredGamesList.Items.Clear();
                foreach (var i in games)
                    DiscoveredGamesList.Items.Add(i);

            }
            catch (Exception)
            {
                MessageBox.Show(this, "There was an issue processing the RPCS library.", "Error");
            }

            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Rpcs3GenerateStubs", "Rpcs3Path", path);
            Rpcs3Searcher.FileName = path;
            RPCS3PathText.Text = path;
        }

        private void SaveHideGuiSetting(bool enabled)
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Rpcs3GenerateStubs", "HideGui", enabled?"TRUE":"FALSE");
            NoGuiOption.Checked = enabled;
        }
        private void SaveShortcutsPath(string path)
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Rpcs3GenerateStubs", "ShortcutsPath", path);
            OutputSearcher.SelectedPath = path;
            ShortcutsPathText.Text = path;
        }

        private void Rpcs3PathBrowseButton_Click(object sender, EventArgs e)
        {
            DialogResult browseResult=DialogResult.Abort;
            do
            {
                browseResult=Rpcs3Searcher.ShowDialog(this);
            }while(browseResult==DialogResult.OK && !File.Exists(Rpcs3Searcher.FileName));

            if (browseResult == DialogResult.OK)
                SaveRpcs3Path(Rpcs3Searcher.FileName);
        }

        private void ShortcutsFolderBrowseButton_Click(object sender, EventArgs e)
        {
            DialogResult browseResult = DialogResult.Abort;
            do
            {
                browseResult = OutputSearcher.ShowDialog(this);
            } while (browseResult == DialogResult.OK && !Directory.Exists(OutputSearcher.SelectedPath));

            if (browseResult == DialogResult.OK)
                SaveShortcutsPath(OutputSearcher.SelectedPath);
        }

        private void GenerateStubsButton_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(ShortcutsPathText.Text) || !Directory.Exists(ShortcutsPathText.Text))
            {
                MessageBox.Show(this, "Please select an output folder", "Error");
                return;
            }

            if (Rpcs3==null)
            {
                MessageBox.Show(this, "Please select the Rpcs3path", "Error");
                return;
            }

            var items = DiscoveredGamesList.SelectedItems.Cast<PS3Utils.PS3Game>();
            if (items == null || !items.Any())
                items = DiscoveredGamesList.Items.Cast<PS3Utils.PS3Game>();

            PS3Utils.PS3Game currentGame=null;
            try
            {
                foreach (var item in items)
                {
                    currentGame = item;
                    if (CbAddAditionalElfStubs.Checked)
                    {
                        var settings = Rpcs3.GetUsefulApplicationTargets(item, ShortcutsPathText.Text, NoGuiOption.Checked);
                        foreach(var setting in settings)
                            PS3Utils.Compiler.CompileLauncherExe(setting);
                    }
                    else
                    {
                        var settings = Rpcs3.GetApplicationTarget(item, ShortcutsPathText.Text, NoGuiOption.Checked);
                        PS3Utils.Compiler.CompileLauncherExe(settings);
                    }
                }
            }catch(Exception ex)
            {
                if (currentGame == null)
                {
                    MessageBox.Show(this, "There was an issue creating shortcuts.\r\n" + ex, "Error");
                } else {
                    MessageBox.Show(this, "There was an issue processing the game " + currentGame.GamePath + "\r\n" + ex, "Error");
                }
            }

        }

        public PS3Utils.RPCS3 Rpcs3=null;

        private void NoGuiOption_CheckedChanged(object sender, EventArgs e)
        {
            SaveHideGuiSetting(NoGuiOption.Checked);
        }
    }
}
