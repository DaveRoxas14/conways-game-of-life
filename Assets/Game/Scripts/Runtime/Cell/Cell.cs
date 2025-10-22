using System;

namespace Game.Scripts.Runtime.Cell
{
    public class Cell : ICell
    {
        #region Members

        public bool IsAlive { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        
        public event Action<ICell> OnStateChanged;

        #endregion

        #region Constructors

        public Cell(int x, int y, bool isAlive = false)
        {
            X = x;
            Y = y;
            IsAlive = isAlive;
        }

        #endregion

        #region Helpers

        public void SetAlive(bool alive)
        {
            IsAlive = alive;
            OnStateChanged?.Invoke(this);
        }

        #endregion

        
    }
}