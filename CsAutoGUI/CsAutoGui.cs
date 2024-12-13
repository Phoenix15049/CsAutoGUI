using OpenCvSharp;
using System.Drawing;
using System.Drawing.Imaging;
using OpenCvSharp.CPlusPlus;

namespace CsAutoGUI;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;
using System;

using System;
using System.IO;

using AForge.Imaging;
using AForge.Imaging.Filters;

public class CsAutoGui
{
    
    [DllImport("user32.dll")]
    static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, uint nFlags);

    [DllImport("user32.dll")]
    static extern IntPtr GetDesktopWindow();
    
    
    
    [DllImport("user32.dll")]
    static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);
    private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    private const uint MOUSEEVENTF_LEFTUP = 0x0004;
    
    
    
    
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

    public Point Size()
    {
        return new Point(GetSystemMetrics(0), GetSystemMetrics(1));
    }
    
    public Point Position()
    {
        GetCursorPos(out Point point);
        return point;
    }

    public void MoveTo(int x, int y, double delay = 0.0, int steps = 64)
    {
        if (steps <= 0) return; // Ensure steps is positive
        double delayInMs = delay * 1000; // Convert Delay to milliseconds
        Console.WriteLine("START");
    
        GetCursorPos(out Point currentPoint);
    
        // Calculate the total sleep time for each step
        int sleepTime = (int)(delayInMs / steps); // Total delay divided by number of steps
    
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

    public bool OnScreen(int x, int y)
    {
        int screenx = Size().X;
        int screeny = Size().Y;
        return x <= screenx && y <= screeny && x >= 0 && y >= 0;
    }

    public void Sleep(double seconds)
    {
        double ms = seconds * 1000;
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
    
    public void Click(int? x = null, int? y = null)
    {
        if (x.HasValue && y.HasValue)
        {
            MoveTo(x.Value, y.Value);
        }

        // کلیک موس
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        Thread.Sleep(50);  // مکث کوتاه
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
    }

    public void DoubleClick(int? x = null, int? y = null)
    {
        Click(x, y);
        Thread.Sleep(50);
        Click(x, y);
    }
    
    public void TripleClick(int? x = null, int? y = null)
    {
        Click(x, y);
        Thread.Sleep(50);
        Click(x, y);
        Thread.Sleep(50);
        Click(x, y);
    }

}

    
    
    
    
    
    
    
    
    
