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
    public partial class AllOfAll : Form
    {
        private DataManager AOA_DM;
        private DataTable DT=new DataTable();
        private int Num;
        private int Num_AP;
        private int Num_AC;
        private int Num_AI;
        private int Num_AG;
        private int Num_AM;
        private string thisProject="产品管理";
        public AllOfAll()
        {
            AOA_DM = new DataManager();
            InitializeComponent();
            changeAdmin.Hide();
            AllData.Hide();
            UpdateDataView_AOA("SELECT * FROM Operation");
            UpdateDataViewAllData_AOA(thisProject, "SELECT * FROM AllProduct");
            string allAdmin = "SELECT name FROM Admin";
            List<string> admins = AOA_DM.GetNoRepeatList(allAdmin);
            foreach (string i in admins)
            {
                comboBox5.Items.Add(i);
            }
        }
        //↓得到所有操作数目
        public int GetProductsNumber()
        {
            Num = AOA_DM.GetAll("Operation");
            return Num;
        }
        //↓得到所有产品数目
        public int GetAllProductNumber()
        {
            Num_AP = AOA_DM.GetAll("AllProduct");
            return Num_AP;
        }
        //↓得到所有顾客数目
        public int GetAllCustomerNumber()
        {
            Num_AC = AOA_DM.GetAll("AllCustomer");
            return Num_AC;
        }
        //↓得到所有送货数目
        public int GetAllGoodsNumber()
        {
            Num_AG = AOA_DM.GetAll("AllGoods");
            return Num_AG;
        }
        //↓得到所有维修数目
        public int GetAllMaintainNumber()
        {
            Num_AM = AOA_DM.GetAll("AllMaintain");
            return Num_AM;
        }
        //↓得到所有安装数目
        public int GetAllInstallNumber()
        {
            Num_AI = AOA_DM.GetAll("AllInstall");
            return Num_AI;
        }
        //↓更新操作记录视图显示
        public void UpdateDataView_AOA(string sql)
        {
            dataGridView1.DataSource = null;
            DT = AOA_DM.GetDataTable(sql);
            this.dataGridView1.DataSource = DT;
            this.dataGridView1.Columns[0].HeaderText = "序号";
            this.dataGridView1.Columns[1].HeaderText = "操作人员";
            this.dataGridView1.Columns[2].HeaderText = "操作项";
            this.dataGridView1.Columns[3].HeaderText = "操作时间";

            this.dataGridView1.Columns[0].Width = 40;
            this.dataGridView1.Columns[1].Width = 100;
            this.dataGridView1.Columns[2].Width = 500;
            this.dataGridView1.Columns[3].Width = 150;
        }
        //↓更新管理员视图显示
        public void UpdateDataViewAdmin_AOA(string sql)
        {
            dataGridView2.DataSource = null;
            DT = AOA_DM.GetDataTable(sql);
            this.dataGridView2.DataSource = DT;
            this.dataGridView2.Columns[0].HeaderText = "序号";
            this.dataGridView2.Columns[0].ReadOnly = true;
            this.dataGridView2.Columns[1].HeaderText = "管理员名";
            this.dataGridView2.Columns[2].HeaderText = "管理员密码";

            this.dataGridView2.Columns[0].Width = 40;
            this.dataGridView2.Columns[1].Width = 150;
            this.dataGridView2.Columns[2].Width = 150;
        }
        //↓更新总数据库视图显示
        public void UpdateDataViewAllData_AOA(string name,string sql)
        {
            if (name == "产品管理")
            {
                dataGridView3.DataSource = null;
                DT = AOA_DM.GetDataTable(sql);
                this.dataGridView3.DataSource = DT;
                this.dataGridView3.Columns[0].HeaderText = "序号";
                this.dataGridView3.Columns[1].HeaderText = "功能";
                this.dataGridView3.Columns[2].HeaderText = "种类";
                this.dataGridView3.Columns[3].HeaderText = "品牌";
                this.dataGridView3.Columns[4].HeaderText = "价格";
                this.dataGridView3.Columns[5].HeaderText = "名字";
                this.dataGridView3.Columns[6].HeaderText = "序列号";
                this.dataGridView3.Columns[7].HeaderText = "更改日期";

                this.dataGridView3.Columns[0].Width = 40;
                this.dataGridView3.Columns[1].Width = 60;
                this.dataGridView3.Columns[2].Width = 100;
                this.dataGridView3.Columns[3].Width = 60;
                this.dataGridView3.Columns[4].Width = 40;
                this.dataGridView3.Columns[5].Width = 400;
                this.dataGridView3.Columns[6].Width = 120;
                this.dataGridView3.Columns[7].Width = 150;
            }
            else if (name == "顾客管理")
            {
                dataGridView3.DataSource = null;
                DT = AOA_DM.GetDataTable(sql);
                this.dataGridView3.DataSource = DT;
                this.dataGridView3.Columns[0].HeaderText = "序号";
                this.dataGridView3.Columns[1].HeaderText = "姓名";
                this.dataGridView3.Columns[2].HeaderText = "性别";
                this.dataGridView3.Columns[3].HeaderText = "电话";
                this.dataGridView3.Columns[4].HeaderText = "位置";
                this.dataGridView3.Columns[5].HeaderText = "商品名";
                this.dataGridView3.Columns[6].HeaderText = "订单号";
                this.dataGridView3.Columns[7].HeaderText = "更改日期";

                this.dataGridView3.Columns[0].Width = 40;
                this.dataGridView3.Columns[1].Width = 60;
                this.dataGridView3.Columns[2].Width = 60;
                this.dataGridView3.Columns[3].Width = 100;
                this.dataGridView3.Columns[4].Width = 300;
                this.dataGridView3.Columns[5].Width = 400;
                this.dataGridView3.Columns[6].Width = 100;
                this.dataGridView3.Columns[7].Width = 120;
            }
            else if (name == "送货管理")
            {
                dataGridView3.DataSource = null;
                DT = AOA_DM.GetDataTable(sql);
                this.dataGridView3.DataSource = DT;
                this.dataGridView3.Columns[0].HeaderText = "序号";
                this.dataGridView3.Columns[1].HeaderText = "姓名";
                this.dataGridView3.Columns[2].HeaderText = "性别";
                this.dataGridView3.Columns[3].HeaderText = "电话";
                this.dataGridView3.Columns[4].HeaderText = "位置";
                this.dataGridView3.Columns[5].HeaderText = "商品名";
                this.dataGridView3.Columns[6].HeaderText = "送货日期";
                this.dataGridView3.Columns[7].HeaderText = "是否签收";
                this.dataGridView3.Columns[8].HeaderText = "订单号";
                this.dataGridView3.Columns[9].HeaderText = "送货员";
                this.dataGridView3.Columns[10].HeaderText = "更改日期";

                this.dataGridView3.Columns[0].Width = 40;
                this.dataGridView3.Columns[1].Width = 60;
                this.dataGridView3.Columns[2].Width = 60;
                this.dataGridView3.Columns[3].Width = 100;
                this.dataGridView3.Columns[4].Width = 300;
                this.dataGridView3.Columns[5].Width = 400;
                this.dataGridView3.Columns[6].Width = 100;
                this.dataGridView3.Columns[7].Width = 120;
                this.dataGridView3.Columns[8].Width = 100;
                this.dataGridView3.Columns[9].Width = 60;
                this.dataGridView3.Columns[10].Width = 150;
            }
            else if (name == "安装管理")
            {
                dataGridView3.DataSource = null;
                DT = AOA_DM.GetDataTable(sql);
                this.dataGridView3.DataSource = DT;
                this.dataGridView3.Columns[0].HeaderText = "序号";
                this.dataGridView3.Columns[1].HeaderText = "姓名";
                this.dataGridView3.Columns[2].HeaderText = "性别";
                this.dataGridView3.Columns[3].HeaderText = "电话";
                this.dataGridView3.Columns[4].HeaderText = "住址";
                this.dataGridView3.Columns[5].HeaderText = "购买商品名";
                this.dataGridView3.Columns[6].HeaderText = "安装日期";
                this.dataGridView3.Columns[7].HeaderText = "是否安装完成";
                this.dataGridView3.Columns[8].HeaderText = "订单号";
                this.dataGridView3.Columns[9].HeaderText = "安装人员";
                this.dataGridView3.Columns[10].HeaderText = "更改日期";

                this.dataGridView3.Columns[0].Width = 40;
                this.dataGridView3.Columns[1].Width = 60;
                this.dataGridView3.Columns[2].Width = 60;
                this.dataGridView3.Columns[3].Width = 100;
                this.dataGridView3.Columns[4].Width = 300;
                this.dataGridView3.Columns[5].Width = 400;
                this.dataGridView3.Columns[6].Width = 100;
                this.dataGridView3.Columns[7].Width = 120;
                this.dataGridView3.Columns[8].Width = 100;
                this.dataGridView3.Columns[9].Width = 60;
                this.dataGridView3.Columns[10].Width = 150;
            }
            else if (name == "维修管理")
            {
                dataGridView3.DataSource = null;
                DT = AOA_DM.GetDataTable(sql);
                this.dataGridView3.DataSource = DT;
                this.dataGridView3.Columns[0].HeaderText = "序号";
                this.dataGridView3.Columns[1].HeaderText = "姓名";
                this.dataGridView3.Columns[2].HeaderText = "性别";
                this.dataGridView3.Columns[3].HeaderText = "电话";
                this.dataGridView3.Columns[4].HeaderText = "住址";
                this.dataGridView3.Columns[5].HeaderText = "购买商品名";
                this.dataGridView3.Columns[6].HeaderText = "维修日期";
                this.dataGridView3.Columns[7].HeaderText = "是否维修完成";
                this.dataGridView3.Columns[8].HeaderText = "订单号";
                this.dataGridView3.Columns[9].HeaderText = "维修人员";
                this.dataGridView3.Columns[10].HeaderText = "错误原因";
                this.dataGridView3.Columns[11].HeaderText = "更改日期";

                this.dataGridView3.Columns[0].Width = 40;
                this.dataGridView3.Columns[1].Width = 60;
                this.dataGridView3.Columns[2].Width = 60;
                this.dataGridView3.Columns[3].Width = 100;
                this.dataGridView3.Columns[4].Width = 300;
                this.dataGridView3.Columns[5].Width = 400;
                this.dataGridView3.Columns[6].Width = 100;
                this.dataGridView3.Columns[7].Width = 120;
                this.dataGridView3.Columns[8].Width = 100;
                this.dataGridView3.Columns[9].Width = 60;
                this.dataGridView3.Columns[10].Width = 150;
                this.dataGridView3.Columns[11].Width = 150;
            }
        
        }
        //↓主界面 操作记录按钮
        private void button1_Click(object sender, EventArgs e)
        {
            checkProject.Visible = true;
            changeAdmin.Hide();
            AllData.Hide();
            UpdateDataView_AOA("SELECT * FROM Operation");
        }
        //↓主界面 增删改管理员按钮
        private void button2_Click(object sender, EventArgs e)
        {
            AllData.Hide();
            checkProject.Hide();
            changeAdmin.Visible = true;
            UpdateDataViewAdmin_AOA("SELECT * FROM Admin");
        }
        //↓主界面 总数据库管理按钮
        private void button3_Click(object sender, EventArgs e)
        {
            AllData.Visible = true;
            checkProject.Hide();
            changeAdmin.Hide();
        }
        //↓查看操作记录界面 年份检索
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "全部")
            {
                UpdateDataView_AOA("SELECT * FROM Operation WHERE time LIKE '"+comboBox1.Text+"%'");
            }
            else
            {
                UpdateDataView_AOA("SELECT * FROM Operation ");
            }
        }
        //↓查看操作记录界面 月份检索
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text != "全部")
            {
                UpdateDataView_AOA("SELECT * FROM Operation WHERE time LIKE '" + comboBox1.Text + "/" + comboBox2.Text + "%'");
         

            }
            else
            {
                UpdateDataView_AOA("SELECT * FROM Operation WHERE time LIKE '%" + comboBox1.Text + "%' ");
            }
        }
        //↓查看操作记录界面 日期检索
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text != "全部")
            {
                UpdateDataView_AOA("SELECT * FROM Operation WHERE time LIKE '%" + comboBox1.Text + "/" + comboBox2.Text + "/"+comboBox3.Text+"%'");
            }
            else
            {
                UpdateDataView_AOA("SELECT * FROM Operation WHERE time LIKE '" + comboBox1.Text + "/" + comboBox2.Text + "%'");
            }
        }
        //↓查看操作记录界面 项目检索
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text != "全部")
            {
                UpdateDataView_AOA("SELECT * FROM Operation WHERE project LIKE '%" + comboBox4.Text.Replace("管理","") + "%'");
            }
            else
            {
                UpdateDataView_AOA("SELECT * FROM Operation");
            }
        }
        //↓查看操作记录界面 管理员名
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.Text != "全部")
            {
                UpdateDataView_AOA("SELECT * FROM Operation WHERE name = '" + comboBox5.Text + "'");
            }
            else
            {
                UpdateDataView_AOA("SELECT * FROM Operation");
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        //↓管理员操作界面，添加管理员按钮
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("管理员名不可为空!");

            }
            else if (textBox2.Text.Length == 0)
            {
                MessageBox.Show("管理员密码不可为空!");
            }
            else
            {
                string findAdmin = "SELECT * FROM Admin WHERE name = '" + textBox1.Text + "'";
                bool flag = AOA_DM.RunSql(findAdmin);
                if (flag)
                {
                    MessageBox.Show("添加失败，管理员名重复!");
                }
                else
                {
                    int num = AOA_DM.GetAll("Admin");
                    AOA_DM.GetData_Admin(num, textBox1.Text, textBox2.Text);
                    MessageBox.Show("添加成功！");
                    textBox1.Text = null;
                    textBox2.Text = null;
                    UpdateDataViewAdmin_AOA("SELECT * FROM Admin");
                }
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //↓管理员操作界面，保存更改按钮
        private void button5_Click(object sender, EventArgs e)
        {
                 DialogResult dr = MessageBox.Show("确定更改吗?", "确认", MessageBoxButtons.YesNo);
                 if (dr == DialogResult.Yes)
                 {
                     for (int i = 0; i < dataGridView2.Rows.Count; i++)
                     {
                         string sql = "UPDATE Admin SET name = " + "'" + dataGridView2.Rows[i].Cells[1].Value.ToString() + "', key = '" + dataGridView2.Rows[i].Cells[2].Value.ToString() + "' WHERE id =" + dataGridView2.Rows[i].Cells["id"].Value.ToString();
                         AOA_DM.RunSql(sql);
                     }
                     MessageBox.Show("更改成功！");
                     dataGridView2.DataSource = null;
                     UpdateDataViewAdmin_AOA("SELECT * FROM Admin");
                 }
        }
        //↓管理员操作界面，确认删除按钮
        private void button6_Click(object sender, EventArgs e)
        {
            int rows =Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value);
                DialogResult dr = MessageBox.Show("确定删除吗?", "确认", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    string sql = "DELETE FROM Admin WHERE id = '"+rows+"'";
                    AOA_DM.RunSql(sql);
                    MessageBox.Show("删除成功!");
                    dataGridView2.DataSource = null;
                    UpdateDataViewAdmin_AOA("SELECT * FROM Admin");
                }
        }
        //↓总数据库查看 按项目检索
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            thisProject = comboBox6.Text;
            if (thisProject == "产品管理")
                UpdateDataViewAllData_AOA(comboBox6.Text, "SELECT * FROM AllProduct");
            else if (thisProject == "顾客管理")
                UpdateDataViewAllData_AOA(comboBox6.Text, "SELECT * FROM AllCustomer");
            else if (thisProject == "送货管理")
                UpdateDataViewAllData_AOA(comboBox6.Text, "SELECT * FROM AllGoods");
            else if (thisProject == "安装管理")
                UpdateDataViewAllData_AOA(comboBox6.Text, "SELECT * FROM AllInstall");
            else if (thisProject == "维修管理")
                UpdateDataViewAllData_AOA(comboBox6.Text, "SELECT * FROM AllMaintain");
         
        }
        //↓总数据库查看 按年份检索
        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox7.Text != "全部")
            {
                if (thisProject=="产品管理")
                    UpdateDataViewAllData_AOA("产品管理", "SELECT * FROM AllProduct WHERE changeDate LIKE '" + comboBox7.Text + "%'");
                else if (thisProject=="顾客管理")
                    UpdateDataViewAllData_AOA("顾客管理", "SELECT * FROM AllCustomer WHERE changeDate LIKE '" + comboBox7.Text + "%'");
                else if (thisProject == "送货管理")
                    UpdateDataViewAllData_AOA("送货管理", "SELECT * FROM AllGoods WHERE changeDate LIKE '" + comboBox7.Text + "%'");
                else if (thisProject == "安装管理")
                    UpdateDataViewAllData_AOA("安装管理", "SELECT * FROM AllInstall WHERE changeDate LIKE '" + comboBox7.Text + "%'");
                else if (thisProject == "维修管理")
                    UpdateDataViewAllData_AOA("维修管理", "SELECT * FROM AllMaintain WHERE changeDate LIKE '" + comboBox7.Text + "%'");
            }
            else
            {
                if (thisProject == "产品管理")
                    UpdateDataViewAllData_AOA("产品管理", "SELECT * FROM AllProduct");
                else if (thisProject == "顾客管理")
                    UpdateDataViewAllData_AOA("顾客管理", "SELECT * FROM AllCustomer");
                else if (thisProject == "送货管理")
                    UpdateDataViewAllData_AOA("送货管理", "SELECT * FROM AllGoods");
                else if (thisProject == "安装管理")
                    UpdateDataViewAllData_AOA("安装管理", "SELECT * FROM AllInstall");
                else if (thisProject == "维修管理")
                    UpdateDataViewAllData_AOA("维修管理", "SELECT * FROM AllMaintain");
            }
        }
        //↓总数据库查看 按月份检索
        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox8.Text != "全部")
            {
                if (thisProject == "产品管理")
                    UpdateDataViewAllData_AOA("产品管理", "SELECT * FROM AllProduct WHERE changeDate LIKE '" + comboBox7.Text + "/" + comboBox8.Text + "%'");
                else if (thisProject == "顾客管理")
                    UpdateDataViewAllData_AOA("顾客管理", "SELECT * FROM AllCustomer WHERE changeDate LIKE '" + comboBox7.Text + "/" + comboBox8.Text + "%'");
                else if (thisProject == "送货管理")
                    UpdateDataViewAllData_AOA("送货管理", "SELECT * FROM AllGoods WHERE changeDate LIKE '" + comboBox7.Text + "/" + comboBox8.Text + "%'");
                else if (thisProject == "安装管理")
                    UpdateDataViewAllData_AOA("安装管理", "SELECT * FROM AllInstall WHERE changeDate LIKE '" + comboBox7.Text + "/" + comboBox8.Text + "%'");
                else if (thisProject == "维修管理")
                    UpdateDataViewAllData_AOA("维修管理", "SELECT * FROM AllMaintain WHERE changeDate LIKE '" + comboBox7.Text + "/" + comboBox8.Text + "%'");


            }
            else
            {
                if (thisProject == "产品管理")
                    UpdateDataViewAllData_AOA("产品管理", "SELECT * FROM AllProduct WHERE changeDate LIKE '%" + comboBox7.Text + "%'");
                else if (thisProject == "顾客管理")
                    UpdateDataViewAllData_AOA("顾客管理", "SELECT * FROM AllCustomer WHERE changeDate LIKE '%" + comboBox7.Text + "%'");
                else if (thisProject == "送货管理")
                    UpdateDataViewAllData_AOA("送货管理", "SELECT * FROM AllGoods WHERE changeDate LIKE '%" + comboBox7.Text + "%'");
                else if (thisProject == "安装管理")
                    UpdateDataViewAllData_AOA("安装管理", "SELECT * FROM AllInstall WHERE changeDate LIKE '%" + comboBox7.Text + "%'");
                else if (thisProject == "维修管理")
                    UpdateDataViewAllData_AOA("维修管理", "SELECT * FROM AllMaintain WHERE changeDate LIKE '%" + comboBox7.Text + "%'");
            }
        }
        //↓总数据库查看 按日期检索
        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox9.Text != "全部")
            {
                if (thisProject == "产品管理")
                    UpdateDataViewAllData_AOA("产品管理", "SELECT * FROM AllProduct WHERE changeDate LIKE '%" + comboBox7.Text + "/" + comboBox8.Text + "/" + comboBox9.Text + "%'");
                else if (thisProject == "顾客管理")
                    UpdateDataViewAllData_AOA("顾客管理", "SELECT * FROM AllCustomer WHERE changeDate LIKE '%" + comboBox7.Text + "/" + comboBox8.Text + "/" + comboBox9.Text + "%'");
                else if (thisProject == "送货管理")
                    UpdateDataViewAllData_AOA("送货管理", "SELECT * FROM AllGoods WHERE changeDate LIKE '%" + comboBox7.Text + "/" + comboBox8.Text + "/" + comboBox9.Text + "%'");
                else if (thisProject == "安装管理")
                    UpdateDataViewAllData_AOA("安装管理", "SELECT * FROM AllInstall WHERE changeDate LIKE '%" + comboBox7.Text + "/" + comboBox8.Text + "/" + comboBox9.Text + "%'");
                else if (thisProject == "维修管理")
                    UpdateDataViewAllData_AOA("维修管理", "SELECT * FROM AllMaintain WHERE changeDate LIKE '%" + comboBox7.Text + "/" + comboBox8.Text + "/" + comboBox9.Text + "%'");

            } 
            else
            {
                if (thisProject == "产品管理")
                    UpdateDataViewAllData_AOA("产品管理", "SELECT * FROM AllProduct WHERE changeDate LIKE '%" + comboBox7.Text + "/" + comboBox8.Text + "%'");
                else if (thisProject == "顾客管理")
                    UpdateDataViewAllData_AOA("顾客管理", "SELECT * FROM AllCustomer WHERE changeDate LIKE '%" + comboBox7.Text + "/" + comboBox8.Text + "%'");
                else if (thisProject == "送货管理")
                    UpdateDataViewAllData_AOA("送货管理", "SELECT * FROM AllGoods WHERE changeDate LIKE '%" + comboBox7.Text + "/" + comboBox8.Text + "%'");
                else if (thisProject == "安装管理")
                    UpdateDataViewAllData_AOA("安装管理", "SELECT * FROM AllInstall WHERE changeDate LIKE '%" + comboBox7.Text + "/" + comboBox8.Text + "%'");
                else if (thisProject == "维修管理")
                    UpdateDataViewAllData_AOA("维修管理", "SELECT * FROM AllMaintain WHERE changeDate  LIKE '%" + comboBox7.Text + "/" + comboBox8.Text + "%'");
            }
        }
        //↓总数据库查看 按产品序列号检索按钮
        private void button7_Click(object sender, EventArgs e)
        {
            if (thisProject == "产品管理")
            {
               string FindP= "SELECT * FROM AllProduct WHERE serialNumber LIKE " + "'%" + textBox3.Text + "%'";
               UpdateDataViewAllData_AOA(thisProject, FindP);
            }
        }
       
        //↓总数据库查看 按订单号检索按钮
        private void button8_Click(object sender, EventArgs e)
        {
            if (thisProject == "顾客管理")
                UpdateDataViewAllData_AOA("顾客管理", "SELECT * FROM AllCustomer WHERE orderNumber LIKE " + "'%" + textBox4.Text + "%'");
            else if (thisProject == "送货管理")
                UpdateDataViewAllData_AOA("送货管理", "SELECT * FROM AllGoods WHERE orderNumber LIKE " + "'%" + textBox4.Text + "%'");
            else if (thisProject == "安装管理")
                UpdateDataViewAllData_AOA("安装管理", "SELECT * FROM AllInstall WHERE orderNumber LIKE " + "'%" + textBox4.Text + "%'");
            else if (thisProject == "维修管理")
                UpdateDataViewAllData_AOA("维修管理", "SELECT * FROM AllMaintain WHERE orderNumber LIKE " + "'%" + textBox4.Text + "%'");
        }
    }
}
