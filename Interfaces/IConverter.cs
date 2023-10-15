namespace CurrencyConverter.Interfaces
{
    public interface IConverter
    {
        float ConvertCurrency(string mainCurrency, string moneyCurrency, float amount);
    }
}
