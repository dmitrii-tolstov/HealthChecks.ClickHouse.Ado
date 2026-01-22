# HealthChecks.ClickHouse.Ado

Health check implementation for Clickhouse based on Clickhouse.Ado package.

## Usage

Startup.cs
```
public void ConfigureServices(IServiceCollection services) 
{
    // ...
    services.AddHealthChecks()
        .AddClickHouseAdo(
            connectionString : connectionString,
            name: "ClickHouse Health Check",
            failureStatus: HealthStatus.Unhealthy,
            tags: ["ClickHouse"]
        );
    // ...
}
```


## Built With

* [ClickHouse.Ado](https://github.com/killwort/ClickHouse-Net)
* [Microsoft.Extensions.Diagnostics.HealthChecks](https://www.nuget.org/packages/Microsoft.Extensions.Diagnostics.HealthChecks)

