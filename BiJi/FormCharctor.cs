using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BiJi
{
    public partial class FormCharctor : Form
    {

        public delegate void deMoveData(string sr);
        public event deMoveData eventHandle;

        public void issure(string sr)
        {
            if (eventHandle != null)
            {
                eventHandle(txtInPut.Text.Trim());
            }         
        }

        public FormCharctor(string sr,int passchar)
        {  
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;       
            txtInPut.Text = sr;
            if (passchar == 1)
                txtInPut.PasswordChar = '*';
        }

        private void btn_Backspace_Click(object sender, EventArgs e)
        {
            if (txtInPut.Text.Trim().Length > 0)
            {
                string sr = txtInPut.Text.Trim();
                txtInPut.Text = sr.Remove(sr.Length - 1, 1);

            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            txtInPut.Clear();
        }

        private void btn_Enter_Click(object sender, EventArgs e)
        {
            issure(txtInPut.Text.Trim());
            this.Close();
        }

        private void btn_1_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText("1");
        }

        private void btn_2_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText("2");
        }

        private void btn_3_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText("3");
        }

        private void btn_4_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText("4");
        }

        private void btn_5_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText("5");
        }

        private void btn_6_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText("6");
        }

        private void btn_7_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText("7");
        }

        private void btn_8_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText("8");
        }

        private void btn_9_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText("9");
        }

        private void btn_0_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText("0");
        }

        private void btn_dot_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(".");
        }

        private void btn_sub_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText("-");
        }

        bool bCapsLock = false;
        private void btnCapsLock_Click(object sender, EventArgs e)
        {
            if (bCapsLock)
            {
                bCapsLock = false;
                btnCapsLock.Text = "大写";
                btn_A.Text = "A";
                btn_B.Text = "B";
                btn_C.Text = "C";
                btn_D.Text = "D";
                btn_E.Text = "E";
                btn_F.Text = "F";
                btn_G.Text = "G";
                btn_H.Text = "H";
                btn_I.Text = "I";
                btn_J.Text = "J";
                
                btn_K.Text = "K";

                btn_L.Text = "L";
                btn_M.Text = "M";
                btn_N.Text = "N";
                btn_O.Text = "O";
                btn_P.Text = "P";
                btn_Q.Text = "Q";
                btn_R.Text = "R";
                btn_S.Text = "S";
                btn_T.Text = "T";
                btn_U.Text = "U";

                btn_V.Text = "V";

                btn_W.Text = "W";
                btn_X.Text = "X";
                btn_Y.Text = "Y";
                btn_Z.Text = "Z";
              
            }
            else if (bCapsLock == false)
            {
                bCapsLock = true;
                btnCapsLock.Text = "小写";
                btn_A.Text = "a";
                btn_B.Text = "b";
                btn_C.Text = "c";
                btn_D.Text = "d";
                btn_E.Text = "e";
                btn_F.Text = "f";
                btn_G.Text = "g";
                btn_H.Text = "h";
                btn_I.Text = "i";
                btn_J.Text = "j";

                btn_K.Text = "k";

                btn_L.Text = "l";
                btn_M.Text = "m";
                btn_N.Text = "n";
                btn_O.Text = "o";
                btn_P.Text = "p";
                btn_Q.Text = "q";
                btn_R.Text = "r";
                btn_S.Text = "s";
                btn_T.Text = "t";
                btn_U.Text = "u";

                btn_V.Text = "v";

                btn_W.Text = "w";
                btn_X.Text = "x";
                btn_Y.Text = "y";
                btn_Z.Text = "z";
            }
        }

        private void btn_Q_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(btn_Q.Text);
        }

        private void btn_W_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(btn_W.Text);
        }

        private void btn_E_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(btn_E.Text);
        }

        private void btn_R_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(btn_R.Text);
        }

        private void btn_T_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(btn_T.Text);
        }

        private void btn_Y_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(btn_Y.Text);
        }

        private void btn_U_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(btn_U.Text);
        }

        private void btn_I_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(btn_I.Text);
        }

        private void btn_O_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(btn_O.Text);
        }

        private void btn_P_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(btn_P.Text);
        }

        private void btn_A_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(btn_A.Text);
        }

        private void btn_S_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(btn_S.Text);
        }

        private void btn_D_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(btn_D.Text);
        }

        private void btn_F_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(btn_F.Text);
        }

        private void btn_G_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(btn_G.Text);
        }

        private void btn_H_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(btn_H.Text);
        }

        private void btn_J_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(btn_J.Text);
        }

        private void btn_K_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(btn_K.Text);
        }

        private void btn_L_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(btn_L.Text);
        }

        private void btn_Z_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(btn_Z.Text);
        }

        private void btn_X_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(btn_X.Text);
        }

        private void btn_C_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(btn_C.Text);
        }

        private void btn_V_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(btn_V.Text);
        }

        private void btn_B_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(btn_B.Text);
        }

        private void btn_N_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(btn_N.Text);
        }

        private void btn_M_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(btn_M.Text);
        }

        private void btn_char1_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText(",");
        }

        private void btn_char2_Click(object sender, EventArgs e)
        {
            txtInPut.AppendText("|");
        }

        private void txtInPut_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                issure(txtInPut.Text.Trim());
                this.Close();
            }
        }
    }
}
