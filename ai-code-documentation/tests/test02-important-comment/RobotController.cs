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
        // IMPORTANT: The robot controller rejects motion commands when Z is below the machine safety height.
        // This limit is enforced here because the controller-side configuration is machine-dependent.
        if (z < 50.0)
        {
            throw new InvalidOperationException("Z position is below the machine safety height.");
        }

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
