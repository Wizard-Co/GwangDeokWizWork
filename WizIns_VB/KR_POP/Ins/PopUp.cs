using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizIns
{
    public partial class PopUp : Form
    {
        Image _picture;

        public PopUp()
        {
            InitializeComponent();
        }
        
        public Image Picture
        {
            get
            {
                return _picture;
            }
            set
            {
                _picture = value;
                pictureBox.Image = _picture;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
