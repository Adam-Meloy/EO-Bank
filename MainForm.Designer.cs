using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace EO_Bank
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
            MenuStrip = new MenuStrip();
            FileStrip = new ToolStripMenuItem();
            LoadSaveStrip = new ToolStripMenuItem();
            CreateSaveStrip = new ToolStripMenuItem();
            CreateDecryptedSaveStrip = new ToolStripMenuItem();
            SettingsStrip = new ToolStripMenuItem();
            ExitStrip = new ToolStripMenuItem();
            CharacterStrip = new ToolStripMenuItem();
            ExportCharStrip = new ToolStripMenuItem();
            ImportCharStrip = new ToolStripMenuItem();
            CharacterStatusStrip = new ToolStripMenuItem();
            CreateSaveDialog = new SaveFileDialog();
            LoadSaveDialog = new OpenFileDialog();
            ImportEO1CharDialog = new OpenFileDialog();
            ImportEO2CharDialog = new OpenFileDialog();
            ImportEO3CharDialog = new OpenFileDialog();
            ExportCharDialog = new SaveFileDialog();
            CreateDecryptedSaveDialog = new SaveFileDialog();
            CharacterPanel = new Panel();
            MenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // MenuStrip
            // 
            MenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            MenuStrip.Items.AddRange(new ToolStripItem[] { FileStrip, CharacterStrip, CharacterStatusStrip });
            MenuStrip.Location = new System.Drawing.Point(0, 0);
            MenuStrip.Name = "MenuStrip";
            MenuStrip.Padding = new Padding(5, 2, 0, 2);
            MenuStrip.Size = new System.Drawing.Size(984, 24);
            MenuStrip.TabIndex = 0;
            MenuStrip.Text = "Menu";
            // 
            // FileStrip
            // 
            FileStrip.DropDownItems.AddRange(new ToolStripItem[] { LoadSaveStrip, CreateSaveStrip, CreateDecryptedSaveStrip, SettingsStrip, ExitStrip });
            FileStrip.Name = "FileStrip";
            FileStrip.Size = new System.Drawing.Size(37, 20);
            FileStrip.Text = "File";
            // 
            // LoadSaveStrip
            // 
            LoadSaveStrip.Name = "LoadSaveStrip";
            LoadSaveStrip.Size = new System.Drawing.Size(192, 22);
            LoadSaveStrip.Text = "Load Save";
            LoadSaveStrip.Click += LoadSaveStrip_Click;
            // 
            // CreateSaveStrip
            // 
            CreateSaveStrip.Name = "CreateSaveStrip";
            CreateSaveStrip.Size = new System.Drawing.Size(192, 22);
            CreateSaveStrip.Text = "Create Save";
            CreateSaveStrip.Click += CreateSaveStrip_Click;
            // 
            // CreateDecryptedSaveStrip
            // 
            CreateDecryptedSaveStrip.Name = "CreateDecryptedSaveStrip";
            CreateDecryptedSaveStrip.Size = new System.Drawing.Size(192, 22);
            CreateDecryptedSaveStrip.Text = "Create Decrypted Save";
            CreateDecryptedSaveStrip.Click += CreateDecryptedSaveStrip_Click;
            // 
            // SettingsStrip
            // 
            SettingsStrip.Name = "SettingsStrip";
            SettingsStrip.Size = new System.Drawing.Size(192, 22);
            SettingsStrip.Text = "Settings";
            SettingsStrip.Click += Settings_Click;
            // 
            // ExitStrip
            // 
            ExitStrip.Name = "ExitStrip";
            ExitStrip.Size = new System.Drawing.Size(192, 22);
            ExitStrip.Text = "Exit";
            ExitStrip.Click += Exit_Click;
            // 
            // CharacterStrip
            // 
            CharacterStrip.DropDownItems.AddRange(new ToolStripItem[] { ExportCharStrip, ImportCharStrip });
            CharacterStrip.Name = "CharacterStrip";
            CharacterStrip.Size = new System.Drawing.Size(70, 20);
            CharacterStrip.Text = "Character";
            // 
            // ExportCharStrip
            // 
            ExportCharStrip.Name = "ExportCharStrip";
            ExportCharStrip.Size = new System.Drawing.Size(164, 22);
            ExportCharStrip.Text = "Export Character";
            ExportCharStrip.Click += ExportCharStrip_Click;
            // 
            // ImportCharStrip
            // 
            ImportCharStrip.Name = "ImportCharStrip";
            ImportCharStrip.Size = new System.Drawing.Size(164, 22);
            ImportCharStrip.Text = "Import Character";
            ImportCharStrip.Click += ImportCharStrip_Click;
            // 
            // CharacterStatusStrip
            // 
            CharacterStatusStrip.Name = "CharacterStatusStrip";
            CharacterStatusStrip.Size = new System.Drawing.Size(105, 20);
            CharacterStatusStrip.Text = "Character Status";
            CharacterStatusStrip.Click += CharacterStatus_Click;
            // 
            // CreateSaveDialog
            // 
            CreateSaveDialog.CreatePrompt = true;
            CreateSaveDialog.Filter = "BIN File (*.bin)|*.bin";
            CreateSaveDialog.InitialDirectory = "%appdata%/SEGA/EOHD";
            // 
            // LoadSaveDialog
            // 
            LoadSaveDialog.Filter = "BIN File (*.bin)|*.bin";
            LoadSaveDialog.InitialDirectory = "%appdata%/SEGA";
            LoadSaveDialog.Title = "Load Save";
            // 
            // ImportEO1CharDialog
            // 
            ImportEO1CharDialog.Filter = "EO1 Character File (*.eo1char)|*.eo1char";
            ImportEO1CharDialog.Title = "Import EO1 Character";
            // 
            // ImportEO2CharDialog
            // 
            ImportEO2CharDialog.Filter = "EO2 Character File (*.eo2char)|*.eo2char";
            ImportEO2CharDialog.Title = "Import EO2 Character";
            // 
            // ImportEO3CharDialog
            // 
            ImportEO3CharDialog.Filter = "EO3 Character File (*.eo3char)|*.eo3char";
            ImportEO3CharDialog.Title = "Import EO3 Character";
            // 
            // ExportCharDialog
            // 
            ExportCharDialog.CreatePrompt = true;
            ExportCharDialog.Filter = "EO1 Character File (*.eo1char)|*.eo1char";
            ExportCharDialog.Title = "Export Character";
            // 
            // CreateDecryptedSaveDialog
            // 
            CreateDecryptedSaveDialog.CreatePrompt = true;
            CreateDecryptedSaveDialog.Filter = "BIN File (*.bin)|*.bin";
            CreateDecryptedSaveDialog.InitialDirectory = "%appdata%/SEGA/EOHD";
            // 
            // CharacterPanel
            // 
            CharacterPanel.Location = new System.Drawing.Point(367, 27);
            CharacterPanel.Name = "CharacterPanel";
            CharacterPanel.Size = new System.Drawing.Size(605, 672);
            CharacterPanel.TabIndex = 1;
            // 
            // MainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(984, 711);
            Controls.Add(CharacterPanel);
            Controls.Add(MenuStrip);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = MenuStrip;
            Name = "MainForm";
            Text = "EO Bank";
            Load += Form1_Load;
            MenuStrip.ResumeLayout(false);
            MenuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip MenuStrip;
        private ToolStripMenuItem FileStrip;
        private ToolStripMenuItem LoadSaveStrip;
        private ToolStripMenuItem CreateSaveStrip;
        private ToolStripMenuItem CharacterStrip;
        private ToolStripMenuItem ExportCharStrip;
        private ToolStripMenuItem ImportCharStrip;
        private ToolStripMenuItem SettingsStrip;
        private ToolStripMenuItem ExitStrip;
        private SaveFileDialog CreateSaveDialog;
        private OpenFileDialog LoadSaveDialog;
        private OpenFileDialog ImportEO1CharDialog;
        private OpenFileDialog ImportEO2CharDialog;
        private OpenFileDialog ImportEO3CharDialog;
        private SaveFileDialog ExportCharDialog;
        private ToolStripMenuItem CreateDecryptedSaveStrip;
        private SaveFileDialog CreateDecryptedSaveDialog;
        private Panel CharacterPanel;
        private ToolStripMenuItem CharacterStatusStrip;
    }
}

