using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Media;
using WinFormsApp1.Properties;

namespace WinFormsApp1
{
    public partial class MainForm : Form
    {
        //[DllImport("user32.dll", SetLastError = true)]
        //internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        readonly List<int> xPos = new List<int>();
        readonly List<int> yPos = new List<int>();

        readonly List<Form1> forms = new List<Form1>();

        readonly int sw = Screen.AllScreens[0].Bounds.Width;
        readonly int sh = Screen.AllScreens[0].Bounds.Height;

        public int win = -1;
        bool clickable = false;

        public MainForm()
        {
            InitializeComponent();
        }

        float Lerp(float v0, float v1, float t)
        {
            return (1 - t) * v0 + t * v1;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            int id = 0;
            for (int r = 0; r < 2; r++)
            {
                for (int c = 0; c < 4; c++)
                {

                    Form1 f = new Form1(Resources.empty, id, id);
                    //f.Click += F_Click;
                    f.Show();
                    forms.Add(f);

                    int x = c * sw / 4 + sw / 8 - f.Size.Width / 2;
                    int y = r * sh / 2 + sh / 4 - f.Size.Height / 2;
                    xPos.Add(x);
                    yPos.Add(y);

                    f.x = x;
                    f.y = y;

                    f.SetBounds(x, y, f.Width, f.Height);
                    id++;
                }
            }

            Random rng = new Random();
            win = rng.Next(8);
            forms[win].keyPic.BackColor = Color.Lime;
            forms[win].color = Color.Lime;

            Debug.WriteLine(win);
        }

