﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_1 : Form
    {
        public Frm作業_1()
        {
            InitializeComponent();

            students_scores = new List<Student>()
                                         {
                                            new Student{ Name = "aaa", Class = "CS_101", Chi = 60, Eng = 80, Math = 50, Gender = "Male" },
                                            new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 88, Math = 100, Gender = "Male" },
                                            new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 77, Math = 75, Gender = "Female" },
                                            new Student{ Name = "ddd", Class = "CS_102", Chi = 85, Eng = 72, Math = 85, Gender = "Female" },
                                            new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 89, Math = 50, Gender = "Female" },
                                            new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 99, Gender = "Female" },
                                            new Student{ Name = "aahty", Class = "CS_102", Chi = 80, Eng = 55, Math = 80, Gender = "Female" },
                                            new Student{ Name = "bbcttt", Class = "CS_102", Chi = 80, Eng = 30, Math = 100, Gender = "Male" },
                                            new Student{ Name = "cccttt", Class = "CS_102", Chi = 40, Eng = 80, Math = 100, Gender = "Male" }
                                          };
        }

        List<Student> students_scores;

        public class Student
        {
            public string Name { get; set; }
            public string Class { get; set; }
            public int Chi { get; set; }
            public int Eng { get; internal set; }
            public int Math { get; set; }
            public string Gender { get; set; }
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            //this.nwDataSet1.Products.Take(10);//Top 10 Skip(10)

            //Distinct()
        }

        private void button14_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files =  dir.GetFiles();

            this.dataGridView1.DataSource = files;
        }

        private void button36_Click(object sender, EventArgs e)
        {
            // 共幾個 學員成績 ?

            //this.dataGridView1.DataSource= students_scores;

            // 找出 前面三個 的學員所有科目成績	
            //IEnumerable<Student> q = (from n in students_scores
            //                          orderby n.Name 
            //                          select n).Take(3);
            //this.dataGridView1.DataSource = q.ToList();
            // 找出 後面兩個 的學員所有科目成績	
            //IEnumerable<Student> q1 = (from n in students_scores
            //                           orderby n.Name descending
            //                           select n).Take(2);
            //this.dataGridView1.DataSource = q1.ToList();
            // 找出 Name 'aaa','bbb','ccc' 的學成績		
            IEnumerable<Student> q = from n in students_scores
                                     where n.Name.ToLower() == "aaa" || n.Name.ToLower() == "bbb" || n.Name.ToLower() == "ccc"
                                     select n;
            this.dataGridView1.DataSource = q.ToList();


            #region 搜尋 班級學生成績

            // 
            // 共幾個 學員成績 ?



            // 找出 前面三個 的學員所有科目成績					
            // 找出 後面兩個 的學員所有科目成績					

            // 找出 Name 'aaa','bbb','ccc' 的學成績						

            // 找出學員 'bbb' 的成績	                          

            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	


            // 數學不及格 ... 是誰 
            #endregion

        }

        private void button37_Click(object sender, EventArgs e)
        {
            //new {.....  Min=33, Max=34.}

            // 找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績  |
            //var q = from n in students_scores
            //        where n.Name.ToLower() == "aaa" || n.Name.ToLower() == "bbb" || n.Name.ToLower() == "ccc"
            //        select new {Name=n.Name,chi= n.Chi,math=n.Math };
            //this.dataGridView1.DataSource = q.ToList();

            //個人 所有科的  sum, min, max, avg
            var q1 = students_scores.Select(n => new { n.Name, Sum = new int[] { n.Chi, n.Eng, n.Math }.Sum(), Min = new int[] { n.Chi, n.Eng, n.Math }.Min(),
                                                               Max=new int[] { n.Chi,n.Eng,n.Math}.Max(),Avg=new int[] { n.Chi, n.Eng, n.Math }.Average().ToString("0.00")});

            this.dataGridView1.DataSource = q1.ToList();

        }
    }
}
