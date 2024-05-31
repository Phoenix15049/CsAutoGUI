namespace CsAutoGUI;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;
public class CsAutoGui
{
    private static double PAUSE = 0.0;
    
    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);
    
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
    
    public void move(int x, int y, double Delay = 0.0,int steps = 25)
    {
        // Increase Steps this for more 'smooth' movement
        double DelayInMs = Delay * 1000;
        
        GetCursorPos(out Point currentPoint);
         
        int sleepTime = (int)DelayInMs / steps;
        for (int i = 0; i < steps; i++)
        {
            int tempX = currentPoint.X + (x - currentPoint.X) * i / steps;
            int tempY = currentPoint.Y + (y - currentPoint.Y) * i / steps;
            SetCursorPos(tempX, tempY);
            Thread.Sleep(sleepTime);
        }
        SetCursorPos(x, y); // Ensure the cursor ends up at the exact spot
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