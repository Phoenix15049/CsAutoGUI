namespace CsAutoGUI;

public class MainTest
{
    static void Main(string[] args)
    {
        CsAutoGui c = new CsAutoGui();
        Console.WriteLine(c.size());
        c.moveTo(20,200,3,10);
        c.Sleep(2);
        c.moveTo(200,200,3,10);
    }
}