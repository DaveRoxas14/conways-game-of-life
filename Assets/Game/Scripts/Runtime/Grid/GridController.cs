using System.Collections.Generic;

namespace Game.Scripts.Runtime.Factory
{
    public class GridController
    {
        public ICell[,] Grid { get; private set; }

        private int _width = 0;
        private int _height = 0;

        public GridController(int width, int height, IGridFactory factory)
        {
            _width = width;
            _height = height;
            Grid = factory.Create(width, height);
        }

        public ICell[] GetNeighbors(int x, int y)
        {
            var cellList = new List<ICell>(8);
            for (var dx = -1; dx <= 1; dx++)
            {
                for (var dy = -1; dy <= 1; dy++)
                {
                    if(dx == 0 && dy == 0) continue;

                    var nx = x + dx;
                    var ny = y + dy;
                    
                    if(nx >= 0 && nx < _width && ny >= 0 && ny < _height)
                        cellList.Add(Grid[nx,ny]);
                }
            }
            return cellList.ToArray();
        }

        public void SetCellState(int x, int y, bool alive)
        {
            Grid[x,y].SetAlive(alive);
        }

        public List<ICell> GetAliveCells()
        {
            var aliveList = new List<ICell>();
            for (var x = 0; x < _width; x++)
            {
                for (var y = 0; y < _height; y++)
                {
                    if (Grid[x, y].IsAlive)
                    {
                        aliveList.Add(Grid[x,y]);
                    }
                }
            }

            return aliveList;
        }
    }
}