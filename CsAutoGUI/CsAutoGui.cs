using System.Runtime.InteropServices;

namespace CsAutoGUI;
using System.Drawing;
public class CsAutoGui
{
    
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
    
    
    
}