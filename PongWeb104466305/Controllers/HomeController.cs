using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PongWeb104466305.Models;

namespace PongWeb104466305.Controllers;

public class HomeController : Controller
{
    private readonly IOptionsMonitor<PongOptions> _options;
    private readonly ILogger<HomeController> _logger;

    public HomeController(
        IOptionsMonitor<PongOptions> options,
        ILogger<HomeController> logger)
    {
        _options = options;
        _logger = logger;
    }

    public IActionResult Index()
    {
        var o = _options.CurrentValue;

        _logger.LogInformation(
            "Rendering simulation. Label={Label} LaneWidth={Width} Frames={Frames} ApiKeyConfigured={HasKey}",
            o.DeploymentLabel, o.LaneWidth, o.Frames,
            !string.IsNullOrWhiteSpace(o.ApiKey));

        var model = new PongViewModel
        {
            Options = o,
            Frames = new PongSimulator(o).Run(),
            MachineName = Environment.MachineName,
            ApiKeyMasked = Mask(o.ApiKey)
        };

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }

    private static string Mask(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return "(not set)";
        var prefix = value[..Math.Min(3, value.Length)];
        return prefix + new string('*', 8);
    }
}