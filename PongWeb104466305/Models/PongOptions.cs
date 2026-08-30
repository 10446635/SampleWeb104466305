namespace PongWeb104466305.Models;

public class PongOptions
{
    public const string SectionName = "Pong";

    public string BallChar { get; set; } = "o";
    public string WallChar { get; set; } = "|";
    public int LaneWidth { get; set; } = 40;
    public int Frames { get; set; } = 20;
    public string DeploymentLabel { get; set; } = "unset";
    public string ApiKey { get; set; } = "";
}