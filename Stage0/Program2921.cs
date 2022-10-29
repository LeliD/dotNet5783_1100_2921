namespace Targil0
{
    partial class Program
    {
        private static void Main(string[] args)
        {
            Welcome2921();
            Welcome1100();
            Console.ReadKey();
        }

        private static void Welcome2921()
        {
            Console.Write("Enter your name: ");
            string s = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first consule application", s);
        }
        static partial void Welcome1100();
    }
}