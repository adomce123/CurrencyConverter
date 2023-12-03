using CurrencyConverter.CurrencyConverter.Configuration;
using Microsoft.Extensions.Options;

namespace CurrencyConverter.CurrencyConverter.Tests
{
    public class ConverterShould
    {
        private readonly Converter _converter;

        public ConverterShould()
        {
            var converterConfig = new ConverterConfiguration();
            converterConfig.BaseCurrency = "DKK";
            converterConfig.DividedBy = 100;
            var dict = new Dictionary<string, float>
            {
                { "EUR", 743.94f },
                { "USD", 663.11f }
            };
            converterConfig.ExchangeValuesDict = dict;

            IOptions<ConverterConfiguration> options = Options
                .Create(converterConfig);

            _converter = new Converter(options);
        }

        [Fact]
        public void CalculateExchangeAmount()
        {
            //ARRANGE
            var mainCurrency = "EUR";
            var moneyCurrency = "DKK";
            var amount = 1;

            //ACT
            var result = _converter.ConvertCurrency(mainCurrency, moneyCurrency, amount);

            //ASSERT
            Assert.Equal(7.4394, result, 4);
        }
    }
}