public interface IGameEventObserver<T> where T : IGameEventParam
{
    void Update(T gameEventParam);
}