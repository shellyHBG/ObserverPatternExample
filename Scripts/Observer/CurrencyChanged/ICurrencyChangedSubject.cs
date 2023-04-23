public interface ICurrencyChangedSubject
{
    ICurrencyChangedSubject Attach(ICurrencyChangedObserver observer);
    ICurrencyChangedSubject Detach(ICurrencyChangedObserver observer);
    void Notify(CurrencyCategory currency, decimal changed);
}
