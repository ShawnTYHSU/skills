namespace TestProject.RobotControl;

public class RobotController
{
    private readonly RobotSocket _socket;

    public RobotController(RobotSocket socket)
    {
        _socket = socket;
    }

    public void MoveTo(double x, double y, double z)
    {
        var command = $"MOVE {x:F3} {y:F3} {z:F3}";
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
