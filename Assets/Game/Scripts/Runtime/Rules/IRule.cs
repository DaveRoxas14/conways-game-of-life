namespace Game.Scripts.Runtime.Rules
{
    public interface IRule
    {
        bool ComputeNext(ICell cell, ICell[] neighbors);
    }
}