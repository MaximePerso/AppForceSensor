namespace K2000Rs232App
{
    partial class SetUp
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.BtnLaunchAcquisition = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.dgvSetUp = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fichierSetUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ouvrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.RefreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.IniiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.RefreshtoolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.InitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChartSetUp = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.BtnValidateSetUp = new System.Windows.Forms.Button();
            this.BtnStartSetUp = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabelSetUp = new System.Windows.Forms.ToolStripStatusLabel();
            this.BtnEmergency = new System.Windows.Forms.Button();
            this.openFileDialogSetUp = new System.Windows.Forms.OpenFileDialog();
            this.Status_Error_richTextBoxSetUp = new System.Windows.Forms.RichTextBox();
            this.tbxPositionSetUp = new System.Windows.Forms.TextBox();
            this.tbxLoadSetUp = new System.Windows.Forms.TextBox();
            this.tbxExtensionSetUp = new System.Windows.Forms.TextBox();
            this.lblTimePalierSetUp = new System.Windows.Forms.Label();
            this.lblTimeSetUp = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetUp)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartSetUp)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnLaunchAcquisition
            // 
            this.BtnLaunchAcquisition.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.BtnLaunchAcquisition.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLaunchAcquisition.Location = new System.Drawing.Point(795, 12);
            this.BtnLaunchAcquisition.Name = "BtnLaunchAcquisition";
            this.BtnLaunchAcquisition.Size = new System.Drawing.Size(261, 158);
            this.BtnLaunchAcquisition.TabIndex = 2;
            this.BtnLaunchAcquisition.Text = "Lancer Phase \r\nAcquisition\r\n";
            this.BtnLaunchAcquisition.UseVisualStyleBackColor = false;
            this.BtnLaunchAcquisition.Click += new System.EventHandler(this.BtnLaunchAcquisition_Click);
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox3.Location = new System.Drawing.Point(1062, 120);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(398, 73);
            this.textBox3.TabIndex = 3;
            this.textBox3.Text = "0,00456";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.Black;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox4.Location = new System.Drawing.Point(1062, 199);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(398, 73);
            this.textBox4.TabIndex = 6;
            this.textBox4.Text = "0,004567";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dgvSetUp
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSetUp.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSetUp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSetUp.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSetUp.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvSetUp.Location = new System.Drawing.Point(12, 12);
            this.dgvSetUp.MultiSelect = false;
            this.dgvSetUp.Name = "dgvSetUp";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSetUp.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvSetUp.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvSetUp.Size = new System.Drawing.Size(777, 260);
            this.dgvSetUp.TabIndex = 8;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fichierSetUpToolStripMenuItem,
            this.toolStripSeparator2,
            this.RefreshtoolStripMenuItem1,
            this.toolStripSeparator4,
            this.InitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(157, 82);
            // 
            // fichierSetUpToolStripMenuItem
            // 
            this.fichierSetUpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ouvrirToolStripMenuItem,
            this.toolStripSeparator1,
            this.RefreshToolStripMenuItem,
            this.toolStripSeparator3,
            this.IniiToolStripMenuItem});
            this.fichierSetUpToolStripMenuItem.Name = "fichierSetUpToolStripMenuItem";
            this.fichierSetUpToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.fichierSetUpToolStripMenuItem.Text = "Fichier SetUp";
            // 
            // ouvrirToolStripMenuItem
            // 
            this.ouvrirToolStripMenuItem.Name = "ouvrirToolStripMenuItem";
            this.ouvrirToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.ouvrirToolStripMenuItem.Text = "Ouvrir";
            this.ouvrirToolStripMenuItem.Click += new System.EventHandler(this.OuvrirToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(155, 6);
            // 
            // RefreshToolStripMenuItem
            // 
            this.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem";
            this.RefreshToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.RefreshToolStripMenuItem.Text = "Enregistrer";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(155, 6);
            // 
            // IniiToolStripMenuItem
            // 
            this.IniiToolStripMenuItem.Name = "IniiToolStripMenuItem";
            this.IniiToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.IniiToolStripMenuItem.Text = "Enregistrer Sous";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(153, 6);
            // 
            // RefreshtoolStripMenuItem1
            // 
            this.RefreshtoolStripMenuItem1.Name = "RefreshtoolStripMenuItem1";
            this.RefreshtoolStripMenuItem1.Size = new System.Drawing.Size(156, 22);
            this.RefreshtoolStripMenuItem1.Text = "Rafraichir Table";
            this.RefreshtoolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItem1_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(153, 6);
            // 
            // InitToolStripMenuItem
            // 
            this.InitToolStripMenuItem.Image = global::AppForceSensor.Properties.Resources._5a059a909cf05203c4b6045b1;
            this.InitToolStripMenuItem.Name = "InitToolStripMenuItem";
            this.InitToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.InitToolStripMenuItem.Text = "Init";
            // 
            // ChartSetUp
            // 
            this.ChartSetUp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            chartArea2.Name = "ChartArea1";
            this.ChartSetUp.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.ChartSetUp.Legends.Add(legend2);
            this.ChartSetUp.Location = new System.Drawing.Point(12, 278);
            this.ChartSetUp.Name = "ChartSetUp";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.ChartSetUp.Series.Add(series2);
            this.ChartSetUp.Size = new System.Drawing.Size(1044, 427);
            this.ChartSetUp.TabIndex = 9;
            this.ChartSetUp.Text = "Chart Setup";
            // 
            // BtnValidateSetUp
            // 
            this.BtnValidateSetUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnValidateSetUp.Location = new System.Drawing.Point(1292, 12);
            this.BtnValidateSetUp.Name = "BtnValidateSetUp";
            this.BtnValidateSetUp.Size = new System.Drawing.Size(168, 99);
            this.BtnValidateSetUp.TabIndex = 12;
            this.BtnValidateSetUp.Text = "Valider\r\nRéglage";
            this.BtnValidateSetUp.UseVisualStyleBackColor = true;
            // 
            // BtnStartSetUp
            // 
            this.BtnStartSetUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnStartSetUp.Location = new System.Drawing.Point(1062, 12);
            this.BtnStartSetUp.Name = "BtnStartSetUp";
            this.BtnStartSetUp.Size = new System.Drawing.Size(168, 99);
            this.BtnStartSetUp.TabIndex = 13;
            this.BtnStartSetUp.Text = "Début \r\nRéglage";
            this.BtnStartSetUp.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabelSetUp});
            this.statusStrip1.Location = new System.Drawing.Point(0, 708);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1472, 22);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabelSetUp
            // 
            this.StatusLabelSetUp.Name = "StatusLabelSetUp";
            this.StatusLabelSetUp.Size = new System.Drawing.Size(0, 17);
            // 
            // BtnEmergency
            // 
            this.BtnEmergency.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnEmergency.BackColor = System.Drawing.Color.Red;
            this.BtnEmergency.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEmergency.Location = new System.Drawing.Point(1062, 386);
            this.BtnEmergency.Name = "BtnEmergency";
            this.BtnEmergency.Size = new System.Drawing.Size(398, 319);
            this.BtnEmergency.TabIndex = 16;
            this.BtnEmergency.Text = "ARRET URGENCE";
            this.BtnEmergency.UseVisualStyleBackColor = false;
            // 
            // openFileDialogSetUp
            // 
            this.openFileDialogSetUp.FileName = "openFileDialog1";
            // 
            // Status_Error_richTextBoxSetUp
            // 
            this.Status_Error_richTextBoxSetUp.Location = new System.Drawing.Point(795, 176);
            this.Status_Error_richTextBoxSetUp.Name = "Status_Error_richTextBoxSetUp";
            this.Status_Error_richTextBoxSetUp.Size = new System.Drawing.Size(261, 96);
            this.Status_Error_richTextBoxSetUp.TabIndex = 17;
            this.Status_Error_richTextBoxSetUp.Text = "";
            // 
            // tbxPositionSetUp
            // 
            this.tbxPositionSetUp.Location = new System.Drawing.Point(1344, 278);
            this.tbxPositionSetUp.Name = "tbxPositionSetUp";
            this.tbxPositionSetUp.Size = new System.Drawing.Size(100, 20);
            this.tbxPositionSetUp.TabIndex = 18;
            // 
            // tbxLoadSetUp
            // 
            this.tbxLoadSetUp.Location = new System.Drawing.Point(1344, 304);
            this.tbxLoadSetUp.Name = "tbxLoadSetUp";
            this.tbxLoadSetUp.Size = new System.Drawing.Size(100, 20);
            this.tbxLoadSetUp.TabIndex = 19;
            // 
            // tbxExtensionSetUp
            // 
            this.tbxExtensionSetUp.Location = new System.Drawing.Point(1344, 330);
            this.tbxExtensionSetUp.Name = "tbxExtensionSetUp";
            this.tbxExtensionSetUp.Size = new System.Drawing.Size(100, 20);
            this.tbxExtensionSetUp.TabIndex = 20;
            // 
            // lblTimePalierSetUp
            // 
            this.lblTimePalierSetUp.AutoSize = true;
            this.lblTimePalierSetUp.Location = new System.Drawing.Point(1087, 285);
            this.lblTimePalierSetUp.Name = "lblTimePalierSetUp";
            this.lblTimePalierSetUp.Size = new System.Drawing.Size(49, 13);
            this.lblTimePalierSetUp.TabIndex = 21;
            this.lblTimePalierSetUp.Text = "00:00:00";
            // 
            // lblTimeSetUp
            // 
            this.lblTimeSetUp.AutoSize = true;
            this.lblTimeSetUp.Location = new System.Drawing.Point(1087, 307);
            this.lblTimeSetUp.Name = "lblTimeSetUp";
            this.lblTimeSetUp.Size = new System.Drawing.Size(49, 13);
            this.lblTimeSetUp.TabIndex = 22;
            this.lblTimeSetUp.Text = "00:00:00";
            // 
            // SetUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1472, 730);
            this.Controls.Add(this.lblTimeSetUp);
            this.Controls.Add(this.lblTimePalierSetUp);
            this.Controls.Add(this.tbxExtensionSetUp);
            this.Controls.Add(this.tbxLoadSetUp);
            this.Controls.Add(this.tbxPositionSetUp);
            this.Controls.Add(this.Status_Error_richTextBoxSetUp);
            this.Controls.Add(this.BtnEmergency);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.BtnStartSetUp);
            this.Controls.Add(this.BtnValidateSetUp);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.ChartSetUp);
            this.Controls.Add(this.dgvSetUp);
            this.Controls.Add(this.BtnLaunchAcquisition);
            this.Name = "SetUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SetUp";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetUp)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChartSetUp)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BtnLaunchAcquisition;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.DataGridView dgvSetUp;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartSetUp;
        private System.Windows.Forms.Button BtnValidateSetUp;
        private System.Windows.Forms.Button BtnStartSetUp;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button BtnEmergency;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fichierSetUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ouvrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem RefreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem IniiToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem RefreshtoolStripMenuItem1;
        private System.Windows.Forms.OpenFileDialog openFileDialogSetUp;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelSetUp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem InitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.RichTextBox Status_Error_richTextBoxSetUp;
        private System.Windows.Forms.TextBox tbxPositionSetUp;
        private System.Windows.Forms.TextBox tbxLoadSetUp;
        private System.Windows.Forms.TextBox tbxExtensionSetUp;
        private System.Windows.Forms.Label lblTimePalierSetUp;
        private System.Windows.Forms.Label lblTimeSetUp;
    }
}