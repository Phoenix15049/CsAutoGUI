using System.Runtime.InteropServices;

namespace CsAutoGUI;
using System.Drawing;
public class CsAutoGui
{
    
    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out Point lpPoint);
    public Point position()
    {
        GetCursorPos(out Point point);
        return point;
    }
}