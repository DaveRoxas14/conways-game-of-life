namespace Game.Scripts.Runtime.Factory
{
    public class GridFactory : IGridFactory
    {
        public ICell[,] Create(int width, int height)
        {
            var grid = new ICell[height, width];
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    grid[x, y] = new Cell.Cell(x, y, false);
                }
            }

            return grid;
        }
    }
}