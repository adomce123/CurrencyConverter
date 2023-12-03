using CurrencyConverter.Configuration;
using CurrencyConverter.Interfaces;
using Microsoft.Extensions.Options;

namespace CurrencyConverter
{
    public class Converter : IConverter
    {
        private readonly ConverterConfiguration _configuration;
        private readonly IDictionary<string, float> _exchangeValuesDict;

        public Converter(IOptions<ConverterConfiguration> options)
        {
            _configuration = options.Value;
            _exchangeValuesDict = _configuration.ExchangeValuesDict;
            _exchangeValuesDict.Add(_configuration.BaseCurrency, 0);
        }

        public float ConvertCurrency(string mainCurrency, string moneyCurrency, float amount)
        {
            float costToBuy1MainCurr = _exchangeValuesDict[mainCurrency] / _configuration.DividedBy;
            float costToBuy1MoneyCurr = _exchangeValuesDict[moneyCurrency] / _configuration.DividedBy;

            float exchangedAmount;
            if (mainCurrency == moneyCurrency)
            {
                exchangedAmount = amount;
            }
            else if (moneyCurrency == _configuration.BaseCurrency) // EUR/DKK 1
            {
                exchangedAmount = costToBuy1MainCurr * amount;
            }
            else if (mainCurrency == _configuration.BaseCurrency) // DKK/EUR 1 
            {
                exchangedAmount = amount / costToBuy1MoneyCurr;
            }
            else
            {
                exchangedAmount = (costToBuy1MoneyCurr / costToBuy1MainCurr) * amount;
            }

            return exchangedAmount;
        }
    }
}
