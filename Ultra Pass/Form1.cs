using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Ultra_Pass
{
    public partial class Form1 : Form
    {

       
        public Form1()
        {
            InitializeComponent();
            
        }

        public void setGmail(string value)
        {
            textBox1.Text = value;
            pictureBox6.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            pictureBox7.Visible = false;
            pictureBox13.Visible = true;
            textBox2.Select();
        }
       
        private void Form1_Load_1(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Checkbox == true && Properties.Settings.Default.Textbox1 != "" && Properties.Settings.Default.Textbox2 != "")
            { 
            textBox1.Text = Properties.Settings.Default.Textbox1;
            textBox2.Text = Properties.Settings.Default.Textbox2;
            pictureBox9.Visible = Properties.Settings.Default.Checkbox;
            }
            textBox3.Select();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox2.Text == "" ^ textBox2.Text == " ")
            {
                label2.Visible = false;
                pictureBox6.Visible = false;
                label3.Visible = true;
                pictureBox7.Visible = true;
            }
            else
            {
                label2.Visible = false;
                pictureBox6.Visible = false;

            }
            textBox2.Visible = false;
            textBox2.Visible = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "" ^ textBox2.Text == " ")
            {
                label2.Visible = false;
                pictureBox6.Visible = false;
                label3.Visible = true;
                pictureBox7.Visible = true;
            }
            else
            {
                label2.Visible = false;
                pictureBox6.Visible = false;

            }
            textBox2.Visible = false;
            textBox2.Visible = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" ^ textBox2.Text == " ")
            {
                label2.Visible = false;
                pictureBox6.Visible = false;
                label3.Visible = true;
                pictureBox7.Visible = true;
            }
            else
            {
                label2.Visible = false;
                pictureBox6.Visible = false;

            }
            textBox1.Select();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" ^ textBox2.Text == " ")
            {
                label2.Visible = false;
                pictureBox6.Visible = false;
                label3.Visible = true;
                pictureBox7.Visible = true;
            }
            else
            {
                label2.Visible = false;
                pictureBox6.Visible = false;

            }
            textBox1.Select();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" ^ textBox2.Text == " ")
            {
                label2.Visible = false;
                pictureBox6.Visible = false;
                label3.Visible = true;
                pictureBox7.Visible = true;
            }
            else
            {
                label2.Visible = false;
                pictureBox6.Visible = false;

            }
            textBox1.Select();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            pictureBox9.Visible = false;
            pictureBox10.Visible = true;
            Properties.Settings.Default.Reset();
        }
      
        private void pictureBox10_Click_1(object sender, EventArgs e)
        {
            pictureBox10.Visible = false;
            pictureBox9.Visible = true;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            label3.Visible = true;
            pictureBox7.Visible = true;
            label2.Visible = true;
            pictureBox6.Visible = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" ^ textBox2.Text == " ")
            {
                label2.Visible = false;
                pictureBox6.Visible = false;
                label3.Visible = true;
                pictureBox7.Visible = true;
            }
            else
            {
                label2.Visible = false;
                pictureBox6.Visible = false;
                

            }
            textBox1.Select();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "" ^ textBox1.Text == " ")
            {
                label3.Visible = false;
                pictureBox7.Visible = false;
                label2.Visible = true;
                pictureBox6.Visible = true;
            }
            else
            {
                label3.Visible = false;
                pictureBox7.Visible = false;

            }
           
            textBox1.Visible = false;
            textBox1.Visible = true;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" ^ textBox1.Text == " ")
            {
                label3.Visible = false;
                pictureBox7.Visible = false;
                label2.Visible = true;
                pictureBox6.Visible = true;
            }
            else
            {
                label3.Visible = false;
                pictureBox7.Visible = false;

            }
            pictureBox13.Visible = true;
            textBox2.Select();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" ^ textBox1.Text == " ")
            {
                label3.Visible = false;
                pictureBox7.Visible = false;
                label2.Visible = true;
                pictureBox6.Visible = true;
            }
            else
            {
                label3.Visible = false;
                pictureBox7.Visible = false;

            }
            pictureBox13.Visible = true;
            textBox2.Select();

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" ^ textBox1.Text == " ")
            { 
                label3.Visible = false;
                pictureBox7.Visible = false;
                label2.Visible = true;
                pictureBox6.Visible = true;
            }
            else
            {
                label3.Visible = false;
                pictureBox7.Visible = false;
              
            }
            pictureBox13.Visible = true;
            textBox2.Select();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" ^ textBox1.Text == " ")
            {
                label3.Visible = false;
                pictureBox7.Visible = false;
                label2.Visible = true;
                pictureBox6.Visible = true;
            }
            else
            {
                label3.Visible = false;
                pictureBox7.Visible = false;

            }
            pictureBox13.Visible = true;
            textBox2.Select();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            label3.Visible = true;
            pictureBox7.Visible = true;
            label2.Visible = true;
            pictureBox6.Visible = true;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            


        }

        private void label1_Click(object sender, EventArgs e)
        {
            label3.Visible = true;
            pictureBox7.Visible = true;
            label2.Visible = true;
            pictureBox6.Visible = true;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            forgot_password f6 = new forgot_password();
            f6.Show();
            this.Hide();
        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
            label6.ForeColor = Color.Black;
        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {
            label6.ForeColor = Color.FromArgb(64,64,64);
        }

        private void pictureBox8_MouseEnter(object sender, EventArgs e)
        {
            pictureBox8.BackColor = Color.FromArgb(253, 136, 166);
            label5.BackColor = Color.FromArgb(253, 136, 166);
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            pictureBox8.BackColor = Color.FromArgb(253, 136, 166);
            label5.BackColor = Color.FromArgb(253, 136, 166);
        }

        private void pictureBox8_MouseLeave(object sender, EventArgs e)
        {
            pictureBox8.BackColor = Color.FromArgb(252, 107, 143);
            label5.BackColor = Color.FromArgb(252, 107, 143);
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = false;
            pictureBox13.Visible = false;
            pictureBox1.Visible = true;
            if (textBox1.Text == "" ^ textBox1.Text == " ")
            {
                label3.Visible = false;
                pictureBox7.Visible = false;
                label2.Visible = true;
                pictureBox6.Visible = true;
            }
            else
            {
                label3.Visible = false;
                pictureBox7.Visible = false;

            }
            textBox1.Visible = false;
            textBox1.Visible = true;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
            pictureBox13.Visible = true;
            pictureBox1.Visible = false;
            if (textBox1.Text == "" ^ textBox1.Text == " ")
            {
                label3.Visible = false;
                pictureBox7.Visible = false;
                label2.Visible = true;
                pictureBox6.Visible = true;
            }
            else
            {
                label3.Visible = false;
                pictureBox7.Visible = false;

            }
            textBox1.Visible = false;
            textBox1.Visible = true;
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
             if (textBox1.Text == "" ^ textBox1.Text == " ")
            {
                label3.Visible = false;
                pictureBox7.Visible = false;
                label2.Visible = true;
                pictureBox6.Visible = true;
            }
            else
            {
                label3.Visible = false;
                pictureBox7.Visible = false;

            }
            pictureBox13.Visible = true;
            textBox1.Visible = false;
            textBox1.Visible = true;
        
        }

        private void label9_MouseEnter(object sender, EventArgs e)
        {
            label9.ForeColor = Color.Black;
        }

        private void label9_MouseLeave(object sender, EventArgs e)
        {
            label9.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void pictureBox8_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox8.BackColor = Color.FromArgb(251, 55, 104);
            label5.BackColor = Color.FromArgb(251, 55, 104);
        }

        private void pictureBox8_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox8.BackColor = Color.FromArgb(253, 136, 166);
            label5.BackColor = Color.FromArgb(253, 136, 166);
        }

        private void label5_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox8.BackColor = Color.FromArgb(251, 55, 104);
            label5.BackColor = Color.FromArgb(251, 55, 104);
        }

        private void label5_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox8.BackColor = Color.FromArgb(253, 136, 166);
            label5.BackColor = Color.FromArgb(253, 136, 166);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            
            
        }

        private void label5_Click(object sender, EventArgs e)
        {
            
            
        }


        private void pictureBox11_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Active = false;
        }

        private void pictureBox11_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Active = true;
            toolTip1.Show("Close", pictureBox11);
        }

        private void pictureBox12_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Active = true;
            toolTip1.Show("Minimize", pictureBox12);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            Error f4 = new Error();
            Thread thread1 = new Thread(() =>
           {
               Action action12 = () => pictureBox22.Visible = true;
               this.BeginInvoke(action12);
               Action action13 = () => label9.Visible = false;
               this.BeginInvoke(action13);
               Action action15 = () => label9.Visible = true;
               Action action14 = () => pictureBox22.Visible = false;
               Action action17 = () => f4.Show();

               string connetionString = Properties.Settings.Default.Secure.ToString(); ;
               using (SqlConnection cnn = new SqlConnection(connetionString))
               {
                  

                   try
                   {
                     
                       cnn.Open();
                       cnn.Close();
                       
                       Action action10 = () => this.Hide(); ;
                       this.BeginInvoke(action10);
                       Action action11 = () => f2.Show();
                       this.BeginInvoke(action11);
                       
                       
                   }
                   catch (Exception )
                   {
                       this.BeginInvoke(action14);
                       this.BeginInvoke(action15);
                       this.BeginInvoke(action17);

                   }


                   
               }
               
               this.BeginInvoke(action14);
               this.BeginInvoke(action15);
           });
            thread1.IsBackground = true;
            thread1.Start();


         
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (label9.Visible == true && textBox1.Text != "" && textBox2.Text != "")
            { 
            Properties.Settings.Default.Textbox1 = textBox1.Text ;
            Properties.Settings.Default.Textbox2 = textBox2.Text;
            Properties.Settings.Default.Checkbox = pictureBox9.Visible;
            Properties.Settings.Default.Save();
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
