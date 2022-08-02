using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs.作業
{
    public partial class Frm作業_4 : Form
    {
        public Frm作業_4()
        {
            InitializeComponent();
        }
        NorthwindEntities dbContext = new NorthwindEntities();
        
        private void button4_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 2, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 7, 7, 8, 8, 8, 8, 8, 8, 8, 8, 9, 9, 9, 9, 9, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
            var q = from n in nums
                    group n by Myclass(n) into g
                    select new { 分類級數 = g.Key, Mycount = g.Count(),Myave=g.Average(),Mysum=g.Sum(), MyGroup=g };
            this.dataGridView1.DataSource = q.ToList();

            foreach (var group in q)
            {
                string s = $"{group.分類級數} ( {group.Mycount} )";
                TreeNode x = this.treeView1.Nodes.Add(s);// group.MyKey.ToString());

                foreach (var item in group.MyGroup)
                {
                    x.Nodes.Add(item.ToString());
                }
            }

        }

        private string Myclass(int n)
        {
            if (n >= 4 && n <= 6)
                return "中數";
            else if (n <= 3)
                return "蠻小的數";
            else
                return "大數";
        }

        private void button38_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from i in files
                    group i by i.Length into g 
                    orderby g.Key descending
                    select new {檔案大小 =g.Key};
            dataGridView1.DataSource = q.ToList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from i in files
                    group i by i.CreationTime into g
                    orderby g.Key descending
                    select new { 檔案創建時間 = g.Key };
            dataGridView1.DataSource = q.ToList();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var q = from i in this.dbContext.Products.AsEnumerable()
                    group i by Mygroup(i) into g
                    select new { 商品分類 = g.Key, 類別個數 = g.Count(), 類別平均單價 = g.Average(i=>i.UnitPrice) };
            dataGridView1.DataSource = q.ToList();
        }


        public string Mygroup(Product i)

        {
            if (i.UnitPrice > 30)
                return "高價商品";
            else if (i.UnitPrice > 20 && i.UnitPrice > 10)
                return "中價商品";
            else
                return "低價商品";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var q = from i in this.dbContext.Orders
                    group i by i.OrderDate.Value.Year into g
                    
                    select new { 訂單年份=g.Key};
            dataGridView1.DataSource = q.ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            
            var q1 = from i in this.dbContext.Orders
                     group i by i.OrderDate.Value.Year into g
                     select new
                     { 
                         年份 = g.Key, Count = g.Count(), MyGroup = g ,
                         Month=g.GroupBy(p=>p.OrderDate.Value.Month).Select(p=>new {月份=p.Key,MonthCount=p.Count(),Mothgroup=p })
                     };
            dataGridView2.DataSource = q1.ToList();
            foreach (var group in q1)
            {
                string s = $"{group.年份} ( {group.Count} )";
                TreeNode x = this.treeView1.Nodes.Add(s);// group.MyKey.ToString());

                foreach (var item in group.Month)
                {
                    //x.Nodes.Add(item.ToString());
                    string s1 = $"{item.月份} ( {item.MonthCount} )";
                    TreeNode b = this.treeView1.Nodes.Add(s1);
                    foreach (var i in item.Mothgroup)
                    {
                        b.Nodes.Add(i.OrderDate.ToString());

                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var q = from i in this.dbContext.Order_Details.AsEnumerable()

                    select i;
            MessageBox.Show($"{q.Sum(p=>(decimal) p.UnitPrice*(decimal)p.Quantity*(decimal)(1-p.Discount)):c2}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var q = (from i in this.dbContext.Order_Details.AsEnumerable()
                     group i by i.Order.EmployeeID into g
                         //orderby i.UnitPrice*i.Quantity*(1-i.Discount) descending
                     select new
                     {
                         員工編號 = g.Key,總銷售金額=(g.Sum(p=>(decimal)p.UnitPrice*(decimal)p.Quantity*(decimal)(1-p.Discount)))

                     }).OrderByDescending(i=>i.總銷售金額).Take(5);
            
            dataGridView2.DataSource = q.ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var q = (from i in this.dbContext.Products
                    orderby i.UnitPrice descending
                    select new { 產品價格 = i.UnitPrice, 類別名稱 =i.Category.CategoryName }).Take(5);
            dataGridView2.DataSource = q.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var q = from i in this.dbContext.Products
                    group i by i.UnitPrice > 300 ? "有大於300":"沒有大於300" into g
                    select new { 產品搜尋結果=g.Key};

            dataGridView1.DataSource = q.ToList();
        }

        private void button34_Click(object sender, EventArgs e)
        {
            var q = from i in this.dbContext.Order_Details.AsEnumerable()
                    group i by i.Order.OrderDate.Value.Year into g
                    orderby g.Key descending
                    select new {
                        訂單年份=g.Key,總銷售金額=g.Sum(p=>p.UnitPrice*(decimal)p.Quantity*(decimal)(1-p.Discount)) };
            dataGridView1.DataSource = q.ToList();

            this.chart1.DataSource = q.ToList();
            this.chart1.Series[0].XValueMember = "訂單年份";
            this.chart1.Series[0].YValueMembers = "總銷售金額";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

           


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
