using System;

public interface ICell
{ 
    bool IsAlive {
        get;
    }

    int X { get; }
    int Y { get; }

    void SetAlive(bool isAlive);

    event Action<ICell> OnStateChanged;

}
