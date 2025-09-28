using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using CoreRepo.Benchmark;
using Microsoft.Extensions.Configuration;

static IConfiguration BuildConfiguration() =>
    new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
        .Build();

static IConfig BuildBenchmarkConfig() =>
    DefaultConfig.Instance.WithOptions(ConfigOptions.DisableOptimizationsValidator);

var configuration = BuildConfiguration();
var connectionString = configuration.GetConnectionString("CoreDB");

Console.WriteLine($"Connection: {connectionString}");

var summary = BenchmarkRunner.Run<ControllerBenchmark>(BuildBenchmarkConfig());
