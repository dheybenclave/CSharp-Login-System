using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace login_user
{
    public partial class Form1 : Form
    {
        string usertext ="User";
        string admintext = "Admin";
        string choiceuseradmin = "";
         public  static string completechoice = "";
         public static string name1;
       
        MySqlConnection connection;
            MySqlCommand command;
            string conns;


           
          
        public Form1()
        {
            InitializeComponent();

            conns = "SERVER=localhost;PORT=3306;DATABASE=login_user;UID=root;PASSWORD=claveria;" ;
            connection = new MySqlConnection(conns);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)//Create button
        {
            label3.Text = "Create";

            groupBox1.Visible = true;
            //choosing for user or admin  ipapasa to sa completechoice 
            if (choiceuseradmin == admintext)// admin yung na choice
            {
                completechoice = "Admin";
            }
            else if (choiceuseradmin == usertext)//user yung na choice
            {
                completechoice = "User";
            }
            //


            if (label3.Text != "LogIn")
            {


                if ((textBox1.Text == "") && (textBox2.Text == ""))//kapag walang laman yung username and password
                {

                }
                else if (textBox2.Text == "")//kapag walang pass word
                {
                    MessageBox.Show("Please input your password!");
                    textBox2.Text = "";
                }
                else if (choiceuseradmin == "")// kapag yung string na choicuseradmin  wala pang text / and para sa radiobtn
                {
                    MessageBox.Show("Please complete the requirments!");
                
                }
                else
                {

                    string firstname = textBox1.Text.Remove(1).ToUpper() + textBox1.Text.Remove(0, 1);
                    connection.Open();
                    string aud = "SELECT * FROM login_user.new_table WHERE username LIKE  '%" + textBox1.Text + "%';";
                    MySqlCommand cms = new MySqlCommand(aud, connection);
                    MySqlDataReader rd;
                    rd = cms.ExecuteReader();
                    if (!rd.HasRows)//kapag walang kapareho na  username or data sa database mag iinsert
                    {
                        string qwery ="INSERT INTO `login_user`.`new_table` (`username`, `password`, `position`) VALUES ('"+ firstname +"', '"+ textBox2.Text +"', '"+completechoice+"');";
                        rd.Close();
                        

                        DialogResult dlg;
                        dlg = MessageBox.Show("Are you sure you want to save?", "Save?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dlg == DialogResult.Yes)
                        {
                            command = new MySqlCommand(qwery, connection);
                            command.ExecuteNonQuery();
                            MessageBox.Show("Success!");
                            textBox1.Text = "";
                            textBox2.Text = "";
                            connection.Close();
                        }
                        else
                        {
                            Admin form = new Admin();
                            form.Show();
                            textBox1.Text = "";
                            textBox2.Text = "";
                            connection.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("duplicate account!");
                        textBox2.Text = "";
                        rd.Close();
                        connection.Close();
                    }
                }
            }
            else
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)//Login button
        {
            DateTime dt = DateTime.Now;
        
            label3.Text = "LogIn";

            if ((textBox1.Text == "") && (textBox2.Text == ""))//kapag walang laman yung username and password na text
            {
                MessageBox.Show("Please input account!");
            }
            else if (textBox2.Text == "") // kapag walang laman yung password na text
            {
                MessageBox.Show("Please Type your password!");

            }
            else
            {
                try
                {
                connection.Open();
                string firstname = textBox1.Text.Remove(1).ToUpper() + textBox1.Text.Remove(0, 1);
                  
                    string aud = "SELECT * FROM login_user.new_table WHERE username='" + textBox1.Text + "' AND password = '" + textBox2.Text.ToString() + "';";
                    MySqlCommand cms = new MySqlCommand(aud, connection);
                    MySqlDataReader rd;
                    rd = cms.ExecuteReader();
                        if (rd.HasRows)// kapag walang nakitang kapareho sa databse na pangalan
                        {
                          

                            this.Hide();
                            Admin admin1 = new Admin();
                            admin1.Show();
                            admin1.timer1.Start();
                            progressbar prog = new progressbar();
                            prog.Show();
                            prog.timer1.Start();
                            admin1.label1.Text = firstname;
                            textBox2.Text = "";

                        }
                        else//kapag meron lalabas yung bagong form
                        {

                            MessageBox.Show("no account saved!");
                            connection.Close();
                        }
                        rd.Close();
                      
               
              
                }
                catch(Exception ex)
                { MessageBox.Show("No account saved!" + ex); }
               
            }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
           choiceuseradmin= "Admin";           
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            choiceuseradmin = "User";           
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            button3.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)//show/hide
        {

            if (button3.Text == "on")
            {
                button3.Text = "off";
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                button3.Text = "on";
                textBox2.UseSystemPasswordChar = true; ;
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dlg = MessageBox.Show("Are you really sure you want to close this form?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {
                Application.ExitThread();
            }
            else { e.Cancel = true; }
        
        }
    }
}
