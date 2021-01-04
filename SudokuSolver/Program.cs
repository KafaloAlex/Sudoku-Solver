using SudokuSolver.Strategies;
using SudokuSolver.Workers;
using System;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var sudokuMapper = new SudokuMapper();
                var sbStateManager = new StateManager();
                var sbSolverEngine = new SolverEngine(sbStateManager, sudokuMapper);
                var sbFileReader = new SudokuFileReader();
                var sbDisplayer = new SudokuBoardDisplayer();

                Console.Write("Entrez le nom du fichier .txt contenant le puzzle Sudoku : ");
                var filename = Console.ReadLine();

                var sudokuBoard = sbFileReader.ReadFile(filename);
                sbDisplayer.Display("Puzzle Initial", sudokuBoard);

                bool isSolved = sbSolverEngine.Solve(sudokuBoard);
                sbDisplayer.Display("Puzzle résolu", sudokuBoard);

                Console.WriteLine(isSolved ? "Great !!! Resolve Successful" : "Un nain le mouvement est mal hard");
            }
            catch (Exception e)
            {

                Console.WriteLine("{0} : {1}", "Erreur Bro : ", e.Message);
            }
        }
    }
}
