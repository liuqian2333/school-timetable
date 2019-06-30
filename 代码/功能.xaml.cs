using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace 课程管理系统
{
    /// <summary>
    /// 功能.xaml 的交互逻辑
    /// </summary>
    public partial class 功能 : Window
    {
        public MySqlConnection Connection1
        {
            set;
            get;
        }
        
        public string Password1
        {
            set;
            get;
        }
        public string  Name1
        {
            set;
            get;
        }

        //功能 窗口构造函数
        public 功能(MySqlConnection connection, string name, string password)
        {
            Password1 = password;
            Connection1 = connection;
            Name1 = name;
            //MainWindow m1 = new MainWindow();
            InitializeComponent();
            我的课程_grid1_data data2 = new 我的课程_grid1_data();
            data2.Init(Connection1, Name1, this.我的课程_grid1);
            我的课程_grid1_data data3 = new 我的课程_grid1_data();
            data3.选课_init(Connection1, this.选课_grid1);
            我的课程_grid1_data data4 = new 我的课程_grid1_data();
            data4.Init2(Connection1, Name1, this.选课_grid2);

            //公告
            公告_announcment data5 = new 公告_announcment();
            data5.Init(Connection1, 公告_grid1);

            //讨论
            讨论_discussion data6 = new 讨论_discussion();
            data6.Init(Connection1, 讨论_grid1);
        }
        private void 功能_我的课程_listbox1_init()
        {
            //功能_我的课程_textblock1.Text;
        }

        //确认选课
        private void 我的课程_button1click(Object sender, RoutedEventArgs e)
        {

        }

        //底部的超链接
        private void 我的课程_hyperlinkclick1(Object sender, RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            Process.Start(link.NavigateUri.AbsoluteUri);
        }

        //选课——checked
        private void 选课_Checked(Object sender, RoutedEventArgs e)
        {
            Button button1 = sender as Button;
            string course_id = button1.Content.ToString();
            string str1 = "insert ignore into student_course_selected_tempary value ('";
            string str2 = "', '";
            string str3 = "');";
            MySqlCommand cmd = new MySqlCommand(str1 + Name1 + str2 + course_id + str3, Connection1);
            cmd.ExecuteNonQuery();
            MessageBox.Show("选课成功", "选课成功", MessageBoxButton.OK);
            var list = new List<我的课程_grid1_data>();
            this.选课_grid2.DataContext = list;
            讨论_discussion data6 = new 讨论_discussion();
            data6.Init(Connection1, 讨论_grid1);

        }
        private void 选课_Unchecked(Object sender, RoutedEventArgs e)
        {
            Button button1 = sender as Button;
            string course_id = button1.Content.ToString();
            string str1 = "delete from student_course_selected_tempary where student_id ='";
            string str2 = "' and id='";
            string str3 = "';";
            MySqlCommand cmd = new MySqlCommand(str1 + Name1 + str2 + course_id + str3, Connection1);
            cmd.ExecuteNonQuery();
            MessageBox.Show("退课成功", "退课成功", MessageBoxButton.OK);
            var list = new List<我的课程_grid1_data>();
            this.选课_grid2.DataContext = list;
            我的课程_grid1_data data4 = new 我的课程_grid1_data();
            data4.Init2(Connection1, Name1, this.选课_grid2);

        }

        //提交讨论
        public void 讨论_click1(Object sender, RoutedEventArgs e)
        {
            string id = 讨论_学号.Text;
            string content = 讨论_讨论内容.Text;
            string str1 = "insert ignore into discussion value ('";
            string str2 = "', '";
            string str3 = "');";
            MySqlCommand cmd = new MySqlCommand(str1 + id + str2 + content + str3, Connection1);
            cmd.ExecuteNonQuery();
            MessageBox.Show("发布成功", "发布成功", MessageBoxButton.OK);
            var list = new List<讨论_discussion>();
            this.讨论_grid1.DataContext = list;
            我的课程_grid1_data data4 = new 我的课程_grid1_data();
            data4.Init2(Connection1, Name1, this.选课_grid2);

        }
    }

    //已选定课程
    class 我的课程_grid1_data
    {
        public string Courseid
        {
            get;
            set;
        }
        public string Coursename
        {
            get;
            set;
        }
        public string Courseteacher
        {
            get;
            set;
        }
        public string Coursecredit
        {
            get;
            set;
        }
        public string Coursepreid
        {
            get;
            set;
        }
        public string Coursetime
        {
            get;
            set;
        }
        public string Coursenotation
        {
            get;
            set;
        }
        public Button Button1
        {
            get;
            set;
        }

        //初始化选课窗口
        //功能-选课-grid1
        public void 选课_init(MySqlConnection Connection, Grid grid1)
        {
            var list = new List<我的课程_grid1_data>();
            
            string str1 = "select * from student_course;";
            
            MySqlCommand cmd = new MySqlCommand(str1, Connection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                我的课程_grid1_data data = new 我的课程_grid1_data();
                data.Courseid = rdr[0].ToString();
                data.Coursename = rdr[1].ToString();
                data.Courseteacher = rdr[2].ToString();
                data.Coursecredit = rdr[3].ToString();
                data.Coursepreid = rdr[4].ToString();
                data.Coursenotation = rdr[5].ToString();
                data.Coursetime = rdr[6].ToString();
                data.Button1 = new Button();
                data.Button1.Content = data.Courseid;
                list.Add(data);

            }
            grid1.DataContext = list;
            rdr.Close();
        }

        //显示已有的课程
        //功能_我的课程——grid
        public void Init(MySqlConnection Connection, string name1, Grid grid1)
        {
            
            var list = new List<我的课程_grid1_data>();
            //我的课程_grid1_data data = new 我的课程_grid1_data();
            string str1 = "select student_course_old.id, name, teacher, credit, preid, notation, time from student_course_old natural join student_course_selected where student_course_selected.student_id='";
            string str2 = "';";
            MySqlCommand cmd = new MySqlCommand(str1 + name1 + str2, Connection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            
            while(rdr.Read())
            {
                我的课程_grid1_data data = new 我的课程_grid1_data();
                data.Courseid = rdr[0].ToString();
                data.Coursename = rdr[1].ToString();
                data.Courseteacher = rdr[2].ToString();
                data.Coursecredit = rdr[3].ToString();
                data.Coursepreid = rdr[4].ToString();
                data.Coursenotation = rdr[5].ToString();
                data.Coursetime = rdr[6].ToString();
                
                list.Add(data);
                
            }
            grid1.DataContext = list;
            rdr.Close();
        }
        
        //显示已临时选择的课程
        //功能-选课-grid2
        public void Init2(MySqlConnection Connection, string name1, Grid grid1)
        {

            var list = new List<我的课程_grid1_data>();
            //我的课程_grid1_data data = new 我的课程_grid1_data();
            string str1 = "select student_course.id, name, teacher, credit, preid, notation, time from student_course natural join student_course_selected_tempary where student_course_selected_tempary.student_id='";
            string str2 = "';";
            MySqlCommand cmd = new MySqlCommand(str1 + name1 + str2, Connection);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                我的课程_grid1_data data = new 我的课程_grid1_data();
                data.Courseid = rdr[0].ToString();
                data.Coursename = rdr[1].ToString();
                data.Courseteacher = rdr[2].ToString();
                data.Coursecredit = rdr[3].ToString();
                data.Coursepreid = rdr[4].ToString();
                data.Coursenotation = rdr[5].ToString();
                data.Coursetime = rdr[6].ToString();

                list.Add(data);

            }
            grid1.DataContext = list;
            rdr.Close();
        }
    }//class 我的课程_grid1_data

    //用于公告的类
    class 公告_announcment
    {
        public string Announcmenttime
        {
            set;
            get;
        }
        public string Announcmentcontent
        {
            set;
            get;
        }
        public void Init(MySqlConnection Connection, Grid grid1)
        {
            var list = new List<公告_announcment>();

            string str1 = "select * from announcment";
            //string str2 = "';";
            MySqlCommand cmd = new MySqlCommand(str1, Connection);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                公告_announcment data = new 公告_announcment();
                data.Announcmenttime = rdr[0].ToString();
                data.Announcmentcontent = rdr[1].ToString();
                

                list.Add(data);

            }
            grid1.DataContext = list;
            rdr.Close();
        }
    }//class

    //讨论
    class 讨论_discussion
    {
        public string Discussionperson
        {
            set;
            get;
        }
        public string Discussioncontent
        {
            set;
            get;
        }
        public void Init(MySqlConnection Connection, Grid grid1)
        {
            var list = new List<讨论_discussion>();

            string str1 = "select * from discussion";
      
            MySqlCommand cmd = new MySqlCommand(str1, Connection);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                讨论_discussion data = new 讨论_discussion();
                data.Discussionperson = rdr[0].ToString();
                data.Discussioncontent = rdr[1].ToString();


                list.Add(data);

            }
            grid1.DataContext = list;
            rdr.Close();
        }
    }
}