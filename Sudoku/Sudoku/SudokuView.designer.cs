/* http://multiplayersudoku.codeplex.com/ Version: 1.0.1.0
 * Author: Shakti Saran
 * Web-site: http://shaktisaran.tech.officelive.com/SudokuApp.aspx
 * Days: 3-4 (this program can be improved for errors, usability, performance, maintenance, reuse)
 * Idea: The basic idea of algorithm from http://www.academicearth.org/courses/programming-abstractions
 * Next: A server hosting a much larger Sudoku which multiple clients solve together
 * 
 * Sudoku project was to practice on C# by making a desktop application using some algorithm
 * Before this, I had worked on C, Windows Programming, Drivers etc. by making 
 * http://shaktisaran.tech.officelive.com/PersonalFolder.aspx and other software
 */

namespace Sudoku
{
    partial class Form_Sudoku
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Sudoku));
            this.buttonComputerSolve = new System.Windows.Forms.Button();
            this.buttonSetBoard = new System.Windows.Forms.Button();
            this.textBoxNumPreEntries = new System.Windows.Forms.TextBox();
            this.labelNumPreEntries = new System.Windows.Forms.Label();
            this.labelSudoku_TotalEntries = new System.Windows.Forms.Label();
            this.labelSudoku = new System.Windows.Forms.Label();
            this.labelTwoMinsSolve = new System.Windows.Forms.Label();
            this.DataGridView_Sudoku = new System.Windows.Forms.DataGridView();
            this.labelTwoMinsSet = new System.Windows.Forms.Label();
            this.linkLabelWebsite = new System.Windows.Forms.LinkLabel();
            this.comboBoxSudoku = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Sudoku)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonComputerSolve
            // 
            this.buttonComputerSolve.Enabled = false;
            this.buttonComputerSolve.Location = new System.Drawing.Point(520, 501);
            this.buttonComputerSolve.Name = "buttonComputerSolve";
            this.buttonComputerSolve.Size = new System.Drawing.Size(114, 25);
            this.buttonComputerSolve.TabIndex = 1;
            this.buttonComputerSolve.TabStop = false;
            this.buttonComputerSolve.Text = "Computer Solve";
            this.buttonComputerSolve.UseVisualStyleBackColor = true;
            this.buttonComputerSolve.Click += new System.EventHandler(this.buttonComputerSolve_Click);
            // 
            // buttonSetBoard
            // 
            this.buttonSetBoard.Location = new System.Drawing.Point(520, 72);
            this.buttonSetBoard.Name = "buttonSetBoard";
            this.buttonSetBoard.Size = new System.Drawing.Size(114, 25);
            this.buttonSetBoard.TabIndex = 2;
            this.buttonSetBoard.TabStop = false;
            this.buttonSetBoard.Text = "Set Board";
            this.buttonSetBoard.UseVisualStyleBackColor = true;
            this.buttonSetBoard.Click += new System.EventHandler(this.buttonSetBoard_Click);
            // 
            // textBoxNumPreEntries
            // 
            this.textBoxNumPreEntries.BackColor = System.Drawing.Color.Azure;
            this.textBoxNumPreEntries.Font = new System.Drawing.Font("Stencil", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNumPreEntries.ForeColor = System.Drawing.Color.DarkSalmon;
            this.textBoxNumPreEntries.Location = new System.Drawing.Point(583, 35);
            this.textBoxNumPreEntries.MaxLength = 0;
            this.textBoxNumPreEntries.Name = "textBoxNumPreEntries";
            this.textBoxNumPreEntries.Size = new System.Drawing.Size(51, 30);
            this.textBoxNumPreEntries.TabIndex = 4;
            this.textBoxNumPreEntries.TabStop = false;
            this.textBoxNumPreEntries.Text = "0";
            this.textBoxNumPreEntries.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxNumPreEntries.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxNumPreEntries_Validating);
            // 
            // labelNumPreEntries
            // 
            this.labelNumPreEntries.AutoSize = true;
            this.labelNumPreEntries.BackColor = System.Drawing.Color.DimGray;
            this.labelNumPreEntries.Font = new System.Drawing.Font("Script MT Bold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNumPreEntries.ForeColor = System.Drawing.SystemColors.Info;
            this.labelNumPreEntries.Location = new System.Drawing.Point(523, 9);
            this.labelNumPreEntries.Name = "labelNumPreEntries";
            this.labelNumPreEntries.Size = new System.Drawing.Size(111, 23);
            this.labelNumPreEntries.TabIndex = 3;
            this.labelNumPreEntries.Text = "# Pre Entries";
            // 
            // labelSudoku_TotalEntries
            // 
            this.labelSudoku_TotalEntries.AutoSize = true;
            this.labelSudoku_TotalEntries.BackColor = System.Drawing.Color.DimGray;
            this.labelSudoku_TotalEntries.Font = new System.Drawing.Font("Script MT Bold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSudoku_TotalEntries.ForeColor = System.Drawing.Color.Azure;
            this.labelSudoku_TotalEntries.Location = new System.Drawing.Point(523, 42);
            this.labelSudoku_TotalEntries.Name = "labelSudoku_TotalEntries";
            this.labelSudoku_TotalEntries.Size = new System.Drawing.Size(20, 23);
            this.labelSudoku_TotalEntries.TabIndex = 7;
            this.labelSudoku_TotalEntries.Text = "0";
            // 
            // labelSudoku
            // 
            this.labelSudoku.AutoSize = true;
            this.labelSudoku.BackColor = System.Drawing.Color.Transparent;
            this.labelSudoku.Font = new System.Drawing.Font("Snap ITC", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSudoku.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelSudoku.Location = new System.Drawing.Point(528, 276);
            this.labelSudoku.Name = "labelSudoku";
            this.labelSudoku.Size = new System.Drawing.Size(97, 27);
            this.labelSudoku.TabIndex = 8;
            this.labelSudoku.Text = "Sudoku";
            // 
            // labelTwoMinsSolve
            // 
            this.labelTwoMinsSolve.AutoSize = true;
            this.labelTwoMinsSolve.BackColor = System.Drawing.Color.DimGray;
            this.labelTwoMinsSolve.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTwoMinsSolve.ForeColor = System.Drawing.SystemColors.Info;
            this.labelTwoMinsSolve.Location = new System.Drawing.Point(515, 530);
            this.labelTwoMinsSolve.Name = "labelTwoMinsSolve";
            this.labelTwoMinsSolve.Size = new System.Drawing.Size(124, 17);
            this.labelTwoMinsSolve.TabIndex = 1;
            this.labelTwoMinsSolve.Text = "Uses 2 mins max";
            // 
            // DataGridView_Sudoku
            // 
            this.DataGridView_Sudoku.AllowUserToAddRows = false;
            this.DataGridView_Sudoku.AllowUserToDeleteRows = false;
            this.DataGridView_Sudoku.AllowUserToResizeColumns = false;
            this.DataGridView_Sudoku.AllowUserToResizeRows = false;
            this.DataGridView_Sudoku.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.DataGridView_Sudoku.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridView_Sudoku.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DataGridView_Sudoku.BackgroundColor = System.Drawing.Color.Black;
            this.DataGridView_Sudoku.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DataGridView_Sudoku.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.DataGridView_Sudoku.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DataGridView_Sudoku.ColumnHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Matura MT Script Capitals", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView_Sudoku.DefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView_Sudoku.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DataGridView_Sudoku.EnableHeadersVisualStyles = false;
            this.DataGridView_Sudoku.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.DataGridView_Sudoku.Location = new System.Drawing.Point(0, 0);
            this.DataGridView_Sudoku.MultiSelect = false;
            this.DataGridView_Sudoku.Name = "DataGridView_Sudoku";
            this.DataGridView_Sudoku.RowHeadersVisible = false;
            this.DataGridView_Sudoku.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DataGridView_Sudoku.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.DataGridView_Sudoku.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DataGridView_Sudoku.Size = new System.Drawing.Size(510, 560);
            this.DataGridView_Sudoku.StandardTab = true;
            this.DataGridView_Sudoku.TabIndex = 0;
            this.DataGridView_Sudoku.Visible = false;
            this.DataGridView_Sudoku.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.DataGridView_Sudoku_CellValidating);
            this.DataGridView_Sudoku.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DataGridView_Sudoku_DataError);
            // 
            // labelTwoMinsSet
            // 
            this.labelTwoMinsSet.AutoSize = true;
            this.labelTwoMinsSet.BackColor = System.Drawing.Color.DimGray;
            this.labelTwoMinsSet.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTwoMinsSet.ForeColor = System.Drawing.SystemColors.Info;
            this.labelTwoMinsSet.Location = new System.Drawing.Point(515, 102);
            this.labelTwoMinsSet.Name = "labelTwoMinsSet";
            this.labelTwoMinsSet.Size = new System.Drawing.Size(124, 17);
            this.labelTwoMinsSet.TabIndex = 4;
            this.labelTwoMinsSet.Text = "Uses 2 mins max";
            // 
            // linkLabelWebsite
            // 
            this.linkLabelWebsite.AutoSize = true;
            this.linkLabelWebsite.Location = new System.Drawing.Point(554, 307);
            this.linkLabelWebsite.Name = "linkLabelWebsite";
            this.linkLabelWebsite.Size = new System.Drawing.Size(49, 13);
            this.linkLabelWebsite.TabIndex = 9;
            this.linkLabelWebsite.TabStop = true;
            this.linkLabelWebsite.Text = "Web-site";
            this.linkLabelWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelWebsite_LinkClicked);
            // 
            // comboBoxSudoku
            // 
            this.comboBoxSudoku.BackColor = System.Drawing.Color.Azure;
            this.comboBoxSudoku.Font = new System.Drawing.Font("Snap ITC", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxSudoku.ForeColor = System.Drawing.Color.DarkCyan;
            this.comboBoxSudoku.FormattingEnabled = true;
            this.comboBoxSudoku.Location = new System.Drawing.Point(533, 243);
            this.comboBoxSudoku.Name = "comboBoxSudoku";
            this.comboBoxSudoku.Size = new System.Drawing.Size(92, 33);
            this.comboBoxSudoku.TabIndex = 10;
            this.comboBoxSudoku.SelectedIndexChanged += new System.EventHandler(this.comboBoxSudoku_SelectedIndexChanged);
            // 
            // Form_Sudoku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(643, 556);
            this.Controls.Add(this.comboBoxSudoku);
            this.Controls.Add(this.linkLabelWebsite);
            this.Controls.Add(this.labelTwoMinsSet);
            this.Controls.Add(this.labelTwoMinsSolve);
            this.Controls.Add(this.labelSudoku);
            this.Controls.Add(this.labelSudoku_TotalEntries);
            this.Controls.Add(this.labelNumPreEntries);
            this.Controls.Add(this.textBoxNumPreEntries);
            this.Controls.Add(this.buttonSetBoard);
            this.Controls.Add(this.buttonComputerSolve);
            this.Controls.Add(this.DataGridView_Sudoku);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form_Sudoku";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "http://shaktisaran.tech.officelive.com/SudokuApp.aspx";
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Sudoku)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonComputerSolve;
        private System.Windows.Forms.Button buttonSetBoard;
        private System.Windows.Forms.TextBox textBoxNumPreEntries;
        private System.Windows.Forms.Label labelNumPreEntries;
        private System.Windows.Forms.Label labelSudoku_TotalEntries;
        private System.Windows.Forms.Label labelSudoku;
        private System.Windows.Forms.Label labelTwoMinsSolve;
        private System.Windows.Forms.DataGridView DataGridView_Sudoku;
        private System.Windows.Forms.Label labelTwoMinsSet;
        private System.Windows.Forms.LinkLabel linkLabelWebsite;
        private System.Windows.Forms.ComboBox comboBoxSudoku;

        /*private*/
    }
}