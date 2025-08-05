using System;
using System.Windows.Forms;
using Library_Management.Admin;
using Library_Management.Issue_Book;
using Library_Management.Return_Book;
using Library_Management.User_Management;

namespace Library_Management
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Book());
        }
    }
}
