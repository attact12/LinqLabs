using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LinqLabs;

namespace Starter
{
    public partial class FrmLinq_To_Entity : Form
    {
        public FrmLinq_To_Entity()
        {
            InitializeComponent();
            dbIndex.Database.Log = Console.WriteLine;//查看轉換SQL過程於輸出視窗

        }


        NorthwindEntities dbIndex = new NorthwindEntities();
        private void button1_Click(object sender, EventArgs e)
        {
           
            var q = from d in dbIndex.Products
                    where d.UnitPrice > 30
                    select d;
            dataGridView1.DataSource = q.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = this.dbIndex.Categories.First().Products.ToList();

            MessageBox.Show( this.dbIndex.Products.First().Category.CategoryName);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            var q = from i in this.dbIndex.Products
                    orderby i.UnitsInStock descending, i.UnitPrice//
                    select i;

            dataGridView1.DataSource = q.ToList();

            var q1 = dbIndex.Products.OrderByDescending(i => i.UnitsInStock).ThenBy(i => i.UnitPrice);
            dataGridView2.DataSource = q1.ToList();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var q = from p in dbIndex.Products.AsEnumerable()
                    group p by p.Category.CategoryName into g
                    select new { CategoryName = g.Key, Ave =$"{ g.Average(p => p.UnitPrice)}" };

            dataGridView1.DataSource = q.ToList();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            bool? a;//?等於變數可為空值，故a可能為true，false，null
            
            
            var q = from o in dbIndex.Orders
                    group o by o.OrderDate.Value.Year into g
                    select new { Year=g.Key, Count = g.Count() };
            dataGridView1.DataSource = q.ToList();
        }

        private void button55_Click(object sender, EventArgs e)
        {
            this.dbIndex.Products.Add(new Product { ProductName = "xxx", Discontinued = false });
            dbIndex.SaveChanges();
            MessageBox.Show("新增成功!");
        }
    }
}
