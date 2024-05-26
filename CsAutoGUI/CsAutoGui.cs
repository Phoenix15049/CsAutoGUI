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
    
    
    public Point size()
    {
        return new Point(GetSystemMetrics(0), GetSystemMetrics(1));
    }
    
    public Point position()
    {
        GetCursorPos(out Point point);
        return point;
    }

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