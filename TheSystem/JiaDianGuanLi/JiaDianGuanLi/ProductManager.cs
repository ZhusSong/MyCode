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
    public partial class ProductManager : Form
    {
        DataManager PM_DM;                                               //调用DataManager()
        AllOfAll PM_AOA;                                                 //调用AllOfAll()
        private AdminLogin PM_AL;                                        //调用AdminLogin()                                   
        private int Num;                                                 //所有产品的数量
        private DataTable DT=new DataTable();
        private int id = 0;                                              //产品id，用于更改后的数据库更新
        private bool IsHave = false;                                     //判断是否查询到数据

        //↓构造函数，进行相关数据初始化
        public ProductManager()
        {
            InitializeComponent();
            this.add.Hide();                                             //只显示信息界面
            this.change.Hide();
                PM_DM = new DataManager();                               //实例化PM_DM
                PM_AL = new AdminLogin();
                PM_AOA = new AllOfAll();
                UpdateDataView("SELECT * FROM Product");                 //进行显示界面的初始化             
                GetProductsNumber();                                     //得到产品总数
                string sql = "SELECT function FROM Product";             //查询所有功能 
                List<string> Function = PM_DM.GetNoRepeatList(sql);      //得到产品功能分类
                //↓遍历所有功能分类，若不在默认列表内则添加至列表
                foreach (string i in Function)
                {
                    if (i != "制冷电器" && i != "空调器" && i != "清洁电器" && i != "厨房电器" && i != "电暖器具" && i != "整容保健电器" && i != "声像电器" && i != "报警电器")
                    {
                        comboBox1.Items.Add(i);
                        comboBox6.Items.Add(i);
                    }
                }
        }
        private void ProductManager_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“informationsDataSet.Product”中。您可以根据需要移动或删除它。
            this.productTableAdapter.Fill(this.informationsDataSet.Product);

        }
        //↓更新查看信息界面的数据视图显示
        private void UpdateDataView(string sql)
        {
            dataGridView1.DataSource = null;
            DT = PM_DM.GetDataTable(sql);
            this.dataGridView1.DataSource = DT;
            this.dataGridView1.Columns[0].HeaderText="序号";
            this.dataGridView1.Columns[1].HeaderText = "功能";
            this.dataGridView1.Columns[2].HeaderText = "种类";
            this.dataGridView1.Columns[3].HeaderText = "品牌";
            this.dataGridView1.Columns[4].HeaderText = "价格";
            this.dataGridView1.Columns[5].HeaderText = "名字";
            this.dataGridView1.Columns[6].HeaderText = "序列号";

            this.dataGridView1.Columns[0].Width = 40;
            this.dataGridView1.Columns[1].Width = 60;
            this.dataGridView1.Columns[2].Width = 100;
            this.dataGridView1.Columns[3].Width = 60;
            this.dataGridView1.Columns[4].Width = 40;
            this.dataGridView1.Columns[5].Width = 400;
            this.dataGridView1.Columns[6].Width = 120;
        }
        //↓得到所有产品数目
        private void GetProductsNumber()
        {
            Num = PM_DM.GetAll("Product");
        }
        //↓查看信息的按钮相应事件
        private void check_but_Click(object sender, EventArgs e)
        {
            change.Hide();
            add.Hide();
            check.Visible = true;
        }
        //↓添加信息的按钮相应事件
        private void add_but_Click(object sender, EventArgs e)
        {
            if (MainSystem.IsAdmin || MainSystem.IsExAdmin)
            {
                GetResult(this, "add");
            }
            else
            {
                PM_AL.GetName(this, "ProductManager", "add");
                PM_AL.ShowDialog();
            }
        }
        //↓改变信息的按钮相应事件
        private void change_but_Click(object sender, EventArgs e)
        {
            if (MainSystem.IsAdmin||MainSystem.IsExAdmin)
            {
                GetResult(this, "change");
            }
             else
            {
                PM_AL.GetName(this, "ProductManager", "change");
                PM_AL.ShowDialog();
               
             }
        }
        //↓得到登录结果的返回值
   public static void GetResult(ProductManager PM,string which)
   {
       MainSystem.IsAdmin = true;
       if (which == "add")
       {
           PM.add.Visible = true;
           PM.change.Hide();
           PM.check.Hide();
       }
       else if (which == "change")
       {
           PM.change.Visible = true;
           PM.add.Hide();
           PM.check.Hide();
       }
   }
   
        //↓确认添加时的按钮响应事件
        private void button6_Click(object sender, EventArgs e)
        {
            bool IsHave = false;
            if (textBox2.Text.Length == 0)
            {
                MessageBox.Show("创建失败，功能分类不能为空");
            }
            else if (textBox3.Text.Length == 0)
            {
                MessageBox.Show("创建失败，种类分类不能为空");
            }
            else if (textBox4.Text.Length == 0)
            {
                MessageBox.Show("创建失败，品牌分类不能为空");
             }
            else if (textBox5.Text.Length == 0)
            {
                MessageBox.Show("创建失败，价格不能为空");
             }
            else if (textBox6.Text.Length == 0)
            {
                MessageBox.Show("创建失败，产品名不能为空");
            }
            else if (textBox7.Text.Length == 0)
            {
                MessageBox.Show("创建失败，序列号不能为空");
            }
            else
            {
                GetProductsNumber();                                                    //得到产品数目
                IsHave = PM_DM.GetData_Product(Num, textBox2.Text, textBox3.Text, textBox4.Text, float.Parse(textBox5.Text), textBox6.Text, textBox7.Text);//向产品库中添加数据
            }
           if (!IsHave)
           {
               MessageBox.Show("创建失败，序列号重复");

           }
           else
           {
               int num = PM_AOA.GetProductsNumber();
               PM_DM.GetData_Operation(num, MainSystem.thisAdmin, "管理员:" + MainSystem.thisAdmin + "向产品库中添加序列号为 " + textBox7.Text + "的产品", DateTime.Now.ToString());//向操作记录库中添加数据
               int num_01 = PM_AOA.GetAllProductNumber();
               PM_DM.GetData_AllProduct(num_01, textBox2.Text, textBox3.Text, textBox4.Text, float.Parse(textBox5.Text), textBox6.Text, textBox7.Text, DateTime.Now.ToString());  //向所有产品库中添加数据
               textBox2.Text = null;
               textBox3.Text = null;
               textBox4.Text = null;
               textBox5.Text = null;
               textBox6.Text = null;
               textBox7.Text = null;
               MessageBox.Show("创建成功！");

               string sql_01 = "SELECT * FROM Product";
               UpdateDataView(sql_01);
           }
        }
      
    
        //↓添加页面_功能分类
        //  控制种类分类的下拉列表框的数据显示
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox6.Text != "自定义" && comboBox6.Text != "功能分类")//控制文本框内容
                textBox2.Text = comboBox6.Text;

            comboBox5.Items.Clear();
            string sql_02 = "SELECT type FROM Product WHERE function = " + "'" + comboBox6.Text + "'";
            List<string> Function_03 = PM_DM.GetNoRepeatList(sql_02);//得到产品种类分类
            foreach (string i in Function_03)
            {
                    comboBox5.Items.Add(i);
            }
            comboBox5.Items.Add("自定义");

            if (comboBox6.Text == "自定义")
            {
                textBox2.Text = null;
                comboBox5.Items.Clear();
            }
        }

        //↓添加页面_种类分类
        //  控制品牌分类下拉列表框的数据显示
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.Text != "自定义" && comboBox5.Text != "种类分类")//控制文本框内容
            textBox3.Text =comboBox5.Text;

            comboBox4.Items.Clear();
            string sql_02 = "SELECT  brand FROM  Product WHERE type = "+"'"+comboBox5.Text+"'";
            List<string> Brand_01 = PM_DM.GetNoRepeatList(sql_02);//得到产品品牌分类
            //↓遍历所有种类分类，若不在默认列表内则添加至列表
            foreach (string i in Brand_01)
            {
                comboBox4.Items.Add(i);
            }
            comboBox4.Items.Add("自定义");

            if (comboBox5.Text == "自定义")
            {
                textBox3.Text = null;
                comboBox4.Items.Clear();
            }
        }

        //↓添加页面_品牌分类
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text != "自定义" && comboBox4.Text != "品牌分类")//控制文本框内容
            textBox4.Text = comboBox4.Text;
            if (comboBox4.Text == "自定义")
            {
                textBox4.Text=null;
            }
        }    

        //↓添加页面_功能_文本框
        private void textBox2_TextChanged(object sender, EventArgs e)
        {


        }

        //↓添加页面_种类_文本框
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        //↓查看信息页面_功能分类
        //  控制种类分类的下拉列表内容
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.Items.Add("全部");
            string sql_03 = "SELECT type FROM Product WHERE function = " + "'" + comboBox1.Text+"'";
            List<string> Function_02 = PM_DM.GetNoRepeatList(sql_03);//得到产品种类分类
            //↓遍历所有种类分类，若不在默认列表内则添加至列表
            foreach (string i in Function_02)
            {
                    comboBox2.Items.Add(i);
            }
         

     
            if (comboBox1.Text == "全部")
            {
                comboBox2.Items.Clear();
                string sql_01 = "SELECT * FROM Product";
                UpdateDataView(sql_01);
            }
            else
            {
                string sql_02 = "SELECT * FROM Product WHERE function = " + "'" + comboBox1.Text + "'";
                UpdateDataView(sql_02);
            }
        }
        //↓查看信息页面_种类分类
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();

            comboBox3.Items.Add("全部");
            string sql_03 = "SELECT brand FROM  Product WHERE type = '"+comboBox2.Text+"'";
            List<string> Brand_03 = PM_DM.GetNoRepeatList(sql_03);//得到产品品牌分类
            //↓遍历所有种类分类，若不在默认列表内则添加至列表
            foreach (string i in Brand_03)
            {
                comboBox3.Items.Add(i);
            }

            if (comboBox2.Text == "全部")
            {
                string sql_01 = "SELECT * FROM Product WHERE function = " + "'" + comboBox1.Text + "'";
                UpdateDataView(sql_01);
            }
            else
            {
                string sql_01 = "SELECT * FROM Product WHERE function = " + "'" + comboBox1.Text + "' AND type = " + "'" + comboBox2.Text + "'";
                UpdateDataView(sql_01);
            }
        }

        //↓查看信息页面_品牌分类
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text == "全部")
            {
                string sql_01 = "SELECT * FROM Product WHERE function = " + "'" + comboBox1.Text + "' AND type = " + "'" + comboBox2.Text + "'";
                UpdateDataView(sql_01);
            }
            else
            {
                string sql = "SELECT * FROM Product WHERE function = " + "'" + comboBox1.Text + "' AND type = " + "'" + comboBox2.Text + "'" + " AND brand = " + "'" + comboBox3.Text + "'";
                UpdateDataView(sql);
            }
         
        }

      
        //↓查看信息页面_按序列号查找
        private void button5_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Product WHERE serialNumber LIKE "+"'%"+textBox1.Text+"%'";
            UpdateDataView(sql);
        }
        //↓查看信息页面_按名字查找
        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Product WHERE name LIKE " + "'%" + textBox8.Text + "%'";
            UpdateDataView(sql);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }


        //↓更改或删除页面_查询
        private void button3_Click(object sender, EventArgs e)
        {
            string sql_01 = "SELECT * FROM Product WHERE serialNumber = " + "'" + textBox10.Text + "'";
            bool flag = PM_DM.RunSql(sql_01);
            if (flag)
            {
                IsHave = true;
                dataGridView2.DataSource = null;
                DT = PM_DM.GetDataTable(sql_01);
                this.dataGridView2.DataSource = DT;
                id = Convert.ToInt32(dataGridView2.Rows[0].Cells["id"].Value);
                this.dataGridView2.Columns[0].Visible= false;
                this.dataGridView2.Columns[1].HeaderText = "功能";
                this.dataGridView2.Columns[2].HeaderText = "种类";
                this.dataGridView2.Columns[3].HeaderText = "品牌";
                this.dataGridView2.Columns[4].HeaderText = "价格";
                this.dataGridView2.Columns[5].HeaderText = "名字";
                this.dataGridView2.Columns[6].HeaderText = "序列号";
                this.dataGridView2.Columns[6].ReadOnly = true;
                this.dataGridView2.Columns[1].Width = 60;
                this.dataGridView2.Columns[2].Width = 100;
                this.dataGridView2.Columns[3].Width = 60;
                this.dataGridView2.Columns[4].Width = 40;
                this.dataGridView2.Columns[5].Width = 400;
                this.dataGridView2.Columns[6].Width = 120;


            }
            else
            {
                MessageBox.Show("不存在该序列号！");
            }
        }

        //↓更改或删除页面_更改
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox10.Text.Length == 0)
            {
                MessageBox.Show("序列号不可为空！");
            }
            else if (!IsHave)
            {
                MessageBox.Show("不存在该序列号！");
            }
            else
            {
                DialogResult dr = MessageBox.Show("确定更改吗?", "确认", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    string sql = "UPDATE Product SET function =" + "'" + dataGridView2.Rows[0].Cells[1].Value.ToString() + "', type = '" + dataGridView2.Rows[0].Cells[2].Value.ToString() + "',brand =" + "'" + dataGridView2.Rows[0].Cells[3].Value.ToString() + "',price = '" + dataGridView2.Rows[0].Cells[4].Value + "' ,name = '" + dataGridView2.Rows[0].Cells[5].Value.ToString() + "' ,serialNumber = '" + dataGridView2.Rows[0].Cells[6].Value.ToString()+"' WHERE id ="+id;
                    PM_DM.RunSql(sql);
                    MessageBox.Show("更改成功！");
                    int num = PM_AOA.GetProductsNumber();
                    PM_DM.GetData_Operation(num, MainSystem.thisAdmin, "管理员:" + MainSystem.thisAdmin + "对序列号为 " + dataGridView2.Rows[0].Cells[6].Value.ToString() + "的产品进行更改", DateTime.Now.ToString());//向操作记录库中添加数据        
                    int num_01 = PM_AOA.GetAllProductNumber();
                    PM_DM.GetData_AllProduct(num_01, dataGridView2.Rows[0].Cells[1].Value.ToString(), dataGridView2.Rows[0].Cells[2].Value.ToString(), dataGridView2.Rows[0].Cells[3].Value.ToString(),Convert.ToSingle(dataGridView2.Rows[0].Cells[4].Value), dataGridView2.Rows[0].Cells[5].Value.ToString(), dataGridView2.Rows[0].Cells[6].Value.ToString(), DateTime.Now.ToString()); //向所有产品库中添加数据
                    dataGridView2.DataSource = null;
                    UpdateDataView("SELECT * FROM Product");
                }
            }
        }
        //↓更改或删除页面_删除
        private void button7_Click(object sender, EventArgs e)
        {

            if (textBox10.Text.Length == 0)
            {
                MessageBox.Show("序列号不可为空！");
            }
            else if (!IsHave)
            {
                MessageBox.Show("不存在该序列号！");
            }
            else
            {
                string sql_01 = "DELETE FROM Product WHERE serialNumber = " + "'" + textBox10.Text + "'";
                int num = PM_AOA.GetProductsNumber();
                PM_DM.GetData_Operation(num, MainSystem.thisAdmin, "管理员:" + MainSystem.thisAdmin + "删除了序列号为 " + textBox10.Text + "的产品", DateTime.Now.ToString());//向操作记录库中添加数据
                 
                bool flag = PM_DM.RunSql(sql_01);
                if (flag)
                {
                    MessageBox.Show("删除成功！");
                 
                    dataGridView2.DataSource = null;
                    UpdateDataView("SELECT * FROM Product");
                }
            }
        }

        private void change_Paint(object sender, PaintEventArgs e)
        {

        }
     
    }
}
