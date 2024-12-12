namespace CsAutoGUI;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing;

public class CsAutoGui
{
    private static double PAUSE = 0.0;
    private static volatile bool shouldStop = false; // Flag to indicate if we should stop

    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);
    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out Point lpPoint);
    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int nIndex);
    [DllImport("user32.dll")]
    static extern short GetAsyncKeyState(int vKey);

    public CsAutoGui()
    {
        // Start the key monitoring thread
        Thread keyMonitorThread = new Thread(MonitorKeys);
        keyMonitorThread.IsBackground = true; // Make it a background thread
        keyMonitorThread.Start();
    }

    public Point size()
    {
        return new Point(GetSystemMetrics(0), GetSystemMetrics(1));
    }
    
    public Point position()
    {
        GetCursorPos(out Point point);
        return point;
    }

    public void moveTo(int x, int y, double Delay = 0.0, int steps = 100)
    {
        if (steps <= 0) return; // Ensure steps is positive
        double DelayInMs = Delay * 1000; // Convert Delay to milliseconds
        Console.WriteLine("START");
    
        GetCursorPos(out Point currentPoint);
    
        // Calculate the total sleep time for each step
        int sleepTime = (int)(DelayInMs / steps); // Total delay divided by number of steps
    
        for (int i = 0; i < steps; i++)
        {
            if (shouldStop) // Check if we should stop
            {
                Console.WriteLine("Movement stopped.");
                return;
            }

            int tempX = currentPoint.X + (x - currentPoint.X) * i / steps;
            int tempY = currentPoint.Y + (y - currentPoint.Y) * i / steps;
            SetCursorPos(tempX, tempY);
        
            // Sleep for the calculated time for each step
            Thread.Sleep(sleepTime);
        }
    
        SetCursorPos(x, y); // Ensure the cursor ends up at the exact spot
        Console.WriteLine("END");
    }

    public bool onScreen(int x, int y)
    {
        int screenx = size().X;
        int screeny = size().Y;
        return x <= screenx && y <= screeny && x >= 0 && y >= 0;
    }

    public void Sleep(double Seconds)
    {
        double ms = Seconds * 1000;
        Thread.Sleep((int)ms);
    }

    private void MonitorKeys()
    {
        while (true)
        {
            // Check if the Esc key is pressed
            if (GetAsyncKeyState(0x1B) != 0) // 0x1B is the virtual key code for Esc
            {
                shouldStop = true; // Set the flag to stop
                Console.WriteLine("Esc key pressed. Stopping operations.");
                break; // Exit the loop
            }
            Thread.Sleep(100); // Sleep a bit to avoid high CPU usage
        }
    }
}