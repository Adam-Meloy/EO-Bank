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
            menuStrip = new MenuStrip();
            fileStrip = new ToolStripMenuItem();
            loadSaveStrip = new ToolStripMenuItem();
            createSaveStrip = new ToolStripMenuItem();
            createDecryptedSaveStrip = new ToolStripMenuItem();
            settingsStrip = new ToolStripMenuItem();
            exitStrip = new ToolStripMenuItem();
            characterStrip = new ToolStripMenuItem();
            charStatusStrip = new ToolStripMenuItem();
            exportCharStrip = new ToolStripMenuItem();
            importCharStrip = new ToolStripMenuItem();
            createSaveDialog = new SaveFileDialog();
            loadSaveDialog = new OpenFileDialog();
            importCharDialog = new OpenFileDialog();
            exportCharDialog = new SaveFileDialog();
            createDecryptedSaveDialog = new SaveFileDialog();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { fileStrip, characterStrip });
            menuStrip.Location = new System.Drawing.Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(5, 2, 0, 2);
            menuStrip.Size = new System.Drawing.Size(700, 24);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "Menu";
            menuStrip.ItemClicked += Menu_ItemClicked;
            // 
            // fileStrip
            // 
            fileStrip.DropDownItems.AddRange(new ToolStripItem[] { loadSaveStrip, createSaveStrip, createDecryptedSaveStrip, settingsStrip, exitStrip });
            fileStrip.Name = "fileStrip";
            fileStrip.Size = new System.Drawing.Size(37, 20);
            fileStrip.Text = "File";
            // 
            // loadSaveStrip
            // 
            loadSaveStrip.Name = "loadSaveStrip";
            loadSaveStrip.Size = new System.Drawing.Size(192, 22);
            loadSaveStrip.Text = "Load Save";
            loadSaveStrip.Click += LoadSaveStrip_Click;
            // 
            // createSaveStrip
            // 
            createSaveStrip.Name = "createSaveStrip";
            createSaveStrip.Size = new System.Drawing.Size(192, 22);
            createSaveStrip.Text = "Create Save";
            createSaveStrip.Click += CreateSaveStrip_Click;
            // 
            // createDecryptedSaveStrip
            // 
            createDecryptedSaveStrip.Name = "createDecryptedSaveStrip";
            createDecryptedSaveStrip.Size = new System.Drawing.Size(192, 22);
            createDecryptedSaveStrip.Text = "Create Decrypted Save";
            createDecryptedSaveStrip.Click += CreateDecryptedSaveStrip_Click;
            // 
            // settingsStrip
            // 
            settingsStrip.Name = "settingsStrip";
            settingsStrip.Size = new System.Drawing.Size(192, 22);
            settingsStrip.Text = "Settings";
            settingsStrip.Click += Settings_Click;
            // 
            // exitStrip
            // 
            exitStrip.Name = "exitStrip";
            exitStrip.Size = new System.Drawing.Size(192, 22);
            exitStrip.Text = "Exit";
            exitStrip.Click += Exit_Click;
            // 
            // characterStrip
            // 
            characterStrip.DropDownItems.AddRange(new ToolStripItem[] { charStatusStrip, exportCharStrip, importCharStrip });
            characterStrip.Name = "characterStrip";
            characterStrip.Size = new System.Drawing.Size(70, 20);
            characterStrip.Text = "Character";
            // 
            // charStatusStrip
            // 
            charStatusStrip.Name = "charStatusStrip";
            charStatusStrip.Size = new System.Drawing.Size(164, 22);
            charStatusStrip.Text = "Character Status";
            charStatusStrip.Click += CharStatus_Click;
            // 
            // exportCharStrip
            // 
            exportCharStrip.Name = "exportCharStrip";
            exportCharStrip.Size = new System.Drawing.Size(164, 22);
            exportCharStrip.Text = "Export Character";
            exportCharStrip.Click += ExportCharStrip_Click;
            // 
            // importCharStrip
            // 
            importCharStrip.Name = "importCharStrip";
            importCharStrip.Size = new System.Drawing.Size(164, 22);
            importCharStrip.Text = "Import Character";
            importCharStrip.Click += ImportCharStrip_Click;
            // 
            // createSaveDialog
            // 
            createSaveDialog.CreatePrompt = true;
            createSaveDialog.Filter = "BIN File (*.bin)|*.bin";
            createSaveDialog.InitialDirectory = "%appdata%/SEGA/EOHD";
            // 
            // loadSaveDialog
            // 
            loadSaveDialog.Filter = "BIN File (*.bin)|*.bin";
            loadSaveDialog.InitialDirectory = "%appdata%/SEGA";
            loadSaveDialog.Title = "Load Save";
            // 
            // importCharDialog
            // 
            importCharDialog.Filter = "EO1 Character File (*.eo1char)|*.eo1char";
            importCharDialog.Title = "Import Character";
            // 
            // exportCharDialog
            // 
            exportCharDialog.CreatePrompt = true;
            exportCharDialog.Filter = "EO1 Character File (*.eo1char)|*.eo1char";
            exportCharDialog.Title = "Export Character";
            // 
            // createDecryptedSaveDialog
            // 
            createDecryptedSaveDialog.CreatePrompt = true;
            createDecryptedSaveDialog.Filter = "BIN File (*.bin)|*.bin";
            createDecryptedSaveDialog.InitialDirectory = "%appdata%/SEGA/EOHD";
            // 
            // MainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(700, 422);
            Controls.Add(menuStrip);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip;
            Name = "MainForm";
            Text = "EO Bank";
            Load += Form1_Load;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem fileStrip;
        private ToolStripMenuItem loadSaveStrip;
        private ToolStripMenuItem createSaveStrip;
        private ToolStripMenuItem characterStrip;
        private ToolStripMenuItem charStatusStrip;
        private ToolStripMenuItem exportCharStrip;
        private ToolStripMenuItem importCharStrip;
        private ToolStripMenuItem settingsStrip;
        private ToolStripMenuItem exitStrip;
        private SaveFileDialog createSaveDialog;
        private OpenFileDialog loadSaveDialog;
        private OpenFileDialog importCharDialog;
        private SaveFileDialog exportCharDialog;
        private ToolStripMenuItem createDecryptedSaveStrip;
        private SaveFileDialog createDecryptedSaveDialog;
    }
}

