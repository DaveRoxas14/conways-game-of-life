using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Runtime.Cell;
using Game.Scripts.Runtime.Factory;
using Game.Scripts.Runtime.Rules;
using UnityEngine;

namespace Game.Scripts.Runtime
{
    public class GameManager : MonoBehaviour
    {
        #region MyRegion

        [Header(StrattonConstants.INSPECTOR.GRID)] 
        [SerializeField] private int _gridWidth = 30;
        [SerializeField] private int _gridHeight = 10;
        [SerializeField] private float _interval = 0.1f;
        [SerializeField] private Color _aliveColor = Color.green;
        [SerializeField] private Color _deadColor = Color.red;

        [Header(StrattonConstants.INSPECTOR.REFERENCES)] 
        [SerializeField] private GameObject _cellPrefab;
        [SerializeField] private Transform _cellParent;
        [SerializeField] private RuleSO _rule;

        private GridController gridController;
        private IGridFactory factory = new GridFactory();
        private List<ICellRenderer> renderers = new ();

        public GridController GridController => gridController;

        public int GridWidth => _gridWidth;

        public int GridHeight => _gridHeight;

        #endregion

        #region Unity Functions

        private void Start()
        {
            gridController = new GridController(GridWidth, GridHeight, factory);
            CreateViews();
        }

        #endregion

        #region Helpers

        private void CreateViews()
        {
            var grid = gridController.Grid;
            for (var x = 0; x < GridWidth; x++)
            {
                for (var y = 0; y < GridHeight; y++)
                {
                    var cell = Instantiate(_cellPrefab, _cellParent);
                    var renderer = cell.GetComponent<ICellRenderer>();
                    renderer.SetPosition(x - GridWidth / 2f, y - GridHeight / 2f);
                    renderer.Initialize(grid[x,y], _deadColor, _aliveColor);
                    renderers.Add(renderer);
                }
            }
        }

        private IEnumerator Simulate()
        {
            while (true)
            {
                yield return new WaitForSeconds(_interval);
                Next();
            }
        }

        private void Next()
        {
            var nextStates = new bool[GridWidth, GridHeight];
            for (var x = 0; x < GridWidth; x++)
            {
                for (var y = 0; y < GridHeight; y++)
                {
                    var cell = gridController.Grid[x, y];
                    var neighbors = gridController.GetNeighbors(x, y);
                    nextStates[x, y] = _rule.ComputeNext(cell, neighbors);
                }
            }

            for (var x = 0; x < GridWidth; x++)
            {
                for (var y = 0; y < GridHeight; y++)
                {
                    gridController.SetCellState(x, y, nextStates[x, y]);
                }
            }
        }
        
        public void ToggleCell(int x, int y)
        {
            if (x < 0 || x >= _gridWidth || y < 0 || y >= _gridHeight) return;
            var cell = gridController.Grid[x, y];
            gridController.SetCellState(x, y, !cell.IsAlive);
        }

        #endregion
    }
}