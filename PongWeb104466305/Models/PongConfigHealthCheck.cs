using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace PongWeb104466305.Models;

public class PongConfigHealthCheck : IHealthCheck
{
    private readonly IOptionsMonitor<PongOptions> _options;
    private readonly ILogger<PongConfigHealthCheck> _logger;

    public PongConfigHealthCheck(
        IOptionsMonitor<PongOptions> options,
        ILogger<PongConfigHealthCheck> logger)
    {
        _options = options;
        _logger = logger;
    }

    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var o = _options.CurrentValue;
        var problems = new List<string>();

        if (string.IsNullOrWhiteSpace(o.ApiKey))
            problems.Add("Pong:ApiKey is not configured");

        if (o.LaneWidth < 5 || o.LaneWidth > 200)
            problems.Add($"Pong:LaneWidth ({o.LaneWidth}) is outside the allowed range 5-200");

        if (string.IsNullOrEmpty(o.BallChar) || string.IsNullOrEmpty(o.WallChar))
            problems.Add("Pong:BallChar or Pong:WallChar is empty");

        if (problems.Count > 0)
        {
            var detail = string.Join("; ", problems);
            _logger.LogWarning("Health check FAILED: {Detail}", detail);
            return Task.FromResult(HealthCheckResult.Unhealthy(detail));
        }

        _logger.LogInformation(
            "Health check passed. DeploymentLabel={Label}", o.DeploymentLabel);
        return Task.FromResult(
            HealthCheckResult.Healthy("Configuration loaded and valid"));
    }
}