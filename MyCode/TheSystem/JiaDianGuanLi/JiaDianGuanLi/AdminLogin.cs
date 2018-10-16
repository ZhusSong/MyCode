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
    public partial class AdminLogin : Form
    {
        DataManager AD_DM = new DataManager();
        ProductManager AD_PM;
        CustomerManager AD_CM;
        GoodsManager AD_GM;
        InstallManager AD_IM;
        MaintainManager AD_MM;
        ChooseSever AD_CS;
        AllOfAll AD_AOA;
        public bool CanShow=false;
        private string WhichScript;   //需要进行管理员登录判断的界面名
        private string WhichScreen;
        public AdminLogin()
        {
            InitializeComponent();
        }
        //↓得到需要进行登录的界面、界面脚本即控件名，按调用脚本进行重写
        public void GetName(ProductManager PM,string whichScript,string whichScreen)
        {
            textBox1.Text = null;
            textBox2.Text = null;
            AD_PM = PM;
            WhichScript = whichScript;
            WhichScreen = whichScreen;
        }
        public void GetName(CustomerManager CM, string whichScript, string whichScreen)
        {
            textBox1.Text = null;
            textBox2.Text = null;
            AD_CM = CM;
            WhichScript = whichScript;
            WhichScreen = whichScreen;
        }
        public void GetName(GoodsManager GM, string whichScript, string whichScreen)
        {
            textBox1.Text = null;
            textBox2.Text = null;
            AD_GM = GM;
            WhichScript = whichScript;
            WhichScreen = whichScreen;
        }
        public void GetName(InstallManager IM, string whichScript, string whichScreen)
        {
            textBox1.Text = null;
            textBox2.Text = null;
            AD_IM = IM;
            WhichScript = whichScript;
            WhichScreen = whichScreen;
        }
        public void GetName(MaintainManager MM, string whichScript, string whichScreen)
        {
            textBox1.Text = null;
            textBox2.Text = null;
            AD_MM = MM;
            WhichScript = whichScript;
            WhichScreen = whichScreen;
        }
        public void GetName(ChooseSever CS, string whichScript, string whichScreen)
        {
            textBox1.Text = null;
            textBox2.Text = null;
            AD_CS = CS;
            WhichScript = whichScript;
            WhichScreen = whichScreen;
        }
        //↓点击登录，进行用户名和密码的判断
        private void button1_Click(object sender, EventArgs e)
        {
            if (WhichScript == "ChooseSever")
            {
                string sql = "SELECT * FROM ExAdmin WHERE name = " + "'" + textBox1.Text + "'";
                bool flag = AD_DM.RunSql(sql);
                if (flag)
                {
                    string sql_01 = "SELECT * FROM ExAdmin WHERE name = " + "'" + textBox1.Text + "'AND key LIKE " + "'" + textBox2.Text + "'";
                    bool flag_01 = AD_DM.RunSql(sql_01);
                    if (flag_01)
                    {
                        MessageBox.Show("登录成功!");
                        MainSystem.thisAdmin = textBox1.Text;
                        this.Visible = false;
                        ChooseSever.GetResult(AD_CS,WhichScreen);
                 
                    }
                    else
                        MessageBox.Show("密码错误!");
                }
                else
                {
                    MessageBox.Show("用户名错误!");
                }
            }
            else
            {
                string sql = "SELECT * FROM Admin WHERE name = " + "'" + textBox1.Text + "'";
                bool flag = AD_DM.RunSql(sql);
                if (flag)
                {
                    string sql_01 = "SELECT * FROM Admin WHERE name = " + "'" + textBox1.Text + "'AND key LIKE " + "'" + textBox2.Text + "'";
                    bool flag_01 = AD_DM.RunSql(sql_01);
                    if (flag_01)
                    {
                        MessageBox.Show("登录成功!");
                        MainSystem.thisAdmin = textBox1.Text;
                        AD_AOA = new AllOfAll();
                        int num=AD_AOA.GetProductsNumber();
                        AD_DM.GetData_Operation(num, textBox1.Text, "管理员:" + textBox1.Text + "登录", DateTime.Now.ToString());
                        if (WhichScript == "ProductManager")
                        {
                            ProductManager.GetResult(AD_PM, WhichScreen);
                            this.Visible = false;
                        }
                        if (WhichScript == "CustomerManager")
                        {
                            CustomerManager.GetResult_CM(AD_CM, WhichScreen);
                            this.Visible = false;
                        }
                        if (WhichScript == "GoodsManager")
                        {
                            GoodsManager.GetResult_GM(AD_GM, WhichScreen);
                            this.Visible = false;
                        }
                        if (WhichScript == "InstallManager")
                        {
                            InstallManager.GetResult_IM(AD_IM, WhichScreen);
                            this.Visible = false;
                        }
                        if (WhichScript == "MaintainManager")
                        {
                            MaintainManager.GetResult_MM(AD_MM, WhichScreen);
                            this.Visible = false;
                        }
                    }
                    else
                        MessageBox.Show("密码错误!");
                }
                else
                {
                    MessageBox.Show("用户名错误!");
                }
            }
        }

        private void AdminLogin_Load(object sender, EventArgs e)
        {

        }
    }
    }
