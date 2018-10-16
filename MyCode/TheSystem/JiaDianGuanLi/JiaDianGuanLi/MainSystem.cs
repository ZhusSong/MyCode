using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SQLite;


namespace JiaDianGuanLi
{
    static class MainSystem
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
     
        public static bool IsAdmin = false;          //该次是否已有管理员登录
        public static bool IsExAdmin = false;
        public static string thisAdmin;
         [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ChooseSever());
            DataManager MS_DM = new DataManager();
            MS_DM.CreateDataBase();
        }
   /*    public  void ProductManager()    //产品信息管理
       {
       }
       public  void CustomerManager()   //顾客信息管理
    /   {
        }
       public  void GoodsManager()   //送货信息管理
        {
        }
       public  void InstallManager()   //顾客信息管理
        {
        }
       public  void MaintainManager()   //维修信息管理
        {
        }*/
    }
    
}
