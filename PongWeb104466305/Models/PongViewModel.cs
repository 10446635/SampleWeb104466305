namespace PongWeb104466305.Models;

public class PongViewModel
{
    public PongOptions Options { get; set; } = new();
    public IReadOnlyList<PongFrame> Frames { get; set; } = Array.Empty<PongFrame>();
    public string MachineName { get; set; } = "";
    public string ApiKeyMasked { get; set; } = "";
    public int TotalBounces => Frames.Count == 0 ? 0 : Frames[^1].Bounces;
}