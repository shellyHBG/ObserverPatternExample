using System.Collections.Generic;

public class GameEventSubject<T> where T : IGameEventParam
{
    private List<IGameEventObserver<T>> _listObserver = null;

    public GameEventSubject()
    {
        _listObserver = new List<IGameEventObserver<T>>();
    }

    public void Attach(IGameEventObserver<T> observer)
    {
        _listObserver.Add(observer);
    }

    public void Dettack(IGameEventObserver<T> observer)
    {
        _listObserver.Remove(observer);
    }

    public void Notify(T param)
    {
        foreach (var observer in _listObserver)
        {
            observer.Update(param);
        }
    }
}
