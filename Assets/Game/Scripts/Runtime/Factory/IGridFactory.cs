namespace Game.Scripts.Runtime.Factory
{
    public interface IGridFactory
    {
        ICell[,] Create(int width, int height);
    }
}