using System;

namespace Match_3
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new MainGameClass())
                game.Run();
        }
    }
#endif
}
