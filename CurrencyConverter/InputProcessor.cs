using CurrencyConverter.CurrencyConverter.Interfaces;
using System.Globalization;

namespace CurrencyConverter.CurrencyConverter
{
    public class InputProcessor : IInputProcessor
    {
        private readonly IConverter _converter;
        private readonly IInputValidator _validator;

        public InputProcessor(IConverter converter, IInputValidator validator)
        {
            _converter = converter;
            _validator = validator;
        }

        public void ReadInput()
        {
            string tipMessage = "Usage: Exchange <currency pair> <amount to exchange>";
            Console.WriteLine(tipMessage);

            while (true)
            {
                var userInput = Console.ReadLine();

                bool validInput = _validator.IsInputValid(userInput);
                if (validInput)
                {
                    var exchangedAmount = ProcessInput(userInput);
                    Console.WriteLine($"Exchanged amount: {exchangedAmount}");
                }
                else
                    Console.WriteLine(tipMessage);
            }
        }

        private float ProcessInput(string userInput)
        {
            string[] words = userInput.Split(' ');
            string[] currencies = words[1].Split('/');
            float.TryParse(words[2], CultureInfo.InvariantCulture, out float amount);

            string mainCurrency = currencies[0];
            string moneyCurrency = currencies[1];

            return _converter.ConvertCurrency(mainCurrency, moneyCurrency, amount);
        }
    }
}
