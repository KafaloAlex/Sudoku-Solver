using SudokuSolver.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSolver.Strategies
{
    class SolverEngine
    {
        private readonly SudokuMapper _sudokuMapper;
        private readonly StateManager _sbStateManager;
        public SolverEngine(StateManager sbStateManager, SudokuMapper sudokuMapper)
        {
            _sbStateManager = sbStateManager;
            _sudokuMapper = sudokuMapper;
        }

        public bool Solve(int[,] sudokuBoard)
        {
            List<ISudokuStrategy> strategies = new List<ISudokuStrategy>()
            {
                new MarkUpStrategy(_sudokuMapper),
                new NakedPairStrategy(_sudokuMapper)
            };

            var currentState = _sbStateManager.GenerateState(sudokuBoard);
            var nextState = _sbStateManager.GenerateState(strategies.First().Solve(sudokuBoard));

            while (!_sbStateManager.IsSolved(sudokuBoard) && currentState != nextState)
            {
                currentState = nextState;
                foreach (var strategy in strategies)
                    nextState = _sbStateManager.GenerateState(strategy.Solve(sudokuBoard));
            }
                
            return _sbStateManager.IsSolved(sudokuBoard);
        }
    }
}
