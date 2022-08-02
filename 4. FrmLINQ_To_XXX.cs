using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Starter
{
    public partial class FrmLINQ_To_XXX : Form
    {
        public FrmLINQ_To_XXX()
        {
            InitializeComponent();
            this.categoriesTableAdapter1.Fill(nwDataSet1.Categories);
            this.productsTableAdapter1.Fill(nwDataSet1.Products);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int[] arr = { 1, 2, 3, 4, 5, 7, 6, 8, 9, 10 };
            IEnumerable<IGrouping<string,int>> q = from n in arr
                                                group n by n % 2 ==0 ?"偶數":"奇數";

            var q1 = arr.GroupBy(n => n % 2==0 ? "偶數":"奇數");
            dataGridView1.DataSource = q1.ToList();
            //======================================
            foreach (var n in q1)
            {
                TreeNode nod =this.treeView1.Nodes.Add(n.Key.ToString());
                
                foreach (var item in n)
                {
                    nod.Nodes.Add(item.ToString());
                }
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            int[] arr = { 1, 2, 3, 4, 5, 7, 6, 8, 9, 10 };
            var q = from n in arr
                    group n by n % 2 == 0 ? "偶數" : "奇數" into g
                    select new { Mykey = g.Key, Mycount = g.Count(), MyAve = g.Average(),Mygroup=g };

            var q1 = arr.GroupBy(n => n % 2 == 0 ? "偶數" : "奇數").Select(n=>new { Mykey = n.Key, Mycount = n.Count(), MyAve = n.Average(), Mygroup = n });

            dataGridView1.DataSource = q1.ToList();

            foreach (var n in q1)
            {
                string a = $"{n.Mykey}({n.Mycount})";
                TreeNode nod = this.treeView1.Nodes.Add(a);

                foreach (var item in n.Mygroup)
                {
                    nod.Nodes.Add(item.ToString());
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] arr = { 1, 2, 3, 4, 5, 7, 6, 8, 9, 10 ,11,12,13};
            var q = from n in arr
                    group n by Mykey(n) into g
                    select new { Mykey = g.Key, Mycount = g.Count(), MyAve = g.Average(), Mygroup = g };

            var q1 = arr.GroupBy(n => Mykey(n)  /*n % 2 == 0 ? "偶數" : "奇數"*/).Select(n => new { Mykey = n.Key, Mycount = n.Count(), MyAve = n.Average(), Mygroup = n });
            dataGridView1.DataSource = q.ToList();
            foreach (var n in q1)
            {
                string a = $"{n.Mykey}({n.Mycount})";
                TreeNode nod = this.treeView1.Nodes.Add(a);

                foreach (var item in n.Mygroup)
                {
                    nod.Nodes.Add(item.ToString());
                }
            }
            //============================================================================================
            this.chart1.DataSource = q1.ToList();
            this.chart1.Series[0].XValueMember = " Mykey";
            this.chart1.Series[0].YValueMembers = "Mycount";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            this.chart1.Series[1].XValueMember = " Mykey";
            this.chart1.Series[1].YValueMembers = "MyAve";
            this.chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

        }

        private string Mykey(int n)
        {
            if (n <= 3)
                return "小";
            else if (n <= 10)
                return "中";
            else
                return "大";
        }

        private void button38_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from n in files
                    group n by n.Extension into g
                    select new {Mykey= g.Key, Mycount = g.Count(),Mygroup=g };

            foreach (var n in q)
            {
                string a = $"{n.Mykey}({n.Mycount})";
                TreeNode nod = this.treeView1.Nodes.Add(a);

                foreach (var item in n.Mygroup)
                {
                    nod.Nodes.Add(item.ToString());
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.ordersTableAdapter1.Fill(nwDataSet1.Orders);
            var q = from n in nwDataSet1.Orders
                    group n by n.OrderDate.Year into g
                    select new { Mykey = g.Key, Mycount = g.Count(), Mygroup = g };
            foreach (var n in q)
            {
                string a = $"{n.Mykey}({n.Mycount})";
                TreeNode nod = this.treeView1.Nodes.Add(a);

                foreach (var item in n.Mygroup)
                {
                    nod.Nodes.Add(item.ToString());
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from i in files
                    let s = i.Extension
                    where s == ".exe"
                    select s;
            MessageBox.Show(".exe count:" + q.Count());
        }

        private void button15_Click(object sender, EventArgs e)
        {
            int[] num1 = { 1, 2, 3, 45, 66 };
            int[] num2 = { 1, 2, 3, 44, 77, 88, 2 };

            var q = num1.Intersect(num2);
            q = num2.Distinct();
            bool q1 = num1.Any(n => n > 100);
            q1 = num2.All(n => n > 100);
            q = num2.Take(2);


        }

        private void button10_Click(object sender, EventArgs e)
        {
            var q = from i in nwDataSet1.Products
                    group i by i.CategoryID into g
                    orderby g.Key
                    select new { CategoryID = g.Key, AvgUnitPrice = g.Average(i => i.UnitPrice) };

            this.dataGridView1.DataSource = q.ToList();

            var q1=from c in nwDataSet1.Categories 
                   join i in nwDataSet1.Products on c.CategoryID equals i.CategoryID
                   group i by c.CategoryName into g
                   select new { CategoryID = g.Key, AvgUnitPrice = g.Average(i => i.UnitPrice) };
            this.dataGridView2.DataSource = q1.ToList();
        }


    }
}
