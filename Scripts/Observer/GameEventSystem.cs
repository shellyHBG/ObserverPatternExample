using System.Collections.Generic;

public class GameEventSystem
{
    private Dictionary<EGameEvent, object> _dictEventSubject;

    public void Initialize()
    {
        _dictEventSubject = new Dictionary<EGameEvent, object>();
    }

    public void Release()
    {
        _dictEventSubject.Clear();
    }

    public void ResisterObserver<T>(EGameEvent eventType, IGameEventObserver<T> observer) where T : IGameEventParam
    {
        GameEventSubject<T> subject = GetGameEventSubject<T>(eventType);
        if (subject != null)
        {
            subject.Attach(observer);
        }
    }

    public void NotifySubject<T>(T Param) where T : IGameEventParam
    {
        if (!_dictEventSubject.TryGetValue(Param.GetEventType(), out object subject))
            return;

        (subject as GameEventSubject<T>).Notify(Param);
    }

    private GameEventSubject<T> GetGameEventSubject<T>(EGameEvent eventType) where T : IGameEventParam
    {
        if (_dictEventSubject.TryGetValue(eventType, out object subject))
            return subject as GameEventSubject<T>;

        subject = new GameEventSubject<T>();
        _dictEventSubject.Add(eventType, subject);
        return subject as GameEventSubject<T>;
    }
}