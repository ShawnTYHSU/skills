namespace TestProject.RobotControl;

public class RobotController
{
    private readonly RobotSocket _socket;

    // 建構子，初始化 socket
    public RobotController(RobotSocket socket)
    {
        // 設定 socket
        _socket = socket;
    }

    // ⚠️ 2026-09-18 新增：控制器把 MOVE 的目標解讀成 TCP 位置，不是法蘭面，尚未實機驗證
    // 原本是直接送公尺，後來發現控制器要毫米，已修正（見上一輪討論）
    public void MoveTo(double xMeters, double yMeters, double zMeters)
    {
        // IMPORTANT: Z 低於 50 mm 會撞到治具。控制器端的限制依機台設定而異，所以在這裡強制檢查。
        // ✅ 2026-09-20 已在 2 號機確認治具高度為 48 mm，取 50 mm 保留餘量
        var zMillimeters = zMeters * 1000.0;
        if (zMillimeters < 50.0)
        {
            throw new InvalidOperationException("Z position is below the fixture safety height.");
        }

        // 轉換成毫米
        var xMillimeters = xMeters * 1000.0;
        var yMillimeters = yMeters * 1000.0;

        // 組出指令字串並送出
        var command = $"MOVE {xMillimeters:F3} {yMillimeters:F3} {zMillimeters:F3}";
        _socket.Send(command);
    }

    // TODO: 支援速度參數
}

public class RobotSocket
{
    public void Send(string command)
    {
        // Test implementation.
    }
}
