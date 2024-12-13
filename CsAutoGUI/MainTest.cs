using System.Drawing;

namespace CsAutoGUI;

public class MainTest
{
    static void Main(string[] args)
    {
        CsAutoGui c = new CsAutoGui();
        Console.WriteLine(c.Size());
        // c.moveTo(20,200,3);
        // c.Sleep(2);
        c.MoveTo(600,300,1);
        
    }
}