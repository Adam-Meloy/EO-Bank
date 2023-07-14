using System;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace EO_Bank
{
    public partial class MainForm : Form
    {
        int Game = 0; // 1: EO1, 2: EO2, 3: EO3
        /// <summary>
        /// Save File
        /// </summary>
        public EO1SaveFile SaveFile;
        public DecryptSave Decrypted;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            characterStrip.Enabled = false;
            createDecryptedSaveStrip.Enabled = false;
            createSaveStrip.Enabled = false;
        }

        // Create Save
        private void CreateSaveStrip_Click(object sender, EventArgs e)
        {
            if (createSaveDialog.ShowDialog() == DialogResult.OK)
            {
                SaveFile.WriteEncryptedSave(createSaveDialog.FileName);
            }
        }

        // Load Save
        private void LoadSaveStrip_Click(object sender, EventArgs e)
        {
            // i have no idea how to decrypt a save bro
            if (loadSaveDialog.ShowDialog() == DialogResult.OK)
            {
                DetermineSaveGame(loadSaveDialog.FileName);
                switch (Game)
                {
                    case 0:
                    default:
                        MessageBox.Show("The file is not a valid EO save or was renamed. Exiting program.");
                        Application.Exit();
                        break;
                    case 1:
                        try
                        { SaveFile = new EO1SaveFile(loadSaveDialog.FileName); }
                        catch (CryptographicException)
                        {
                            MessageBox.Show("The file was invalid, decrypted, or named incorrectly. Exiting program.");
                            Application.Exit();
                        }
                        break;
                    case 2:
                        //SaveFile = new EO2SaveFile(loadSaveDialog.FileName);
                        // TODO: Implement EO2 Save Reading
                        break;
                    case 3:
                        //SaveFile = new EO3SaveFile(loadSaveDialog.FileName);
                        // TODO: Implement EO3 Save Reading
                        break;
                }
                Decrypted = new(loadSaveDialog.FileName);

                MessageBox.Show($"Welcome, {SaveFile.GuildName}");

                // debug reasons
                CharNames.Text = SaveFile.Characters[0].PrintName();

                createSaveStrip.Enabled = true;
                createDecryptedSaveStrip.Enabled = true;
                characterStrip.Enabled = true;
            }
        }

        private void CreateDecryptedSaveStrip_Click(object sender, EventArgs e)
        {
            if (createDecryptedSaveDialog.ShowDialog() == DialogResult.OK)
            {
                Decrypted.WriteDecryptedSave(createDecryptedSaveDialog.FileName);
            }
        }

        private void ExportCharStrip_Click(object sender, EventArgs e)
        {
            // Character file will be decrypted bin files with guild name at the end.
            MessageBox.Show("Hello! This doesn't actually do anything yet.");

            // commented out to prevent shenanigans
            //if (exportCharDialog.ShowDialog() == DialogResult.OK)
            //{
            //    EO1CharacterSave EO1CharacterSave = new();
            //}
        }

        // Import Character
        private void ImportCharStrip_Click(object sender, EventArgs e)
        {
            // i assume i put the character info into like, hex, maybe json

            MessageBox.Show("Hello! This doesn't actually do anything yet.");

            // commented out to prevent shenanigans
            //if (importCharDialog.ShowDialog() == DialogResult.OK)
            //{
            //    // commence reading of character file and put it somewhere
            //}
        }

        // Show Character Status
        private void CharStatus_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                $"{SaveFile.Characters[0].PrintName()}\n" +
                $"Job: {SaveFile.Characters[0].ClassName()}\n" +
                $"Level: {SaveFile.Characters[0].Level}\n" +
                $"Exp: {SaveFile.Characters[0].Exp}"
            );
        }

        // Settings
        private void Settings_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello! This doesn't actually do anything yet.");
        }

        // Exit Program
        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }

        /// <Summary>
        /// determine what game the save is from
        /// </Summary>
        public void DetermineSaveGame(string FileName)
        {
            if (FileName.Contains("EOHD_game"))
                Game = 1;
            else if (FileName.Contains("EO2HD_game"))
                Game = 2;
            else if (FileName.Contains("EO3HD_game"))
                Game = 3;
        }
    }
}
