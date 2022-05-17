using System;
using System.Text;

//NOTE download Data.SqlClient Packages on nuget.org by command: dotnet add package System.Data.SqlClient
namespace adonet
{
    class Program
    {
        public static byte choices;

        static void Main()
        {
            Console.InputEncoding = Encoding.Unicode;//Để nhập tiếng việt
            Console.OutputEncoding = Encoding.Unicode;//Để xuất tiếng việt

            do
            {
                try
                {
                    choices = Handing.menu();
                }
                catch (System.Exception)
                {
                    choices = 13;
                }
                switch (choices)
                {
                    case 1:
                        {
                            Handing.program1();
                            break;
                        }
                    case 2:
                        {
                            Handing.program2(@"select * from dbo.Student");
                            break;
                        }
                    case 3:
                        {
                            Handing.program3();
                            break;
                        }
                    case 4:
                        {
                            Handing.program4();
                            break;
                        }
                    case 5:
                        {
                            Handing.program2(@"select * from Student Order by Mark DESC");
                            break;
                        }
                    case 6:
                        {
                            Handing.program2(@"select top 5 * from Student Order by Mark DESC");
                            break;
                        }
                    case 7:
                        {
                            Handing.program7();
                            break;
                        }

                    case 0:
                        {
                            Handing.endingProgram();
                            System.Environment.Exit(0);//Thoát chương trình

                            break;
                        }
                    default:
                        {
                            Handing.notificationError();//Xuất thông báo nhập lỗi
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