using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0303_управління_потоками
{
    public partial class Form1 : Form
    {
        private Thread soundThread;
        private Thread textThread;
        private bool stopSound;

        private void PlaySound(int frequency, int time)
        {
            Console.Beep(frequency, time);
        }
        private void UpdateLabelText(string text)
        {
            if (label1.InvokeRequired)
            {
                label1.BeginInvoke(new Action(() => label1.Text = text));
            }
            else
            {
                label1.Text = text;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int frequency = 47;
            stopSound = false;
            int nud1 = Convert.ToInt32(numericUpDown1.Value);
            int nud2 = Convert.ToInt32(numericUpDown2.Value);
            int step =19953 / nud1;
            soundThread = new Thread(() =>
            {
                for (int i = 1; i <= nud1; i++)
                {
                    if (stopSound == false)
                    {
                        PlaySound(frequency, (nud2 *1000));
                    }
                    frequency += step;
                }
            });
            soundThread.Start();
            frequency = 47;
            textThread = new Thread(() =>
            {
                for (int i = 1; i <= nud1; i++)
                {
                    if (stopSound == false)
                    {
                        UpdateLabelText("Частота звучання: " + frequency);
                        Thread.Sleep(nud2 * 1000);
                    }
                    frequency += step;
                }
            });
            textThread.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stopSound = true;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
           // nud1 = Convert.ToInt32(numericUpDown1.Value);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
           // nud2 = Convert.ToInt32(numericUpDown2.Value);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
