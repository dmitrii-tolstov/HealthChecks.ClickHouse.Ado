using System;
using System.Threading;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading.Tasks;
using ClickHouse.Ado;

namespace HealthChecks.Clickhouse.Ado
{
    public class ClickHouseAdoHealthCheck(string connectionString) : IHealthCheck
    {
        private readonly string _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken)
        {
            try
            {
                bool result = false;

                var settings = new ClickHouseConnectionSettings(_connectionString);
                using (var cnn = new ClickHouseConnection(settings))
                {
                    cnn.Open();

                    var selectCommand = "SELECT 1";

                    using var cmd = cnn.CreateCommand(selectCommand);
                    var resultInt = (Byte)cmd.ExecuteScalar();

                    result = resultInt == 1;
                }

                return result
                    ? Task.FromResult(HealthCheckResult.Healthy())
                    : Task.FromResult(HealthCheckResult.Unhealthy());
            }
            catch (Exception ex)
            {
                var checkResult = new HealthCheckResult(
                    context.Registration.FailureStatus,
                    description: "exception while clickhouse health check",
                    exception: ex,
                    data: null);
                return Task.FromResult(checkResult);
            }
        }
    }
}
