using Autofac;
using TwitterImgSaverCmd.Configurations;

namespace TwitterImgSaverCmd;

/// <summary>
/// A test project that only tests the workflow of downloading images in original size from given Twitter web page
/// </summary>
public class Program
{
    private static void Main(string[] args)
    {
        var container = RegisterServices();
        var configs = container.Resolve<IConfiguration>();
        var runner = container.Resolve<IRunner>();

        try
        {
            configs.LoadConfigs();

            runner.Run().Wait();
        }
        finally
        {
            configs.SaveConfigs();
        }
    }
    
    private static IContainer RegisterServices()
    {
        var builder = new ContainerBuilder();

        builder.RegisterType<Runner>().As<IRunner>();
        builder.RegisterType<CommandParser>().As<ICommandParser>();
        builder.RegisterType<Configuration>().As<IConfiguration>();
        
        var container = builder.Build();
        return container;
    }
}