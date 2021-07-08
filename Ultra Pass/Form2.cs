using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.IO;


namespace Ultra_Pass
{

    public partial class Form2 : Form
    {
        

        private bool Drag;
        private int MouseX;
        private int MouseY;

        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        private bool m_aeroEnabled;

        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]




        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
            );


       

        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();
                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW; return cp;
            }
        }
        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0; DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }

        private const int WM_NCLBUTTONDBLCLK = 0x00A3;

        protected override void WndProc(ref Message m)
        {
            //this code stop's maximize forme by double click th mouse
            if (m.Msg == WM_NCLBUTTONDBLCLK)
            {
                m.Result = IntPtr.Zero;
                return;
            }
            base.WndProc(ref m);
            //end


            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 0
                        }; DwmExtendFrameIntoClientArea(this.Handle, ref margins);
                    }
                    break;
                default: break;
            }
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT) m.Result = (IntPtr)HTCAPTION;
        }


        private void PanelMove_MouseDown(object sender, MouseEventArgs e)
        {
            Drag = true;
            MouseX = Cursor.Position.X - this.Left;
            MouseY = Cursor.Position.Y - this.Top;
        }
        private void PanelMove_MouseMove(object sender, MouseEventArgs e)
        {
            if (Drag)
            {
                this.Top = Cursor.Position.Y - MouseY;
                this.Left = Cursor.Position.X - MouseX;
            }
        }
        private void PanelMove_MouseUp(object sender, MouseEventArgs e) { Drag = false; }
        public Form2()
        {
            InitializeComponent();

            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox7.Select();
            
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == "Firstname")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.FromArgb(17, 17, 17);
            }
          
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            textBox3.Select();
            if (textBox3.Text == "Username")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.FromArgb(17, 17, 17);
            }

        }

        private void label8_MouseClick(object sender, MouseEventArgs e)
        {
            textBox4.Select();
            if (textBox4.Text == "Email address")
            {
                textBox4.Text = "";
                textBox4.ForeColor = Color.FromArgb(17, 17, 17);
            }

        }

        private void textBox5_MouseClick(object sender, MouseEventArgs e)
        {
            textBox5.Select();
            if (textBox5.Text == "Password")
            {
                textBox5.Text = "";
                textBox5.ForeColor = Color.FromArgb(17, 17, 17);
            }

        }

        private void label10_MouseClick(object sender, MouseEventArgs e)
        {
            
            if (textBox6.Text == "Re-enter password")
            {
                textBox6.Select();
                textBox6.Text = "";
                textBox6.ForeColor = Color.FromArgb(17, 17, 17);
                
            }

        }

        private void pictureBox15_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Active = true;
            toolTip1.Show("Close",pictureBox15);
        }

        private void pictureBox14_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Active = true;
            toolTip1.Show("Minimize", pictureBox14);
        }

        private void pictureBox14_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Active = false;
        }

        private void pictureBox15_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Active = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox1.Select();
            if (textBox1.Text == "Firstname")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.FromArgb(17, 17, 17);
            }
        }

        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBox2.Select();
            if (textBox2.Text == "Lastname")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.FromArgb(17, 17, 17);
            }
        }


        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBox3.Select();
            if (textBox3.Text == "Username")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.FromArgb(17, 17, 17);
            }
        }


        private void pictureBox4_Click(object sender, EventArgs e)
        {
            textBox4.Select();
            if (textBox4.Text == "Email address")
            {
                textBox4.Text = "";
                textBox4.ForeColor = Color.FromArgb(17, 17, 17);
            }
        }


        private void pictureBox5_Click(object sender, EventArgs e)
        {
            textBox5.Select();
            if (textBox5.Text == "Password")
            {
                textBox5.Text = "";
                textBox5.ForeColor = Color.FromArgb(17, 17, 17);
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            textBox6.Select();
            if (textBox6.Text == "Re-enter password")
            {
                textBox6.Text = "";
                textBox6.ForeColor = Color.FromArgb(17, 17, 17);
            }
        }


        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.ForeColor = Color.FromArgb(17, 17, 17);
            pictureBox12.Visible = false;
            pictureBox13.Visible = false;

            if (textBox2.Text == "" || textBox2.Text == "Lastname")
            {
                textBox2.ForeColor = Color.FromArgb(17, 17, 17);
                textBox2.Select();
                textBox2.Text = "";
            }

            if ((textBox6.Text == textBox5.Text) && (label12.Text != "Weak"))
            {
                pictureBox20.Visible = false;
                pictureBox21.Visible = true;
                textBox6.ForeColor = Color.FromArgb(17, 17, 17);
            }
            else if ((textBox6.Text != "Re-enter password") && (label12.Text == "Weak"))
            {
                pictureBox20.Visible = true;
                pictureBox21.Visible = false;
                textBox6.ForeColor = Color.Red;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            textBox3.ForeColor = Color.FromArgb(17, 17, 17);
            pictureBox16.Visible = false;
            pictureBox17.Visible = false;

            if (textBox3.Text == "" || textBox3.Text == "Username")
            {
                textBox3.ForeColor = Color.FromArgb(17, 17, 17);
                textBox3.Select();
                textBox3.Text = "";
            }

            if ((textBox6.Text == textBox5.Text) && (label12.Text != "Weak"))
            {
                pictureBox20.Visible = false;
                pictureBox21.Visible = true;
                textBox6.ForeColor = Color.FromArgb(17, 17, 17);
            }
            else if ((textBox6.Text != "Re-enter password") && (label12.Text == "Weak"))
            {
                pictureBox20.Visible = true;
                pictureBox21.Visible = false;
                textBox6.ForeColor = Color.Red;
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            label6.Visible = false;
            textBox4.ForeColor = Color.FromArgb(17, 17, 17);
            pictureBox9.Visible = false;
            pictureBox7.Visible = false;

            if (textBox4.Text == "" || textBox4.Text == "Email address")
            {
                textBox4.ForeColor = Color.FromArgb(17, 17, 17);
                textBox4.Select();
                textBox4.Text = "";
            }


            if ((textBox6.Text == textBox5.Text) && (label12.Text != "Weak"))
            {
                pictureBox20.Visible = false;
                pictureBox21.Visible = true;
                textBox6.ForeColor = Color.FromArgb(17, 17, 17);
            }
            else if ((textBox6.Text != "Re-enter password") && (label12.Text == "Weak"))
            {
                pictureBox20.Visible = true;
                pictureBox21.Visible = false;
                textBox6.ForeColor = Color.Red;
            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            textBox5.ForeColor = Color.FromArgb(17, 17, 17);
            pictureBox18.Visible = false;
            pictureBox19.Visible = false;

            if (textBox5.Text == "" || textBox5.Text == "Password" )
            {
                textBox5.ForeColor = Color.FromArgb(17, 17, 17);
                textBox5.Select();
                textBox5.Text = "";
            }



            if ((textBox6.Text == textBox5.Text) && (label12.Text != "Weak"))
            {
                pictureBox20.Visible = false;
                pictureBox21.Visible = true;
                textBox6.ForeColor = Color.FromArgb(17, 17, 17);
            }
            else if ((textBox6.Text != "Re-enter password") && (label12.Text == "Weak"))
            {
                pictureBox20.Visible = true;
                pictureBox21.Visible = false;
                textBox6.ForeColor = Color.Red;
            }
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            textBox6.ForeColor = Color.FromArgb(17, 17, 17);
            pictureBox20.Visible = false;
            pictureBox21.Visible = false;

            if (textBox6.Text == "" || textBox6.Text == "Re-enter password")
            {
                textBox6.ForeColor = Color.FromArgb(17, 17, 17);
                textBox6.Select();
                textBox6.Text = "";
            }
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            pictureBox8.BackColor = Color.FromArgb(253, 136, 166);
            label5.BackColor = Color.FromArgb(253, 136, 166);
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox8.BackColor = Color.FromArgb(252, 107, 143);
            label5.BackColor = Color.FromArgb(252, 107, 143);

            
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar)&& !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.FromArgb(17, 17, 17);
            pictureBox10.Visible = false;
            pictureBox11.Visible = false;
            if (textBox1.Text == "" || textBox1.Text == "Firstname")
            {
               
                textBox1.Select();
                textBox1.Text = "";
            }

            //*******
            if ((textBox6.Text == textBox5.Text) && (label12.Text != "Weak"))
            {
                pictureBox20.Visible = false;
                pictureBox21.Visible = true;
                textBox6.ForeColor = Color.FromArgb(17, 17, 17);
            }
            else if ((textBox6.Text != "Re-enter password") && (label12.Text == "Weak"))
            {
                pictureBox20.Visible = true;
                pictureBox21.Visible = false;
                textBox6.ForeColor = Color.Red;
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f2 = new Form1();
            f2.Show();
            
        }


        public bool isvalid_name(string n)
        {
            Regex check = new Regex(@"^([A-Z][a-z-A-z]+)$");
            bool valid = false;
            valid = check.IsMatch(n);
            if (valid == true)
            {
                return valid;
            }
            else
            {
                
                return valid;
            }
        }

       

        private void textBox4_Leave(object sender, EventArgs e)
        {
            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

            if (Regex.IsMatch(textBox4.Text, pattern))
            {
                pictureBox7.Visible = true;
                pictureBox9.Visible = false;
                
            }
            else
            {
                pictureBox7.Visible = false;
                pictureBox9.Visible = true;
              
            }
            if (textBox4.Text == "Email address")
            {
                textBox4.ForeColor = Color.Red;
            }
            if (pictureBox9.Visible == true && textBox4.Text != "Email address")
            {
                textBox4.ForeColor = Color.Red;
            }
            if (pictureBox9.Visible == true && textBox4.Text == "")
            {
                textBox4.Text = "Email address";
                textBox4.ForeColor = Color.Red;
            }
            if (pictureBox7.Visible == true && textBox4.Text != "Email address")
            {
                textBox4.ForeColor = Color.FromArgb(17,17,17);
            }



        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            
            if (textBox1.Text == "" || textBox1.Text == "Firstname")
            {
                pictureBox10.Visible = true;
                pictureBox11.Visible = false;
                textBox1.Text = "Firstname";
                textBox1.ForeColor = Color.Red;
            }
            else
            {
                pictureBox10.Visible = false;
                pictureBox11.Visible = true;
            }
           
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox2.Text == "Lastname")
            {
                pictureBox12.Visible = true;
                pictureBox13.Visible = false;
                textBox2.Text = "Lastname";
                textBox2.ForeColor = Color.Red;
            }
            else
            {
                pictureBox12.Visible = false;
                pictureBox13.Visible = true;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "" || textBox3.Text == "Username")
            {
                pictureBox16.Visible = true;
                pictureBox17.Visible = false;
                textBox3.Text = "Username";
                textBox3.ForeColor = Color.Red;
            }
            else
            {
                pictureBox16.Visible = false;
                pictureBox17.Visible = true;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "" || textBox5.Text == "Password" )
            {
                pictureBox18.Visible = true;
                pictureBox19.Visible = false;
                textBox5.Text = "Password";
                textBox5.ForeColor = Color.Red;
            }
            else if (label12.Text == "Weak")
            {
                pictureBox18.Visible = true;
                pictureBox19.Visible = false;
                textBox5.ForeColor = Color.Red;
            }
            else
            {
                pictureBox18.Visible = false;
                pictureBox19.Visible = true;
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (textBox6.Text == "" || textBox6.Text == "Re-enter password")
            {
                pictureBox20.Visible = true;
                pictureBox21.Visible = false;
                textBox6.Text = "Re-enter password";
                textBox6.ForeColor = Color.Red;
            }
            else if ((textBox6.Text == textBox5.Text) && (label12.Visible == false))
            {
                pictureBox20.Visible = false;
                pictureBox21.Visible = true;
                textBox6.ForeColor = Color.FromArgb(17, 17, 17);
            }
            else if (label12.Visible == true)
            {
                pictureBox20.Visible = true;
                pictureBox21.Visible = false;
                textBox6.ForeColor = Color.Red;
            }
            else
            {
                pictureBox20.Visible = true;
                pictureBox21.Visible = false;
                textBox6.ForeColor = Color.Red;
            }
        }
    
        private void show()
        {
            pictureBox22.Visible = true;
            
        }
       
       

        private void pictureBox8_MouseClick(object sender, MouseEventArgs e)
        {

            textBox7.Select();

            label4.Visible = false;
            label2.Text = "Thank you for signing up with Ultra Pass! ";

            textBox4.Text = (UppercaseFirst(textBox4.Text));
            
            

            if (label5.Text == "VERIFY MY EMAIL")
            {


                //show the account created if random num = verfication code
                Action action4 = () => pictureBox22.Visible = false;
                Action action5 = () => pictureBox8.Visible = true;
                Action action6 = () => label5.Visible = true;
                
                Thread thread = new Thread(() =>
                {


                    string vv;


                    Action action1 = () => pictureBox22.Visible = true;
                    this.BeginInvoke(action1);
                    Action action2 = () => pictureBox8.Visible = false;
                    this.BeginInvoke(action2);
                    Action action3 = () => label5.Visible = false;
                    this.BeginInvoke(action3);

                    string connetionString = null;
                    string sql = null;

                    // All the info required to reach your db. See connectionstrings.com
                    connetionString = "Data Source=den1.mssql8.gear.host;Initial Catalog=ultrapass4users;Persist Security Info=True;User ID=ultrapass4users;Password=Yw378_NNX~WE";

                    // Prepare a proper parameterized query 
                    sql = "insert into Ultrapass1 ([Email_address], [Firstname], [Lastname], [Username], [Password]) values(@Email_address,@Firstname,@Lastname,@Username,@Password)";

                    // Create the connection (and be sure to dispose it at the end)
                    using (SqlConnection cnn = new SqlConnection(connetionString))
                    {
                        try
                        {
                            // Open the connection to the database. 
                            // This is the first critical step in the process.
                            // If we cannot reach the db then we have connectivity problems
                            cnn.Open();

                            // Prepare the command to be executed on the db
                            using (SqlCommand cmd = new SqlCommand(sql, cnn))
                            {
                                // Create and set the parameters values 
                                cmd.Parameters.Add("@Email_address", SqlDbType.NVarChar).Value = textBox4.Text;
                                cmd.Parameters.Add("@Firstname", SqlDbType.NVarChar).Value = textBox1.Text;
                                cmd.Parameters.Add("@Lastname", SqlDbType.NVarChar).Value = textBox2.Text;
                                cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = textBox3.Text;
                                cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = textBox5.Text;

                                // Let's ask the db to execute the query
                                int rowsAdded = cmd.ExecuteNonQuery();
                                if (rowsAdded > 0)
                                {
                                    
                                    vv = "done";
                                    
                                    Form1 f8 = new Form1();
                                    Action action01511p = () => f8.setGmail(textBox4.Text);
                                    Action action01511 = () => f8.Show();
                                    this.BeginInvoke(action01511p);
                                    this.BeginInvoke(action01511);

                                    Error f5 = new Error(vv);
                                    Action action0111 = () => f5.Show();
                                    this.BeginInvoke(action0111);

                                    Action close = () => this.Close();
                                    this.BeginInvoke(close);
                                }
                                else
                                { 
                                    cnn.Close();

                                    vv = "";
                                
                                    Error f51 = new Error(vv);

                                
                                    Action action0111b = () => f51.Show();
                                    this.BeginInvoke(action0111b);
                                }
                            }


                        }
                        catch (Exception )
                        {
                            
                            this.BeginInvoke(action4);
                            this.BeginInvoke(action5);
                            this.BeginInvoke(action6);

                            vv = "";
                            
                            Error f5 = new Error(vv);

                            
                            Action action0111b1 = () => f5.Show();
                            this.BeginInvoke(action0111b1);

                        }
                        cnn.Close();
                    }


                    //stop load
                    this.BeginInvoke(action4);
                    this.BeginInvoke(action5);
                    this.BeginInvoke(action6);


                });
                thread.IsBackground = true;

                if (Verification_code_txt.Text == textBox10.Text)
                {
                    
                    thread.Start();
  

                }
                else
                {
                    False_Verification.Visible = true;
                    False_Verification.BringToFront();
                    True_Verification.Visible = false;
                    label8.Visible = true;
                    label8.BringToFront();
                    Verification_code_txt.ForeColor = Color.Red;
                }


            }
            else
            {

            
            
            //check text boxs if all good
            if (textBox1.Text == "" || textBox1.Text == "Firstname")
            {
                pictureBox10.Visible = true;
                pictureBox11.Visible = false;
                textBox1.Text = "Firstname";
                textBox1.ForeColor = Color.Red;
            }
            else
            {
                pictureBox10.Visible = false;
                pictureBox11.Visible = true;
            }
            if (textBox2.Text == "" || textBox2.Text == "Lastname")
            {
                pictureBox12.Visible = true;
                pictureBox13.Visible = false;
                textBox2.Text = "Lastname";
                textBox2.ForeColor = Color.Red;
            }
            else
            {
                pictureBox12.Visible = false;
                pictureBox13.Visible = true;
            }
            if (textBox3.Text == "" || textBox3.Text == "Username")
            {
                pictureBox16.Visible = true;
                pictureBox17.Visible = false;
                textBox3.Text = "Username";
                textBox3.ForeColor = Color.Red;
            }
            else
            {
                pictureBox16.Visible = false;
                pictureBox17.Visible = true;
            }

            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

            if (Regex.IsMatch(textBox4.Text, pattern))
            {
                pictureBox7.Visible = true;
                pictureBox9.Visible = false;

            }
            else
            {
                pictureBox7.Visible = false;
                pictureBox9.Visible = true;

            }
            if (textBox4.Text == "Email address")
            {
                textBox4.ForeColor = Color.Red;
            }
            if (pictureBox9.Visible == true && textBox4.Text != "Email address")
            {
                textBox4.ForeColor = Color.Red;
            }
            if (pictureBox9.Visible == true && textBox4.Text == "")
            {
                textBox4.Text = "Email address";
                textBox4.ForeColor = Color.Red;
            }
            if (pictureBox7.Visible == true && textBox4.Text != "Email address")
            {
                textBox4.ForeColor = Color.FromArgb(17, 17, 17);
            }

            if (textBox5.Text == "" || textBox5.Text == "Password" )
            {
                pictureBox18.Visible = true;
                pictureBox19.Visible = false;
                textBox5.Text = "Password";
                textBox5.ForeColor = Color.Red;
            }
            else if (label12.Text != "Weak")
            {
                pictureBox18.Visible = false;
                pictureBox19.Visible = true;
            }

            if (textBox6.Text == "" || textBox6.Text == "Re-enter password")
            {
                pictureBox20.Visible = true;
                pictureBox21.Visible = false;
                textBox6.Text = "Re-enter password";
                textBox6.ForeColor = Color.Red;
            }
            else if ((textBox6.Text == textBox5.Text) && (label12.Text != "Weak"))
            {
                pictureBox20.Visible = false;
                pictureBox21.Visible = true;
                textBox6.ForeColor = Color.FromArgb(17, 17, 17);
            }
            else if ((textBox6.Text != "Re-enter password") && (label12.Text == "Weak"))
            {
                pictureBox20.Visible = true;
                pictureBox21.Visible = false;
                textBox6.ForeColor = Color.Red;
            }

                if (pictureBox10.Visible == false && pictureBox12.Visible == false && pictureBox16.Visible == false && pictureBox9.Visible == false && pictureBox20.Visible == false && pictureBox23.Visible == false && label12.Text != "Weak")
                {
                    //generate random number
                    if (pictureBox23.Visible == false)
                    {
                        Random rd = new Random();
                        int rand_num = rd.Next(100000, 900000);
                        textBox10.Text = rand_num.ToString();

                    }
                }

                if (pictureBox10.Visible == false && pictureBox12.Visible == false && pictureBox16.Visible == false && pictureBox9.Visible == false && pictureBox20.Visible == false && label12.Text != "Weak")
            {
                
                Error f4 = new Error();
                Thread thread3 = new Thread(() =>
                {
                    //send email
                    if (label6.Visible == false)
                {
                    SmtpClient clinet = new SmtpClient()
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential()
                        {
                            UserName = "ultra.pass.100@gmail.com",
                            Password = "afqsfdxiwcbnragw",
                        }

                    };
                    string x;
                    MailAddress formailaddress = new MailAddress("ultra.pass.100@gmail.com", "Ultra Pass");
                    MailAddress Tomailaddress = new MailAddress(textBox4.Text, "New User");
                    using (StreamReader reader = File.OpenText("Web/index.html")) // Path to your 
                    {
                        x = reader.ReadToEnd();
                    }

                        string ReplaceFirstOccurrence(string Source, string Find, string Replace)
                        {
                            int Place = Source.IndexOf(Find);
                            string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
                            return result;
                        }


                        x = ReplaceFirstOccurrence(x, "{UserName}", UppercaseFirst(textBox1.Text));
                        x = ReplaceFirstOccurrence(x, "{Vr}", textBox10.Text);

                        MailMessage message = new MailMessage()
                    {
                        From = formailaddress,
                        Subject = "Verification code",
                        Body = x,
                        IsBodyHtml = true,
                    };

                    message.IsBodyHtml = true;
                    message.To.Add(Tomailaddress);
                    try
                    {
                        clinet.Send(message);

                    }
                    catch (Exception)
                    {
                            Action action757 = () => f4.Show();
                            this.BeginInvoke(action757);
                    }
                        Action action101 = () => pictureBox11.Visible = false;
                        Action action102 = () => pictureBox13.Visible = false;
                        Action action103 = () => pictureBox17.Visible = false;
                        Action action104 = () => pictureBox7.Visible = false;
                        Action action105 = () => pictureBox19.Visible = false;
                        Action action106 = () => pictureBox21.Visible = false;
                        this.BeginInvoke(action101);
                        this.BeginInvoke(action102);
                        this.BeginInvoke(action103);
                        this.BeginInvoke(action104);
                        this.BeginInvoke(action105);
                        this.BeginInvoke(action106);

                        //the verfay page 
                        Action action107 = () => pictureBox23.Visible = true;
                        Action action108 = () => label7.Visible = true;
                        Action actionup1 = () => label7.BringToFront();
                        Action action1000 = () => label11.Visible = true;
                        Action actionup10_0 = () => label11.BringToFront();
                        Action action110 = () => Verification_code_txt_back.Visible = true;
                        Action actionup2 = () => Verification_code_txt_back.BringToFront();
                        Action action109 = () => Verification_code_txt.Visible = true;
                        Action actionup3 = () => Verification_code_txt.BringToFront();
                        
                        Action action111 = () => label5.Text = "VERIFY MY EMAIL";
                        Action action112 = () => label5.Location = new Point(562, 561);

                        Action action11i3 = () => pictureBox24.SendToBack();

                        Action action113 = () => Resend_btn_back.BringToFront();
                        Action action114 = () => Resend_btn_back.Visible = true;
                        Action action115 = () => Resend_btn_front.Visible = true;
                        Action action116 = () => Resend_btn_front.BringToFront();
                        Action action117 = () => Back_btn_back.Visible = true;
                        Action action118 = () => Back_btn_back.BringToFront();
                        Action action119 = () => Back_btn_front.Visible = true;
                        Action action120 = () => Back_btn_front.BringToFront();
                        this.BeginInvoke(action107);
                        this.BeginInvoke(action108);
                        this.BeginInvoke(actionup1);
                        this.BeginInvoke(action1000);
                        this.BeginInvoke(actionup10_0);
                        this.BeginInvoke(action110);
                        this.BeginInvoke(actionup2);
                        this.BeginInvoke(action109);
                        this.BeginInvoke(actionup3);
                        this.BeginInvoke(action111);
                        this.BeginInvoke(action112);
                        this.BeginInvoke(action113);
                        this.BeginInvoke(action114);
                        this.BeginInvoke(action115);
                        this.BeginInvoke(action116);
                        this.BeginInvoke(action117);
                        this.BeginInvoke(action118);
                        this.BeginInvoke(action119);
                        this.BeginInvoke(action120);

                        this.BeginInvoke(action11i3);

                        //stop loding
                        Action action4 = () => pictureBox22.Visible = false;
                        this.BeginInvoke(action4);
                        Action action5 = () => pictureBox8.Visible = true;
                        this.BeginInvoke(action5);
                        Action action6 = () => label5.Visible = true;
                        this.BeginInvoke(action6);

                    }

                });
                thread3.IsBackground = true;
                

                Thread thread1 = new Thread(() =>
                {
                    Action action1 = () => pictureBox22.Visible = true;
                    this.BeginInvoke(action1);
                    Action action2 = () => pictureBox8.Visible = false;
                    this.BeginInvoke(action2);
                    Action action3 = () => label5.Visible = false;
                    this.BeginInvoke(action3);

                    try
                    {

                    //checking the email if it's already used
                    
                    string cmdText = "SELECT * From dbo.Ultrapass1 WHERE " + "Email_address" + " = @Email_address";
                    using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.Secure.ToString()))
                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("@Email_address", textBox4.Text);

                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        dataGridView1.DataSource = ds.Tables[0];

                            string jobId = dataGridView1.Rows[0].Cells[1].Value + string.Empty;
                            Action action12 = () => label9.Text = jobId;
                            this.BeginInvoke(action12);
                            Action actionn = () => label10.Text = (UppercaseFirst(label9.Text));
                            this.BeginInvoke(actionn);
                            

                            conn.Close();
                    }

                    if (label9.Text == textBox4.Text || textBox4.Text == label10.Text) //check
                        {
                            Action action17 = () => label6.Visible = true;
                            this.BeginInvoke(action17);
                            Action action20 = () => textBox4.ForeColor = Color.Red;
                            this.BeginInvoke(action20);
                            Action action21 = () => pictureBox9.Visible = true;
                            this.BeginInvoke(action21);

                            //stop loding when email is the same
                            Action action4001 = () => pictureBox22.Visible = false;
                            this.BeginInvoke(action4001);
                            Action action5002 = () => pictureBox8.Visible = true;
                            this.BeginInvoke(action5002);
                            Action action6003 = () => label5.Visible = true;
                            this.BeginInvoke(action6003);

                        }
                   
                        
                            if (label6.Visible == false)
                            { 
                            thread3.Start();
                            }
                        

                    }
                    catch(Exception )
                    {
                        //show error page
                        Action action18 = () => f4.Show();
                        this.BeginInvoke(action18);
                        Action action19 = () => this.Refresh();
                        this.BeginInvoke(action19);

                        //stop loding when error
                        Action action4000 = () => pictureBox22.Visible = false;
                        this.BeginInvoke(action4000);
                        Action action5000 = () => pictureBox8.Visible = true;
                        this.BeginInvoke(action5000);
                        Action action6000 = () => label5.Visible = true;
                        this.BeginInvoke(action6000);
                    }

                });
                thread1.IsBackground = true;
                thread1.Start();
               
                }

            }


        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text == textBox5.Text && label12.Text != "Weak")
            {
                pictureBox20.Visible = false;
                pictureBox21.Visible = true;
                textBox6.ForeColor = Color.FromArgb(17, 17, 17);
            }
            else
            {
                pictureBox20.Visible = true;
                pictureBox21.Visible = false;
                
            }
        }

        //Used for capitalize the first letter
        static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.FromArgb(17, 17, 17);
            

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.ForeColor = Color.FromArgb(17, 17, 17);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.ForeColor = Color.FromArgb(17, 17, 17);
        }

        //USED to chek the password if it contine a digit
        private int numberpass(string pass)
        {
            int num = 0;
            foreach (char ch in pass)
            {
                if (char.IsDigit(ch))
                {
                    num++;
                }
            }
            return num;
        }
        //USED to chek if the password contine Uppercase
        private int uppercase(string pass)
        {
            int num = 0;
            foreach (char ch in pass)
            {
                if (char.IsUpper(ch))
                {
                    num++;
                }
            }
            return num;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            label6.Visible = false;
            textBox4.ForeColor = Color.FromArgb(17, 17, 17);
            
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox5.ForeColor = Color.FromArgb(17, 17, 17);

            const int min_lghent = 10;
            const int max_lghent = 18;
            string password = textBox5.Text;
            if (textBox5.Text.Length >= 1 && textBox5.Text != "Password")
            { 
            if (password.Length >= min_lghent && numberpass(password) >= 1 && uppercase(password) >= 1 && password.Length < 18)
            {
                
               
                pictureBox24.Visible = true;
                label12.Text = "Good";
                label12.ForeColor = Color.FromArgb(0, 174, 239);
                Size size = new Size(180, 4);
                pictureBox24.Size = size;
                pictureBox24.Image = Ultra_Pass.Properties.Resources.good;
            }
            else if (password.Length >= max_lghent && numberpass(password) >= 1 && uppercase(password) >= 1)
            {
                    
                   
                    pictureBox24.Visible = true;
                    label12.Text = "Great!";
                    label12.ForeColor = Color.FromArgb(0, 166, 81);
                    Size size = new Size(360, 4);
                    pictureBox24.Size = size;
                    pictureBox24.Image = Ultra_Pass.Properties.Resources.good1;
                }
            else
            {
                
                
                pictureBox24.Visible = true;
                label12.Text = "Weak";
                label12.ForeColor = Color.FromArgb(237, 28, 36);
                Size size = new Size(35, 4);
                pictureBox24.Size = size;
                pictureBox24.Image = Ultra_Pass.Properties.Resources.weakpass1;
            }
            }
            else
            {
                
                
                pictureBox24.Visible = false;
                label12.Text = "Weak";
                label12.ForeColor = Color.FromArgb(237, 28, 36);
                Size size = new Size(35, 4);
                pictureBox24.Size = size;
                pictureBox24.Image = Ultra_Pass.Properties.Resources.weakpass1;
            }

        }

        private void Resend_btn_back_MouseEnter(object sender, EventArgs e)
        {
            if (Resend_btn_back.BackColor != Color.FromArgb(254, 209, 221))
            {
                Resend_btn_back.BackColor = Color.FromArgb(253, 136, 166);
                Resend_btn_front.BackColor = Color.FromArgb(253, 136, 166);
            }
        }

        private void Resend_btn_back_MouseDown(object sender, MouseEventArgs e)
        {
            if (Resend_btn_back.BackColor != Color.FromArgb(254, 209, 221))
            { 
            Resend_btn_back.BackColor = Color.FromArgb(251, 55, 104);
            Resend_btn_front.BackColor = Color.FromArgb(251, 55, 104);
            }
        }

        private void Resend_btn_back_MouseLeave(object sender, EventArgs e)
        {
            if (Resend_btn_back.BackColor != Color.FromArgb(254, 209, 221))
            {
                Resend_btn_back.BackColor = Color.FromArgb(252, 107, 143);
                Resend_btn_front.BackColor = Color.FromArgb(252, 107, 143);
            }
        }

        private void Resend_btn_back_MouseUp(object sender, MouseEventArgs e)
        {
            if (Resend_btn_back.BackColor != Color.FromArgb(254, 209, 221))
            {
                Resend_btn_back.BackColor = Color.FromArgb(253, 136, 166);
                Resend_btn_front.BackColor = Color.FromArgb(253, 136, 166);
            }
        }

        private void Back_btn_front_MouseDown(object sender, MouseEventArgs e)
        {
            Back_btn_front.BackColor = Color.FromArgb(251, 55, 104);
            Back_btn_back.BackColor = Color.FromArgb(251, 55, 104);
        }

        private void Back_btn_front_MouseEnter(object sender, EventArgs e)
        {
            Back_btn_front.BackColor = Color.FromArgb(253, 136, 166);
            Back_btn_back.BackColor = Color.FromArgb(253, 136, 166);
        }

        private void Back_btn_front_MouseLeave(object sender, EventArgs e)
        {
            Back_btn_front.BackColor = Color.FromArgb(252, 107, 143);
            Back_btn_back.BackColor = Color.FromArgb(252, 107, 143);
        }

        private void Back_btn_front_MouseUp(object sender, MouseEventArgs e)
        {
            Back_btn_front.BackColor = Color.FromArgb(253, 136, 166);
            Back_btn_back.BackColor = Color.FromArgb(253, 136, 166);
        }

        private void Back_btn_front_MouseClick(object sender, MouseEventArgs e)
        {
            count = 1;
            Resend_btn_back.BackColor = Color.FromArgb(252, 107, 143);
            Resend_btn_front.BackColor = Color.FromArgb(252, 107, 143);

            pictureBox23.Visible = false;
            label7.Visible = false;
            label11.Visible = false;
            label8.Visible = false;
            Verification_code_txt.Visible = false;
            Verification_code_txt_back.Visible = false;
            label5.Text = "CREATE ACCOUNT";
            label5.Location = new Point(560, 561);
            Resend_btn_back.Visible = false;
            Resend_btn_front.Visible = false;
            Back_btn_back.Visible = false;
            Back_btn_front.Visible = false;
            label9.Text = "";
            label10.Text = "";
            label6.Visible = false;
            True_Verification.Visible = false;
            False_Verification.Visible = false;
            pictureBox24.BringToFront();

            //reback check buttons
            pictureBox11.Visible = true;
            pictureBox13.Visible = true;
            pictureBox17.Visible = true;
            pictureBox7.Visible = true;
            pictureBox19.Visible = true;
            pictureBox21.Visible = true;

        }

        private void pictureBox23_VisibleChanged(object sender, EventArgs e)
        {
            textBox1.Text = (UppercaseFirst(textBox1.Text));
            textBox2.Text = (UppercaseFirst(textBox2.Text));
            textBox3.Text = (UppercaseFirst(textBox3.Text));
            textBox4.Text = (UppercaseFirst(textBox4.Text));

            if (pictureBox23.Visible == false)
            {
                label4.Visible = true;
                label2.Text = "Don't have an account? it takes less than a minutes. if you\n" + "already have an account";
            }
            else
            { 
            label4.Visible = false;
            label2.Text = "Thank you for signing up with Ultra Pass! ";
            }
        }

        public int count = 1;

        private void Resend_btn_front_MouseClick(object sender, MouseEventArgs e)
        {
            

            if (count<=3)
            { 
            Random rd = new Random();
                int rand_num = rd.Next(100000, 900000);
                textBox10.Text = rand_num.ToString();
            
            SmtpClient clinet = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = "ultra.pass.100@gmail.com",
                    Password = "afqsfdxiwcbnragw",
                }

            };
            MailAddress formailaddress = new MailAddress("ultra.pass.100@gmail.com", "Ultra Pass");
            MailAddress Tomailaddress = new MailAddress(textBox4.Text, "New User");
            MailMessage message = new MailMessage()
            {
                From = formailaddress,
                Subject = "Verification code",
                Body = (string)("Hi " + textBox1.Text + ",\n\n" +
                "Thank you for signing up with Ultra Pass! Your account will now be setup \n" + "after using the verification code below.\n\n\n" +
                "Your verification code is: " + textBox10.Text + "\n\n\n" +
                "What to do now?\n" +
                "The first thing to do now copies that code to your Ultra Pass app From there\n" +
                "click (Verify my email) then your account will be created, the second thing to\n" +
                "do is sign in to that account using your email and your password.\n\n\n" +
                "Sincerely,\n" +
                "The Ultra Pass Team"),
                IsBodyHtml = false,
            };

            message.IsBodyHtml = false;
            message.To.Add(Tomailaddress);
            try
            {
                    clinet.Send(message);
                    count++;
            }
            catch (Exception)
            {
                Error f5 = new Error();
                f5.Show();
            }
                if (count == 3)
                {
                    Resend_btn_back.BackColor = Color.FromArgb(254, 209, 221);
                    Resend_btn_front.BackColor = Color.FromArgb(254, 209, 221);
                }
            }//end if

        }

        private void Verification_code_txt_Enter(object sender, EventArgs e)
        {
            Verification_code_txt.ForeColor = Color.FromArgb(17, 17, 17);
            False_Verification.Visible = false;
            True_Verification.Visible = false;
            if (Verification_code_txt.Text == "" || Verification_code_txt.Text == "Verification code")
            {
                Verification_code_txt.Select();
                Verification_code_txt.Text = "";
            }

        }

        private void Verification_code_txt_Leave(object sender, EventArgs e)
        {
            if (Verification_code_txt.Text == textBox10.Text)
            {
                False_Verification.Visible = false;
                True_Verification.Visible = true;
                True_Verification.BringToFront();
                label8.Visible = false;
                

                //hide resend button and back button
                Back_btn_back.Visible = false;
                Back_btn_front.Visible = false;
                Resend_btn_back.Visible = false;
                Resend_btn_front.Visible = false;

            }
            else
            {
                False_Verification.Visible = true;
                False_Verification.BringToFront();
                True_Verification.Visible = false;

                Verification_code_txt.ForeColor = Color.Red;
                label8.Visible = true;
                label8.BringToFront();
            }
        }

        private void Verification_code_txt_back_Click(object sender, EventArgs e)
        {
            Verification_code_txt.Select();
        }

        private void pictureBox8_VisibleChanged(object sender, EventArgs e)
        {
            
        }

        private void pictureBox22_VisibleChanged(object sender, EventArgs e)
        {
            
        }
    }
}
