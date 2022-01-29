using System;

namespace ShieldDefender
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new ShieldDefenderGame())
                game.Run();
        }
    }
}
