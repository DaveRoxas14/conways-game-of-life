using System;
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

        #endregion

        #region Unity Functions

        private void Start()
        {
            
        }

        #endregion

        private void Next()
        {
            
        }
    }
}