using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthChecks.Clickhouse.Ado.DependencyInjection
{
    public static class ClickhouseAdoHealthCheckBuilderExtensions
    {
        public static IHealthChecksBuilder AddClickHouseAdo(this IHealthChecksBuilder builder,
            string connectionString,
            HealthStatus failureStatus = HealthStatus.Unhealthy,
            string name = default,
            IEnumerable<string> tags = default)
        {   
            var healthCheckName = name ?? "clickhouse";

            return builder.Add(new HealthCheckRegistration(
                healthCheckName,
                sp => new ClickHouseAdoHealthCheck(connectionString),
                failureStatus,
                tags));
        }
    }
}