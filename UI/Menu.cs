using System;
using System.Runtime.InteropServices;

namespace BasicRPG.UI
{
    class Menu
    {
        string title;
        string[] menuChoices;

        public Menu()
        {
            title =
                "███████╗██╗  ██╗██╗   ██╗██╗     ██╗ ██████╗ ███████╗\n" +
                "██╔════╝██║ ██╔╝╚██╗ ██╔╝██║     ██║██╔═══██╗██╔════╝\n" +
                "█████╗  █████╔╝  ╚████╔╝ ██║     ██║██║   ██║███████╗\n" +
                "██╔══╝  ██╔═██╗   ╚██╔╝  ██║     ██║██║   ██║╚════██║\n" +
                "███████╗██║  ██╗   ██║   ███████╗██║╚██████╔╝███████║\n" +
                "╚══════╝╚═╝  ╚═╝   ╚═╝   ╚══════╝╚═╝ ╚═════╝ ╚══════╝\n";

            menuChoices = new string[] { "Start Adventure", "Exit Application" };
        }

        public int StartMenu()
        {
            return UIHandler.SelectiveChoice(title, menuChoices, TextPosition.Center);
        }

        public int ExitCode()
        {
            return menuChoices.Length-1;
        }

        // // Using Win32 API
        // public const int HIDE = 0;
        // public const int MAXIMIZE = 3;
        // public const int MINIMIZE = 6;
        // public const int RESTORE = 9;
        //
        // [DllImport("kernel32.dll", ExactSpelling = true)]
        // private static extern IntPtr GetConsoleWindow();
        //
        // public static IntPtr ThisConsole = GetConsoleWindow();
        //
        // [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        // public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}
