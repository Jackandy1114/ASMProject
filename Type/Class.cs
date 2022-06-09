
//NOTE download Data.SqlClient Packages on nuget.org by command: dotnet add package System.Data.SqlClient
using static System.Console;
namespace adonet
{
    class Class
    {

        public int IdClass { get; private set; }
        public string? NameClass { get; private set; }

        //take IDclass
        public Class(int maxClassID)
        {
            WriteLine($"ClassID: {maxClassID}");
            this.IdClass = maxClassID;
            Write("Nhập tên Class: ");
            NameClass = ReadLine();
        }
    }
}
