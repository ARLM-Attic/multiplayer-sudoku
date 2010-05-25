/* http://multiplayersudoku.codeplex.com/ Version: 1.0.1.0
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
using System.Timers;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Sudoku
{
    public class Sudoku
    {
        public Sudoku()
        {
            arrAREventSolve[0] = new AutoResetEvent(true);
            arrAREventSolve[0].Set();

            arrAREventSet[0] = new AutoResetEvent(true);
            arrAREventSet[0].Set();

            Size = 0;
            NumPreEntries = 0;
        }

        private AutoResetEvent[] arrAREventSet = new AutoResetEvent[1];
        private AutoResetEvent[] arrAREventSolve = new AutoResetEvent[1];

        /* Sudoku board entries */
        private DataTable data;

        private const UInt32 cuiFIRST_ROW = 0;
        private const UInt32 cuiFIRST_COL = 0;

        public const String cstrUNASSIGNED_VALUE = "";
        public const UInt32 cuiFIRST_NUM = 1;

        /* Only works for segments = 3 which view assumes, 2-2-2 for 6x6, 3-3-3 for 9x9 */
        public const UInt32 cuiSEGMENTS = 3;

        /* tried for 6x6 and 9x9 */
        public enum SIZES { eSIZE6 = 6, eSIZE9 = 9 };

        private UInt32 uiSize;

        public UInt32 Size
        {
            get
            {
                return uiSize;
            }
            set
            {
                if (value != (UInt32)SIZES.eSIZE6 && value != (UInt32)SIZES.eSIZE9)
                {
                    uiSize = (UInt32)SIZES.eSIZE9;
                }
                else
                {
                    uiSize = value;
                }
                NumTotalEntries = uiSize * uiSize;
            }
        }

        private UInt32 uiNumPreEntries;

        public UInt32 NumPreEntries
        {
            get
            {
                return uiNumPreEntries;
            }
            set
            {
                if (value < uiSize || value > NumTotalEntries)
                {
                    uiNumPreEntries = ((UInt32)SIZES.eSIZE6 == uiSize) ? (UInt32)SIZES.eSIZE6 : (UInt32)SIZES.eSIZE9;
                }
                else
                {
                    uiNumPreEntries = value;
                }
            }
        }

        private UInt32 uiNumTotalEntries;

        public UInt32 NumTotalEntries
        {
            get
            {
                return uiNumTotalEntries;
            }
            private set
            {
                uiNumTotalEntries = value;
            }
        }

        private bool bSolved = false;

        public bool Solved
        {
            get
            {
                return bSolved;
            }
            set
            {
                bSolved = value;
            }
        }

        private void DBNullToStringData()
        {
            for (UInt32 uiRow = cuiFIRST_ROW; uiRow < uiSize; uiRow++)
            {
                for (UInt32 uiCol = cuiFIRST_COL; uiCol < uiSize; uiCol++)
                {
                    if (DBNull.Value == data.Rows[(Int32)uiRow][(Int32)uiCol])
                    {
                        data.Rows[(Int32)uiRow].SetField((Int32)uiCol, cstrUNASSIGNED_VALUE);
                    }
                }
            }
        }

        public AutoResetEvent GetAREventSet()
        {
            return arrAREventSet[0];
        }

        private bool bSettingBoard = false;

        public bool SettingBoard
        {
            get
            {
                return bSettingBoard;
            }
            set
            {
                bSettingBoard = value;
            }
        }

        public void SetBoard(DataGridView dgvSudoku)
        {
            dgvSudoku.DataSource = data;
        }

        public void SetBoard()
        {
            WaitHandle.WaitAny(arrAREventSet);
            WaitHandle.WaitAny(arrAREventSolve);

            bSettingBoard = true;

            try
            {
                InitData();

                if (null == data)
                {
                    arrAREventSolve[0].Set();
                    arrAREventSet[0].Set();

                    bSolved = false;
                    bSettingBoard = false;

                    return;
                }

                DBNullToStringData();
                SolveBoard();

                if (bSolved)
                {
                    RemoveFromBoard();
                }
            }
            catch (Exception)
            {
                bSolved = false;
            }

            bSettingBoard = false;

            arrAREventSolve[0].Set();
            arrAREventSet[0].Set();
        }

        /* Int32.MinValue + 1 for index out of bounds, Int32.MinValue for non-integer value in cell */
        public const Int32 ciGET_INDEX_OUT_OF_BOUNDS = Int32.MinValue + 1;
        public const Int32 ciGET_NON_INTEGER_IN_CELL = Int32.MinValue;

        public Int32 GetRowColData(UInt32 uiRow, UInt32 uiCol)
        {
            Int32 iRowColData;

            if (uiRow >= uiSize || uiCol >= uiSize)
            {
                iRowColData = ciGET_INDEX_OUT_OF_BOUNDS;
            }
            else if (!System.Int32.TryParse(((String)data.Rows[(Int32)uiRow][(Int32)uiCol]), out iRowColData))
            {
                iRowColData = ciGET_NON_INTEGER_IN_CELL;
            }

            return iRowColData;
        }

        public bool SetRowColData(UInt32 uiRow, UInt32 uiCol, String strData)
        {
            Int32 iRowColData;

            if (uiRow < uiSize || uiCol < uiSize)
            {
                if (!System.Int32.TryParse(strData, out iRowColData))
                {
                    data.Rows[(Int32)uiRow].SetField((Int32)uiCol, cstrUNASSIGNED_VALUE);
                }
                else
                {
                    data.Rows[(Int32)uiRow].SetField((Int32)uiCol, strData);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private void InitData()
        {
            if (null != data)
            {
                data.Clear();
                data.Dispose();
                data = null;
            }

            data = new DataTable();

            if (null == data)
            {
                return;
            }

            UInt32 uiCol = cuiFIRST_COL;

            const String sCOL = "C";

            while (uiCol < uiSize)
            {
                data.Columns.Add(sCOL + uiCol);

                uiCol++;
            }

            data.Rows.Clear();

            string[] arrstrRowInitData = new string[uiSize];

            for (uiCol = cuiFIRST_COL; uiCol < uiSize; uiCol++)
            {
                arrstrRowInitData[uiCol] = cstrUNASSIGNED_VALUE;
            }
            for (UInt32 uiRow = cuiFIRST_ROW; uiRow < uiSize; uiRow++)
            {
                data.LoadDataRow(arrstrRowInitData, false);
            }
        }

        private void RemoveFromBoard()
        {
            List<UInt32> lstAssigned = new List<UInt32>();
            UInt32 uiCount = 0;

            while (uiCount < NumTotalEntries)
            {
                lstAssigned.Insert((Int32)uiCount, 0);

                ++uiCount;
            }

            Random rndInsert = new Random();
            UInt32 uiRndInsert = 0;

            uiRndInsert = (UInt32)rndInsert.Next(0, (Int32)uiCount);

            while (uiCount > 0)
            {
                --uiCount;

                lstAssigned[(Int32)uiCount] = (uiCount + uiRndInsert) % (NumTotalEntries);
            }

            UInt32 uiRemoveAt = 0;
            UInt32 uiToRemove = 0;
            Random rndRemove = new Random();

            uiCount = NumTotalEntries;

            while (uiCount > NumPreEntries)
            {
                uiRemoveAt = (UInt32)rndRemove.Next(0, (Int32)uiCount);
                uiToRemove = lstAssigned[(Int32)uiRemoveAt];
                lstAssigned.RemoveAt((Int32)uiRemoveAt);
                data.Rows[(Int32)(uiToRemove / uiSize)].SetField((Int32)(uiToRemove % uiSize), cstrUNASSIGNED_VALUE);

                --uiCount;
            }
        }

        public AutoResetEvent GetAREventSolve()
        {
            return arrAREventSolve[0];
        }

        public void SolveBoard()
        {
            if (!bSettingBoard)
            {
                WaitHandle.WaitAny(arrAREventSolve);
            }

            try
            {
                StartSolver();
            }
            catch (Exception)
            {
                bSolved = false;
            }

            if (!bSettingBoard)
            {
                arrAREventSolve[0].Set();
            }
        }

        /* for debugging
        public UInt32 uiAssigned = 0;
         */

        private bool bAbortSolver = false;

        /* no testing for selecting this after which the solver aborts */
        private const UInt32 cuiMILLI_SECONDS_TO_SOLVE = 2 * 60 * 1000;

        private void StartSolver()
        {
            bSolved = false;
            bAbortSolver = false;
            bRowColRndInitFind = false;

            System.Timers.Timer tSolver = new System.Timers.Timer(cuiMILLI_SECONDS_TO_SOLVE);

            tSolver.AutoReset = false;
            tSolver.Elapsed += AbortSolve;
            tSolver.Start();

            try
            {
                /* uiAssigned = 0; */

                bSolved = Solve();
            }
            catch (DataException)
            {
                bSolved = false;
            }

            tSolver.Stop();
            tSolver.Dispose();
            tSolver = null;

            if (bAbortSolver)
            {
                bSolved = false;
            }
        }

        private void AbortSolve(object sender, System.Timers.ElapsedEventArgs e)
        {
            bAbortSolver = true;
        }

        private bool Solve()
        {
            UInt32 iRowUnassigned = cuiFIRST_ROW, iColUnassigned = cuiFIRST_COL;

            if (bAbortSolver)
            {
                return true;
            }

            if (!FindUnassignedLocation(ref iRowUnassigned, ref iColUnassigned))
            {
                return true;
            }

            for (UInt32 uiDigit = cuiFIRST_NUM; uiDigit <= uiSize; uiDigit++)
            {
                if (NoConflicts(iRowUnassigned, iColUnassigned, uiDigit))
                {
                    data.Rows[(Int32)iRowUnassigned].SetField((Int32)iColUnassigned, uiDigit);

                    /*                    
                    uiAssigned++;
                    if (uiSize == uiAssigned)
                    {
                        MessageBox.Show("uiAssigned", "uiAssigned", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        bAbortSolver = true;
                    }
                     */

                    if (Solve())
                    {
                        return true;
                    }
                }
            }

            data.Rows[(Int32)iRowUnassigned].SetField((Int32)iColUnassigned, cstrUNASSIGNED_VALUE);

            /*
            uiAssigned--;
             */

            return false;
        }

        public bool NoConflictsDuringPlay(UInt32 uiCurrRow, UInt32 uiCurrCol, UInt32 uiDigit)
        {
            Int32 iRowColData;

            for (UInt32 uiRow = cuiFIRST_ROW; uiRow < uiSize; uiRow++)
            {
                if (DBNull.Value == data.Rows[(Int32)uiRow][(Int32)uiCurrCol])
                {
                    data.Rows[(Int32)uiRow].SetField((Int32)uiCurrCol, cstrUNASSIGNED_VALUE);

                    continue;
                }

                if (System.Int32.TryParse(((String)data.Rows[(Int32)uiRow][(Int32)uiCurrCol]), out iRowColData))
                {
                    if ((uiRow != uiCurrRow) && (Int32)uiDigit == iRowColData)
                    {
                        return false;
                    }
                }
            }

            for (UInt32 uiCol = cuiFIRST_COL; uiCol < uiSize; uiCol++)
            {
                if (DBNull.Value == data.Rows[(Int32)uiCurrRow][(Int32)uiCol])
                {
                    data.Rows[(Int32)uiCurrRow].SetField((Int32)uiCol, cstrUNASSIGNED_VALUE);

                    continue;
                }

                if (System.Int32.TryParse(((String)data.Rows[(Int32)uiCurrRow][(Int32)uiCol]), out iRowColData))
                {
                    if ((uiCol != uiCurrCol) && (Int32)(uiDigit) == iRowColData)
                    {
                        return false;
                    }
                }
            }

            UInt32 uiSegment_Size = uiSize / cuiSEGMENTS;

            UInt32 uiRow_Segment_Start = (uiCurrRow / uiSegment_Size) * uiSegment_Size;
            UInt32 uiRow_Segment_End = uiRow_Segment_Start + uiSegment_Size;
            UInt32 uiCol_Segment_Start = (uiCurrCol / uiSegment_Size) * uiSegment_Size;
            UInt32 uiCol_Segment_End = uiCol_Segment_Start + uiSegment_Size;

            for (UInt32 uiRow = uiRow_Segment_Start; uiRow < uiRow_Segment_End; uiRow++)
            {
                for (UInt32 uiCol = uiCol_Segment_Start; uiCol < uiCol_Segment_End; uiCol++)
                {
                    if (DBNull.Value == data.Rows[(Int32)uiRow][(Int32)uiCol])
                    {
                        data.Rows[(Int32)uiRow].SetField((Int32)uiCol, cstrUNASSIGNED_VALUE);

                        continue;
                    }

                    if (System.Int32.TryParse(((String)data.Rows[(Int32)uiRow][(Int32)uiCol]), out iRowColData))
                    {
                        if ((Int32)(uiDigit) == iRowColData && (uiRow != uiCurrRow) && (uiCol != uiCurrCol))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public bool NoConflicts(UInt32 uiCurrRow, UInt32 uiCurrCol, UInt32 uiDigit)
        {
            Int32 iRowColData;

            for (UInt32 uiRow = cuiFIRST_ROW; uiRow < uiSize; uiRow++)
            {
                if (System.Int32.TryParse(((String)data.Rows[(Int32)uiRow][(Int32)uiCurrCol]), out iRowColData))
                {
                    if ((Int32)uiDigit == iRowColData)
                    {
                        return false;
                    }
                }
            }

            for (UInt32 uiCol = cuiFIRST_COL; uiCol < uiSize; uiCol++)
            {
                if (System.Int32.TryParse(((String)data.Rows[(Int32)uiCurrRow][(Int32)uiCol]), out iRowColData))
                {
                    if ((Int32)uiDigit == iRowColData)
                    {
                        return false;
                    }
                }
            }

            UInt32 uiSegment_Size = uiSize / cuiSEGMENTS;

            UInt32 uiRow_Segment_Start = (uiCurrRow / uiSegment_Size) * uiSegment_Size;
            UInt32 uiRow_Segment_End = uiRow_Segment_Start + uiSegment_Size;
            UInt32 uiCol_Segment_Start = (uiCurrCol / uiSegment_Size) * uiSegment_Size;
            UInt32 uiCol_Segment_End = uiCol_Segment_Start + uiSegment_Size;

            for (UInt32 uiRow = uiRow_Segment_Start; uiRow < uiRow_Segment_End; uiRow++)
            {
                for (UInt32 uiCol = uiCol_Segment_Start; uiCol < uiCol_Segment_End; uiCol++)
                {
                    if (System.Int32.TryParse(((String)data.Rows[(Int32)uiRow][(Int32)uiCol]), out iRowColData))
                    {
                        if ((Int32)uiDigit == iRowColData)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private bool bRowColRndInitFind = false;
        private UInt32 uiRowRndFind = 0;
        private UInt32 uiColRndFind = 0;

        private bool FindUnassignedLocation(ref UInt32 uiCurrRow, ref UInt32 uiCurrCol)
        {
            if (bAbortSolver)
            {
                return false;
            }

            if (!bRowColRndInitFind)
            {
                Random rnd = new Random();

                uiRowRndFind = (UInt32)rnd.Next(0, (Int32)uiSize);
                uiColRndFind = (UInt32)rnd.Next(0, (Int32)uiSize);

                bRowColRndInitFind = true;
            }

            Int32 iRowColData;

            for (UInt32 uiRow = uiCurrRow; uiRow < uiSize; uiRow++)
            {
                for (UInt32 uiCol = uiCurrCol; uiCol < uiSize; uiCol++)
                {
                    if (!System.Int32.TryParse(((String)data.Rows[(Int32)((uiRow + uiRowRndFind) % uiSize)][(Int32)((uiCol + uiColRndFind) % uiSize)]), out iRowColData))
                    {
                        uiCurrRow = (uiRow + uiRowRndFind) % uiSize;
                        uiCurrCol = (uiCol + uiColRndFind) % uiSize;

                        return true;
                    }
                }
            }

            return false;
        }
    }
}