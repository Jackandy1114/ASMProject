//NOTE download Data.SqlClient Packages on nuget.org by command: dotnet add package System.Data.SqlClient
// using System.Data.Common;
// using System.Data.SqlClient;
using static System.Console;
namespace adonet
{
    class Student
    {
        public int StId { get; private set; }
        public string? Name { get; private set; }
        public double Mark { get; private set; }
        public string? Email { get; private set; }
        public int idClass { get; private set; }
        public Student(int maxStudentID, int numberClassID)
        {

            StId = maxStudentID;
            WriteLine($"Student ID: {maxStudentID}");
            do
            {
                Write("Nhập tên: ");
                Name = ReadLine();
            } while (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name));
            bool test = true;
            double mark;
            int idTemp;
            do
            {
                Write("Nhập điểm: ");
                test = Double.TryParse(ReadLine(), out mark);
            } while (test == false || mark < 0 || mark > 10);
            Mark = Math.Round(mark, 2);
            do
            {
                Write("Nhập email: ");
                Email = ReadLine();
            } while (Handing.validateEmail(Email) == false);

            do
            {
                Write("Nhập idClass: ");
                test = int.TryParse(ReadLine(), out idTemp);
            } while (idTemp > numberClassID || test == false);
            idClass = idTemp;
        }

    }
}

/*
Trần Hoàng
9
ackandy249@gmail.com
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