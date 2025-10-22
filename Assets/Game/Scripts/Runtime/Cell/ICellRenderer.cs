using UnityEngine;

namespace Game.Scripts.Runtime.Cell
{
    public interface ICellRenderer
    {
        void Initialize(ICell cell, Color alive, Color dead);
        void SetPosition(float x, float y);
    }
}