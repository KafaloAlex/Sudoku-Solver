using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SudokuSolver.Workers
{
    //Cette classe permettra de lire les données du fichier txt
    class SudokuFileReader
    {
        public int[,] ReadFile(string fileName)
        {
            int[,] sudokuBoard = new int[9, 9];

            try
            {
                var sudokuBoardLines = File.ReadAllLines(fileName); //Lis toutes les lignes du fichiers

                //Parcours le tableau pour remplacer les espaces vides par des 0
                int row = 0;
                foreach (var item in sudokuBoardLines)
                {
                    string[] sbLineElements = item.Split('|').Skip(1).Take(9).ToArray();

                    int col = 0;
                    foreach (var element in sbLineElements)
                    {
                        sudokuBoard[row, col] = element.Equals(" ") ? 0 : Convert.ToInt32(element);
                        col++;
                    }
                    row++;
                }
            }
            catch (Exception e)
            {

                throw new Exception("Erreur de lecture du fichier : " + e.Message);
            }
            return sudokuBoard;
        }
    }
}
