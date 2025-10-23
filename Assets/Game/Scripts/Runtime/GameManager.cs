using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Game.Scripts.Runtime.Cell;
using Game.Scripts.Runtime.Factory;
using Game.Scripts.Runtime.Rules;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.Scripts.Runtime
{
    public class GameManager : MonoBehaviour
    {
        #region Members

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
        [SerializeField] private Button _playButton;
        [SerializeField] private TextMeshProUGUI _playbuttonText;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Slider _intervalSlider;
        [SerializeField] private TextMeshProUGUI _generationText;
        [SerializeField] private TextMeshProUGUI _livingCellsText;

        private GridController gridController;
        private IGridFactory factory = new GridFactory();
        private List<ICellRenderer> renderers = new ();
        private bool isPlaying;
        private int currentGen;

        public GridController GridController => gridController;

        public int GridWidth => _gridWidth;

        public int GridHeight => _gridHeight;

        #endregion

        #region Unity Functions

        private void Start()
        {
            _playButton.onClick.AddListener(PlaySimulation);
            _nextButton.onClick.AddListener(Next);
            _intervalSlider.onValueChanged.AddListener(OnIntervalTextChanged);
            _intervalSlider.value = _interval;
            gridController = new GridController(GridWidth, GridHeight, factory);
            CreateViews();
            UpdateGeneration(0, true);
            UpdateAliveText(true);
            
            StartCoroutine(Simulate());
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
                    renderer.Initialize(grid[x,y], _aliveColor, _deadColor);
                    renderers.Add(renderer);
                }
            }
        }

        private void OnIntervalTextChanged(float value)
        {
            try
            {
                _interval = 1 - value;
            }
            catch (Exception e)
            {
                // ignore
                _interval = 0.1f;
            }
        }

        private void PlaySimulation()
        {
            isPlaying = !isPlaying;

            _playbuttonText.text = isPlaying ? StrattonConstants.BUTTON.STOP : StrattonConstants.BUTTON.PLAY;
        }

        private IEnumerator Simulate()
        {
            while (true)
            {
                if (!isPlaying)
                {
                    yield return null;
                    continue;
                }
                
                var count = 0f;
                while (count < _interval)
                {
                    if (!isPlaying)
                    {
                        yield return null;
                        continue;
                    }
                    
                    count += Time.fixedDeltaTime;

                    yield return null;
                }
                
                // yield return new WaitForSeconds(_interval);
                yield return null;
                
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
            
            UpdateGeneration();
            UpdateAliveText();
        }

        private void ResetToInitial()
        {
            UpdateGeneration();
            UpdateAliveText();
        }

        private void Clear()
        {
            
        }
        
        public void ToggleCell(int x, int y)
        {
            if (x < 0 || x >= _gridWidth || y < 0 || y >= _gridHeight) return;
            var cell = gridController.Grid[x, y];
            gridController.SetCellState(x, y, !cell.IsAlive);
            UpdateGeneration(0, true);
            UpdateAliveText();
        }
        
        public List<ICell> GetAliveCells()
        {
            return gridController.GetAliveCells();
        }

        private void UpdateGeneration(int value = 1, bool reset = false)
        {
            if (!reset)
            {
                currentGen += value;
                _generationText.text = $"Generation: {currentGen}";
            }
            else
            {
                currentGen = 0;
                _generationText.text = "Generation: 0";
            }
        }
        
        private void UpdateAliveText(bool reset = false)
        {
            _livingCellsText.text = reset ? "Living Cells: 0" : $"Living Cells: {GetAliveCells().Count}";
        }

        #endregion
    }
}