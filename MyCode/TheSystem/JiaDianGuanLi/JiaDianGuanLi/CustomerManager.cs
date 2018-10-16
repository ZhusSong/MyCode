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
    public partial class CustomerManager : Form
    {
        private int Num;                                           //id数
        private AdminLogin CM_AL;
        DataManager CM_DM;
        private InstallManager CM_IM;
        private MaintainManager CM_MM;
        private GoodsManager CM_GM;
        private AllOfAll CM_AOA;
        private DataTable DT = new DataTable();
        private bool IsHave = false;                                //判断是否查询到数据
        private int id = 0;                                         //顾客id，用于更改后的数据库更新
        public static bool HasLogin_CM = false;                               //该次是否已有管理员登录
        public CustomerManager()
        {
            InitializeComponent();
            add.Hide();
            change.Hide();
            delete.Hide();
            CM_DM = new DataManager();
            CM_AL = new AdminLogin();
            CM_IM = new InstallManager();
            CM_MM=new MaintainManager();
            CM_GM=new GoodsManager();
            CM_AOA = new AllOfAll();
            UpdateDataView_CM("SELECT * FROM Customer");                 //进行显示界面的初始化    
        }
        public void UpdateDataView_CM(string sql)
        {
            dataGridView1.DataSource = null;
            DT = CM_DM.GetDataTable(sql);
            this.dataGridView1.DataSource = DT;
            this.dataGridView1.Columns[0].HeaderText = "序号";
            this.dataGridView1.Columns[1].HeaderText = "姓名";
            this.dataGridView1.Columns[2].HeaderText = "性别";
            this.dataGridView1.Columns[3].HeaderText = "电话";
            this.dataGridView1.Columns[4].HeaderText = "住址";
            this.dataGridView1.Columns[5].HeaderText = "购买商品名";
            this.dataGridView1.Columns[6].HeaderText = "订单号";

            this.dataGridView1.Columns[0].Width = 40;
            this.dataGridView1.Columns[1].Width = 60;
            this.dataGridView1.Columns[2].Width = 60;
            this.dataGridView1.Columns[3].Width =100;
            this.dataGridView1.Columns[4].Width = 300;
            this.dataGridView1.Columns[5].Width = 400;
            this.dataGridView1.Columns[6].Width = 120;
        }
        //↓得到所有顾客数目
        private void GetProductsNumber()
        {
            Num = CM_DM.GetAll("Customer");
        }
        //↓显示界面_按订单号查找
        private void button5_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Customer WHERE orderNumber LIKE " + "'%" + textBox1.Text + "%'";
            UpdateDataView_CM(sql);
        }

        //↓显示界面_按姓名查找
        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Customer WHERE name LIKE " + "'%" + textBox8.Text + "%'";
            UpdateDataView_CM(sql);
        }
        //↓显示界面_按性别检索
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "全部")
            {
                string sql = "SELECT * FROM Customer WHERE sex = " + "'" + comboBox1.Text + "'";
                UpdateDataView_CM(sql);
            }
            else
            {
                string sql = "SELECT * FROM Customer";
                UpdateDataView_CM(sql);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        //↓得到登录结果的返回值
        public static void GetResult_CM(CustomerManager CM,string which)
        {
            MainSystem.IsAdmin = true;
            if (which == "add")
            {
                CM.add.Visible = true;
                CM.change.Hide();
                CM.check.Hide();
                CM.delete.Hide();
            }
            else if (which == "change")
            {
               CM.change.Visible = true;
               CM.add.Hide();
               CM.delete.Hide();
                CM.check.Hide();
            }
            else if (which == "delete")
            {
                CM.delete.Visible = true;
                CM.change.Hide();
                CM.add.Hide();
                CM.check.Hide();
            }
        }
        //↓主界面_添加信息按钮
        private void add_but_Click(object sender, EventArgs e)
        {
            if (MainSystem.IsAdmin || MainSystem.IsExAdmin)
            {
                GetResult_CM(this, "add");
            
            }
            else
            {
                CM_AL.GetName(this, "CustomerManager", "add");
                CM_AL.ShowDialog();
            }
        }
        //↓主界面_删除信息按钮
        private void button7_Click_1(object sender, EventArgs e)
        {
            if (MainSystem.IsAdmin || MainSystem.IsExAdmin)
            {
                GetResult_CM(this, "delete");
            
            }
            else
            {
                CM_AL.GetName(this, "CustomerManager", "delete");
                CM_AL.ShowDialog();
            }
        }
        //↓主界面_查看信息按钮
        private void check_but_Click(object sender, EventArgs e)
        {
            change.Hide();
            add.Hide();
            delete.Hide();
            check.Visible = true;
        }
        //↓主界面_更改信息按钮
        private void change_but_Click(object sender, EventArgs e)
        {
            if (MainSystem.IsAdmin || MainSystem.IsExAdmin)
            {
                GetResult_CM(this, "change");
              
            }
            else
            {
                CM_AL.GetName(this, "CustomerManager", "change");
                CM_AL.ShowDialog();
             
            }
        }
        //↓添加界面_性别检索
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox3.Text = comboBox5.Text;
        }
        //↓添加页面_确认添加
        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 0)
            {
                MessageBox.Show("添加失败，姓名不能为空");
            }
            else if (textBox3.Text.Length == 0)
            {
                MessageBox.Show("添加失败，性别分类不能为空");
            }
            else if (textBox4.Text.Length == 0)
            {
                MessageBox.Show("添加失败，电话不能为空");
            }
            else if (textBox5.Text.Length == 0)
            {
                MessageBox.Show("添加失败，住址不能为空");
            }
            else if (textBox6.Text.Length == 0)
            {
                MessageBox.Show("添加失败，商品名不能为空");
            }

            else
            {
                string sql = "SELECT name FROM Product";
                List<string> Name = CM_DM.GetNoRepeatList(sql);
                bool Have = false;
                foreach (string i in Name)
                {
                    if (i.Contains(textBox6.Text))
                    {
                        Have = true;
                        break;
                    }
                }
                if (Have)
                {
                    GetProductsNumber();
                    CM_DM.GetData_Customer(Num, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text);

                    int num = CM_AOA.GetProductsNumber();                     //向操作记录库中添加信息
                    CM_DM.GetData_Operation(num, MainSystem.thisAdmin, "管理员:" + MainSystem.thisAdmin + "添加了订单号为 " + textBox7.Text + "的顾客信息", DateTime.Now.ToString());

                    int num_01 = CM_AOA.GetAllCustomerNumber();              //向所有顾客信息库中添加信息
                    CM_DM.GetData_AllCustomer(num_01, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text,DateTime.Now.ToString());
                    AddAll();
                    MessageBox.Show("创建成功！");
                    textBox2.Text = null;
                    textBox3.Text = null;
                    textBox4.Text = null;
                    textBox5.Text = null;
                    textBox6.Text = null;
                    textBox7.Text = null;
                  
                    string sql_01 = "SELECT * FROM Customer";
                    UpdateDataView_CM(sql_01);
                }
                else
                    MessageBox.Show("创建失败！商品名不存在!");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
        //↓添加信息界面_生成订单号
            bool getOrder = false;
            string j = null;
            while (!getOrder)
            {
                Random ran = new Random(); ;
                int i = ran.Next(0, 10000000);
                j = i.ToString();
                j = j.PadLeft(10, '0');
                string sql = "SELECT orderNumber FROM Customer";
                List<string> order = CM_DM.GetNoRepeatList(sql);
                foreach (string m in order)
                {
                    if (j != m)
                    {
                        getOrder = true;
                    }
                    else if (j == m)
                    {
                        getOrder = false;
                        break;
                    }
                }
            }
            textBox7.Text = j;
        }
        //↓添加顾客信息成功后，自动添加送货、维修、安装等基本信息，同时向所有信息库中添加相应信息
        //  基本信息包括:1.id 2.名字 3.性别 4.电话 5.住址 6.购买商品名 7.订单号
        public void AddAll()
        {
            int i_1=CM_IM.GetProductsNumber();
            int i_2=CM_MM.GetProductsNumber();
            int i_3=CM_GM.GetProductsNumber();
            CM_DM.GetData_Install(i_1, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, null, "否", textBox7.Text);
            CM_DM.GetData_Maintain(i_2, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, null, "否", textBox7.Text);
            CM_DM.GetData_Goods(i_3, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, null, "否", textBox7.Text);

            int num_1 = CM_AOA.GetAllInstallNumber();
            int num_2 = CM_AOA.GetAllMaintainNumber();
            int num_3 = CM_AOA.GetAllGoodsNumber();
            CM_DM.GetData_AllInstall(num_1, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, null, "否", textBox7.Text, DateTime.Now.ToString());
            CM_DM.GetData_AllMaintain(num_2, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, null, "否", textBox7.Text, DateTime.Now.ToString());
            CM_DM.GetData_AllGoods(num_3, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, null, "否", textBox7.Text, DateTime.Now.ToString());
        }
  
        //↓更改信息界面_查询
        private void button3_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Customer WHERE name LIKE " + "'%" + textBox10.Text + "%' AND telephone LIKE " + "'%" + textBox9.Text + "%'" + " AND  orderNumber LIKE " + "'%" + textBox11.Text + "%'";
            dataGridView2.DataSource = null;
            DT = CM_DM.GetDataTable(sql);
            this.dataGridView2.DataSource = DT;
            this.dataGridView2.Columns[0].Visible = false;
            this.dataGridView2.Columns[1].HeaderText = "姓名";
            this.dataGridView2.Columns[2].HeaderText = "性别";
            this.dataGridView2.Columns[3].HeaderText = "电话";
            this.dataGridView2.Columns[4].HeaderText = "住址";
            this.dataGridView2.Columns[5].HeaderText = "购买商品名";
            this.dataGridView2.Columns[6].HeaderText = "订单号";
            this.dataGridView2.Columns[6].ReadOnly = true;

            this.dataGridView2.Columns[0].Width = 40;
            this.dataGridView2.Columns[1].Width = 60;
            this.dataGridView2.Columns[2].Width = 60;
            this.dataGridView2.Columns[3].Width = 100;
            this.dataGridView2.Columns[4].Width = 300;
            this.dataGridView2.Columns[5].Width = 400;
            this.dataGridView2.Columns[6].Width = 120;
        }
     
        //↓更改信息界面_确认更改
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确定更改吗?", "确认", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    if (dataGridView2.Rows[i].Cells[0] != null)
                    {
                        string sql = "UPDATE Customer SET name = " + "'" + dataGridView2.Rows[i].Cells[1].Value.ToString() + "', sex = '" + dataGridView2.Rows[i].Cells[2].Value.ToString() + "',telephone =" + "'" + dataGridView2.Rows[i].Cells[3].Value.ToString() + "',position = '" + dataGridView2.Rows[i].Cells[4].Value.ToString() + "' ,whichgoods = '" + dataGridView2.Rows[i].Cells[5].Value.ToString() + "' ,orderNumber = '" + dataGridView2.Rows[i].Cells[6].Value.ToString() + "' WHERE id =" + dataGridView2.Rows[i].Cells["id"].Value.ToString();
                        int num = CM_AOA.GetProductsNumber();
                        CM_DM.GetData_Operation(num, MainSystem.thisAdmin, "管理员:" + MainSystem.thisAdmin + "更改了订单号为 " + dataGridView2.Rows[i].Cells[6].Value.ToString() + "的顾客信息", DateTime.Now.ToString());
              
                        CM_DM.RunSql(sql);
                        string sql_Find_IM = "SELECT * FROM Install WHERE orderNumber = " + "'" + dataGridView2.Rows[i].Cells[6].Value.ToString()+"'";
                        string sql_Find_MM = "SELECT * FROM Maintain WHERE orderNumber = " + "'" + dataGridView2.Rows[i].Cells[6].Value.ToString()+"'";
                        string sql_Find_GM = "SELECT * FROM Goods WHERE orderNumber = " + "'" + dataGridView2.Rows[i].Cells[6].Value.ToString()+"'";

                        bool flag_IM = CM_DM.RunSql(sql_Find_IM);
                        bool flag_MM = CM_DM.RunSql(sql_Find_MM);
                        bool flag_GM = CM_DM.RunSql(sql_Find_GM);
                        if (!flag_IM)
                        {
                            int i_1 = CM_IM.GetProductsNumber();
                            CM_DM.GetData_Install(i_1, dataGridView2.Rows[i].Cells[1].Value.ToString(), dataGridView2.Rows[i].Cells[2].Value.ToString(), dataGridView2.Rows[i].Cells[3].Value.ToString(), dataGridView2.Rows[i].Cells[4].Value.ToString(), dataGridView2.Rows[i].Cells[5].Value.ToString(), null, "否", dataGridView2.Rows[i].Cells[6].Value.ToString());
                        }
                        if (!flag_MM)
                        {
                            int i_1 = CM_MM.GetProductsNumber();
                            CM_DM.GetData_Maintain(i_1, dataGridView2.Rows[i].Cells[1].Value.ToString(), dataGridView2.Rows[i].Cells[2].Value.ToString(), dataGridView2.Rows[i].Cells[3].Value.ToString(), dataGridView2.Rows[i].Cells[4].Value.ToString(), dataGridView2.Rows[i].Cells[5].Value.ToString(), null, "否", dataGridView2.Rows[i].Cells[6].Value.ToString());
                      
                        }
                        if (!flag_GM)
                        {
                            int i_1 = CM_GM.GetProductsNumber();
                            CM_DM.GetData_Goods(i_1, dataGridView2.Rows[i].Cells[1].Value.ToString(), dataGridView2.Rows[i].Cells[2].Value.ToString(), dataGridView2.Rows[i].Cells[3].Value.ToString(), dataGridView2.Rows[i].Cells[4].Value.ToString(), dataGridView2.Rows[i].Cells[5].Value.ToString(), null, "否", dataGridView2.Rows[i].Cells[6].Value.ToString());
                      
                        }
                        string sql_IM = "UPDATE Install SET name = " + "'" + dataGridView2.Rows[i].Cells[1].Value.ToString() + "', sex = '" + dataGridView2.Rows[i].Cells[2].Value.ToString() + "',telephone =" + "'" + dataGridView2.Rows[i].Cells[3].Value.ToString() + "',position = '" + dataGridView2.Rows[i].Cells[4].Value + "' ,whichgoods = '" + dataGridView2.Rows[i].Cells[5].Value.ToString() + "' ,isOrNo = '否'  WHERE orderNumber = '" + dataGridView2.Rows[i].Cells[6].Value.ToString() + "'";
                        string sql_MM = "UPDATE Maintain SET name = " + "'" + dataGridView2.Rows[i].Cells[1].Value.ToString() + "', sex = '" + dataGridView2.Rows[i].Cells[2].Value.ToString() + "',telephone =" + "'" + dataGridView2.Rows[i].Cells[3].Value.ToString() + "',position = '" + dataGridView2.Rows[i].Cells[4].Value + "' ,whichgoods = '" + dataGridView2.Rows[i].Cells[5].Value.ToString() + "' ,isOrNo = '否' WHERE orderNumber = '" + dataGridView2.Rows[i].Cells[6].Value.ToString() + "'";
                        string sql_GM = "UPDATE Goods SET name = " + "'" + dataGridView2.Rows[i].Cells[1].Value.ToString() + "', sex = '" + dataGridView2.Rows[i].Cells[2].Value.ToString() + "',telephone =" + "'" + dataGridView2.Rows[i].Cells[3].Value.ToString() + "',position = '" + dataGridView2.Rows[i].Cells[4].Value + "' ,whichgoods = '" + dataGridView2.Rows[i].Cells[5].Value.ToString() + "' ,isOrNo = '否' WHERE orderNumber = '" + dataGridView2.Rows[i].Cells[6].Value.ToString() + "'";
                        CM_DM.RunSql(sql_IM);
                        CM_DM.RunSql(sql_MM);
                        CM_DM.RunSql(sql_GM);

                        int num_1 = CM_AOA.GetAllInstallNumber();
                        int num_2 = CM_AOA.GetAllMaintainNumber();
                        int num_3 = CM_AOA.GetAllGoodsNumber();
                        CM_DM.GetData_AllInstall(num_1, dataGridView2.Rows[i].Cells[1].Value.ToString(), dataGridView2.Rows[i].Cells[2].Value.ToString(), dataGridView2.Rows[i].Cells[3].Value.ToString(), dataGridView2.Rows[i].Cells[4].Value.ToString(), dataGridView2.Rows[i].Cells[5].Value.ToString(), null, "否", dataGridView2.Rows[i].Cells[6].Value.ToString(), DateTime.Now.ToString());
                        CM_DM.GetData_AllMaintain(num_2, dataGridView2.Rows[i].Cells[1].Value.ToString(), dataGridView2.Rows[i].Cells[2].Value.ToString(), dataGridView2.Rows[i].Cells[3].Value.ToString(), dataGridView2.Rows[i].Cells[4].Value.ToString(), dataGridView2.Rows[i].Cells[5].Value.ToString(), null, "否", dataGridView2.Rows[i].Cells[6].Value.ToString(), DateTime.Now.ToString());
                        CM_DM.GetData_AllGoods(num_3, dataGridView2.Rows[i].Cells[1].Value.ToString(), dataGridView2.Rows[i].Cells[2].Value.ToString(), dataGridView2.Rows[i].Cells[3].Value.ToString(), dataGridView2.Rows[i].Cells[4].Value.ToString(), dataGridView2.Rows[i].Cells[5].Value.ToString(), null, "否", dataGridView2.Rows[i].Cells[6].Value.ToString(), DateTime.Now.ToString());

                    }
                }
                MessageBox.Show("更改成功！");
                dataGridView2.DataSource = null;
                UpdateDataView_CM("SELECT * FROM Customer");
            }
        }
        //↓更改界面_删除
        private void button7_Click(object sender, EventArgs e)
        {

        }
    
        //↓删除界面_查询
        private void button10_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Customer WHERE orderNumber =" + "'" +textBox12.Text+ "'";
            bool flag = CM_DM.RunSql(sql);
            if (flag)
            {
                IsHave = true;
                dataGridView3.DataSource = null;
                DT = CM_DM.GetDataTable(sql);
                this.dataGridView3.DataSource = DT;
                id = Convert.ToInt32(dataGridView3.Rows[0].Cells["id"].Value);
                this.dataGridView3.Columns[0].Visible = false;
                this.dataGridView3.Columns[1].HeaderText = "姓名";
                this.dataGridView3.Columns[2].HeaderText = "性别";
                this.dataGridView3.Columns[3].HeaderText = "电话";
                this.dataGridView3.Columns[4].HeaderText = "住址";
                this.dataGridView3.Columns[5].HeaderText = "购买商品名";
                this.dataGridView3.Columns[6].HeaderText = "订单号";

                this.dataGridView3.Columns[0].Width = 40;
                this.dataGridView3.Columns[1].Width = 60;
                this.dataGridView3.Columns[2].Width = 60;
                this.dataGridView3.Columns[3].Width = 100;
                this.dataGridView3.Columns[4].Width = 300;
                this.dataGridView3.Columns[5].Width = 400;
                this.dataGridView3.Columns[6].Width = 120;
            }
            else
            {
                MessageBox.Show("不存在该订单号！");
            }
        }
        //↓删除界面_删除
        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox12.Text.Length == 0)
            {
                MessageBox.Show("订单号不可为空！");
            }
            else if (!IsHave)
            {
                MessageBox.Show("不存在该订单号！");
            }
            else
            {
                string sql_01 = "DELETE FROM Customer WHERE orderNumber = " + "'" + textBox12.Text + "'";
                bool flag = CM_DM.RunSql(sql_01);
                if (flag)
                {
                    int num = CM_AOA.GetProductsNumber();
                    CM_DM.GetData_Operation(num, MainSystem.thisAdmin, "管理员:" + MainSystem.thisAdmin + "删除了订单号为 " + textBox12.Text + "的顾客信息", DateTime.Now.ToString());
              
                    MessageBox.Show("删除成功！");
                    dataGridView3.DataSource = null;
                    UpdateDataView_CM("SELECT * FROM Customer");
                    string sql_02="DELETE FROM Goods WHERE orderNumber = " + "'" + textBox12.Text + "'";
                    bool flag_02 = CM_DM.RunSql(sql_02);
                    string sql_03 = "DELETE FROM Maintain WHERE orderNumber = " + "'" + textBox12.Text + "'";
                    bool flag_03 = CM_DM.RunSql(sql_02);
                    string sql_04 = "DELETE FROM Install WHERE orderNumber = " + "'" + textBox12.Text + "'";
                    bool flag_04 = CM_DM.RunSql(sql_02);
                }
            }
        }
      
        }
    }
