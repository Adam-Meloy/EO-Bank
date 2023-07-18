using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace EO_Bank
{
    public partial class MainForm : Form
    {
        short Game = 0; // 1: EO1, 2: EO2, 3: EO3
        /// <summary>Save File</summary>
        public SaveFile SaveFile;

        public MainForm() { InitializeComponent(); }

        private void Form1_Load(object sender, EventArgs e)
        {
            CharacterStrip.Enabled = false;
            CreateDecryptedSaveStrip.Enabled = false;
            CreateSaveStrip.Enabled = false;
        }

        // Create Save
        private void CreateSaveStrip_Click(object sender, EventArgs e)
        {
            try
            {
                if (CreateSaveDialog.ShowDialog() == DialogResult.OK)
                    SaveFile.WriteEncryptedSave(CreateSaveDialog.FileName);
            }
            catch (Exception ex) { MessageBox.Show("You broke me. Here's what happened: " + ex); }
        }

        // Create Decrypted Save
        private void CreateDecryptedSaveStrip_Click(object sender, EventArgs e)
        {
            if (CreateDecryptedSaveDialog.ShowDialog() == DialogResult.OK)
                SaveFile.WriteDecryptedSave(CreateDecryptedSaveDialog.FileName);
        }

        // Load Save
        private void LoadSaveStrip_Click(object sender, EventArgs e)
        {
            if (LoadSaveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DetermineSaveGame(LoadSaveDialog.FileName);
                    switch (Game)
                    {
                        case 1:
                            SaveFile = new EO1SaveFile(LoadSaveDialog.FileName);
                            break;
                        case 2:
                            SaveFile = new EO2SaveFile(LoadSaveDialog.FileName);
                            break;
                        case 3:
                            //  try
                            //  { SaveFile = new EO3SaveFile(loadSaveDialog.FileName); }
                            //  catch
                            //  { throw new CryptographicException("The file was not a valid EO2HD save."); }
                            //  TODO: Implement EO3 Save Reading
                            break;
                        default:
                            throw new InvalidDataException("The file is not a valid save or has been renamed.");
                    }
                }
                catch (Exception ex) { MessageBox.Show("You broke me. Here's what happened: " + ex); }

                MessageBox.Show($"Welcome, {SaveFile.GuildName}!");

                CreateSaveStrip.Enabled = true;
                CreateDecryptedSaveStrip.Enabled = true;
                CharacterStrip.Enabled = true;
            }
        }

        // Export EOChar
        private void ExportCharStrip_Click(object sender, EventArgs e)
        {
            try
            {
                if (ExportCharDialog.ShowDialog() == DialogResult.OK)
                    { CharacterSaveFile CharacterSave = new(ExportCharDialog.FileName, SaveFile.Characters[0], SaveFile.GuildName, Game); }
            }
            catch (Exception ex) { MessageBox.Show("You broke me. Here's what happened: " + ex); }
        }

        // Import EOChar
        private void ImportCharStrip_Click(object sender, EventArgs e)
        {
            try
            {
                CharacterSaveFile importedData = new();

                switch (Game)
                {
                    case 1:
                        if (ImportEO1CharDialog.ShowDialog() == DialogResult.OK)
                            importedData = new(ImportEO1CharDialog.FileName, Game);
                        break;
                    case 2:
                        if (ImportEO2CharDialog.ShowDialog() == DialogResult.OK)
                            importedData = new(ImportEO2CharDialog.FileName, Game);
                        break;
                    case 3:
                        throw new NotImplementedException();
                        //if (ImportEO3CharDialog.ShowDialog() == DialogResult.OK)
                        //    importedData = new(ImportEO3CharDialog.FileName, Game);
                        //break;
                    default:
                        throw new InvalidDataException("You did not provide a valid EO1Char, EO2Char, or EO3Char file.");
                }
                MessageBox.Show($"Welcome, {importedData.Character.GetName()} from Guild {new string(importedData.GuildName).Replace("\0", "")}!");
            }
            catch (Exception ex) { MessageBox.Show("You broke me. Here's what happened: " + ex); }
        }

        // Settings
        private void Settings_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        // Exit Program
        private void Exit_Click(object sender, EventArgs e) { Application.Exit(); }

        /// <Summary>Determine what Game the Save is from</Summary>
        public void DetermineSaveGame(string FileName)
        {
            if (FileName.Contains("EOHD_game", StringComparison.CurrentCultureIgnoreCase))
                Game = 1;
            else if (FileName.Contains("EO2HD_game", StringComparison.CurrentCultureIgnoreCase))
                Game = 2;
            else if (FileName.Contains("EO3HD_game", StringComparison.CurrentCultureIgnoreCase))
                Game = 3;
            else
                throw new InvalidDataException("Invalid file provided.");
        }

        // Show Character Status (Debug Tool)
        private void CharacterStatus_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                $"{SaveFile.Characters[0].GetName()}\n" +
                $"Job: {SaveFile.Characters[0].GetClass()}\n" +
                $"Level: {SaveFile.Characters[0].Level}\n" +
                $"Exp: {SaveFile.Characters[0].EXP}"
            );
        }
    }
}
