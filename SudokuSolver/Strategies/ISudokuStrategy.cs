using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver.Strategies
{
    interface ISudokuStrategy
    {
        int[,] Solve(int[,] sudokuBoard);
    }
}
