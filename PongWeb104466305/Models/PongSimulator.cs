namespace PongWeb104466305.Models;

public class PongFrame
{
    public string Lane { get; set; } = "";
    public int Position { get; set; }
    public int Bounces { get; set; }
}

public class PongSimulator
{
    private readonly PongOptions _options;

    public PongSimulator(PongOptions options) => _options = options;

    public IReadOnlyList<PongFrame> Run()
    {
        int width = Math.Clamp(_options.LaneWidth, 5, 200);
        int frameCount = Math.Clamp(_options.Frames, 1, 200);

        char ball = _options.BallChar.Length > 0 ? _options.BallChar[0] : 'o';
        char wall = _options.WallChar.Length > 0 ? _options.WallChar[0] : '|';

        var frames = new List<PongFrame>(frameCount);
        int position = 1;
        int direction = 1;
        int bounces = 0;

        for (int f = 0; f < frameCount; f++)
        {
            var lane = new char[width];
            for (int i = 0; i < width; i++) lane[i] = ' ';
            lane[0] = wall;
            lane[width - 1] = wall;
            lane[position] = ball;

            frames.Add(new PongFrame
            {
                Lane = new string(lane),
                Position = position,
                Bounces = bounces
            });

            int next = position + direction;
            if (next <= 0 || next >= width - 1)
            {
                direction = -direction;
                bounces++;
                next = position + direction;
            }
            position = next;
        }

        return frames;
    }
}