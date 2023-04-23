public interface ICurrencyChangedObserver
{
    void Update(CurrencyCategory currency, decimal changed);
}
