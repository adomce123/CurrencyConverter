namespace CurrencyConverter.CurrencyConverter.Configuration
{
    public class ConverterConfiguration
    {
        public string BaseCurrency { get; set; }
        public float DividedBy { get; set; }
        public IDictionary<string, float> ExchangeValuesDict { get; set; }
    }
}
