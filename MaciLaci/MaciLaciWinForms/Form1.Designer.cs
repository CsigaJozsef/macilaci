namespace MaciLaciWinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.nehézségToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easyModeButton = new System.Windows.Forms.ToolStripMenuItem();
            this.mediumModeButton = new System.Windows.Forms.ToolStripMenuItem();
            this.hardModeButton = new System.Windows.Forms.ToolStripMenuItem();
            this.startButton = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutGrid = new System.Windows.Forms.TableLayoutPanel();
            this.statStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statStrip
            // 
            this.statStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statStrip.Location = new System.Drawing.Point(0, 428);
            this.statStrip.Name = "statStrip";
            this.statStrip.Size = new System.Drawing.Size(800, 22);
            this.statStrip.TabIndex = 0;
            this.statStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(211, 17);
            this.statusLabel.Text = "Nyomj Játékot a vadászat elkezdéséhez";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nehézségToolStripMenuItem,
            this.startButton});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // nehézségToolStripMenuItem
            // 
            this.nehézségToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.easyModeButton,
            this.mediumModeButton,
            this.hardModeButton});
            this.nehézségToolStripMenuItem.Name = "nehézségToolStripMenuItem";
            this.nehézségToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.nehézségToolStripMenuItem.Text = "Nehézség";
            // 
            // easyModeButton
            // 
            this.easyModeButton.Name = "easyModeButton";
            this.easyModeButton.Size = new System.Drawing.Size(117, 22);
            this.easyModeButton.Text = "Könnyű";
            this.easyModeButton.Click += new System.EventHandler(this.easyModButton_Click);
            // 
            // mediumModeButton
            // 
            this.mediumModeButton.Name = "mediumModeButton";
            this.mediumModeButton.Size = new System.Drawing.Size(117, 22);
            this.mediumModeButton.Text = "Közepes";
            this.mediumModeButton.Click += new System.EventHandler(this.mediumModButton_Click);
            // 
            // hardModeButton
            // 
            this.hardModeButton.Name = "hardModeButton";
            this.hardModeButton.Size = new System.Drawing.Size(117, 22);
            this.hardModeButton.Text = "Nehéz";
            this.hardModeButton.Click += new System.EventHandler(this.hardModButton_Click);
            // 
            // startButton
            // 
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(45, 20);
            this.startButton.Text = "Játék";
            this.startButton.Click += new System.EventHandler(this.startButton_Clicked);
            // 
            // tableLayoutGrid
            // 
            this.tableLayoutGrid.ColumnCount = 2;
            this.tableLayoutGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutGrid.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutGrid.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutGrid.Name = "tableLayoutGrid";
            this.tableLayoutGrid.RowCount = 2;
            this.tableLayoutGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutGrid.Size = new System.Drawing.Size(800, 404);
            this.tableLayoutGrid.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutGrid);
            this.Controls.Add(this.statStrip);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "MaciLaci";
            this.statStrip.ResumeLayout(false);
            this.statStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StatusStrip statStrip;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem nehézségToolStripMenuItem;
        private ToolStripMenuItem easyModeButton;
        private ToolStripMenuItem mediumModeButton;
        private ToolStripMenuItem startButton;
        private TableLayoutPanel tableLayoutGrid;
        private ToolStripMenuItem hardModeButton;
        private ToolStripStatusLabel statusLabel;
    }
}