public interface ITimePerSecondSubject
{
    ITimePerSecondSubject Attach(ITimePerSecondObserver observer);
    ITimePerSecondSubject Detach(ITimePerSecondObserver observer);
    void Notify(long nowTicks);
}