        private void F_Click(object? sender, EventArgs e)
        {
            if (sender != null)
            {
                Form1 f = sender as Form1;

                if (f.clickable && clickable)
                {
                    int steps = 30;
                    for (int i = 0; i <= steps; i++)
                    {
                        for (int b = 0; b < 8; b++)
                        {
                            if (b != f.id)
                            {
                                int y = (int)Lerp(forms[b].y, forms[b].y - sh, i / (float)steps);

                                forms[b].SetBounds(forms[b].x, y, forms[b].Width, forms[b].Height);
                            }
                            else
                            {
                                int x = (int)Lerp(forms[b].x, sw / 2f - forms[b].Width / 2f, i / (float)steps);
                                int y = (int)Lerp(forms[b].y, sh / 2f - forms[b].Height / 2f, i / (float)steps);
                                forms[b].SetBounds(x, y, forms[b].Width, forms[b].Height);
                            }
                        }
                        Thread.Sleep(200 / steps);
                    }
                    Thread.Sleep(800);

                    if (f.id == win)
                    {
                        using (var soundPlayer = new SoundPlayer(Resources.secretKey))
                        {
                            soundPlayer.Stop();
                            soundPlayer.Play();
                        }
                        Thread.Sleep(800);
                        MessageBox.Show("you passed limbo", "no way bro this is epic", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                    else
                    {
                        using (var soundPlayer = new SoundPlayer(Resources.explode_11))
                        {
                            soundPlayer.Stop();
                            soundPlayer.Play();
                            Thread.Sleep(100);
                            MessageBox.Show("Died at 98%", "skill issue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Close();
                        }
                    }
                }
            }
        }

        int[] Shuffle(Random rng, int[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                int temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
            return array;
        }

        void Run(int[] from, int[] to, int delay)
        {
            int steps = 30;

            for (int i = 0; i <= steps; i++)
            {
                for (int b = 0; b < 8; b++)
                {
                    int x = (int)Lerp(xPos[from[b]], xPos[to[b]], i / (float)steps);
                    int y = (int)Lerp(yPos[from[b]], yPos[to[b]], i / (float)steps);

                    forms[b].pos = to[b];
                    //forms[b].id = b;

                    forms[b].x = x;
                    forms[b].y = y;

                    //MoveWindow(forms[b].Handle, x, y, forms[b].Width, forms[b].Height, true);
                    forms[b].SetBounds(x, y, forms[b].Width, forms[b].Height);
                }
                Thread.Sleep(delay / steps);
            }
        }
        void Spin()
        {
            Debug.WriteLine("Lerp");
            // lerp to circle shape
            int[] val = Shuffle(new Random(), Enumerable.Range(0, 8).ToArray());

            int steps = 40;
            for (int i = 0; i <= steps; i++)
            {
                for (int b = 0; b < 8; b++)
                {
                    double p = 2 * Math.PI * (val[b] / 8.0);
                    int x_1 = (int)(sw / 2 - 130 - Math.Cos(p) * 700);
                    int y_1 = (int)(sh / 2 - 100 - Math.Sin(p) * 500);

                    int x = (int)Lerp(forms[b].x, x_1, i / (float)steps);
                    int y = (int)Lerp(forms[b].y, y_1, i / (float)steps);

                    forms[b].SetBounds(x, y, forms[b].Width, forms[b].Height);
                }
                Thread.Sleep(10);
            }

            Debug.WriteLine("Spin");

            // spin
            double t = 0;
            while (t < 1.1 * Math.PI)
            {
                for (int b = 0; b < 8; b++)
                {
                    double p = 2 * Math.PI * (val[b] / 8.0);
                    double x_1 = Math.Cos(p + t);
                    double y_1 = Math.Sin(p + t);

                    int x = (int)(sw / 2 - 130 - x_1 * 700);
                    int y = (int)(sh / 2 - 100 - y_1 * 500);

                    forms[b].x = x;
                    forms[b].y = y;

                    forms[b].SetBounds(x, y, forms[b].Width, forms[b].Height);
                    //Thread.Sleep(20);
                }
                t += 0.001;
            }
        }

        void Colors()
        {
            Debug.WriteLine("Colors");
            List<Color> colors = new List<Color>
            {
                Color.LightGreen,
                Color.LightBlue,
                Color.DeepPink,
                Color.Red,
                Color.Lime,
                Color.Cyan,
                Color.BlueViolet,
                Color.Gold,
            };
            for (int b = 0; b < 8; b++)
            {
                forms[forms[b].pos].keyPic.BackColor = colors[b];
                forms[forms[b].pos].color = colors[b];
                Thread.Sleep(100);
            }
        }

        void Sequence()
        {
            int[] from = Enumerable.Range(0, 8).ToArray();
            Random rng = new Random();

            List<int[]> anim_orig = new List<int[]>
            {
                new int[] { 2, 0, 4, 1, 6, 3, 7, 5 }, // rotate everything
                new int[] { 2, 0, 3, 1, 6, 4, 7, 5 }, // rotate clockwise
                new int[] { 1, 3, 0, 2, 5, 7, 4, 6 }, // rotate c-clockwise
                new int[] { 4, 5, 6, 7, 0, 1, 2, 3 }, // switch 4
                new int[] { 1, 0, 3, 2, 5, 4, 7, 6 }, // switch row
                new int[] { 7, 6, 0, 1, 2, 3, 4, 5 }, // switch col
                new int[] { 2, 3, 4, 5, 6, 7, 1, 0 }, // switch col
                new int[] { 3, 2, 1, 0, 7, 6, 5, 4 }, // cross
                new int[] { 1, 2, 3, 4, 5, 6, 7, 0 }, // next

                Shuffle(rng, Enumerable.Range(0, 8).ToArray()), // random
            };
            List<int[]> anim = new List<int[]>
            {
                new int[] { 1, 2, 3, 7, 0, 4, 5, 6 }, // rotate all cw
                new int[] { 1, 5, 3, 7, 0, 4, 2, 6 }, // rotate 4 cw
                new int[] { 4, 0, 6, 2, 5, 1, 7, 3 }, // rotate 4 ccw
                new int[] { 2, 3, 0, 1, 6, 7, 4, 5 }, // switch 4
                new int[] { 4, 5, 6, 7, 0, 1, 2, 3 }, // switch row
                new int[] { 7, 0, 1, 2, 3, 4, 5, 6 }, // switch col
                new int[] { 5, 4, 7, 6, 1, 0, 3, 2 }, // cross
                new int[] { 1, 2, 3, 4, 5, 6, 7, 0 }, // switch col
                new int[] { 4, 5, 6, 7, 1, 2, 3, 0 }, // next
            };

            for (int i = 0; i < 15; i++)
            {
                // yeahhh I know its wrong but I rly don't want to spend any more time trying to make it better
                int index = rng.Next(0, anim.Count);
                anim[index] = anim[index].Select(x => from[x]).ToArray();
                Run(from, anim[index], 200);

                Thread.Sleep(40);
                from = forms.Select(x => x.pos).ToArray();
            }
            Task.Run(Colors).Wait();
            foreach (int ff in from)
            {
                Debug.Write(ff.ToString() + ", ");
            }
            Debug.WriteLine("");
            Spin();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Start");
            // change to red
            foreach (Form1 form in forms)
            {
                Task.Run(() =>
                {
                    form.keyPic.BackColor = Color.Red;
                    form.color = Color.Red;
                }).Wait();
            }

            Hide();
            using (var soundPlayer = new SoundPlayer(Resources.limbo))
            {
                soundPlayer.Play();
                Sequence();
                foreach (Form1 f in forms)
                {
                    f.clickable = true;
                    f.Click += F_Click;
                }
                //soundPlayer.Stop();
            }
            Thread.Sleep(200);
            clickable = true;
        }
    }
}
