using UnityEngine;

namespace Game.Scripts.Runtime.Cell
{
    public interface ICellRenderer
    {
        public Transform CellTransform {
            get;
        }
        void Initialize(ICell cell, Color alive, Color dead);
        void SetPosition(float x, float y);
    }
}