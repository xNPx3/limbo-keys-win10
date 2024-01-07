using System.Diagnostics;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public bool clickable = true;
        public int id = -1;
        public int pos = -1;

        public int x = -1;
        public int y = -1;

        public Color color = Color.Red;
        public Form1(Image key, int formId, int position)
        {
            InitializeComponent();
            keyPic.Image = key;
            id = formId;
            pos = position;
        }
    }
}