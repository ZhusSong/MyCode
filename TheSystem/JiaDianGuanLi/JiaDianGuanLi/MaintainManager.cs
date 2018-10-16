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
    public partial class MaintainManager : Form
    {
        private int Num;                                           //id数
        private DataManager MM_DM;
        private AdminLogin MM_AL;
        private DataTable DT = new DataTable();
        private AllOfAll MM_AOA;
        private bool IsHave = false;                                //判断是否查询到数据
        private int id = 0;                                         //顾客id，用于更改后的数据库更新
        public static bool HasLogin_MM = false;                     
        public MaintainManager()
        {
            InitializeComponent();
            change.Hide();
            MM_DM = new DataManager();
            MM_AL = new AdminLogin();
            MM_AOA = new AllOfAll();
            UpdateDataView_MM("SELECT * FROM Maintain");
        }
        //↓得到登录结果的返回值
        public static void GetResult_MM(MaintainManager MM, string which)
        {
            MainSystem.IsAdmin = true;
            if (which == "change")
            {
                MM.change.Visible = true;
                MM.check.Hide();
            }
        }
        //↓得到所有顾客数目
        public  int GetProductsNumber()
        {
            Num = MM_DM.GetAll("Maintain");
            return Num;
        }
        public void UpdateDataView_MM(string sql)
        {
            dataGridView1.DataSource = null;
            DT = MM_DM.GetDataTable(sql);
            this.dataGridView1.DataSource = DT;
            this.dataGridView1.Columns[0].HeaderText = "序号";
            this.dataGridView1.Columns[1].HeaderText = "姓名";
            this.dataGridView1.Columns[2].HeaderText = "性别";
            this.dataGridView1.Columns[3].HeaderText = "电话";
            this.dataGridView1.Columns[4].HeaderText = "住址";
            this.dataGridView1.Columns[5].HeaderText = "购买商品名";
            this.dataGridView1.Columns[6].HeaderText = "维修日期";
            this.dataGridView1.Columns[7].HeaderText = "是否维修完成";
            this.dataGridView1.Columns[8].HeaderText = "订单号";
            this.dataGridView1.Columns[9].HeaderText = "维修人员";
            this.dataGridView1.Columns[10].HeaderText = "错误原因";

            this.dataGridView1.Columns[0].Width = 40;
            this.dataGridView1.Columns[1].Width = 60;
            this.dataGridView1.Columns[2].Width = 60;
            this.dataGridView1.Columns[3].Width = 100;
            this.dataGridView1.Columns[4].Width = 300;
            this.dataGridView1.Columns[5].Width = 400;
            this.dataGridView1.Columns[6].Width = 100;
            this.dataGridView1.Columns[7].Width = 60;
            this.dataGridView1.Columns[8].Width = 120;
        }
        //↓主界面_查看信息按钮
        private void check_but_Click(object sender, EventArgs e)
        {
            change.Hide();
            check.Visible = true;
        }
        //↓主界面_添加更改或删除按钮
        private void add_but_Click(object sender, EventArgs e)
        {
            if (MainSystem.IsAdmin || MainSystem.IsExAdmin)
            {
                GetResult_MM(this, "change");
            
            }
            else
            {
                MM_AL.GetName(this, "MaintainManager", "change");
                MM_AL.ShowDialog();
            }
        }
        //↓添加更改或删除界面_查询按钮
        private void button6_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM  Customer  WHERE orderNumber =" + "'" + textBox2.Text + "'";
            bool flag = MM_DM.RunSql(sql);
            if (flag)
            {
                IsHave = true;
                dataGridView4.DataSource = null;
                DT = MM_DM.GetDataTable(sql);
                this.dataGridView4.DataSource = DT;
                string sql_02 = "SELECT id FROM Maintain WHERE orderNumber =" + "'" + textBox2.Text + "'";
                id = Convert.ToInt32(MM_DM.FindData(sql_02));
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

                string sql_01 = "SELECT date FROM Maintain WHERE orderNumber =" + "'" + textBox2.Text + "'";
                try
                {
                    string Date = (string)MM_DM.FindData(sql_01);
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
        //↓添加更改或删除界面_维修情况
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
                MessageBox.Show("维修日期不可为空！");
            else
            {
                string sql = "SELECT orderNumber FROM Maintain WHERE id = " + id;
                string orderNumber_01 = (string)MM_DM.FindData(sql);
                if (orderNumber_01 == textBox2.Text)
                    MessageBox.Show("添加失败，订单号重复！");
                else
                {
                    GetProductsNumber();
                    int num = MM_AOA.GetProductsNumber();
                    MM_DM.GetData_Operation(num, MainSystem.thisAdmin, "管理员:" + MainSystem.thisAdmin + "添加了订单号为 " + textBox2.Text + "的维修信息", DateTime.Now.ToString());
                   
                    MM_DM.GetData_Maintain(Num, dataGridView4.Rows[0].Cells[1].Value.ToString(), dataGridView4.Rows[0].Cells[2].Value.ToString(), dataGridView4.Rows[0].Cells[3].Value.ToString(), dataGridView4.Rows[0].Cells[4].Value.ToString(), dataGridView4.Rows[0].Cells[5].Value.ToString(), textBox3.Text, "否", textBox2.Text);
                    MessageBox.Show("添加成功！");
                    dataGridView4.DataSource = null;
                    textBox3.Text = null;
                    textBox4.Text = null;
                }
            }
            string sql_01 = "SELECT * FROM Maintain";
            UpdateDataView_MM(sql_01);
        }
        //↓添加更改或删除界面_更改按钮
        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 0)
                MessageBox.Show("订单号不可为空！");
            else if (textBox3.Text.Length == 0)
                MessageBox.Show("维修日期不可为空！");
            else if (textBox4.Text.Length == 0)
                MessageBox.Show("请确认安装情况！");
            else if (textBox6.Text.Length == 0)
                MessageBox.Show("请输入维修人员姓名！");
            else if (textBox7.Text.Length == 0)
                MessageBox.Show("请输入家电的维修原因！");
            else
            {
                   string sql_isOrNo = "SELECT isOrNo FROM Goods WHERE orderNumber = '" + textBox2.Text + "'";
               string ion=(string)MM_DM.FindData(sql_isOrNo);
               if (ion == "否" && textBox4.Text == "是")
               {
                   MessageBox.Show("该订单并未签收，维修情况不可为已维修!");
                   textBox4.Text = null;
               }
               else
               {
                   string sql = "UPDATE Maintain SET name = " + "'" + dataGridView4.Rows[0].Cells[1].Value.ToString() + "', sex = '" + dataGridView4.Rows[0].Cells[2].Value.ToString() + "', telephone =" + "'" + dataGridView4.Rows[0].Cells[3].Value.ToString() + "', position = '" + dataGridView4.Rows[0].Cells[4].Value.ToString() + "' , whichgoods = '" + dataGridView4.Rows[0].Cells[5].Value.ToString() + "' , date = '" + textBox3.Text + "' , isOrNo = '" + textBox4.Text + "' , maintainMan = '" + textBox6.Text + "' , errorInfor = '" + textBox7.Text + "' WHERE orderNumber = " + "'" + textBox2.Text + "'";
                   int num = MM_AOA.GetProductsNumber();                             //向操作记录库中添加信息
                   MM_DM.GetData_Operation(num, MainSystem.thisAdmin, "管理员:" + MainSystem.thisAdmin + "更改了订单号为 " + textBox2.Text + "的维修信息", DateTime.Now.ToString());

                   int num_01 = MM_AOA.GetAllMaintainNumber();                       //向所有维修信息库中添加信息
                   MM_DM.GetData_AllMaintain(num_01, dataGridView4.Rows[0].Cells[1].Value.ToString(), dataGridView4.Rows[0].Cells[2].Value.ToString(), dataGridView4.Rows[0].Cells[3].Value.ToString(), dataGridView4.Rows[0].Cells[4].Value.ToString(), dataGridView4.Rows[0].Cells[5].Value.ToString(), textBox3.Text, textBox4.Text,textBox2.Text , textBox6.Text, textBox7.Text,DateTime.Now.ToString());

                   MM_DM.Run(sql);
                   MessageBox.Show("更改成功！");
                   dataGridView4.DataSource = null;
                   textBox3.Text = null;
                   textBox4.Text = null;
               }
            }
            string sql_01 = "SELECT * FROM Maintain";
            UpdateDataView_MM(sql_01);
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
                    string sql_02 = "SELECT isOrNo FROM  Maintain WHERE orderNumber = " + "'" + textBox2.Text + "'";
                    string isOrNo = (string)MM_DM.FindData(sql_02);
                    if (isOrNo == "否")
                    {
                        DialogResult dr_01 = MessageBox.Show("该订单并未维修完成，确认要删除吗?", "确认", MessageBoxButtons.YesNo);
                        if (dr_01 == DialogResult.Yes)
                        {
                            string sql_01 = "DELETE FROM Maintain WHERE orderNumber = " + "'" + textBox2.Text + "'";
                            bool flag = MM_DM.RunSql(sql_01);

                            if (flag)
                            {
                                int num = MM_AOA.GetProductsNumber();
                                MM_DM.GetData_Operation(num, MainSystem.thisAdmin, "管理员:" + MainSystem.thisAdmin + "删除了订单号为 " + textBox2.Text + "的维修信息", DateTime.Now.ToString());
                   
                                MessageBox.Show("删除成功！");
                                dataGridView4.DataSource = null;
                                textBox3.Text = null;
                                textBox4.Text = null;
                                UpdateDataView_MM("SELECT * FROM Maintain");
                            }
                        }
                    }
                    else
                    {
                        string sql_01 = "DELETE FROM Maintain WHERE orderNumber = " + "'" + textBox2.Text + "'";
                        bool flag = MM_DM.RunSql(sql_01);

                        if (flag)
                        {
                            MessageBox.Show("删除成功！");
                            dataGridView4.DataSource = null;
                            textBox3.Text = null;
                            textBox4.Text = null;
                            UpdateDataView_MM("SELECT * FROM Maintain");
                        }
                    }
                }
            }
            string sql_04 = "SELECT * FROM Maintain";
            UpdateDataView_MM(sql_04);
        }
        //↓查看信息界面_按性别检索
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "全部")
            {
                string sql = "SELECT * FROM Maintain WHERE sex = " + "'" + comboBox1.Text + "'";
                UpdateDataView_MM(sql);
            }
            else
            {
                string sql = "SELECT * FROM Maintain";
                UpdateDataView_MM(sql);
            }
        }
        //↓查看信息界面_按维修情况检索
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text != "全部")
            {
                string sql = "SELECT * FROM Maintain WHERE isOrNo = " + "'" + comboBox3.Text + "'";
                UpdateDataView_MM(sql);
            }
            else
            {
                string sql = "SELECT * FROM Maintain";
                UpdateDataView_MM(sql);
            }
        }
        //↓查看信息界面_按地名检索
        private void button3_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Maintain WHERE position LIKE " + "'%" + textBox5.Text + "%'";
            UpdateDataView_MM(sql);

        }
        //↓查看信息界面_按订单号检索
        private void button5_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Maintain WHERE orderNumber LIKE " + "'%" + textBox1.Text + "%'";
            UpdateDataView_MM(sql);
        }
        //↓查看信息界面_按姓名检索
        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Maintain WHERE name LIKE " + "'%" + textBox8.Text + "%'";
            UpdateDataView_MM(sql);
        }
    }
}
