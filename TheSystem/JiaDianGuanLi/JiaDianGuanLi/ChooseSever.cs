using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JiaDianGuanLi
{
    public partial class ChooseSever : Form
    {
        ProductManager CS_PM ;  //调用窗口 ProductManager（产品管理）
        CustomerManager CS_CM;  //调用窗口  CustomerManager(顾客管理）
        GoodsManager CS_GM;     //调用窗口   GoodsManager(送货管理）
        InstallManager CS_IM;   //调用窗口  InstallManager(安装管理）
        MaintainManager CS_MM;  //调用窗口  MaintainManager(维修管理）
      public AllOfAll CS_AOA;        //调用窗口  AllOfAll(统一管理)
        AdminLogin CS_AL=new AdminLogin();       //调用窗口 AdminLogin(管理员登录)
        public ChooseSever()
        {
            InitializeComponent();
        }
        //↓显示窗口 ProductManager（产品管理）
        private void button1_Click(object sender, EventArgs e)
        {
            CS_PM = new ProductManager();
            CS_PM.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
        //↓显示窗口  CustomerManager(顾客管理）
        private void button3_Click(object sender, EventArgs e)
        {
            CS_CM = new CustomerManager();
            CS_CM.ShowDialog();
        }
        //↓显示窗口   GoodsManager(送货管理）
        private void button2_Click(object sender, EventArgs e)
        {
            CS_GM = new GoodsManager();
            CS_GM.ShowDialog();
        }
        //↓显示窗口  InstallManager(安装管理）
        private void button4_Click(object sender, EventArgs e)
        {
            CS_IM = new InstallManager();
            CS_IM.ShowDialog();
        }
        //↓显示窗口  MaintainManager(维修管理）
        private void button5_Click(object sender, EventArgs e)
        {
            CS_MM = new MaintainManager();
            CS_MM.ShowDialog();
        }
        //↓显示窗口  AllOfAll(统一管理)
        private void button6_Click(object sender, EventArgs e)
        {
            if (!MainSystem.IsExAdmin)
            {
                CS_AL.GetName(this, "ChooseSever", "Show");
                CS_AL.ShowDialog();
                //   GetResult(this,"change");
            }
            else
            {
                GetResult(this, "change");
            }
        }
        public static void GetResult(ChooseSever CS,string Ex)
        {
            MainSystem.IsExAdmin = true;
            CS.CS_AOA = new AllOfAll();
            CS.CS_AOA.ShowDialog();
        }
    }
}
