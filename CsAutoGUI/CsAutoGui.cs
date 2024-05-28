namespace CsAutoGUI;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;
public class CsAutoGui
{
    private static double PAUSE = 0.0;
    
    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out Point lpPoint);

    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int nIndex);

    [DllImport("gdi32.dll")]
    static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

    public const int LOGPIXELSX = 88;
    public const int LOGPIXELSY = 90;

    public Point size()
    {
        IntPtr desktop = GetDC(IntPtr.Zero);
        int LogicalScreenHeight = GetSystemMetrics(1);
        int PhysicalScreenHeight = GetDeviceCaps(desktop, LOGPIXELSY);
        float ScreenScalingFactor = (float)PhysicalScreenHeight / (float)LogicalScreenHeight;

        int width = (int)(GetSystemMetrics(0) * ScreenScalingFactor);
        int height = (int)(GetSystemMetrics(1) * ScreenScalingFactor);

        ReleaseDC(IntPtr.Zero, desktop);

        return new Point(width, height);
    }

    public Point position()
    {
        GetCursorPos(out Point point);

        IntPtr desktop = GetDC(IntPtr.Zero);
        int LogicalScreenHeight = GetSystemMetrics(1);
        int PhysicalScreenHeight = GetDeviceCaps(desktop, LOGPIXELSY);
        float ScreenScalingFactor = (float)PhysicalScreenHeight / (float)LogicalScreenHeight;

        point.X = (int)(point.X * ScreenScalingFactor);
        point.Y = (int)(point.Y * ScreenScalingFactor);

        ReleaseDC(IntPtr.Zero, desktop);

        return point;
    }

    [DllImport("user32.dll", SetLastError = true)]
    static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError = true)]
    static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

    public bool onScreen(int x, int y)
    {
        int screenx = size().X;
        int screeny = size().Y;
        if (x <= screenx && y <= screeny && x >= 0 && y >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Sleep(double Seconds)
    {
        double ms = Seconds * 1000;
        Thread.Sleep((int)ms);
    }
    
    
    
}