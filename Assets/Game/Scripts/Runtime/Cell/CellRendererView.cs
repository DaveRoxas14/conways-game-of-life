using System;
using UnityEngine;

namespace Game.Scripts.Runtime.Cell
{
    public class CellRendererView : MonoBehaviour, ICellRenderer
    {
        #region Members

        [Header(StrattonConstants.INSPECTOR.REFERENCES)]
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        
        private ICell localCell;
        private Color aliveColor;
        private Color deadColor;

        public Transform CellTransform => transform;

        #endregion

        #region Unity Methods

        private void OnDestroy()
        {
            localCell.OnStateChanged += OnCellStateChanged;
        }

        #endregion

        #region Helpers

        public void Initialize(ICell cell, Color alive, Color dead)
        {
            localCell = cell;
            localCell.OnStateChanged += OnCellStateChanged;
            aliveColor = alive;
            deadColor = dead;
            
            // initial setup
            OnCellStateChanged(cell);
        }
        
        private void OnCellStateChanged(ICell obj)
        {
            spriteRenderer.color = obj.IsAlive ? aliveColor : deadColor;
            
        }

        public void SetPosition(float x, float y)
        {
            transform.localPosition = new Vector3(x, y, 0);
        }

        #endregion
    }
}