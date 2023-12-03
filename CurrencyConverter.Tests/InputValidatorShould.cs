using CurrencyConverter.CurrencyConverter.Configuration;
using Microsoft.Extensions.Options;

namespace CurrencyConverter.CurrencyConverter.Tests
{
    public class InputValidatorShould
    {
        private readonly InputValidator _validator;

        public InputValidatorShould()
        {
            var converterConfig = new ConverterConfiguration();
            var dict = new Dictionary<string, float>
            {
                { "EUR", 0 },
                { "USD", 0 }
            };
            converterConfig.ExchangeValuesDict = dict;

            IOptions<ConverterConfiguration> options = Options
                .Create(converterConfig);

            _validator = new InputValidator(options);
        }

        [Fact]
        public void ReturnFalseIfInputInvalid()
        {
            //ARRANGE
            string input = "Exchange EUR/AAA 1";

            //ACT
            var isValid = _validator.IsInputValid(input);

            //ASSERT
            Assert.False(isValid);
        }

        [Fact]
        public void ReturnTrueIfInputValid()
        {
            //ARRANGE
            string input = "Exchange EUR/USD 1";

            //ACT
            var isValid = _validator.IsInputValid(input);

            //ASSERT
            Assert.True(isValid);
        }
    }
}
