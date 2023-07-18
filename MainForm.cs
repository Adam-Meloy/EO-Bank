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
            if (loadSaveDialog.ShowDialog() == DialogResult.OK)
            {
                DetermineSaveGame(loadSaveDialog.FileName);
                switch (Game)
                {
                    case 1:
                        try
                        { SaveFile = new EO1SaveFile(loadSaveDialog.FileName); }
                        catch
                        { throw new CryptographicException("The file was not a valid EO1HD save."); }
                        break;
                    case 2:
                        try
                        { SaveFile = new EO2SaveFile(loadSaveDialog.FileName); }
                        catch
                        { throw new CryptographicException("The file was not a valid EO2HD save."); }
                        break;
                    case 3:
                    //  try
                    //  { SaveFile = new EO3SaveFile(loadSaveDialog.FileName); }
                    //  catch
                    //  { throw new CryptographicException("The file was not a valid EO2HD save."); }
                    //  TODO: Implement EO3 Save Reading
                    break;
                    default:
                        throw new InvalidDataException("The file is not a valid EO1HD, EO2HD, or EO3HD save.");
                }

                // MessageBox.Show($"Welcome, {SaveFile.GuildName}!");

                createSaveStrip.Enabled = true;
                createDecryptedSaveStrip.Enabled = true;
                characterStrip.Enabled = true;
            }
        }

        private void CreateDecryptedSaveStrip_Click(object sender, EventArgs e)
        {
            if (createDecryptedSaveDialog.ShowDialog() == DialogResult.OK)
            {
                SaveFile.WriteDecryptedSave(createDecryptedSaveDialog.FileName);
            }
        }

        private void ExportCharStrip_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();

            // commented out to prevent anything from happening
            //if (exportCharDialog.ShowDialog() == DialogResult.OK)
            //{
            //    CharacterSaveFile CharacterSave = new(exportCharDialog.FileName, SaveFile.Characters[0], SaveFile.GuildName, Game);
            //}
        }

        // Import Character
        private void ImportCharStrip_Click(object sender, EventArgs e)
        {
            if (importCharDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    CharacterSaveFile import = new(importCharDialog.FileName, Game);
                    Character selectedCharacter = import.Character;
                    string selectedGuildName = new string(import.GuildName).Replace("\0", "");
                    switch (Game)
                    {
                        case 1:
                            MessageBox.Show($"Welcome, {((EO1Character)selectedCharacter).GetName()} from Guild {selectedGuildName}!");
                            break;
                        case 2:
                            MessageBox.Show($"Welcome, {((EO2Character)selectedCharacter).GetName()} from Guild {selectedGuildName}!");
                            break;
                        case 3:
                            MessageBox.Show($"Welcome, {((EO3Character)selectedCharacter).GetName()} from Guild {selectedGuildName}!");
                            break;
                        default:
                            throw new InvalidDataException("You did not provide a valid EO1Char, EO2Char, or EO3Char file.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("You broke me. Here's what happened: " + ex);
                }
            }
        }

        // Show Character Status (Debug Tool)
        private void CharStatus_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                $"{SaveFile.Characters[0].GetName()}\n" +
                $"Job: {SaveFile.Characters[0].GetClass()}\n" +
                $"Level: {SaveFile.Characters[0].Level}\n" +
                $"Exp: {SaveFile.Characters[0].Exp}"
            );
        }

        // Settings
        private void Settings_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        // Exit Program
        private void Exit_Click(object sender, EventArgs e) { Application.Exit(); }

        private void Menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }

        /// <Summary>Determine what Game the Save is from</Summary>
        public void DetermineSaveGame(string FileName)
        {
            if (FileName.Contains("EOHD_game"))
                Game = 1;
            else if (FileName.Contains("EO2HD_game"))
                Game = 2;
            else if (FileName.Contains("EO3HD_game"))
                Game = 3;
            else
            {
                // This is peak exception handling right here.
                MessageBox.Show("System.IO.InvalidDataException: Invalid file provided.");
                throw new InvalidDataException("Invalid file provided.");
            }
        }
    }
}
