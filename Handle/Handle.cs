using System.ComponentModel.DataAnnotations;
using System.Data.Common;
//NOTE download Data.SqlClient Packages on nuget.org by command: dotnet add package System.Data.SqlClient
using System.Data.SqlClient; //NOTE in .netcore 6 data.SqlClient is no longer available
using static System.Console;
using static adonet.Program;
namespace adonet
{
    public class Handing
    {
        static int width = 92; //Chiều dài của Menu. Sử dụng cho UI.
        /*Chương trình thứ 1 Nhập dữ liệu vào database*/
        #region Input
        public static void program1()
        {
            byte choices;
            do
            {
                try
                {
                    choices = menu(1);
                }
                catch (System.Exception)
                {
                    choices = 13;
                }
                switch (choices)
                {
                    case 1:
                        {
                            program1_1();
                            break;
                        }
                    case 2:
                        {
                            program1_2();
                            break;
                        }
                    case 3:
                        {
                            program1_3();
                            break;
                        }
                    case 0:
                        {
                            //Thoát Program 1
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

        public static void program1_1()
        {

            var sqlStringConnection = $"Data Source={User_Pass[0]};Initial Catalog=ASM_C#2;{User_Pass[1]}";
            //NOTE sqlServer infomation connect

            using var connection = new SqlConnection(sqlStringConnection);
            connection.Open();//NOTE open connection C# to SQLserver
            centerWrite(30);
            WriteLine("Connection to database is " + connection.State);

            using DbCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @$"
            select * from Class
            ";
            var dataReader_local = command.ExecuteReader();
            string col_1 = "IdClass", col_2 = "NameClass";
            WriteLine($"| {col_1,-10} | {col_2,-10} |");
            while (dataReader_local.Read())
            {
                WriteLine($"| {dataReader_local["IdClass"],-10} | {dataReader_local["NameClass"],-10}");
            }
            dataReader_local.Close();
            connection.Close();
            centerWrite(32);
            WriteLine("Connection to database is " + connection.State);
            notification1();
        }
        public static void program1_2()
        {
            var sqlStringConnection = $"Data Source={User_Pass[0]};Initial Catalog=ASM_C#2;{User_Pass[1]}";
            //NOTE sqlServer infomation connect

            using var connection = new SqlConnection(sqlStringConnection);
            connection.Open();//NOTE open connection C# to SQLserver
            centerWrite(30);
            WriteLine("Connection to database is " + connection.State);

            using DbCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @"
select ISNULL(max(IdClass),0) as maxID from class
            ";
            var maxClassID = command.ExecuteReader();
            maxClassID.Read();
            int numberClassID = Convert.ToInt32(maxClassID["maxID"]);
            maxClassID.Close();
            command.CommandText = @$"
            select * from Class
            ";
            var dataReader_local = command.ExecuteReader();
            string col_1 = "IdClass", col_2 = "NameClass";
            WriteLine($"| {col_1,-10} | {col_2,-10} |");
            while (dataReader_local.Read())
            {
                WriteLine($"| {dataReader_local["IdClass"],-10} | {dataReader_local["NameClass"],-10}");
            }
            dataReader_local.Close();
            centerWrite(50);
            WriteLine("___________________ Request: a ___________________");

            for (; ; )
            {
                Class cls = new Class(numberClassID += 1);
                command.CommandText = @$"
set identity_insert Class on;
insert into dbo.Class(IdClass,NameClass)
values ({cls.IdClass}, N'{cls.NameClass}')
";
                var dataReader_inFor = command.ExecuteReader();//NOTE execute SQL command...
                dataReader_inFor.Close();
                WriteLine();
                Write("Continue to enter? (y/n): ");
                String? n = ReadLine();
                if (n == "n" || n == "N")
                {
                    break;
                }
            }
            connection.Close();
            centerWrite(32);
            WriteLine("Connection to database is " + connection.State);
            notification1();

        }
        public static void program1_3() //REVIEW nhập danh sách student và class
        {
            var sqlStringConnection = $"Data Source={User_Pass[0]};Initial Catalog=ASM_C#2;{User_Pass[1]}";
            //NOTE sqlServer infomation connect

            using var connection = new SqlConnection(sqlStringConnection);
            connection.Open();//NOTE open connection C# to SQLserver
            WriteLine("Connection to database is " + connection.State);

            using DbCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @"
select ISNULL(max(Student.StId),0) as maxID from Student
            ";
            var maxStID = command.ExecuteReader();
            maxStID.Read();
            int numberStudentID = Convert.ToInt32(maxStID["maxID"]);
            maxStID.Close();
            command.CommandText = @"
select ISNULL(max(IdClass),0) as maxID from class
";
            var maxClassID = command.ExecuteReader();
            maxClassID.Read();
            int numberClassID = Convert.ToInt32(maxClassID["maxID"]);
            maxClassID.Close();
            centerWrite(50);
            WriteLine("___________________ Request: b ___________________");
            if (numberClassID == 0)
            {
                centerWrite(40);
                WriteLine("Chưa thể nhập được vì chưa có lớp nào!!!");
            }
            else
            {

                for (; ; )
                {
                    Student sv = new Student(numberStudentID += 1, numberClassID);
                    command.CommandText = @$"
                      set identity_insert Class off;

set identity_insert dbo.Student on;
insert into dbo.Student (StId,Name,Mark,Email,idClass)
values ({sv.StId},N'{sv.Name}',{sv.Mark},'{sv.Email}',{sv.idClass});
";
                    var dataReader_inFor = command.ExecuteReader();//NOTE execute SQL command...
                    dataReader_inFor.Close();
                    WriteLine();
                    Write("Continue to enter? (y/n): ");
                    String? n = ReadLine();
                    if (n == "n" || n == "N")
                    {
                        break;
                    }
                }
            }
            connection.Close();
            centerWrite(32);
            WriteLine("Connection to database is " + connection.State);
            notification1();
        }
        #endregion
        /*Chương trình thứ 2 Xuất thông tinh xử lý xếp loại*/
        #region Output
        public static void program2(string CommandSQL) //REVIEW xuất danh sach đồng thời xuất xếp loại
        {
            var sqlStringConnection = $"Data Source={User_Pass[0]};Initial Catalog=ASM_C#2;{User_Pass[1]}";
            //NOTE sqlServer infomation connect
            using var connection = new SqlConnection(sqlStringConnection);
            connection.Open();//NOTE open connection C# to SQLserver
            centerWrite(30); //NOTE Căng giữa chữ
            WriteLine("Connection to database is " + connection.State);

            using DbCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @$"
{CommandSQL}
";
            var dataReader_local = command.ExecuteReader();
            string col_1 = "StId", col_2 = "Name", col_3 = "Mark", col_4 = "Rank", col_5 = "Email", col_6 = "IdClass";
            WriteLine($"|| {col_1,-4} | {col_2,-20} | {col_3,-5} | {col_4,-10} | {col_5,-25} | {col_6,-7} ||");
            WriteLine("".PadRight(92, '-'));
            while (dataReader_local.Read())
            {
                WriteLine($"|| {dataReader_local["StId"],-4} | {dataReader_local["Name"],-20} | {dataReader_local["Mark"],-5} | {handleRank(Convert.ToDouble(dataReader_local["Mark"])),-10} | {dataReader_local["Email"],-25} | {dataReader_local["IdClass"],-7} ||");
                WriteLine("".PadRight(92, '-'));
            }
            dataReader_local.Close();
            connection.Close();
            centerWrite(32);

            WriteLine("Connection to database is " + connection.State);
            notification1();
        }
        #endregion

        /*Chương trình thứ 3 Tìm kiếm sinh viên từ khoảng điểm nhập vào*/
        #region Filter
        public static void program3()
        {
            bool test;
            Double Mark_a, Mark_b, mark;
            centerWrite(18);
            WriteLine("Nhập khoảng điểm: ");
            do
            {
                Write("Nhập khoảng điểm a: ");
                test = Double.TryParse(ReadLine(), out mark);
            } while (test == false || mark < 0 || mark > 10);
            Mark_a = mark;
            do
            {
                Write("Nhập khoảng điểm b: ");
                test = Double.TryParse(ReadLine(), out mark);
            } while (test == false || mark < 0 || mark > 10);

            Mark_b = mark;
            if (Mark_b < Mark_a)
            {
                (Mark_a, Mark_b) = (Mark_b, Mark_a);
            }
            program2(@$"select * from Student where Mark > {Mark_a} And Mark < {Mark_b}");

        }
        public static void program4()
        {
            int StId = 0;
            byte choices;
            do
            {
                try
                {
                    choices = menu(4);
                }
                catch (System.Exception)
                {
                    choices = 13;
                }
                switch (choices)
                {
                    case 1:
                        {
                            program4_1(ref StId);
                            break;
                        }
                    case 2:
                        {
                            if (StId == 0)
                            {
                                program4_1(ref StId);
                            }
                            program4_2(StId);
                            break;
                        }

                    case 0:
                        {
                            //Thoát Program 4
                            break;
                        }
                    default:
                        {
                            Handing.notificationError();//Xuất thông báo nhập lỗi
                            break;
                        }
                }
            } while (choices != 0);
            notification1();
        }
        public static void program4_1(ref int StId)
        {
            bool test;
            int id;
            do
            {
                centerWrite(18);
                Write("Nhập StId: ");
                test = Int32.TryParse(ReadLine(), out id);
            } while (test == false);
            StId = id;
            program2(@$"select * from student where StId ={StId}");
        }
        public static void program4_2(int StId)
        {
            byte choices;
            do
            {
                try
                {
                    choices = menuProgram4_2(StId);
                }
                catch (System.Exception)
                {
                    choices = 13;
                }

                bool test;
                switch (choices)
                {
                    case 1:
                        {
                            string? name;
                            do
                            {
                                WriteLine("Nhập tên mới ");
                                name = ReadLine();
                            } while (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name));
                            program2(@$"
update Student set Name = N'{name}' where StId = {StId};
select * from student where StId ={StId}");
                            break;
                        }
                    case 2:
                        {
                            double mark;
                            do
                            {
                                WriteLine("Nhập điểm mới: ");
                                test = Double.TryParse(ReadLine(), out mark);
                            } while (test == false || mark < 0 || mark > 10);
                            program2(@$"
update Student set Mark = {mark} where StId = {StId};
select * from student where StId ={StId}");
                            break;
                        }
                    case 3:
                        {
                            string? email;
                            do
                            {
                                WriteLine("Nhập email mới: ");
                                email = ReadLine();
                            } while (validateEmail(email) == false);
                            program2(@$"
update Student set Email = '{email}' where StId = {StId};
select * from student where StId ={StId}");
                            break;
                        }
                    case 4:
                        {
                            var sqlStringConnection = $"Data Source={User_Pass[0]};Initial Catalog=ASM_C#2;{User_Pass[1]}";
                            //NOTE sqlServer infomation connect

                            using var connection = new SqlConnection(sqlStringConnection);
                            connection.Open();//NOTE open connection C# to SQLserver
                            WriteLine("Connection to database is " + connection.State);

                            using DbCommand command = new SqlCommand();
                            command.Connection = connection;
                            command.CommandText = @"
select ISNULL(max(IdClass),0) as maxID from class
";
                            var maxClassID = command.ExecuteReader();
                            maxClassID.Read();
                            int numberClassID = Convert.ToInt32(maxClassID["maxID"]);
                            maxClassID.Close();
                            int idTemp;
                            do
                            {
                                WriteLine("Nhập idClass mới: ");
                                test = int.TryParse(ReadLine(), out idTemp);
                            } while (idTemp > numberClassID || test == false);
                            program2(@$"
update Student set IdClass = {idTemp} where StId = {StId};
select * from student where StId ={StId}");
                            break;
                        }
                    case 5:
                        {
                            int numberClassID;
                            var sqlStringConnection = $"Data Source={User_Pass[0]};Initial Catalog=ASM_C#2;{User_Pass[1]}";
                            //NOTE sqlServer infomation connect
                            using var connection = new SqlConnection(sqlStringConnection);
                            connection.Open();//NOTE open connection C# to SQLserver
                            using DbCommand command = new SqlCommand();
                            command.Connection = connection;
                            command.CommandText = @"
select ISNULL(max(IdClass),0) as maxID from class
";
                            var maxClassID = command.ExecuteReader();
                            maxClassID.Read();
                            numberClassID = Convert.ToInt32(maxClassID["maxID"]);
                            Student sv = new Student(StId, numberClassID);
                            maxClassID.Close();
                            program2(@$"
update Student 
set Name = N'{sv.Name}',Mark = {sv.Mark},Email = '{sv.Email}', IdClass = {sv.idClass}
where StId = {StId}
select * from student where StId ={StId}");

                            break;
                        }

                    case 0:
                        {
                            //Thoát Program 4
                            break;
                        }
                    default:
                        {
                            Handing.notificationError();//Xuất thông báo nhập lỗi
                            break;
                        }
                }
            } while (choices != 0);
            notification1();
        }


        #endregion
        /*Sử dụng lập trình bất đồng bộ vừa tính điểm trung bình vừa lưu dữ liệu vào file*/
        #region ASync
        public static async void program7()
        {
            var Ts = new CancellationTokenSource();
            CancellationToken ct = Ts.Token;

            DirectoryInfo dir = new DirectoryInfo("Test_Out");
            try
            {
                //REVIEW check if dir Exists
                if (dir.Exists)
                {
                }
                else
                {
                    dir.Create();
                }
            }
            catch (DirectoryNotFoundException d)
            {
                WriteLine(d.Message.ToString());
            }
            var DTB = new Task(() =>
            {

                FileInfo file = new FileInfo("Test_Out\\Asm_C#2.txt");
                using (StreamWriter writer = file.AppendText())
                {
                    var sqlStringConnection = $"Data Source={User_Pass[0]};Initial Catalog=ASM_C#2;{User_Pass[1]}";
                    //NOTE sqlServer infomation connect

                    using var connection = new SqlConnection(sqlStringConnection);
                    connection.Open();//NOTE open connection C# to SQLserver

                    using DbCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = @$"
select Class.IdClass, Class.NameClass, AVG(Mark) as DTB 
from Student inner join Class on Student.idClass = Class.idClass 
Group by Class.IdClass, Class.NameClass;
";
                    var dataReader_local = command.ExecuteReader();
                    string col_1 = "IDclass", col_2 = "NameClass", col_3 = "DTB";
                    writer.WriteLineAsync($"________________________________________\n| {col_1,-10} | {col_2,-10} | {col_3,-10} |");
                    lock (dataReader_local)
                    {
                        while (dataReader_local.Read())
                        {
                            Thread.Sleep(400);
                            writer.WriteLineAsync($"| {dataReader_local["IdClass"],-10} | {dataReader_local["NameClass"],-10} | {Math.Round(Convert.ToDouble(dataReader_local["DTB"]), 2),-10} |");
                        }
                        dataReader_local.Close();
                        connection.Close();
                        //    for (int i = 1; i < 100; i++)
                        //    {

                        centerWrite(31);
                        WriteLine("*********** Success ***********");
                        Ts.Cancel();
                        ct.ThrowIfCancellationRequested();
                        if (ct.IsCancellationRequested)
                        {
                            // Clean up here, then...
                            ct.ThrowIfCancellationRequested();
                        }

                    }
                }
            }, Ts.Token);
            // Thread t2 = new Thread(() =>
            //  {
            //      lock (DTB)
            //      {
            //          string Dat = "TRẦN PHÚ ĐẠT";
            //          for (int i = 0; i < Dat.Length; i++)
            //          {
            //              Thread.Sleep(200);
            //              Console.Write(Dat[i]);
            //          }
            //          Console.WriteLine();
            //      }
            //  });
            DTB.Start();
            try
            {
                await DTB;
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine($"{nameof(OperationCanceledException)} thrown with message: {e.Message}");
            }
            finally
            {
                Ts.Dispose();
            }            // t2.Start();
        }

        public static bool program8()
        {

            User_Pass = Server.InputServer().Split("|");
            var sqlStringConnection = $"Data Source={User_Pass[0]};Initial Catalog=ASM_C#2;{User_Pass[1]}";
            using var connection = new SqlConnection(sqlStringConnection);
            try
            {
                connection.Open();
            }
            catch (Exception)
            {
                return Server_Connect = false;
            }
            return connection.State.ToString() == "Open";
        }

        public static void program9()
        {
            string J_A = @"
                Chào bạn tui là Trần Phú Đạt
            Nickname J.A đến từ Dotnet Universe
                  Sinh viên FPoly khóa 17            
            ", dotnet_Universe = @"
            Dotnet Universe khởi nguồn ban đầu là một CLB trường vì trường không có bất kì CLB nào
            Dành cho sinh viền theo .Net Nên chúng mình 9 thành viên đầu tiên là sinh viên của Khóa 17
            đã đồng lòng cùng xây dựng lên Dotnet Universe cùng cố gắng để không phải ai cô đơn học code.
            Gồm:
            - J.A:        Trần Phú Đạt,
            - YinGH:      Thái Dân,
            - Đăng Duy,
            - Hannee:     Gia Hân,
            - Như Ý Lake: Như Ý,
            - Văn Quỳnh,
            - Tuyết Nhi,
            - Sâm Chấn,
            - Đình Hướng.

            Dotnet Universe - 10 - 5 - 2022.Net
            Đây sẽ là một kỷ nguyên mới nơi tất cả mọi người đều không bị bỏ lại phía sau...
            Learning With 100% Focus.
            Learning With 80%  Passion.
            Learning With 50%  Love.
            Learning With 20%  Negative.
            And
            Learning With 0%   loneliness.

            Rất mong có được sự tin yêu từ các đồng môn C#, .Net";

            centerWrite(35);
            WriteLine(J_A);
            WriteLine(dotnet_Universe);
            notification1();

        }

        #endregion
        /*Kiểm tra email có phải là email hem*/
        #region Validation
        public static bool validateEmail(string email)
        {
            if (string.IsNullOrEmpty(email) == true)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(email) == true)
            {
                return false;
            }
            if (new EmailAddressAttribute().IsValid(email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        /*Xử lý xếp loại*/
        #region Handle_Rank
        public static string handleRank(Double mark)
        {
            string? rank;
            if (mark < 5)
            {
                rank = "Yếu";
            }
            else if (mark < 6.5)
            {
                rank = "Trung Bình";
            }
            else if (mark < 7.5)
            {
                rank = "Khá";
            }
            else if (mark < 9)
            {
                rank = "Giỏi";
            }
            else
            {
                rank = "Xuất sắc";
            }

            return rank;
        }
        #endregion
        /*Xử lý giao diện và thông báo cho chương trình*/
        #region UI
        public static byte menu(int SelectMenu)
        {
            Clear();
            string menu = "";
            ForegroundColor = ConsoleColor.Yellow;
            switch (SelectMenu)
            {
                case 0:
                    {
                        menu = @"
[[========================================================================================]] 
||                     _   .-')       ('-.       .-') _                                   ||    
||                    ( '.( OO )_   _(  OO)     ( OO ) )                                  ||
||                     ,--.   ,--.)(,------.,--./ ,--,' ,--. ,--.                         ||
||                     |   `.'   |  |  .---'|   \ |  |\ |  | |  | Trần Phú Đạt - AKA: J.A ||
||                     |         |  |  |    |    \|  | )|  | | .-')                       ||
||                     |  |'.'|  | (|  '--. |  .     |/ |  |_|( OO )  From:DotNetUniverse ||
||                     |  |   |  |  |  .--' |  |\    |  |  | | `-' /                      ||
||                     |  |   |  |  |  `---.|  | \   | ('  '-'(_.-'             With Love ||
||                     `--'   `--'  `------'`--'  `--'   `-----'                          ||
||----------------------------------------------------------------------------------------||
||                            1.  Nhập danh sách Class/Student                            ||
||                            2.  Xuất danh sách Student                                  ||
||                            3.  Tìm học viên theo khoảng điểm                           ||
||                            4.  Cập nhật thông tin sinh viên theo ID                    ||
||                            5.  Sắp xếp Student theo Mark cao đến thấp                  ||
||                            6.  Xuất 5 Student có Mark cao nhất                         ||
||                            7.  Tính điểm trung bình và ghi vào file                    ||
||                            8.  Thay đổi server                                         ||
||                            9.  Thông tin người thực hiện                               ||
||________________________________________________________________________________________||
||                                0. Thoát chương trình                                   ||
[[========================================================================================]]";
                        break;
                    }
                case 1:
                    {
                        menu = @"
[[========================================================================================]] 
||                     _   .-')       ('-.       .-') _                                   ||    
||                    ( '.( OO )_   _(  OO)     ( OO ) )                                  ||
||                     ,--.   ,--.)(,------.,--./ ,--,' ,--. ,--.                         ||
||                     |   `.'   |  |  .---'|   \ |  |\ |  | |  |                         ||
||                     |         |  |  |    |    \|  | )|  | | .-')                       ||
||                     |  |'.'|  | (|  '--. |  .     |/ |  |_|( OO )                      ||
||                     |  |   |  |  |  .--' |  |\    |  |  | | `-' /                      ||
||                     |  |   |  |  |  `---.|  | \   | ('  '-'(_.-'                       ||
||                     `--'   `--'  `------'`--'  `--'   `-----'                          ||
||----------------------------------------------------------------------------------------||
||                            1.  Xem danh sách Class                                     ||
||                            2.  Nhập danh sách Class                                    ||
||                            3.  Nhập danh sách Student                                  ||
||________________________________________________________________________________________||
||                                  0. Trở về MENU                                        ||
[[========================================================================================]]";
                        break;
                    }
                case 4:
                    {
                        menu = @"
[[========================================================================================]] 
||                     _   .-')       ('-.       .-') _                                   ||    
||                    ( '.( OO )_   _(  OO)     ( OO ) )                                  ||
||                     ,--.   ,--.)(,------.,--./ ,--,' ,--. ,--.                         ||
||                     |   `.'   |  |  .---'|   \ |  |\ |  | |  |                         ||
||                     |         |  |  |    |    \|  | )|  | | .-')                       ||
||                     |  |'.'|  | (|  '--. |  .     |/ |  |_|( OO )                      ||
||                     |  |   |  |  |  .--' |  |\    |  |  | | `-' /                      ||
||                     |  |   |  |  |  `---.|  | \   | ('  '-'(_.-'                       ||
||                     `--'   `--'  `------'`--'  `--'   `-----'                          ||
||----------------------------------------------------------------------------------------||
||                            1.  Tra cứu thông tin Student                               ||
||                            2.  Cập nhật thông tin Student                              ||
||________________________________________________________________________________________||
||                                  0. Trở về MENU                                        ||
[[========================================================================================]]";
                        break;
                    }
            }
            WriteLine(menu);
            ResetColor();
            centerWrite(6); Write("Nhập: ");
            byte choices = Convert.ToByte(ReadLine());
            return choices;
        }

        public static byte menuProgram4_2(int id)
        {
            Clear();
            string menu = "";
            ForegroundColor = ConsoleColor.Yellow;

            menu = @$"
[[========================================================================================]] 
||                     _   .-')       ('-.       .-') _                                   ||    
||                    ( '.( OO )_   _(  OO)     ( OO ) )                                  ||
||                     ,--.   ,--.)(,------.,--./ ,--,' ,--. ,--.                         ||
||                     |   `.'   |  |  .---'|   \ |  |\ |  | |  |                         ||
||                     |         |  |  |    |    \|  | )|  | | .-')                       ||
||                     |  |'.'|  | (|  '--. |  .     |/ |  |_|( OO )                      ||
||                     |  |   |  |  |  .--' |  |\    |  |  | | `-' /                      ||
||                     |  |   |  |  |  `---.|  | \   | ('  '-'(_.-'                       ||
||                     `--'   `--'  `------'`--'  `--'   `-----'                          ||
||----------------------------------------------------------------------------------------||
||                                   Student ID: {id,-5}                                    ||
||                               1.  Cập nhật Name                                        ||
||                               2.  Cập nhật Mark                                        ||
||                               3.  Cập nhật Email                                       ||
||                               4.  Cập nhật IdClass                                     ||
||                               5.  Cập nhật Tất cả                                      ||
||________________________________________________________________________________________||
||                                  0. Trở về MENU                                        ||
[[========================================================================================]]";
            WriteLine(menu);
            ResetColor();

            centerWrite(6); Write("Nhập: ");

            byte choices = Convert.ToByte(ReadLine());
            return choices;
        }

        public static void endingProgram()
        {
            ForegroundColor = ConsoleColor.Yellow;//Đổi màu
            string text = "";
            text = @"    
                   [[==================================================]]                     
                   ||     (     (                                      ||
                   ||     )\ )  )\ )  *   )                 (          ||
                   ||    (()/( (()/(` )  /(                 )\ (       ||
                   ||     /(_)) /(_))( )(_))___ `  )    (  ((_))\ )    ||
                   ||    (_))_|(_)) (_(_())|___|/(/(    )\  _ (()/(    ||
                   ||    | |_  | _ \|_   _|    ((_)_\  ((_)| | )(_))   ||
                   ||    | __| |  _/  | |      | '_ \)/ _ \| || || |   ||
                   ||    |_|   |_|    |_|      | .__/ \___/|_| \_, |   ||
                   ||                          |_|             |__/    ||
                   [[==================================================]]";
            WriteLine(text);
            ResetColor();
            notification1();
        }
        public static void notification1()
        {
            SetCursorPosition((width - 32) / 2, CursorTop);
            WriteLine("\nPress any key to continue");
            ReadKey();
        }
        public static void centerWrite(int x)
        {
            WriteLine("\n");
            SetCursorPosition((width - x) / 2, CursorTop);//Di chuyển vị trí con trỏ chuột

        }

        public static void notificationError()
        {
            SetCursorPosition((width - 13) / 2, CursorTop);//13 là độ dài chuỗi "Lỗi nhập liệu"
            WriteLine("Lỗi nhập liệu");
            notification1();
        }
        #endregion
    }
}

/*
Người Yêu Đạt
9.2
jackandy249@gmail.com
1
y
Người Yêu Hoàng
7.2
andy249@gmail.com
8
y
Đứa không yêu Hoàng
5.2
jack@gmail.com
2
n
*/