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
        
        
    }
}