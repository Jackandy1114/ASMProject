//NOTE download Data.SqlClient Packages on nuget.org by command: dotnet add package System.Data.SqlClient
// using System.Data.Common;
// using System.Data.SqlClient;
namespace adonet
{
    class Server
    {
        public static string? Data { get; set; }
        // public static string Data = "HOANGTRAN\\JACKANDY1114";
        // public static string Pass = "iloveuzienoi1114";
        public static string? Pass { get; set; }

        public static string InputServer()
        {
            Console.Write("Nhập tên server: ");
            Data = Console.ReadLine();
            Console.Write("Nhập Password: ");
            Pass = Console.ReadLine();
            Data = ""; //NOTE TESTING
            if (Data == "")
            {
                Data = "HOANGTRAN\\JACKANDY1114";
                // Pass = "iloveuzienoi1114"; 
            }
            string[] strings = Data.Split("\\");
            string.Concat(strings[0], "\\", strings[1]);
            return Pass == "" ? String.Concat(Data, "|", "Integrated Security=True") : String.Concat(Data, "|", "User ID=sa;Password=", Pass);
        }
    }
}
