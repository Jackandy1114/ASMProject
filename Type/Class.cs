
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

/*
Trần Hoàng
9.2
jackandy249@gmail.com
1
y
Trần Hoàng
7.2
andy249@gmail.com
1
y
Trần Đạt
5.2
jack@gmail.com
2
n
*/