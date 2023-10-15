using CurrencyConverter;
using CurrencyConverter.Configuration;
using CurrencyConverter.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

IServiceCollection services = new ServiceCollection();

services.Configure<ConverterConfiguration>(configuration.GetSection("ConverterSettings"));

services.AddTransient<IInputProcessor, InputProcessor>();
services.AddTransient<IInputValidator, InputValidator>();
services.AddTransient<IConverter, Converter>();

IServiceProvider serviceProvider = services.BuildServiceProvider();

var inputProcessor = serviceProvider.GetService<IInputProcessor>();

inputProcessor.ReadInput();