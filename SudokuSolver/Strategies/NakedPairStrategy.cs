using SudokuSolver.Workers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver.Strategies
{
    // Cette stratégie consiste à supprimer des numéro pairs ou qui se repète sur une même ligne et une même colonne
    class NakedPairStrategy : ISudokuStrategy
    {
        private readonly SudokuMapper _sudokuMapper;

        public NakedPairStrategy(SudokuMapper sudokuMapper)
        {
            _sudokuMapper = sudokuMapper;
        }
        public int[,] Solve(int[,] sudokuBoard)
        {
            for (int row = 0; row < sudokuBoard.GetLength(0); row++)
            {
                for (int col = 0; col < sudokuBoard.GetLength(1); col++)
                {
                    EliminateNakedPairInRow(sudokuBoard, row, col);
                    EliminateNakedPairInCol(sudokuBoard, row, col);
                    EliminateNakedPairInBlock(sudokuBoard, row, col);
                }
            }

            return sudokuBoard;
        }

        private void EliminateNakedPairInRow(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            if (!HasNakedPairInRow(sudokuBoard, givenRow, givenCol))
                return;

            for (int col = 0; col < sudokuBoard.GetLength(1); col++)
            {
                if (sudokuBoard[givenRow, col] != sudokuBoard[givenRow, givenCol] && sudokuBoard[givenRow, col].ToString().Length > 1)
                {
                    EliminateNakedPair(sudokuBoard, sudokuBoard[givenRow, givenCol], givenRow, col);
                }
            }
        }

       
        private bool HasNakedPairInRow(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            for (int col = 0; col < sudokuBoard.GetLength(1); col++)
            {
                if (givenCol != col && IsNakedPair(sudokuBoard[givenRow, col], sudokuBoard[givenRow, givenCol]))
                    return true;
            }

            return false;
        }


        private void EliminateNakedPairInCol(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            if (!HasNakedPairInCol(sudokuBoard, givenRow, givenCol))
                return;

            for (int row = 0; row < sudokuBoard.GetLength(0); row++)
            {
                if (sudokuBoard[row, givenCol] != sudokuBoard[givenRow, givenCol] && sudokuBoard[row, givenCol].ToString().Length > 1)
                {
                    EliminateNakedPair(sudokuBoard, sudokuBoard[givenRow, givenCol], row, givenCol);
                }
            }
        }

        private bool HasNakedPairInCol(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            for (int row = 0; row < sudokuBoard.GetLength(0); row++)
            {
                if (givenRow != row && IsNakedPair(sudokuBoard[row, givenCol], sudokuBoard[givenRow, givenCol]))
                    return true;
            }

            return false;
        }



        private void EliminateNakedPairInBlock(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            if (!HasNakedPairInBlock(sudokuBoard, givenRow, givenCol))
                return;

            var sudokuMap = _sudokuMapper.Find(givenRow, givenCol);

            for (int row = sudokuMap.StartRow; row <= sudokuMap.StartRow + 2; row++)
            {
                for (int col = sudokuMap.StartCol; col <= sudokuMap.StartCol + 2; col++)
                {
                    if (sudokuBoard[row, col].ToString().Length > 1 && sudokuBoard[row, col] != sudokuBoard[givenRow, givenCol])
                    {
                        EliminateNakedPair(sudokuBoard, sudokuBoard[givenRow, givenCol], row, col);
                    }
                }
            }
        }

        
        private bool HasNakedPairInBlock(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            for (int row = 0; row < sudokuBoard.GetLength(0); row++)
            {
                for (int col = 0; col < sudokuBoard.GetLength(1); col++)
                {
                    var elementSame = givenRow == row && givenCol == col;
                    var elementSameInBlock = _sudokuMapper.Find(givenRow, givenCol).StartRow == _sudokuMapper.Find(row, col).StartRow &&
                        _sudokuMapper.Find(givenRow, givenCol).StartCol == _sudokuMapper.Find(row, col).StartCol;

                    if (!elementSame && elementSameInBlock && IsNakedPair(sudokuBoard[givenRow, givenCol], sudokuBoard[row, col]))
                        return true;
                }
            }
            return false;
        }


        private void EliminateNakedPair(int[,] sudokuBoard, int valuesToEliminate, int eliminateFromRow, int eliminateFromCol)
        {
            var valuesDeleteArray = valuesToEliminate.ToString().ToCharArray();
            foreach (var value in valuesDeleteArray)
            {
                sudokuBoard[eliminateFromRow, eliminateFromCol] = Convert.ToInt32(sudokuBoard[eliminateFromRow, eliminateFromCol].ToString().Replace(value.ToString(), string.Empty));
            }
        }

        private bool IsNakedPair(int firstPair, int secondPair)
        {
            return firstPair.ToString().Length == 2 && firstPair == secondPair;
        }
    }
}
