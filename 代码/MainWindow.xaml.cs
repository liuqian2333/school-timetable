using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace 课程管理系统
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private 功能 w2;
        public string connectionstring = "server=localhost;user=root;database=school;port=3306;password=chengzhi0454?";
        public MySqlConnection Connection
        {
            set;
            get;
        }
        public MainWindow()
        {
            InitializeComponent();
            //Init();
        }
        private void Button_MouseDoubleClick(Object obj, RoutedEventArgs e)
        {

        }
        private void Button1_Click(Object obj, RoutedEventArgs e)
        {
            
            string name1 = TextBox1.Text.ToString();
            string password = TextBox2.Text.ToString();
        
                Connection = new MySqlConnection(connectionstring);
                while (Connection == null)
                {
                    MessageBox.Show("登录失败", "出错", MessageBoxButton.OK);
                    if (MessageBox.Show("是否重新连接", "出错", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    {
                        //退出程序。
                        Close();
                    }
                    else
                    {
                        Connection = new MySqlConnection(connectionstring);
                    }
                }
                MessageBox.Show("连接成功", "连接数据库成功", MessageBoxButton.OK);
                Connection.Open();
            MySqlCommand cmd = new MySqlCommand("select password from studentlogin where studentid='" + name1 + "';", Connection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            if(rdr.HasRows)
            {
                if (rdr[0].ToString() == password)
                {
                    rdr.Close();
                    MessageBox.Show("登录成功", "登录成功", MessageBoxButton.OK);
                    w2 = new 功能(Connection, name1, password);
                    /*{
                        Name1 = name,
                        Password1 = password,
                        Connection1 = Connection
                    };*/
                    w2.Show();
                    this.Owner = w2;
                }
                else
                {
                    MessageBox.Show("用户或密码错误", "用户或密码错误", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("用户或密码错误", "用户或密码错误", MessageBoxButton.OK);
            }
            //while true
            
            //if(w2 != null)
            //{
              //  w2.Close();
            //}
            
        }//button
         /*
          <Grid>
             <ListBox x:Name="Lst">
                 <ListBox.ItemTemplate>
                     <DataTemplate>
                         <Button MouseDoubleClick="Button_MouseDoubleClick">
                             <Grid>
                                 <Image Source="{Binding Path=BackGround}" />
                                 <TextBlock Text="{Binding Path=Name}" Margin="70 10" FontSize="18"></TextBlock>
                             </Grid>
                         </Button>
                     </DataTemplate>
                 </ListBox.ItemTemplate>
             </ListBox>
         </Grid>
              */
         /* private void Init()
          {
              Lst.ItemsSource = new List<UserItem>
              {
                  new UserItem(1,"张三",true)
                  ,new UserItem(2,"李四",true)
                  ,new UserItem(3,"赵五",true)
                  ,new UserItem(4,"钱六",true)
                  ,new UserItem(5,"孙七",false)
                  ,new UserItem(6,"李八",false)
                  ,new UserItem(7,"王九",false)
                  ,new UserItem(8,"陈十",false)
                  ,new UserItem(9,"吴万",false)
                  ,new UserItem(10,"刘十八",false)
              };
          }
      }//class*/
        public class UserItem
        {
            public UserItem(int Id, string Name, bool IsActived)
            {
                this.Id = Id;
                this.Name = Name;
                this.IsActived = IsActived;
            }
            public int Id { get; set; }
            public string Name { get; set; }
            public bool IsActived { get; set; }
            public string BackGround
            {
                get
                {
                    return IsActived
                        ? "/test;component/Assets/Images/UserItemNull.png"
                        : "/test;component/Assets/Images/UserItemNullg.png";
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}