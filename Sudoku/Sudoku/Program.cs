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
using System.Threading;
using System.Windows.Forms;

namespace Sudoku
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Sudoku oSudoku = new Sudoku();
            Form_Sudoku frmSudoku = new Form_Sudoku(oSudoku);
            frmSudoku.ShowDialog();
        }
    }
}