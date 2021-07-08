using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ultra_Pass
{
    public partial class Error : Form
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



        public Error()
        {
            InitializeComponent();
            
        }
        public string data;
        public Error(string passedInfo)
        {
            InitializeComponent();
            data = passedInfo;
            
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            if (data != "done")
            {
                pictureBox1.Image = Ultra_Pass.Properties.Resources.Done_btn_approtch;
            }
            else
            {
                pictureBox1.Image = Ultra_Pass.Properties.Resources.button_Done_Green_in;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (data != "done")
            {
                pictureBox1.Image = Ultra_Pass.Properties.Resources.done_button_non;
            }
            else
            {
                pictureBox1.Image = Ultra_Pass.Properties.Resources.button_Done_Green;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (data != "done")
            {
                pictureBox1.Image = Ultra_Pass.Properties.Resources.Done_btn_click;
            }
            else
            {
                pictureBox1.Image = Ultra_Pass.Properties.Resources.button_Done_Green_Click;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (data != "done")
            {
                pictureBox1.Image = Ultra_Pass.Properties.Resources.Done_btn_approtch;
            }
            else
            {
                pictureBox1.Image = Ultra_Pass.Properties.Resources.button_Done_Green_in;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            data = "";
        }

        private void Error_Shown(object sender, EventArgs e)
        {
            
            if (data == "done")
            {
                this.BackgroundImage = Ultra_Pass.Properties.Resources.Happy_Full;
                pictureBox1.Image = Ultra_Pass.Properties.Resources.button_Done_Green;
                label1.Text = ("Thank you for registering on Ultrapass!, Your\n" + "account has been created and you can now\n" + "log in to it.");
            }
            else
            {
                this.BackgroundImage = Ultra_Pass.Properties.Resources.error_23;
                pictureBox1.Image = Ultra_Pass.Properties.Resources.done_button_non;
                label1.Text = ("Unfortunately, there is an error when we tried\n" + "connecting to the server please check your\n" + "internet connection and try again.");
            }
        }

        private void Error_Load(object sender, EventArgs e)
        {
            
            if (data == "done")
            {
                this.BackgroundImage = Ultra_Pass.Properties.Resources.Happy_Full;
                pictureBox1.Image = Ultra_Pass.Properties.Resources.button_Done_Green;
                label1.Text = ("Thank you for registering on Ultrapass!, Your\n" + "account has been created and you can now\n" + "log in to it.");
            }
            else
            {
                this.BackgroundImage = Ultra_Pass.Properties.Resources.error_23;
                pictureBox1.Image = Ultra_Pass.Properties.Resources.done_button_non;
                label1.Text = ("Unfortunately, there is an error when we tried\n" + "connecting to the server please check your\n" + "internet connection and try again.");
            }
        }
    }
}
