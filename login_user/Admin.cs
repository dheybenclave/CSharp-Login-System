using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace login_user
{
    public partial class Admin : Form
    {
        MySqlConnection connection;
        MySqlCommand command;
        string conns;
        public string iduser = "";
        public string position = "";
        public string username = "";
        public string password = "";
        public string idload, userload, passload, positionload = "";
        public string positionrecall = "";
        string idlistviewclick = "";
        string choiceuseradmin = "";
        public static string completechoice = "";
        public Admin()
        {


            InitializeComponent();

            schedlistall.MultiSelect = false;
           

            conns = "SERVER=localhost;PORT=3306;DATABASE=login_user;UID=root;PASSWORD=claveria;";
            connection = new MySqlConnection(conns);



        }

        private void Form3_Load(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            dateTimePicker1.Value = dt.Date;
            timer1.Start();

        }
        public void listviewlist()
        {
            if (label4.Text == "Admin")
            {

                string all = "SELECT * FROM login_user.new_table ORDER BY username ASC;";
                command = new MySqlCommand(all, connection);
                MySqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    if (rd.HasRows)
                    {
                        idload = rd["iduser"].ToString();
                        userload = rd["username"].ToString();
                        passload = rd["password"].ToString();
                        positionload = rd["position"].ToString();
                        ListViewItem itm = new ListViewItem(rd["iduser"].ToString());
                        itm.SubItems.Add(rd["username"].ToString());
                        itm.SubItems.Add(rd["password"].ToString());
                        itm.SubItems.Add(rd["position"].ToString());
                        allaccount.Items.Add(itm);
                    }
                }
                rd.Close();
            }
            else
            {

                string all = "SELECT * FROM login_user.new_table WHERE iduser='" + label3.Text + "';";
                command = new MySqlCommand(all, connection);
                MySqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    if (rd.HasRows)
                    {
                        idload = rd["iduser"].ToString();
                        userload = rd["username"].ToString();
                        passload = rd["password"].ToString();
                        positionload = rd["position"].ToString();
                        ListViewItem itm = new ListViewItem(rd["iduser"].ToString());
                        itm.SubItems.Add(rd["username"].ToString());
                        itm.SubItems.Add(rd["password"].ToString());
                        itm.SubItems.Add(rd["position"].ToString());
                        allaccount.Items.Add(itm);
                    }
                }
                rd.Close();

            }
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Admin_FormClosing(object sender, FormClosingEventArgs e)
        {

            DialogResult dlg = MessageBox.Show("Are you really sure you want to close this form?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {
                //insertion of sched
                DateTime time = DateTime.Now;
                DateTime daofweek = DateTime.Now;
                DateTime date = DateTime.Now;
                string asd = date.ToLongDateString();
                string sched = "INSERT INTO `login_user`.`schedulelist` (`username`, `position`, `days`, `date`, `time`, `status`) VALUES ('" + label1.Text + "', '" + label4.Text + "', '" + daofweek.DayOfWeek.ToString() + "', '" + asd.Replace(daofweek.DayOfWeek.ToString() + ", ", " ").Trim() + "', '" + time.ToLongTimeString() + "', '" + "LogOut" + "');";
                command = new MySqlCommand(sched, connection);
                command.ExecuteNonQuery();

                connection.Close();
                Application.ExitThread();
            }
            else { e.Cancel = true; }


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            connection.Open();
            string auds = "SELECT iduser,position FROM login_user.new_table where username ='" + label1.Text + "';";
            command = new MySqlCommand(auds, connection);
            MySqlDataReader rd = command.ExecuteReader();
            while (rd.Read())
            {

                iduser = rd["iduser"].ToString();
                position = rd["position"].ToString();

                label3.Text = iduser.ToString();
                label4.Text = position.ToString();         
                timer1.Stop();
            }
            rd.Close();

            //insertion of sched
            DateTime time = DateTime.Now;
            DateTime daofweek = DateTime.Now;
            DateTime date = DateTime.Now;
            string asd = date.ToLongDateString();
            string sched = "INSERT INTO `login_user`.`schedulelist` (`username`, `position`, `days`, `date`, `time`, `status`) VALUES ('" +
                label1.Text + "', '" + label4.Text + "', '" + daofweek.DayOfWeek.ToString() + "', '" + asd.Replace(daofweek.DayOfWeek.ToString()+", ", " ").Trim() +"', '" + time.ToLongTimeString() + "', '" + "LogIn" + "');";
            command = new MySqlCommand(sched, connection);
            command.ExecuteNonQuery();

            connection.Close();

            connection.Open();
            if (position == "Admin")
            {
                string all = "SELECT * FROM login_user.new_table order by username ASC;";
                command = new MySqlCommand(all, connection);
                MySqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    iduser = dr["iduser"].ToString();
                    username = dr["username"].ToString();
                    password = dr["password"].ToString();
                    position = dr["position"].ToString();

                    ListViewItem itm = new ListViewItem(iduser.ToString());
                    itm.SubItems.Add(username.ToString());
                    itm.SubItems.Add(password.ToString());
                    itm.SubItems.Add(position.ToString());
                    allaccount.Items.Add(itm);
                    if (label4.Text == "Admin")
                    {
                        button2.Visible = true;
                        button3.Visible = true;
                        button5.Visible = true;
                        button2.Enabled = false;
                        button3.Text = "Delete";

                    }
                    else { button2.Visible = true; }
                    timer1.Stop();
                }
                dr.Close();
                //For schedule para view sa listview
                string schedlist = "SELECT * FROM login_user.schedulelist;";
                command = new MySqlCommand(schedlist, connection);
                MySqlDataReader read1 = command.ExecuteReader();
                while (read1.Read())
                {
                    if (read1.HasRows)
                    {
                        ListViewItem itm = new ListViewItem(read1["position"].ToString());
                        itm.SubItems.Add(read1["username"].ToString());
                        itm.SubItems.Add(read1["status"].ToString());
                        itm.SubItems.Add(read1["days"].ToString());
                        itm.SubItems.Add(read1["date"].ToString());
                        itm.SubItems.Add(read1["time"].ToString());
                        schedlistall.Items.Add(itm);
                    }
                }
                read1.Close();

            }
            else
            {
                string only = "SELECT * FROM login_user.new_table where username ='" + label1.Text + "';";
                command = new MySqlCommand(only, connection);
                MySqlDataReader rd1 = command.ExecuteReader();
                while (rd1.Read())
                {
                    iduser = rd1["iduser"].ToString();
                    username = rd1["username"].ToString();
                    password = rd1["password"].ToString();
                    position = rd1["position"].ToString();
                    ListViewItem itm = new ListViewItem(iduser.ToString());
                    itm.SubItems.Add(username.ToString());
                    itm.SubItems.Add(password.ToString());
                    itm.SubItems.Add(position.ToString());
                    allaccount.Items.Add(itm);
                    if (label4.Text == "Admin")
                    {
                        button2.Visible = true;
                        button3.Visible = true;

                    }
                    else
                    {
                        button2.Visible = true;
                        button3.Visible = true;
                        button2.Enabled = false;
                        button3.Text = "Save";
                        button3.Enabled = false;
                    }
                   
                }
                rd1.Close();


                string schedlist = "SELECT * FROM login_user.schedulelist WHERE username ='"+label1.Text+"';";
                command = new MySqlCommand(schedlist, connection);
                MySqlDataReader read1 = command.ExecuteReader();
                while (read1.Read())
                {
                    if (read1.HasRows)
                    {
                        ListViewItem itm = new ListViewItem(read1["position"].ToString());
                        itm.SubItems.Add(read1["username"].ToString());
                        itm.SubItems.Add(read1["status"].ToString());
                        itm.SubItems.Add(read1["days"].ToString());
                        itm.SubItems.Add(read1["date"].ToString());
                        itm.SubItems.Add(read1["time"].ToString());
                        schedlistall.Items.Add(itm);
                    }
                    timer1.Stop();
                }
                read1.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {


            DialogResult dlg = MessageBox.Show("Are you really sure you want to close this form?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {
                //insertion of sched
                DateTime time = DateTime.Now;
                DateTime daofweek = DateTime.Now;
                DateTime date = DateTime.Now;
                string asd = date.ToLongDateString();
                string sched = "INSERT INTO `login_user`.`schedulelist` (`username`, `position`, `days`, `date`, `time`, `status`) VALUES ('" + label1.Text + "', '" + label4.Text + "', '" + daofweek.DayOfWeek.ToString() + "', '" + asd.Replace(daofweek.DayOfWeek.ToString() + ", ", " ").Trim() + "', '" + time.ToLongTimeString() + "', '" + "LogOut" + "');";
                command = new MySqlCommand(sched, connection);
                command.ExecuteNonQuery();

                connection.Close();

                Form1 asd1 = new Form1();
                asd1.Show();
                this.Hide();


            }
            else { }

        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {


            if (button4.Text == "on")
            {
                button4.Text = "off";
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                button4.Text = "on";
                textBox2.UseSystemPasswordChar = true; ;
            }
        }



        private void listView1_Click(object sender, EventArgs e)
        {

            if (label4.Text == "Admin")
            {
                button3.Text = "Delete";
            }
            else { }
            button2.Enabled = true;

            try
            {
                string select = allaccount.FocusedItem.SubItems[0].Text;
                idlistviewclick = select.ToString();
                groupBox1.Visible = true;
                groupBox2.Visible = true;
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;

                string query = "SELECT * FROM login_user.new_table where iduser =" + select.ToString() + ";";
                command = new MySqlCommand(query, connection);
                MySqlDataReader rd2 = command.ExecuteReader();
                while (rd2.Read())
                {
                    if (rd2.HasRows)
                    {
                        iduser = rd2["iduser"].ToString();
                        username = rd2["username"].ToString();
                        password = rd2["password"].ToString();
                        position = rd2["position"].ToString();

                        textBox1.Text = username;
                        textBox2.Text = password;
                        textBox3.Text = iduser;


                    }

                }
                rd2.Close();
            }
            catch
            {
                MessageBox.Show("Please select column to edit!");
            }



        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            positionrecall = "Admin";
            choiceuseradmin = "Admin";
            button3.Enabled = true;

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            choiceuseradmin = "User";
            button3.Enabled = true;
            positionrecall = "User";
        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            if (choiceuseradmin == "Admin")
                completechoice = "Admin";
            else completechoice = "User";

            if (groupBox1.Text == "Creating Account")
            {
                button2.Enabled = true;
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
   
                    string aud = "SELECT * FROM login_user.new_table WHERE username LIKE  '%" + textBox1.Text + "%';";
                    MySqlCommand cms = new MySqlCommand(aud, connection);
                    MySqlDataReader rd;
                    rd = cms.ExecuteReader();
                    if (!rd.HasRows)//kapag walang kapareho na  username or data sa database mag iinsert
                    {
                        string qwery = "INSERT INTO `login_user`.`new_table` (`username`, `password`, `position`) VALUES ('" + firstname + "', '" + textBox2.Text + "', '" + completechoice + "');";
                        rd.Close();


                        DialogResult dlg;
                        dlg = MessageBox.Show("Are you sure you want to save?", "Save?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dlg == DialogResult.Yes)
                        {
                            command = new MySqlCommand(qwery, connection);
                            command.ExecuteNonQuery();
                            MessageBox.Show("Success!");
                            groupBox1.Text = "Editing Account";
                            allaccount.Items.Clear();
                            listviewlist();
                            button5.Enabled = true;
                            textBox1.Text = "";
                            textBox2.Text = "";
                      
                        }
                        else
                        {
                            groupBox1.Text = "Editing Account";
                            textBox1.Text = "";
                            textBox2.Text = "";
                       
                        }
                    }
                    else
                    {
                        MessageBox.Show("duplicate account!");
                        textBox2.Text = "";
                        rd.Close();
                  
                    }
                }
            }
            else
            {

                if (button3.Text == "Save")
                {
                    if ((radioButton1.Checked == true) || (radioButton2.Checked == true))
                    {
                        allaccount.Items.Clear();
                        string update = "UPDATE `login_user`.`new_table` SET `username`='" + textBox1.Text + "', `password`='" + textBox2.Text + "', `position`='" + positionrecall.ToString() + "' WHERE `iduser`='" + iduser.ToString() + "';";
                        command = new MySqlCommand(update, connection);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Success!");
                        groupBox1.Text = "Editing Account";
                        listviewlist();
                    }
                    else
                    { MessageBox.Show("Please Complete the updating!"); }
                }
                else
                {
                    allaccount.Items.Clear();
                    string delete = "DELETE FROM `login_user`.`new_table` WHERE `iduser`='" + textBox3.Text + "';";
                    command = new MySqlCommand(delete, connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Success!");
                    groupBox1.Text = "Editing Account";
                    listviewlist();

                }

            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            groupBox1.Text = "Editing Account";
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            button3.Visible = true;
            button3.Text = "Save"; 
          

        }

        private void button5_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = true;
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            groupBox1.Text = "Creating Account";    
            button3.Text = "Save";
            button3.Enabled = false;
            button5.Enabled = false;
        }

        private void allaccount_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}