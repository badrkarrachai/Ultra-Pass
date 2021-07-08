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
    public partial class forgot_password : Form
    {

        // that code makes the forme move and add shadow
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



        public forgot_password()
        {
            InitializeComponent();
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox15_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Active = true;
            toolTip1.Show("Close", pictureBox15);
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

        private void Email_address_Enter(object sender, EventArgs e)
        {
            Email_address.ForeColor = Color.FromArgb(17, 17, 17);
            False_Verification.Visible = false;
            True_Verification.Visible = false;
            label8.Visible = false;
            if (Email_address.Text == "" || Email_address.Text == "Email address")
            {
                Email_address.Select();
                Email_address.Text = "";
            }
        }

        private void forgot_password_Load(object sender, EventArgs e)
        {
            textBox1.Select();
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

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.ForeColor = Color.FromArgb(253, 136, 166);
        }

        private void label4_MouseDown(object sender, MouseEventArgs e)
        {
            label4.ForeColor = Color.FromArgb(251, 55, 104);
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.ForeColor = Color.FromArgb(252, 107, 143);
        }

        private void label4_MouseUp(object sender, MouseEventArgs e)
        {
            label4.ForeColor = Color.FromArgb(253, 136, 166);
        }

        private void Email_address_TextChanged(object sender, EventArgs e)
        {

        }

        private void Verification_code_txt_back_Click(object sender, EventArgs e)
        {
            Email_address.ForeColor = Color.FromArgb(17, 17, 17);
            False_Verification.Visible = false;
            True_Verification.Visible = false;
            label8.Visible = false;
            if (Email_address.Text == "" || Email_address.Text == "Email address")
            {
                Email_address.Select();
                Email_address.Text = "";
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (pictureBox22.Visible == false)
            { 
                Form1 f1 = new Form1();
                f1.Show();
                this.Close();
            }
        }

        public string rand;
        public string data_from_base;
        public string data_from_base_upper;

        private void Done()
        {
            string LowercaseFirst(string s)
            {
                // Check for empty string.
                if (string.IsNullOrEmpty(s))
                {
                    return string.Empty;
                }
                // Return char and concat substring.
                return char.ToLower(s[0]) + s.Substring(1);
            }



            string UppercaseFirst(string s)
            {
                // Check for empty string.
                if (string.IsNullOrEmpty(s))
                {
                    return string.Empty;
                }
                // Return char and concat substring.
                return char.ToUpper(s[0]) + s.Substring(1);
            }

            Error f4 = new Error();




            Thread thread3 = new Thread(() =>
            {
                //send email




                    Random rd = new Random();
                    int rand_num = rd.Next(100000, 900000);
                    rand = rand_num.ToString();



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
                    MailAddress Tomailaddress = new MailAddress(Email_address.Text, "New User");
                    using (StreamReader reader = File.OpenText("Web/index2.html")) // Path to your 
                    {
                        x = reader.ReadToEnd();
                    }

                    string ReplaceFirstOccurrence(string Source, string Find, string Replace)
                    {
                        int Place = Source.IndexOf(Find);
                        string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
                        return result;
                    }


                    x = ReplaceFirstOccurrence(x, "{UserName}", UppercaseFirst(label1.Text));
                    x = ReplaceFirstOccurrence(x, "{Vr}", rand);

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

                string email_add = Email_address.Text;

                try
                {

                    //checking the email if it's already used
                    string data_from_base = "test";
                    string data_from_base_upper = "test";
                    string cmdText = "SELECT * From dbo.Ultrapass1 WHERE " + "Email_address" + " = @Email_address";
                    using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.Secure.ToString()))
                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("@Email_address", Email_address.Text);

                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        dataGridView1.DataSource = ds.Tables[0];

                        string jobId = dataGridView1.Rows[0].Cells[1].Value + string.Empty;
                        data_from_base = jobId;
                        //get the name of the user
                        string user_name = dataGridView1.Rows[0].Cells[2].Value + string.Empty;
                        Action getname = () => label1.Text = user_name;
                        this.BeginInvoke(getname);


                        data_from_base_upper = (LowercaseFirst(data_from_base));



                        conn.Close();
                    }

                    if (email_add != data_from_base && email_add != data_from_base_upper) //check
                    {
                        Action action17 = () => label8.Visible = true;
                        this.BeginInvoke(action17);
                        Action action20 = () => Email_address.ForeColor = Color.FromArgb(249, 55, 55);
                        this.BeginInvoke(action20);
                        Action action21 = () => False_Verification.Visible = true;
                        this.BeginInvoke(action21);

                        //stop loding when email is the same
                        Action action4001 = () => pictureBox22.Visible = false;
                        this.BeginInvoke(action4001);
                        Action action5002 = () => pictureBox8.Visible = true;
                        this.BeginInvoke(action5002);
                        Action action6003 = () => label5.Visible = true;
                        this.BeginInvoke(action6003);



                    }
                    else
                    {




                        //the reset menu



                        //stop loding when email is the same
                        Action action6007 = () => label8.Visible = false;
                        this.BeginInvoke(action6007);
                        Action action4001 = () => pictureBox22.Visible = false;
                        this.BeginInvoke(action4001);
                        Action action5002 = () => pictureBox8.Visible = true;
                        this.BeginInvoke(action5002);
                        Action action6003 = () => label5.Visible = true;
                        this.BeginInvoke(action6003);
                        Action action6004 = () => Email_address.ForeColor = Color.FromArgb(17, 17, 17);
                        this.BeginInvoke(action6004);
                        Action action6005 = () => False_Verification.Visible = false;
                        this.BeginInvoke(action6005);
                        Action action6006 = () => True_Verification.Visible = false;
                        this.BeginInvoke(action6006);


                        thread3.Start();

                    }




                }
                catch (Exception)
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

        private void pictureBox8_MouseClick(object sender, MouseEventArgs e)
        {
            Done();
           
        }

        private void Email_address_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                Done();
                e.Handled = true;

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            


        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox9.BackColor = Color.FromArgb(253, 136, 166);
            label3.BackColor = Color.FromArgb(253, 136, 166);
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox9.BackColor = Color.FromArgb(252, 107, 143);
            label3.BackColor = Color.FromArgb(252, 107, 143);
        }

        private void label3_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox9.BackColor = Color.FromArgb(251, 55, 104);
            label3.BackColor = Color.FromArgb(251, 55, 104);
        }

        private void label3_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox9.BackColor = Color.FromArgb(253, 136, 166);
            label3.BackColor = Color.FromArgb(253, 136, 166);
        }

        private void label9_MouseEnter(object sender, EventArgs e)
        {
            label9.ForeColor = Color.FromArgb(253, 136, 166);
        }

        private void label9_MouseLeave(object sender, EventArgs e)
        {
            label9.ForeColor = Color.FromArgb(252, 107, 143);
        }

        private void label9_MouseDown(object sender, MouseEventArgs e)
        {
            label9.ForeColor = Color.FromArgb(251, 55, 104);
        }

        private void label9_MouseUp(object sender, MouseEventArgs e)
        {
            label9.ForeColor = Color.FromArgb(253, 136, 166);
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.ForeColor = Color.FromArgb(17, 17, 17);
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            //statu label
            label8.Visible = false;
            if (textBox2.Text == "" || textBox2.Text == "New password")
            {
                textBox2.Select();
                textBox2.Text = "";
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox2.Text == "New password")
            {
                textBox2.ForeColor = Color.DimGray;
                textBox1.Select();
                textBox2.Text = "New password";
                pictureBox24.Visible = false;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBox2.ForeColor = Color.FromArgb(17, 17, 17);
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            //statu label
            label8.Visible = false;
            if (textBox2.Text == "" || textBox2.Text == "New password")
            {
                textBox2.Select();
                textBox2.Text = "";
            }
            if (textBox3.Text == "" || textBox3.Text == "Confirm password")
            {
                textBox3.ForeColor = Color.DimGray;
                textBox1.Select();
                textBox3.Text = "Confirm password";
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            textBox3.ForeColor = Color.FromArgb(17, 17, 17);
            pictureBox5.Visible = false;
            pictureBox4.Visible = false;
            //statu label
            label8.Visible = false;
            if (textBox3.Text == "" || textBox3.Text == "Confirm password")
            {
                textBox3.Select();
                textBox3.Text = "";
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "" || textBox3.Text == "Confirm password")
            {
                textBox3.ForeColor = Color.DimGray;
                textBox1.Select();
                textBox3.Text = "Confirm password";
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            textBox3.ForeColor = Color.FromArgb(17, 17, 17);
            pictureBox5.Visible = false;
            pictureBox4.Visible = false;
            //statu label
            label8.Visible = false;
            if (textBox3.Text == "" || textBox3.Text == "Confirm password")
            {
                textBox3.Select();
                textBox3.Text = "";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            bool lowerChar = Regex.IsMatch(textBox2.Text, @"[a-z]+");
            bool numbers = Regex.IsMatch(textBox2.Text, @"[0-9]+");
            bool UpperChar = Regex.IsMatch(textBox2.Text, @"[A-Z]+");
            bool symbols = Regex.IsMatch(textBox2.Text, @"[@#$%^&*()_\-!]+");

            if (lowerChar && numbers && textBox2.Text.Length > 6 && symbols == false)
            {
                pictureBox24.Visible = true;
                Size size = new Size(180, 4);
                pictureBox24.Size = size;
                pictureBox24.Image = Ultra_Pass.Properties.Resources.good;
            }
            else if (lowerChar && symbols && numbers && UpperChar && textBox2.Text.Length > 8)
            {
                pictureBox24.Visible = true;
                Size size = new Size(310, 4);
                pictureBox24.Size = size;
                pictureBox24.Image = Ultra_Pass.Properties.Resources.good1;
            }
            else if (textBox2.Text.Trim() != "")
            {
                pictureBox24.Visible = true;
                Size size = new Size(35, 4);
                pictureBox24.Size = size;
                pictureBox24.Image = Ultra_Pass.Properties.Resources.weakpass1;
            }
        }
    }
}
