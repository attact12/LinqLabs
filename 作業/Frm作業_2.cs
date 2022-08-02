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
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent();
            this.productPhotoTableAdapter1.Fill(work2019DataSet11.ProductPhoto);
            
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var q = from n in work2019DataSet11.ProductPhoto
                    select n;
            this.dataGridView1.DataSource = q.ToList();
        }





        private void comboBox3_DropDown(object sender, EventArgs e)
        {
            this.comboBox3.Items.Clear();
            IEnumerable<int> q = from file in work2019DataSet11.ProductPhoto
                                     //where file.OrderDate.Year==1996
                                 select file.ModifiedDate.Year;
            foreach (int i in q.Distinct())
                this.comboBox3.Items.Add(i);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string year = comboBox3.Text;
            int Year = int.Parse(year);
            var q = from file in work2019DataSet11.ProductPhoto
                                 where file.ModifiedDate.Year == Year
                                 select file;

            this.dataGridView1.DataSource = q.ToList();
        }
        

        private void button10_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "第一季")
            {
                a = first[0];
                b = first[1];
                c = first[2];
            }
            else if (comboBox2.Text == "第二季")
            {
                a = sec[0];
                b = sec[1];
                c = sec[2];
            }
            else if (comboBox2.Text == "第三季")
            {
                a = third[0];
                b = third[1];
                c = third[2];
            }
            else
            {
                a = forth[0];
                b = forth[1];
                c = forth[2];
            }
            var q = from n in work2019DataSet11.ProductPhoto
                        //group n by n.ModifiedDate.Month into g
                    where n.ModifiedDate.Month == a || n.ModifiedDate.Month == b || n.ModifiedDate.Month == c
                    select n;
            dataGridView1.DataSource = q.ToList();
        }
        int[]first={1,2,3};
        int[] sec = { 4, 5, 6 };
        int[] third = { 7, 8, 9 };
        int[] forth = { 10, 11, 12 };
        int a, b, c;

        private void comboBox2_DropDown(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var q = work2019DataSet11.ProductPhoto.Where(n => n.ModifiedDate > dateTimePicker1.Value && n.ModifiedDate < dateTimePicker2.Value).Select(n => n);

            this.dataGridView1.DataSource = q.ToList();
        }

        //作業圖像參考https://www.google.com/search?q=byte+to+image+C%23&rlz=1C1ONGR_zh-TWTW949TW949&ei=XGjiYo64AYrO0QSpzr7YDw&ved=0ahUKEwiO1pins5v5AhUKZ5QKHSmnD_sQ4dUDCA4&uact=5&oq=byte+to+image+C%23&gs_lcp=Cgdnd3Mtd2l6EAMyBQgAEIAEMgQIABAeMgQIABAeMgYIABAeEAgyBggAEB4QBTIGCAAQHhAFMgYIABAeEAUyBggAEB4QBTIGCAAQHhAFMgYIABAeEAg6BwgAEEcQsAM6BAgAEENKBAhBGABKBAhGGABQyARYrQpg6A9oAXABeACAAXWIAYgCkgEDMi4xmAEAoAEByAEKwAEB&sclient=gws-wiz
        //作業圖像參考https://www.google.com/search?q=ienumerable+byte+to+byte&rlz=1C1ONGR_zh-TWTW949TW949&oq=ienumerable+b&aqs=chrome.1.69i57j0i512l6j0i30l3.16462j1j7&sourceid=chrome&ie=UTF-8
    }
}
