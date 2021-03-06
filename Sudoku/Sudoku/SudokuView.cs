﻿/* http://multiplayersudoku.codeplex.com/ Version: 1.0.1.0
 * Author: Shakti Saran
 * Web-site: http://shaktisaran.tech.officelive.com/SudokuApp.aspx
 * Days: 3-4 (this program can be improved for errors, usability, performance, maintenance, reuse)
 * Idea: The basic idea of algorithm is from http://www.academicearth.org/courses/programming-abstractions
 * Next: A server hosts a much larger Sudoku which multiple clients solve together
 * 
 * Sudoku project was to practice on C# by making a desktop application using some algorithm
 * Before this, I had worked on C, Windows Programming, Drivers etc. by making 
 * http://shaktisaran.tech.officelive.com/PersonalFolder.aspx and other software
 * 
 * Some of the program design is for multiplayer which I had thought of when starting this project
 * However, I haven't worked on multiplayer and so the program design might change when I work on it
 * 
 * I would enable the project for others to contribute after working on multiplayer for a while
 * However, you can download this for your use and perhaps make other more fun software
 */

using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Sudoku
{
    /* 
     * View assumes board's number of segments = 3
     * Only tested for 6x6, 9x9
     */

    public partial class Form_Sudoku : Form
    {
        private Sudoku oSudoku;

        private String strDataRangeMsg;
        private String strPreEntries;

        private UInt32 iTotalEntries;

        private const UInt32 cuiMinEntries = 1;

        private const String cstrSIX_BY_SIX = "6 x 6";
        private const String cstrNINE_BY_NINE = "9 x 9";

        private const String cstrErrorInvalidInput = "Sudoku: Invalid Input";
        private const String cstrComputerSolved = "Computer solved the board.\nYou may set the board again.";
        private const String cstrComputerNotSolve = "Computer could not solve the board.\nYou may edit board and try solver again.\nYou may set the board again.";
        private const String cstrSudokuSolved = "Sudoku: Solved";
        private const String cstrSudokuNotSolved = "Sudoku: Not Solved";

        private const String cstrSetBoard = "Set Board";
        private const String cstrSetting = "Setting...";
        private const String cstrComputerSolve = "Computer Solve";
        private const String cstrSolving = "Solving...";

        private const String cstrBoardIsSet = "Computer has set the board.\nYou may set the board again.";
        private const String cstrBoardIsSetTitle = "Sudoku: Board Set";
        private const String cstrBoardNotSet = "Computer could not set the board.\nYou may set the board again.";
        private const String cstrBoardNotSetTitle = "Sudoku: Board Not Set";

        public Form_Sudoku(Sudoku oSudoku)
        {
            InitializeComponent();

            this.oSudoku = oSudoku;

            initComboBoxSudoku();

            initSudokuSizeFromComboBox();

            initSudokuSizeRelatedData();
        }

        /* Only tested for 6x6, 9x9 */
        private void initComboBoxSudoku()
        {
            comboBoxSudoku.Items.Add(cstrNINE_BY_NINE);
            comboBoxSudoku.Items.Add(cstrSIX_BY_SIX);

            comboBoxSudoku.SelectedIndex = comboBoxSudoku.FindString(cstrNINE_BY_NINE);
        }

        private void initSudokuSizeFromComboBox()
        {
            DataGridView_Sudoku.Visible = false;
            DataGridView_Sudoku.DataSource = null;

            String strSelectedItem = comboBoxSudoku.SelectedItem.ToString();

            if (cstrNINE_BY_NINE == strSelectedItem)
            {
                oSudoku.Size = (UInt32)Sudoku.SIZES.eSIZE9;
            }
            else if (cstrSIX_BY_SIX == strSelectedItem)
            {
                oSudoku.Size = (UInt32)Sudoku.SIZES.eSIZE6;
            }
        }

        private void initSudokuSizeRelatedData()
        {
            oSudoku.NumPreEntries = oSudoku.Size;

            iTotalEntries = oSudoku.NumTotalEntries;

            strDataRangeMsg = "Only " + Sudoku.cuiFIRST_NUM + "-" + oSudoku.Size + " or empty allowed.\nEnsure no blank spaces.";
            strPreEntries = "# Pre Entries:\nOnly " + cuiMinEntries + "-" + iTotalEntries + " allowed.\nEnsure no blank spaces.";

            labelSudoku_TotalEntries.Text = cuiMinEntries + " - " + iTotalEntries;

            setTextBoxNumPreEntries();
        }

        private void setTextBoxNumPreEntries()
        {
            const UInt32 cuiBASE_IS_TEN = 10;

            UInt32 uiMaxLength = 0;
            UInt32 uiTotalEntriesTemp = iTotalEntries;

            while (uiTotalEntriesTemp > 0)
            {
                uiTotalEntriesTemp /= cuiBASE_IS_TEN;

                uiMaxLength++;
            }

            textBoxNumPreEntries.MaxLength = (Int32)uiMaxLength;
            textBoxNumPreEntries.Text = oSudoku.NumPreEntries.ToString();
        }

        private void comboBoxSudoku_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            initSudokuSizeFromComboBox();

            initSudokuSizeRelatedData();
        }

        private void setControlsEnable(bool bVal)
        {
            buttonSetBoard.Enabled = bVal;
            buttonComputerSolve.Enabled = bVal;

            textBoxNumPreEntries.Enabled = bVal;

            comboBoxSudoku.Enabled = bVal;
        }

        public delegate void delegateUpdateViewDuringSet(bool bVal);

        /* bVal: use false before set, true after set */
        public void UpdateViewDuringSet(bool bVal)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new delegateUpdateViewDuringSet(UpdateViewDuringSet), new object[] { true });

                return;
            }

            DataGridView_Sudoku.Visible = bVal;

            if (bVal)
            {
                oSudoku.SetBoard(DataGridView_Sudoku);

                DataGridView_Sudoku.Top = (ClientSize.Height - DataGridView_Sudoku.Rows.GetRowsHeight(DataGridViewElementStates.Displayed)) / 2;

                FormatStartDataCells();

                setControlsEnable(true);
                buttonSetBoard.Text = cstrSetBoard;

                Cursor.Current = Cursors.Default;
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;

                buttonSetBoard.Text = cstrSetting;
                setControlsEnable(false);

                DataGridView_Sudoku.DataSource = null;
            }

            if (bVal)
            {
                if (oSudoku.Solved)
                {
                    oSudoku.Solved = false;

                    MessageBox.Show(cstrBoardIsSet, cstrBoardIsSetTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(cstrBoardNotSet, cstrBoardNotSetTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void SetBoard()
        {
            oSudoku.SetBoard();

            UpdateViewDuringSet(true);
        }

        private void buttonSetBoard_Click(object sender, EventArgs e)
        {
            if (!oSudoku.SettingBoard)
            {
                try
                {
                    UpdateViewDuringSet(false);

                    Thread tSet = new Thread(new ThreadStart(SetBoard));
                    tSet.IsBackground = true;
                    tSet.Start();

                    tSet = null;
                }
                catch (Exception)
                {
                    oSudoku.Solved = false;

                    buttonSetBoard.Text = cstrSetBoard;
                    setControlsEnable(true);

                    Cursor.Current = Cursors.Default;
                }
            }
        }

        public delegate void delegateUpdateViewDuringSolve(bool bVal);

        /* bVal: use false before solve, true after solve */
        public void UpdateViewDuringSolve(bool bVal)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new delegateUpdateViewDuringSolve(UpdateViewDuringSolve), new object[] { true });

                return;
            }

            DataGridView_Sudoku.Enabled = bVal;

            if (bVal)
            {
                setControlsEnable(true);
                buttonComputerSolve.Text = cstrComputerSolve;

                Cursor.Current = Cursors.Default;
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;

                buttonComputerSolve.Text = cstrSolving;
                setControlsEnable(false);
            }

            if (bVal)
            {
                if (oSudoku.Solved)
                {
                    MessageBox.Show(cstrComputerSolved, cstrSudokuSolved, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(cstrComputerNotSolve, cstrSudokuNotSolved, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void SolveBoard()
        {
            oSudoku.SolveBoard();

            UpdateViewDuringSolve(true);
        }

        private void buttonComputerSolve_Click(object sender, EventArgs e)
        {
            if (!oSudoku.SettingBoard)
            {
                try
                {
                    UpdateViewDuringSolve(false);

                    Thread tSolve = new Thread(new ThreadStart(SolveBoard));
                    tSolve.IsBackground = true;
                    tSolve.Start();

                    tSolve = null;
                }
                catch (Exception)
                {
                    oSudoku.Solved = false;

                    buttonComputerSolve.Text = cstrComputerSolve;
                    setControlsEnable(true);

                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void DataGridView_Sudoku_DataError(object sender, System.Windows.Forms.DataGridViewDataErrorEventArgs e)
        {
            //MessageBox.Show("Exception: row = " + e.RowIndex + " col = " + e.ColumnIndex + "\n\n" + e.ToString(), "DataGridView_Sudoku_DataError", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private Color[] arrBoardColors = new Color[] { Color.LightCyan, Color.LightGray, Color.LightSalmon };

        /* View assumes board's number of segments = 3 */
        public void FormatStartDataCells()
        {
            UInt32 uiSegment_Size = oSudoku.Size / Sudoku.cuiSEGMENTS;
            UInt32 uiSegment_Two_Start = uiSegment_Size;
            UInt32 uiSegment_Three_Start = uiSegment_Size * (Sudoku.cuiSEGMENTS - 1);

            Random rndBoardColor = new Random();
            Int32 iBoardColor = rndBoardColor.Next(0, arrBoardColors.Length);

            for (UInt32 uiRow = 0; uiRow < oSudoku.Size; uiRow++)
            {
                for (UInt32 uiCol = 0; uiCol < oSudoku.Size; uiCol++)
                {
                    if ((uiRow < uiSegment_Two_Start && (uiCol >= uiSegment_Two_Start && uiCol < uiSegment_Three_Start)) ||
                        ((uiRow >= uiSegment_Two_Start && uiRow < uiSegment_Three_Start) &&
                        (uiCol < uiSegment_Two_Start || uiCol >= uiSegment_Three_Start)) ||
                        (uiRow >= uiSegment_Three_Start && (uiCol >= uiSegment_Two_Start && uiCol < uiSegment_Three_Start)))
                    {
                        DataGridView_Sudoku.Rows[(Int32)uiRow].Cells[(Int32)uiCol].Style.BackColor = arrBoardColors[iBoardColor];
                    }
                    else if ((uiRow >= uiSegment_Two_Start && uiRow < uiSegment_Three_Start) && (uiCol >= uiSegment_Two_Start || uiCol < uiSegment_Three_Start))
                    {
                        DataGridView_Sudoku.Rows[(Int32)uiRow].Cells[(Int32)uiCol].Style.BackColor = arrBoardColors[(iBoardColor + 1) % arrBoardColors.Length];
                    }
                    else
                    {
                        DataGridView_Sudoku.Rows[(Int32)uiRow].Cells[(Int32)uiCol].Style.BackColor = arrBoardColors[(iBoardColor + 2) % arrBoardColors.Length];
                    }

                    if (Int32.MinValue != oSudoku.GetRowColData(uiRow, uiCol))
                    {
                        DataGridView_Sudoku.Rows[(Int32)uiRow].Cells[(Int32)uiCol].Style.ForeColor = Color.DarkGoldenrod;
                        DataGridView_Sudoku.Rows[(Int32)uiRow].Cells[(Int32)uiCol].ReadOnly = true;
                    }
                }
            }

            DataGridView_Sudoku.CurrentCell = DataGridView_Sudoku[(Int32)(uiSegment_Size * (Sudoku.cuiSEGMENTS / 2)) + 1, (Int32)(uiSegment_Size * (Sudoku.cuiSEGMENTS / 2)) + 1];
        }

        private void DataGridView_Sudoku_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            Int32 iRowColData = 0;

            if (e.FormattedValue.ToString() == Sudoku.cstrUNASSIGNED_VALUE)
            {
                oSudoku.SetRowColData((UInt32)e.RowIndex, (UInt32)e.ColumnIndex, Sudoku.cstrUNASSIGNED_VALUE);

                e.Cancel = false;
            }
            else if (e.FormattedValue.ToString().Length == 0)
            {
                MessageBox.Show(strDataRangeMsg, cstrErrorInvalidInput, MessageBoxButtons.OK, MessageBoxIcon.Error);

                oSudoku.SetRowColData((UInt32)e.RowIndex, (UInt32)e.ColumnIndex, Sudoku.cstrUNASSIGNED_VALUE);

                e.Cancel = true;
            }
            else if (!int.TryParse(e.FormattedValue.ToString(), out iRowColData) || iRowColData < Sudoku.cuiFIRST_NUM || iRowColData > oSudoku.Size)
            {
                MessageBox.Show(strDataRangeMsg, cstrErrorInvalidInput, MessageBoxButtons.OK, MessageBoxIcon.Error);

                oSudoku.SetRowColData((UInt32)e.RowIndex, (UInt32)e.ColumnIndex, Sudoku.cstrUNASSIGNED_VALUE);

                e.Cancel = true;
            }
            else
            {
                if (oSudoku.NoConflictsDuringPlay((UInt32)e.RowIndex, (UInt32)e.ColumnIndex, (UInt32)iRowColData))
                {
                    e.Cancel = false;
                }
                else
                {
                    MessageBox.Show("Conflicting Digit: " + iRowColData, cstrErrorInvalidInput, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    oSudoku.SetRowColData((UInt32)e.RowIndex, (UInt32)e.ColumnIndex, Sudoku.cstrUNASSIGNED_VALUE);

                    e.Cancel = true;
                }
            }
        }

        private void textBoxNumPreEntries_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Int32 iPreEntries = 0;

            if (textBoxNumPreEntries.Text.Length == 0)
            {
                textBoxNumPreEntries.Text = oSudoku.NumPreEntries.ToString();

                e.Cancel = true;
            }
            else if (!int.TryParse(textBoxNumPreEntries.Text, out iPreEntries) || iPreEntries < cuiMinEntries || iPreEntries > iTotalEntries)
            {
                MessageBox.Show(strPreEntries, cstrErrorInvalidInput, MessageBoxButtons.OK, MessageBoxIcon.Error);

                textBoxNumPreEntries.Text = oSudoku.NumPreEntries.ToString();

                e.Cancel = true;
            }
            else
            {
                oSudoku.NumPreEntries = (UInt32)iPreEntries;

                e.Cancel = false;
            }
        }

        private void linkLabelWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://shaktisaran.tech.officelive.com/SudokuApp.aspx");
        }
    }
}