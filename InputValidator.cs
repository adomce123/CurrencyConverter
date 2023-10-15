using CurrencyConverter.Configuration;
using CurrencyConverter.Interfaces;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace CurrencyConverter
{
    public class InputValidator : IInputValidator
    {
        private readonly ConverterConfiguration _configuration;

        private const int CurrencyPairLength = 7;
        private const int InputWordsCount = 3;
        private const char Separator = '/';

        public InputValidator(IOptions<ConverterConfiguration> options)
        {
            _configuration = options.Value;
        }

        public bool IsInputValid(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            string[] words = input.Split(' ');

            if (words.Length != InputWordsCount)
                return false;

            if (words[1].Length != CurrencyPairLength || !words[1].Contains("/"))
                return false;

            string[] currencies = words[1].Split(Separator);

            if (!_configuration.ExchangeValuesDict.ContainsKey(currencies[0])
                || !_configuration.ExchangeValuesDict.ContainsKey(currencies[1]))
            {
                Console.WriteLine("Currency pair contains unknown currency");
                return false;
            }

            if (!float.TryParse(words[2], CultureInfo.InvariantCulture, out float amount))
                return false;

            return true;
        }
    }
}