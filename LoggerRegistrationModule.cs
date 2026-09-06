using Autofac;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace TwitterImgSaverCmd;

public class LoggerRegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // Register the LoggerFactory as a single instance
        builder.Register(c => LoggerFactory.Create(config => config.AddConsole()))
            .As<ILoggerFactory>()
            .SingleInstance();
        builder.Register(c => LoggerFactory.Create(config =>
        {
            config.AddConsole(opts => opts.FormatterName = "color");
            config.AddConsoleFormatter<ColorConsoleFormatter, ConsoleFormatterOptions>();
        })).As<ILoggerFactory>().SingleInstance();

        builder.RegisterGeneric(typeof(Logger<>))
            .As(typeof(ILogger<>))
            .SingleInstance();
    }
    
}