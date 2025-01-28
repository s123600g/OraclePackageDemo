namespace OracleDbStartProgram;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Extensions.Logging;
using OracleDbStartProgram.Extensions;

public class InitConfig
{
    private static IServiceProvider _ServiceProvider;

    private static IConfiguration _config;

    private static Logger _logger;

    public static (IServiceProvider serviceProvider, Logger _logger) Startup()
    {
        Configure();
        ConfigureServices();

        return (_ServiceProvider, _logger);
    }

    private static void Configure()
    {
        var _defaultConfigRootFilePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "Config"
        );

        var config = new ConfigurationBuilder()
                .SetBasePath(_defaultConfigRootFilePath)
            ;

        string[] getJsonFile = Directory.GetFiles(_defaultConfigRootFilePath);

        foreach (string item in getJsonFile)
        {
            config.AddJsonFile(
                item,
                optional: true,
                reloadOnChange: false
            );
        }

        _config = config.Build();

        // NLog configuration with appsettings.json
        // https://github.com/NLog/NLog.Extensions.Logging/wiki/NLog-configuration-with-appsettings.json
        // 從組態設定檔載入NLog設定
        LogManager.Configuration = new NLogLoggingConfiguration(_config.GetSection("NLog"));
        _logger = LogManager.GetCurrentClassLogger();
    }

    private static void ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddConfig(_config);
        services.AddRepository();

        _ServiceProvider = services.BuildServiceProvider();
    }
}