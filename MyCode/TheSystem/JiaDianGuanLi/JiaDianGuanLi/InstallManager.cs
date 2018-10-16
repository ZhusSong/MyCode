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
    public partial class InstallManager : Form
    {
        private int Num;                                           //id数
        private DataManager IM_DM;
        private AdminLogin IM_AL;
        private AllOfAll IM_AOA;
        private DataTable DT = new DataTable();
        private bool IsHave = false;                                //判断是否查询到数据
        private int id = 0;                                         //顾客id，用于更改后的数据库更新
        public static bool HasLogin_IM = false;                     //该次是否已有管理员登录
        public InstallManager()
        {
            InitializeComponent();
            change.Hide();
            IM_DM = new DataManager();
            IM_AL = new AdminLogin();
            IM_AOA = new AllOfAll();
            UpdateDataView_IM("SELECT * FROM Install");
        }
        //↓得到登录结果的返回值
        public static void GetResult_IM(InstallManager IM, string which)
        {

            MainSystem.IsAdmin = true;
            if (which == "change")
            {
                IM.change.Visible = true;
                IM.check.Hide();
            }
        }
        //↓得到所有顾客数目
        public int GetProductsNumber()
        {
            Num = IM_DM.GetAll("Install");
            return Num;
        }
        public void UpdateDataView_IM(string sql)
        {
           dataGridView1.DataSource = null;
            DT = IM_DM.GetDataTable(sql);
            this.dataGridView1.DataSource = DT;
            this.dataGridView1.Columns[0].HeaderText = "序号";
            this.dataGridView1.Columns[1].HeaderText = "姓名";
            this.dataGridView1.Columns[2].HeaderText = "性别";
            this.dataGridView1.Columns[3].HeaderText = "电话";
            this.dataGridView1.Columns[4].HeaderText = "住址";
            this.dataGridView1.Columns[5].HeaderText = "购买商品名";
            this.dataGridView1.Columns[6].HeaderText = "安装日期";
            this.dataGridView1.Columns[7].HeaderText = "是否安装完成";
            this.dataGridView1.Columns[8].HeaderText = "订单号";
            this.dataGridView1.Columns[9].HeaderText = "安装人员";

            this.dataGridView1.Columns[0].Width = 40;
            this.dataGridView1.Columns[1].Width = 60;
            this.dataGridView1.Columns[2].Width = 60;
            this.dataGridView1.Columns[3].Width = 100;
            this.dataGridView1.Columns[4].Width = 300;
            this.dataGridView1.Columns[5].Width = 400;
            this.dataGridView1.Columns[6].Width = 100;
            this.dataGridView1.Columns[7].Width = 120;
            this.dataGridView1.Columns[8].Width = 100;
        }
        //↓主界面_添加更改或删除按钮
        private void add_but_Click(object sender, EventArgs e)
        {
            if (MainSystem.IsAdmin || MainSystem.IsExAdmin)
            {
                GetResult_IM(this, "change");
            
            }
            else
            {
                IM_AL.GetName(this, "InstallManager", "change");
                IM_AL.ShowDialog();
            }
        }
        //↓主界面_查看信息按钮
        private void check_but_Click_1(object sender, EventArgs e)
        {
            change.Hide();
            check.Visible = true;
        }
     
        
     
        //↓添加更改或删除界面_查询按钮
        private void button6_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM  Customer  WHERE orderNumber =" + "'" + textBox2.Text + "'";
            bool flag = IM_DM.RunSql(sql);
            if (flag)
            {
                IsHave = true;
                dataGridView4.DataSource = null;
                DT = IM_DM.GetDataTable(sql);
                this.dataGridView4.DataSource = DT;
                string sql_02 = "SELECT id FROM Install WHERE orderNumber =" + "'" + textBox2.Text + "'";
                id = Convert.ToInt32(IM_DM.FindData(sql_02));
                this.dataGridView4.Columns[0].Visible = false;
                this.dataGridView4.Columns[1].HeaderText = "姓名";
                this.dataGridView4.Columns[2].HeaderText = "性别";
                this.dataGridView4.Columns[3].HeaderText = "电话";
                this.dataGridView4.Columns[4].HeaderText = "住址";
                this.dataGridView4.Columns[5].HeaderText = "购买商品名";
                this.dataGridView4.Columns[6].HeaderText = "订单号";
                //基本信息均只可读，若想更改则去顾客信息界面更改
                this.dataGridView4.Columns[1].ReadOnly = true;
                this.dataGridView4.Columns[2].ReadOnly = true;
                this.dataGridView4.Columns[3].ReadOnly = true;
                this.dataGridView4.Columns[4].ReadOnly = true;
                this.dataGridView4.Columns[5].ReadOnly = true;
                this.dataGridView4.Columns[6].ReadOnly = true;

                this.dataGridView4.Columns[0].Width = 40;
                this.dataGridView4.Columns[1].Width = 60;
                this.dataGridView4.Columns[2].Width = 60;
                this.dataGridView4.Columns[3].Width = 100;
                this.dataGridView4.Columns[4].Width = 300;
                this.dataGridView4.Columns[5].Width = 400;
                this.dataGridView4.Columns[6].Width = 120;

                string sql_01 = "SELECT date FROM Install WHERE orderNumber =" + "'" + textBox2.Text + "'";
                try
                {
                    string Date = (string)IM_DM.FindData(sql_01);
                    textBox3.Text = Date;
                }
                catch
                {
                }
            }
            else
            {
                MessageBox.Show("不存在该订单号！");
            }
        }
        //↓添加更改或删除界面_安装情况
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox4.Text = comboBox2.Text;
        }
        //↓添加更改或删除界面_添加按钮
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 0)
                MessageBox.Show("订单号不可为空！");
            else if (textBox3.Text.Length == 0)
                MessageBox.Show("安装日期不可为空！");
            else
            {
                string sql = "SELECT orderNumber FROM Install WHERE id = " + id;
                string orderNumber_01 = (string)IM_DM.FindData(sql);
                if (orderNumber_01 == textBox2.Text)
                    MessageBox.Show("添加失败，订单号重复！");
                else
                {
                    GetProductsNumber();

                    IM_DM.GetData_Install(Num, dataGridView4.Rows[0].Cells[1].Value.ToString(), dataGridView4.Rows[0].Cells[2].Value.ToString(), dataGridView4.Rows[0].Cells[3].Value.ToString(), dataGridView4.Rows[0].Cells[4].Value.ToString(), dataGridView4.Rows[0].Cells[5].Value.ToString(), textBox3.Text, "否", textBox2.Text);
                    int num = IM_AOA.GetProductsNumber();
                    IM_DM.GetData_Operation(num, MainSystem.thisAdmin, "管理员:" + MainSystem.thisAdmin + "添加了订单号为 " + textBox2.Text + "的安装信息", DateTime.Now.ToString());
                 
                    MessageBox.Show("添加成功！");
                    dataGridView4.DataSource = null;
                    textBox3.Text = null;
                    textBox4.Text = null;
                }
            }
            string sql_01 = "SELECT * FROM Install";
            UpdateDataView_IM(sql_01);
        }
        //↓添加更改或删除界面_更改按钮
        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 0)
                MessageBox.Show("订单号不可为空！");
            else if (textBox3.Text.Length == 0)
                MessageBox.Show("安装日期不可为空！");
            else if (textBox4.Text.Length == 0)
                MessageBox.Show("请确认安装情况！");
            else if (textBox6.Text.Length == 0)
                MessageBox.Show("安装人员不可为空！");
            else
            {
                string sql_isOrNo = "SELECT isOrNo FROM Goods WHERE orderNumber = '" + textBox2.Text + "'";
               string ion=(string)IM_DM.FindData(sql_isOrNo);
               if (ion == "否" && textBox4.Text == "是")
               {
                   MessageBox.Show("该订单并未签收，安装情况不可为已安装!");
                   textBox4.Text = null;
               }
               else
               {
                   string sql = "UPDATE Install SET name = " + "'" + dataGridView4.Rows[0].Cells[1].Value.ToString() + "', sex = '" + dataGridView4.Rows[0].Cells[2].Value.ToString() + "', telephone =" + "'" + dataGridView4.Rows[0].Cells[3].Value.ToString() + "', position = '" + dataGridView4.Rows[0].Cells[4].Value.ToString() + "' , whichgoods = '" + dataGridView4.Rows[0].Cells[5].Value.ToString() + "' , date = '" + textBox3.Text + "' , isOrNo = '" + textBox4.Text + "' , InstallMan = '" + textBox6.Text + "' WHERE orderNumber = " + "'" + textBox2.Text + "'";
                   int num = IM_AOA.GetProductsNumber();                         //向操作记录库中添加信息
                   IM_DM.GetData_Operation(num, MainSystem.thisAdmin, "管理员:" + MainSystem.thisAdmin + "更改了订单号为 " + textBox2.Text + "的安装信息", DateTime.Now.ToString());
                    
                   int num_01 = IM_AOA.GetAllInstallNumber();                    //向所有安装信息库中添加信息
                   IM_DM.GetData_AllInstall(num_01, dataGridView4.Rows[0].Cells[1].Value.ToString(), dataGridView4.Rows[0].Cells[2].Value.ToString(), dataGridView4.Rows[0].Cells[3].Value.ToString(), dataGridView4.Rows[0].Cells[4].Value.ToString(), dataGridView4.Rows[0].Cells[5].Value.ToString(), textBox3.Text, textBox4.Text, textBox2.Text, textBox6.Text, DateTime.Now.ToString());

                   IM_DM.Run(sql);
                   MessageBox.Show("更改成功！");
                   dataGridView4.DataSource = null;
                   textBox3.Text = null;
                   textBox4.Text = null;
               }
            }
            string sql_01 = "SELECT * FROM Install";
            UpdateDataView_IM(sql_01);
        }
        //↓添加更改或删除界面_删除按钮
        private void button9_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 0)
                MessageBox.Show("订单号不可为空！");
            else
            {
                DialogResult dr = MessageBox.Show("确定删除吗?", "确认", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    string sql_02 = "SELECT isOrNo FROM  Install WHERE orderNumber = " + "'" + textBox2.Text + "'";
                    string isOrNo = (string)IM_DM.FindData(sql_02);
                    if (isOrNo == "否")
                    {
                        DialogResult dr_01 = MessageBox.Show("该订单并未安装完成，确认要删除吗?", "确认", MessageBoxButtons.YesNo);
                        if (dr_01 == DialogResult.Yes)
                        {
                            string sql_01 = "DELETE FROM Install WHERE orderNumber = " + "'" + textBox2.Text + "'";
                            bool flag = IM_DM.RunSql(sql_01);

                            if (flag)
                            {
                                int num = IM_AOA.GetProductsNumber();
                                IM_DM.GetData_Operation(num, MainSystem.thisAdmin, "管理员:" + MainSystem.thisAdmin + "删除了订单号为 " + textBox2.Text + "的安装信息", DateTime.Now.ToString());
                   
                                MessageBox.Show("删除成功！");
                                dataGridView4.DataSource = null;
                                UpdateDataView_IM("SELECT * FROM Install");
                            }
                        }
                    }
                    else
                    {
                        string sql_01 = "DELETE FROM Install WHERE orderNumber = " + "'" + textBox2.Text + "'";
                        bool flag = IM_DM.RunSql(sql_01);

                        if (flag)
                        {
                            MessageBox.Show("删除成功！");
                            dataGridView4.DataSource = null;
                            textBox3.Text = null;
                            textBox4.Text = null;
                            UpdateDataView_IM("SELECT * FROM Install");
                        }
                    }
                }
            }
            string sql_04 = "SELECT * FROM Install";
            UpdateDataView_IM(sql_04);
        }
        //↓查看信息界面_按安装情况检索
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text != "全部")
            {
                string sql = "SELECT * FROM Install WHERE isOrNo = " + "'" + comboBox3.Text + "'";
                UpdateDataView_IM(sql);
            }
            else
            {
                string sql = "SELECT * FROM Install";
                UpdateDataView_IM(sql);
            }
        }
        //↓查看信息界面_按性别检索
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "全部")
            {
                string sql = "SELECT * FROM Install WHERE sex = " + "'" + comboBox1.Text + "'";
                UpdateDataView_IM(sql);
            }
            else
            {
                string sql = "SELECT * FROM Install";
                UpdateDataView_IM(sql);
            }
        }
        //↓查看信息界面_按地名检索
        private void button3_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Install WHERE position LIKE " + "'%" + textBox5.Text + "%'";
            UpdateDataView_IM(sql);
        }
        //↓查看信息界面_按订单号检索
        private void button5_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Install WHERE orderNumber LIKE " + "'%" + textBox1.Text + "%'";
            UpdateDataView_IM(sql);
        }
        //↓查看信息界面_按姓名检索
        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Install WHERE name LIKE " + "'%" + textBox8.Text + "%'";
            UpdateDataView_IM(sql);
        }


        private void check_but_Click(object sender, EventArgs e)
        {

        }
    }
}
