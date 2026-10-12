namespace TestProject.RobotControl;

public class RobotController
{
    private readonly RobotSocket _socket;

    public RobotController(RobotSocket socket)
    {
        _socket = socket;
    }

    public void MoveTo(double xMeters, double yMeters, double zMeters)
    {
        var xMillimeters = xMeters * 1000.0;
        var yMillimeters = yMeters * 1000.0;
        var zMillimeters = zMeters * 1000.0;

        var command =
            $"MOVE {xMillimeters:F3} {yMillimeters:F3} {zMillimeters:F3}";

        _socket.Send(command);
    }
}

public class RobotSocket
{
    public void Send(string command)
    {
        // Test implementation.
    }
}
