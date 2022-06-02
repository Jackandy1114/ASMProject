using System;
using System.Text;
using static adonet.Handing;

//NOTE download Data.SqlClient Packages on nuget.org by command: dotnet add package System.Data.SqlClient
namespace adonet
{
    class Program
    {
        // public static string dataSource()
        // {
        //     centerWrite(17);
        //     Console.WriteLine("Nhập tên server: ");
        //     string dataSource = Console.ReadLine();
        //     string[] strings = dataSource.Split("\\");
        //     strings[0] = strings[0] + "\\\\";
        //     dataSource = string.Concat(strings[0], strings[1]);
        //     Console.WriteLine(dataSource);
        //     return dataSource;
        // }
        public static byte choices;
        public static string Data = "HOANGTRAN\\JACKANDY1114";

        static void Main()
        {
            Console.InputEncoding = Encoding.Unicode;//Để nhập tiếng việt
            Console.OutputEncoding = Encoding.Unicode;//Để xuất tiếng việt

            do
            {
                try
                {
                    choices = menu();
                }
                catch (System.Exception)
                {
                    choices = 13;
                }
                switch (choices)
                {
                    case 1:
                        {
                            program1();
                            break;
                        }
                    case 2:
                        {
                            program2(@"select * from dbo.Student");
                            break;
                        }
                    case 3:
                        {
                            program3();
                            break;
                        }
                    case 4:
                        {
                            program4();
                            break;
                        }
                    case 5:
                        {
                            program2(@"select * from Student Order by Mark DESC");
                            break;
                        }
                    case 6:
                        {
                            program2(@"select top 5 * from Student Order by Mark DESC");
                            break;
                        }
                    case 7:
                        {
                            program7();
                            break;
                        }
                    case 8:
                        {
                            program8();
                            break;
                        }

                    case 0:
                        {
                            endingProgram();
                            System.Environment.Exit(0);//Thoát chương trình

                            break;
                        }
                    default:
                        {
                            notificationError();//Xuất thông báo nhập lỗi
                            break;
                        }
                }
            } while (choices != 0);
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

*/