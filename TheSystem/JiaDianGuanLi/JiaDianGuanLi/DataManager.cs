using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace JiaDianGuanLi
{
    public class DataManager  //数据管理类
    {
        public void CreateDataBase()  //创建数据库
        {
            string path = @"G:\JiaDianGuanLi\Informations.db";
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path);
            con.Open();
            //↓产品表：1.id 2.功能 3.种类 4.品牌 5.价格 6.名字 7.产品序列号
            string ProductTable = "CREATE TABLE IF NOT EXISTS Product(id INTEGER,function TEXT,type TEXT,brand TEXT,price REAL,name TEXT,serialNumber TEXT);";
            //↓顾客表：1.id 2.名字 3.性别 4.电话 5.住址 6.购买商品 7.订单号
            string CustomerTable = "CREATE TABLE IF NOT EXISTS Customer(id INTEGER,name TEXT,sex TEXT,telephone TEXT,position TEXT,whichgoods TEXT);";
            //↓送货表：1.id 2.名字 3.性别 4.电话 5.住址 6.购买商品 7.送货日期 8.是否签收完成 9.订单号
            string GoodsTable = "CREATE TABLE IF NOT EXISTS Goods(id INTEGER,name TEXT,sex TEXT,telephone TEXT,position TEXT,whichgoods TEXT,date TEXT,isOrNo TEXT );";
            //↓安装表:1.id 2.名字 3.性别 4.电话 5.住址 6.购买商品 7.安装日期 8.是否安装完成 9.订单号
            string InstallTable = "CREATE TABLE IF NOT EXISTS Install(id INTEGER,name TEXT,sex TEXT,telephone TEXT,position TEXT,whichgoods TEXT,date text, isOrNo TEXT);";
            //↓维修表:1.id 2.名字 3.性别 4.电话 5.住址 6.购买商品 7.维修日期 8.是否维修完成 9.订单号
            string MaintainTable = "CREATE TABLE IF NOT EXISTS Maintain(id INTEGER,name TEXT,sex TEXT,telephone TEXT,position TEXT,whichgoods TEXT,date text,isOrNo TEXT );";
            //↓管理员表: 1.id 2.用户名 3.密码
            string AdminsTable = "CREATE TABLE IF NOT EXISTS Admin(id INTEGER,name TEXT,key TEXT)";
            //↓最高管理员表: 1.id 2.用户名 3.密码
            string ExAdminsTable = "CREATE TABLE IF NOT EXISTS ExAdmin(id INTEGER,name TEXT,key TEXT)";
            //↓操作记录表 1.id 2.操作用户名 3.操作项 4.操作时间
            string OperationTable = "CREATE TABLE IF NOT EXISTS Operation(id INTEGER,name TEXT,project TEXT,time TEXT);";
            //↓总产品表：1.id 2.功能 3.种类 4.品牌 5.价格 6.名字 7.产品序列号 8.更改日期
            string AllProductTable = "CREATE TABLE IF NOT EXISTS AllProduct(id INTEGER,function TEXT,type TEXT,brand TEXT,price REAL,name TEXT,serialNumber TEXT);";
            //↓总顾客表：1.id 2.名字 3.性别 4.电话 5.住址 6.购买商品 7.订单号 8.更改日期
            string AllCustomerTable = "CREATE TABLE IF NOT EXISTS AllCustomer(id INTEGER,name TEXT,sex TEXT,telephone TEXT,position TEXT,whichgoods TEXT,orderNumber TEXT );";
            //↓总送货表：1.id 2.名字 3.性别 4.电话 5.住址 6.购买商品 7.送货日期 8.是否签收完成 9.订单号 10.送货员 11.更改日期
            string AllGoodsTable = "CREATE TABLE IF NOT EXISTS AllGoods(id INTEGER,name TEXT,sex TEXT,telephone TEXT,position TEXT,whichgoods TEXT,date TEXT,isOrNo TEXT,orderNumber TEXT,courier TEXT );";
            //↓总安装表:1.id 2.名字 3.性别 4.电话 5.住址 6.购买商品 7.安装日期 8.是否安装完成 9.订单号 10.安装员 11.更改日期
            string AllInstallTable = "CREATE TABLE IF NOT EXISTS AllInstall(id INTEGER,name TEXT,sex TEXT,telephone TEXT,position TEXT,whichgoods TEXT,date text, isOrNo TEXT,orderNumber TEXT,installMan TEXT );";
            //↓总维修表:1.id 2.名字 3.性别 4.电话 5.住址 6.购买商品 7.维修日期 8.是否维修完成 9.订单号 10.维修员 11.错误原因 12.更改日期
            string AllMaintainTable = "CREATE TABLE IF NOT EXISTS AllMaintain(id INTEGER,name TEXT,sex TEXT,telephone TEXT,position TEXT,whichgoods TEXT,date text,isOrNo TEXT,orderNumber TEXT,maintainMan TEXT,errorInfor TEXT );";
            //↓添加管理员
            string admin_01 = "INSERT INTO Admin(id,name,key)  VALUES(0, 'admin', 'admin')";
            string admin_02 = "INSERT INTO Admin(id,name,key) VALUES(1,'admin02','admin02')";
            //↓添加最高管理员
            string ExAdmin = "INSERT INTO ExAdmin(id,name,key) VALUES(0,'ExAdmin','ExAdmin')";
            SQLiteCommand com_EA = new SQLiteCommand(ExAdminsTable, con);
            com_EA.ExecuteNonQuery();
            string Find = "SELECT id FROM Admin";
            string Find_ex = "SELECT id FROM ExAdmin";
            int id_ex =Convert.ToInt32(FindData(Find_ex));
            if (id_ex != 0)
            {
                SQLiteCommand com_ea = new SQLiteCommand(ExAdmin, con);
                com_ea.ExecuteNonQuery();
            }
            List <int> id=new List<int>();
           id =GetNoRepeatList_01(Find);
            foreach (int i in id)
            {
                if (i == 0||i==1)
                {
                    break;
                }
                else
                {
                    SQLiteCommand com_ad_01 = new SQLiteCommand(admin_01, con);
                    com_ad_01.ExecuteNonQuery();
                    SQLiteCommand com_ad_02 = new SQLiteCommand(admin_02, con);
                    com_ad_02.ExecuteNonQuery();
                }
            }
            SQLiteCommand com_PT = new SQLiteCommand(ProductTable, con);
            com_PT.ExecuteNonQuery();
            SQLiteCommand com_CT = new SQLiteCommand(CustomerTable, con);
            com_CT.ExecuteNonQuery();
            SQLiteCommand com_GT = new SQLiteCommand(GoodsTable, con);
            com_GT.ExecuteNonQuery();
            SQLiteCommand com_IT = new SQLiteCommand(InstallTable, con);
            com_IT.ExecuteNonQuery();
            SQLiteCommand com_MT = new SQLiteCommand(MaintainTable, con);
            com_MT.ExecuteNonQuery();
            SQLiteCommand com_AT = new SQLiteCommand(AdminsTable, con);
            com_AT.ExecuteNonQuery();
            SQLiteCommand com_OT = new SQLiteCommand(OperationTable, con);
            com_OT.ExecuteNonQuery();
            SQLiteCommand com_APT = new SQLiteCommand(AllProductTable, con);
            com_APT.ExecuteNonQuery();
            SQLiteCommand com_ACT = new SQLiteCommand(AllCustomerTable, con);
            com_ACT.ExecuteNonQuery();
            SQLiteCommand com_AGT = new SQLiteCommand(AllGoodsTable, con);
            com_AGT.ExecuteNonQuery();
            SQLiteCommand com_AIT = new SQLiteCommand(AllInstallTable, con);
            com_AIT.ExecuteNonQuery();
            SQLiteCommand com_AMT = new SQLiteCommand(AllMaintainTable, con);
            com_AMT.ExecuteNonQuery();

         
         
            con.Close();
        }
        //↓运行指定的sql语句,并返回运行结果
        public bool RunSql(string sql)
        {
            bool flag = false;
            bool flag_01 = true;
            string path = @"G:\JiaDianGuanLi\Informations.db";
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path);
            con.Open();
            SQLiteCommand com = new SQLiteCommand();
            com.Connection = con;
            com.CommandText = sql;
            if (sql.Contains("SELECT"))
            {
                SQLiteDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    flag = true;
                    reader.Close();
                    con.Close();
                    break;
                }
                return flag;
            }
            else
            {
                try
                {
                    com.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
                catch
                {
                    flag_01 = false;
                }
            }
            con.Close();
            return flag;
        }
        //↓运行指定语句
        public void Run(string sql)
        {
            string path = @"G:\JiaDianGuanLi\Informations.db";
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path);
            con.Open();
            SQLiteCommand com = new SQLiteCommand();
            com.Connection = con;
            com.CommandText = sql;
            SQLiteDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                MessageBox.Show("Yes");
                reader.Close();
                com.ExecuteNonQuery();
            }
            con.Close();
        }
        //↓遍历指定表并返回此次的id值
        public int GetAll(string tableName)
        {
            List<int> thisList = new List<int>();
            string path = @"G:\JiaDianGuanLi\Informations.db";
            int Num=0;
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path);
            con.Open();
            string sql = "SELECT id FROM " + tableName;
            SQLiteCommand com = new SQLiteCommand(sql,con);
              SQLiteDataReader reader = com.ExecuteReader();
              while (reader.Read())
              {
                  thisList.Add(Convert.ToInt32(reader[0]));
                  foreach (int i in thisList)
                  {
                       if (Num==i)
                       Num += 1;
                   }
              }
              foreach (int i in thisList)
              {
                  if (Num == i)
                      Num += 1;
              }
              reader.Close();
            con.Close();
            return Num;
        }
        //↓遍历指定条件并返回DataTable，用于显示界面的条件检索
        public DataTable GetDataTable(string sql)
        {
            string path = @"G:\JiaDianGuanLi\Informations.db";
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path);
            con.Open();
            SQLiteCommand com = new SQLiteCommand();
            com.Connection = con;
            com.CommandText = sql;
            SQLiteDataAdapter da=new SQLiteDataAdapter();
            da.SelectCommand = com;
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        //↓遍历指定表的字符串列，并返回不重复的结果
        public List<string> GetNoRepeatList(string sql)
        {
            List<string> thisList = new List<string>();
            string path = @"G:\JiaDianGuanLi\Informations.db";
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path);
            con.Open();
            SQLiteCommand com = new SQLiteCommand();
            com.Connection = con;
            com.CommandText = sql;
            SQLiteDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                int num = 0;
                thisList.Add(reader[0].ToString());
                foreach (string i in thisList)
                {
                   if (String.Equals(i, reader[0].ToString()))
                    {
                        num += 1;
                        if (num > 1)
                        {
                            thisList.Remove(i);
                            break;
                        }
                    }
                }
            }
            reader.Close();
            con.Close();
            return thisList;
        }
        //↓遍历指定表的整型变量列，并返回不重复的结果
        public List<int> GetNoRepeatList_01(string sql)
        {
            List<int> thisList = new List<int>();
            string path = @"G:\JiaDianGuanLi\Informations.db";
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path);
            con.Open();
            SQLiteCommand com = new SQLiteCommand();
            com.Connection = con;
            com.CommandText = sql;
            SQLiteDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                int num = 0;
                thisList.Add(Convert.ToInt32(reader[0]));
                foreach (int i in thisList)
                {
                    if (String.Equals(i, reader[0].ToString()))
                    {
                        num += 1;
                        if (num > 1)
                        {
                            thisList.Remove(i);
                            break;
                        }
                    }
                }
            }
            reader.Close();
            con.Close();
            return thisList;
        }
        //↓得到操作项并添加进最高管理库 1.id 2.操作员姓名 3.操作项 4.操作时间
        public void GetData_Operation(int id, string name, string project, string time)
        {
            string path = @"G:\JiaDianGuanLi\Informations.db";
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path);
            con.Open();
            SQLiteCommand com = new SQLiteCommand();
            com.Connection = con;
            com.CommandText = "INSERT INTO Operation(id,name,project,time) VALUES (?,?,?,?)";
            com.Parameters.Add("id", DbType.Int32).Value = id;
            com.Parameters.Add("name", DbType.String).Value = name;
            com.Parameters.Add("project", DbType.String).Value = project;
            com.Parameters.Add("time", DbType.String).Value = time;
            com.ExecuteNonQuery();
            con.Close();
        }
        //↓得到管理员数据并添加进管理员库 1.id 2.管理员名 3.管理员密码
        public void GetData_Admin(int id, string name, string key)
        {
            string path = @"G:\JiaDianGuanLi\Informations.db";
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path);
            con.Open();
            SQLiteCommand com = new SQLiteCommand();
            com.Connection = con;
            com.CommandText = "INSERT INTO Admin(id,name,key) VALUES (?,?,?)";
            com.Parameters.Add("id", DbType.Int32).Value = id;
            com.Parameters.Add("name", DbType.String).Value = name;
            com.Parameters.Add("key", DbType.String).Value = key;
            com.ExecuteNonQuery();
            con.Close();
        }
        //↓得到产品数据并添加进数据库 1.id 2.功能 3.种类 4.品牌 5.价格 6.名字 7.产品序列号
        public bool GetData_Product(int id, string function, string type, string brand, float price, string name,string SN)
        {
            bool flag=true;
            string path = @"G:\JiaDianGuanLi\Informations.db";
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path);
            con.Open();
            SQLiteCommand com= new SQLiteCommand();
            com.Connection = con;
            com.CommandText = "SELECT * FROM Product WHERE serialNumber =" + "'" + SN + "'";
            SQLiteDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                flag=false;
                return flag;
            }
            reader.Close();
            com.CommandText = "INSERT INTO Product(id,function,type,brand,price,name,serialNumber ) VALUES (?,?,?,?,?,?,?)";
            com.Parameters.Add("id", DbType.Int32).Value = id;
            com.Parameters.Add("function", DbType.String).Value = function;
            com.Parameters.Add("type", DbType.String).Value = type;
            com.Parameters.Add("brand", DbType.String).Value = brand;
            com.Parameters.Add("price", DbType.Single).Value = price;
            com.Parameters.Add("name", DbType.String).Value = name;
            com.Parameters.Add("serialNumber ", DbType.String).Value = SN;
            com.ExecuteNonQuery();
            con.Close();
            return flag;
            }
        //↓得到顾客数据并添加进数据库 1.id 2.名字 3.性别 4.电话 5.住址 6.购买商品名 7.订单号
        public void GetData_Customer(int id, string name, string sex, string telephone, string position, string whichgoods, string orderNumber)
        {
            string path = @"G:\JiaDianGuanLi\Informations.db";
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path);
            con.Open();
            SQLiteCommand com = new SQLiteCommand();
            com.Connection = con;
            com.CommandText = "INSERT INTO Customer(id,name,sex,telephone,position,whichgoods,orderNumber ) VALUES (?,?,?,?,?,?,?)";
            com.Parameters.Add("id", DbType.Int32).Value = id;
            com.Parameters.Add("name", DbType.String).Value = name;
            com.Parameters.Add("sex", DbType.String).Value = sex;
            com.Parameters.Add("telephone", DbType.String).Value = telephone;
            com.Parameters.Add("position", DbType.String).Value = position;
            com.Parameters.Add("whichgoods", DbType.String).Value = whichgoods;
            com.Parameters.Add("orderNumber", DbType.String).Value = orderNumber;
            com.ExecuteNonQuery();
            con.Close();
        }
        //↓得到送货数据并添加进数据库 1.id 2.名字 3.性别 4.电话 5.住址 6.购买商品名 7.送货日期 8.是否签收完成 9.订单号 (10.送货员)
        public void GetData_Goods(int id, string name, string sex, string telephone, string position, string whichgoods, string date, string isOrNo,string orderNumber)
        {
            string path = @"G:\JiaDianGuanLi\Informations.db";
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path);
            con.Open();
            SQLiteCommand com = new SQLiteCommand();
            com.Connection = con;
            com.CommandText = "INSERT INTO Goods(id,name,sex,telephone,position,whichgoods,date,isOrNo,orderNumber ) VALUES (?,?,?,?,?,?,?,?,?)";
            com.Parameters.Add("id", DbType.Int32).Value = id;
            com.Parameters.Add("name", DbType.String).Value = name;
            com.Parameters.Add("sex", DbType.String).Value = sex;
            com.Parameters.Add("telephone", DbType.String).Value = telephone;
            com.Parameters.Add("position", DbType.String).Value = position;
            com.Parameters.Add("whichgoods", DbType.String).Value = whichgoods;
            com.Parameters.Add("date", DbType.String).Value = date;
            com.Parameters.Add("isOrNo", DbType.String).Value = isOrNo;
            com.Parameters.Add("orderNumber", DbType.String).Value = orderNumber;
            com.ExecuteNonQuery();
            con.Close();
        }
        //↓得到安装数据并添加进数据库 1.id 2.名字 3.性别 4.电话 5.住址 6.购买商品名 7.安装日期 8.是否安装完成 9.订单号 (10.安装员)
        public void GetData_Install(int id, string name, string sex, string telephone, string position, string whichgoods, string date, string isOrNo, string orderNumber)
        {
            string path = @"G:\JiaDianGuanLi\Informations.db";
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path);
            con.Open();
            SQLiteCommand com = new SQLiteCommand();
            com.Connection = con;
            com.CommandText = "INSERT INTO Install(id,name,sex,telephone,position,whichgoods,date,isOrNo,orderNumber ) VALUES (?,?,?,?,?,?,?,?,?)";
            com.Parameters.Add("id", DbType.Int32).Value = id;
            com.Parameters.Add("name", DbType.String).Value = name;
            com.Parameters.Add("sex", DbType.String).Value = sex;
            com.Parameters.Add("telephone", DbType.String).Value = telephone;
            com.Parameters.Add("position", DbType.String).Value = position;
            com.Parameters.Add("whichgoods", DbType.String).Value = whichgoods;
            com.Parameters.Add("date", DbType.String).Value = date;
            com.Parameters.Add("isOrNo", DbType.String).Value = isOrNo;
            com.Parameters.Add("orderNumber", DbType.String).Value = orderNumber;
            com.ExecuteNonQuery();
            con.Close();
        }
        //↓得到维修数据并添加进数据库 1.id 2.名字 3.性别 4.电话 5.住址 6.购买商品名 7.维修日期 8.是否维修完成 9.订单号 (10.维修员 11.错误原因)
        public void GetData_Maintain(int id, string name, string sex, string telephone, string position, string whichgoods, string date, string isOrNo, string orderNumber)
        {
            string path = @"G:\JiaDianGuanLi\Informations.db";
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path);
            con.Open();
            SQLiteCommand com = new SQLiteCommand();
            com.Connection = con;
            com.CommandText = "INSERT INTO Maintain(id,name,sex,telephone,position,whichgoods,date,isOrNo,orderNumber ) VALUES (?,?,?,?,?,?,?,?,?)";
            com.Parameters.Add("id", DbType.Int32).Value = id;
            com.Parameters.Add("name", DbType.String).Value = name;
            com.Parameters.Add("sex", DbType.String).Value = sex;
            com.Parameters.Add("telephone", DbType.String).Value = telephone;
            com.Parameters.Add("position", DbType.String).Value = position;
            com.Parameters.Add("whichgoods", DbType.String).Value = whichgoods;
            com.Parameters.Add("date", DbType.String).Value = date;
            com.Parameters.Add("isOrNo", DbType.String).Value = isOrNo;
            com.Parameters.Add("orderNumber", DbType.String).Value = orderNumber;
            com.ExecuteNonQuery();
            con.Close();
        }

        //*************************************************
        //*************************************************
        //*************************************************

        //↓得到所有产品数据并添加进数据库 1.id 2.功能 3.种类 4.品牌 5.价格 6.名字 7.产品序列号 8.更改日期
        public void GetData_AllProduct(int id, string function, string type, string brand, float price, string name, string SN,string changeDate)
        {
            bool flag = true;
            string path = @"G:\JiaDianGuanLi\Informations.db";
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path);
            con.Open();
            SQLiteCommand com = new SQLiteCommand();
            com.Connection = con;
            com.CommandText = "INSERT INTO AllProduct(id,function,type,brand,price,name,serialNumber,changeDate ) VALUES (?,?,?,?,?,?,?,?)";
            com.Parameters.Add("id", DbType.Int32).Value = id;
            com.Parameters.Add("function", DbType.String).Value = function;
            com.Parameters.Add("type", DbType.String).Value = type;
            com.Parameters.Add("brand", DbType.String).Value = brand;
            com.Parameters.Add("price", DbType.Single).Value = price;
            com.Parameters.Add("name", DbType.String).Value = name;
            com.Parameters.Add("serialNumber ", DbType.String).Value = SN;
            com.Parameters.Add("changeDate", DbType.String).Value = changeDate;
            com.ExecuteNonQuery();
            con.Close();
        }
        //↓得到所有顾客数据并添加进数据库 1.id 2.名字 3.性别 4.电话 5.住址 6.购买商品名 7.订单号 8.更改日期
        public void GetData_AllCustomer(int id, string name, string sex, string telephone, string position, string whichgoods, string orderNumber, string changeDate)
        {
            string path = @"G:\JiaDianGuanLi\Informations.db";
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path);
            con.Open();
            SQLiteCommand com = new SQLiteCommand();
            com.Connection = con;
            com.CommandText = "INSERT INTO AllCustomer(id,name,sex,telephone,position,whichgoods,orderNumber,changeDate ) VALUES (?,?,?,?,?,?,?,?)";
            com.Parameters.Add("id", DbType.Int32).Value = id;
            com.Parameters.Add("name", DbType.String).Value = name;
            com.Parameters.Add("sex", DbType.String).Value = sex;
            com.Parameters.Add("telephone", DbType.String).Value = telephone;
            com.Parameters.Add("position", DbType.String).Value = position;
            com.Parameters.Add("whichgoods", DbType.String).Value = whichgoods;
            com.Parameters.Add("orderNumber", DbType.String).Value = orderNumber;
            com.Parameters.Add("changeDate", DbType.String).Value = changeDate;
            com.ExecuteNonQuery();
            con.Close();
        }

        //↓得到所有送货数据并添加进数据库 1.id 2.名字 3.性别 4.电话 5.住址 6.购买商品名 7.送货日期 8.是否签收完成 9.订单号 (10.送货员) 10.更改日期
        public void GetData_AllGoods(int id, string name, string sex, string telephone, string position, string whichgoods, string date, string isOrNo, string orderNumber, string changeDate)
        {
            string path = @"G:\JiaDianGuanLi\Informations.db";
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path);
            con.Open();
            SQLiteCommand com = new SQLiteCommand();
            com.Connection = con;
            com.CommandText = "INSERT INTO AllGoods(id,name,sex,telephone,position,whichgoods,date,isOrNo,orderNumber,changeDate ) VALUES (?,?,?,?,?,?,?,?,?,?)";
            com.Parameters.Add("id", DbType.Int32).Value = id;
            com.Parameters.Add("name", DbType.String).Value = name;
            com.Parameters.Add("sex", DbType.String).Value = sex;
            com.Parameters.Add("telephone", DbType.String).Value = telephone;
            com.Parameters.Add("position", DbType.String).Value = position;
            com.Parameters.Add("whichgoods", DbType.String).Value = whichgoods;
            com.Parameters.Add("date", DbType.String).Value = date;
            com.Parameters.Add("isOrNo", DbType.String).Value = isOrNo;
            com.Parameters.Add("orderNumber", DbType.String).Value = orderNumber;
            com.Parameters.Add("changeDate", DbType.String).Value = changeDate;
            com.ExecuteNonQuery();
            con.Close();
        }

        //↓得到所有送货数据并添加进数据库 1.id 2.名字 3.性别 4.电话 5.住址 6.购买商品名 7.送货日期 8.是否签收完成 9.订单号 10.送货员 11.更改日期
        public void GetData_AllGoods(int id, string name, string sex, string telephone, string position, string whichgoods, string date, string isOrNo, string orderNumber,string courier, string changeDate)
        {
            string path = @"G:\JiaDianGuanLi\Informations.db";
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path);
            con.Open();
            SQLiteCommand com = new SQLiteCommand();
            com.Connection = con;
            com.CommandText = "INSERT INTO AllGoods(id,name,sex,telephone,position,whichgoods,date,isOrNo,orderNumber,courier,changeDate ) VALUES (?,?,?,?,?,?,?,?,?,?,?)";
            com.Parameters.Add("id", DbType.Int32).Value = id;
            com.Parameters.Add("name", DbType.String).Value = name;
            com.Parameters.Add("sex", DbType.String).Value = sex;
            com.Parameters.Add("telephone", DbType.String).Value = telephone;
            com.Parameters.Add("position", DbType.String).Value = position;
            com.Parameters.Add("whichgoods", DbType.String).Value = whichgoods;
            com.Parameters.Add("date", DbType.String).Value = date;
            com.Parameters.Add("isOrNo", DbType.String).Value = isOrNo;
            com.Parameters.Add("orderNumber", DbType.String).Value = orderNumber;
            com.Parameters.Add("courier", DbType.String).Value = courier;
            com.Parameters.Add("changeDate", DbType.String).Value = changeDate;
            com.ExecuteNonQuery();
            con.Close();
        }

        //↓得到所有安装数据并添加进数据库 1.id 2.名字 3.性别 4.电话 5.住址 6.购买商品名 7.安装日期 8.是否安装完成 9.订单号 (10.安装员) 10.更改日期
        public void GetData_AllInstall(int id, string name, string sex, string telephone, string position, string whichgoods, string date, string isOrNo, string orderNumber, string changeDate)
        {
            string path = @"G:\JiaDianGuanLi\Informations.db";
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path);
            con.Open();
            SQLiteCommand com = new SQLiteCommand();
            com.Connection = con;
            com.CommandText = "INSERT INTO AllInstall(id,name,sex,telephone,position,whichgoods,date,isOrNo,orderNumber,changeDate ) VALUES (?,?,?,?,?,?,?,?,?,?)";
            com.Parameters.Add("id", DbType.Int32).Value = id;
            com.Parameters.Add("name", DbType.String).Value = name;
            com.Parameters.Add("sex", DbType.String).Value = sex;
            com.Parameters.Add("telephone", DbType.String).Value = telephone;
            com.Parameters.Add("position", DbType.String).Value = position;
            com.Parameters.Add("whichgoods", DbType.String).Value = whichgoods;
            com.Parameters.Add("date", DbType.String).Value = date;
            com.Parameters.Add("isOrNo", DbType.String).Value = isOrNo;
            com.Parameters.Add("orderNumber", DbType.String).Value = orderNumber;
            com.Parameters.Add("changeDate", DbType.String).Value = changeDate;
            com.ExecuteNonQuery();
            con.Close();
        }
        //↓得到所有安装数据并添加进数据库 1.id 2.名字 3.性别 4.电话 5.住址 6.购买商品名 7.安装日期 8.是否安装完成 9.订单号 10.安装员 11.更改日期
        public void GetData_AllInstall(int id, string name, string sex, string telephone, string position, string whichgoods, string date, string isOrNo, string orderNumber,string installMan, string changeDate)
        {
            string path = @"G:\JiaDianGuanLi\Informations.db";
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path);
            con.Open();
            SQLiteCommand com = new SQLiteCommand();
            com.Connection = con;
            com.CommandText = "INSERT INTO AllInstall(id,name,sex,telephone,position,whichgoods,date,isOrNo,orderNumber,installMan,changeDate ) VALUES (?,?,?,?,?,?,?,?,?,?,?)";
            com.Parameters.Add("id", DbType.Int32).Value = id;
            com.Parameters.Add("name", DbType.String).Value = name;
            com.Parameters.Add("sex", DbType.String).Value = sex;
            com.Parameters.Add("telephone", DbType.String).Value = telephone;
            com.Parameters.Add("position", DbType.String).Value = position;
            com.Parameters.Add("whichgoods", DbType.String).Value = whichgoods;
            com.Parameters.Add("date", DbType.String).Value = date;
            com.Parameters.Add("isOrNo", DbType.String).Value = isOrNo;
            com.Parameters.Add("orderNumber", DbType.String).Value = orderNumber;
            com.Parameters.Add("installMan", DbType.String).Value = installMan;
            com.Parameters.Add("changeDate", DbType.String).Value = changeDate;
            com.ExecuteNonQuery();
            con.Close();
        }
      

        //↓得到所有维修数据并添加进数据库 1.id 2.名字 3.性别 4.电话 5.住址 6.购买商品名 7.维修日期 8.是否维修完成 9.订单号 (10.维修员 11.错误原因) 10.更改日期
        public void GetData_AllMaintain(int id, string name, string sex, string telephone, string position, string whichgoods, string date, string isOrNo, string orderNumber, string changeDate)
        {
            string path = @"G:\JiaDianGuanLi\Informations.db";
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path);
            con.Open();
            SQLiteCommand com = new SQLiteCommand();
            com.Connection = con;
            com.CommandText = "INSERT INTO AllMaintain(id,name,sex,telephone,position,whichgoods,date,isOrNo,orderNumber,changeDate ) VALUES (?,?,?,?,?,?,?,?,?,?)";
            com.Parameters.Add("id", DbType.Int32).Value = id;
            com.Parameters.Add("name", DbType.String).Value = name;
            com.Parameters.Add("sex", DbType.String).Value = sex;
            com.Parameters.Add("telephone", DbType.String).Value = telephone;
            com.Parameters.Add("position", DbType.String).Value = position;
            com.Parameters.Add("whichgoods", DbType.String).Value = whichgoods;
            com.Parameters.Add("date", DbType.String).Value = date;
            com.Parameters.Add("isOrNo", DbType.String).Value = isOrNo;
            com.Parameters.Add("orderNumber", DbType.String).Value = orderNumber;
            com.Parameters.Add("changeDate", DbType.String).Value = changeDate;
            com.ExecuteNonQuery();
            con.Close();
        }
        //↓得到所有维修数据并添加进数据库 1.id 2.名字 3.性别 4.电话 5.住址 6.购买商品名 7.维修日期 8.是否维修完成 9.订单号 10.维修员 11.错误原因 12.更改日期
        public void GetData_AllMaintain(int id, string name, string sex, string telephone, string position, string whichgoods, string date, string isOrNo, string orderNumber,string maintainMan,string errorInfor, string changeDate)
        {
            string path = @"G:\JiaDianGuanLi\Informations.db";
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path);
            con.Open();
            SQLiteCommand com = new SQLiteCommand();
            com.Connection = con;
            com.CommandText = "INSERT INTO AllMaintain(id,name,sex,telephone,position,whichgoods,date,isOrNo,orderNumber,maintainMan,errorInfor,changeDate ) VALUES (?,?,?,?,?,?,?,?,?,?,?,?)";
            com.Parameters.Add("id", DbType.Int32).Value = id;
            com.Parameters.Add("name", DbType.String).Value = name;
            com.Parameters.Add("sex", DbType.String).Value = sex;
            com.Parameters.Add("telephone", DbType.String).Value = telephone;
            com.Parameters.Add("position", DbType.String).Value = position;
            com.Parameters.Add("whichgoods", DbType.String).Value = whichgoods;
            com.Parameters.Add("date", DbType.String).Value = date;
            com.Parameters.Add("isOrNo", DbType.String).Value = isOrNo;
            com.Parameters.Add("orderNumber", DbType.String).Value = orderNumber;
            com.Parameters.Add("maintainMan", DbType.String).Value = maintainMan;
            com.Parameters.Add("errorInfor", DbType.String).Value = errorInfor;
            com.Parameters.Add("changeDate", DbType.String).Value = changeDate;
            com.ExecuteNonQuery();
            con.Close();
        }

        //↓查询产品数据并返回数据 1.id 2.功能 3.种类 4.品牌 5.价格 6.名字
        public void FindData_Product(string sql)
        {
            string path = @"G:\JiaDianGuanLi\Informations.db";
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path);
            con.Open();
            SQLiteCommand com_PT = new SQLiteCommand(sql, con);
            com_PT.ExecuteNonQuery();
            con.Close();
        }
        //↓查询顾客数据并返回数据  1.id 2.名字 3.性别 4.电话 5.住址 6.购买商品
        public void FindData_Customer(string sql)
        {
            string path = @"G:\JiaDianGuanLi\Informations.db";
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path);
            con.Open();
            SQLiteCommand com_CT = new SQLiteCommand(sql, con);
            com_CT.ExecuteNonQuery();
            con.Close();
        }
        //↓查询并返回单个数据  1.id 2.名字 3.性别 4.电话 5.住址 6.购买商品 7.送货日期 8.是否签收完成 9.订单号
        public object FindData(string sql)
        {
            object This=null;
            string path = @"G:\JiaDianGuanLi\Informations.db";
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path);
            con.Open();
            SQLiteCommand com = new SQLiteCommand(sql, con);
            SQLiteDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                This = reader[0];
                reader.Close();
                break;
            }
            con.Close();
            return This;
        }
    }
}
