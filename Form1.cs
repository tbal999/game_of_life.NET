using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lifeforms
{
    public partial class Form1 : Form
    {
        public Lifeform.Life L = new Lifeform.Life();
        public List<List<Lifeform.Life>> World = new List<List<Lifeform.Life>>();
        public System.Windows.Forms.Timer Timer1;
        int counter = 80;
        int mousex = 0;
        int mousey = 0;
        bool clicking = false;
        public Form1()
        {
            
            InitializeComponent();
            InitializeTimer();
            label1.Text = "Speed up or slow down.";
            label2.Text = "S at top, B at bottom.";
            label3.Text = "Type in how many lifeforms to spawn here.";
        }
        private void InitializeTimer()
        {
            // Call this procedure when the application starts.  
            // Set to 1 second.
            Timer1 = new System.Windows.Forms.Timer();
            Timer1.Interval = counter;
            Timer1.Tick += new EventHandler(Timer1_Tick);

            // Enable timer.  
            Timer1.Enabled = false;
        }

        private void Timer1_Tick(object Sender, EventArgs e)
        {
            try
            {
                string Srule = textBox1.Text;
                var SSS = Srule.Split(',');
                int[] SS = Array.ConvertAll(SSS, int.Parse);
                var S = new List<int>(SS);
                string Brule = textBox2.Text;
                var BBB = Brule.Split(',');
                int[] BB = Array.ConvertAll(BBB, int.Parse);
                var B = new List<int>(BB);
                World = L.Adjust(World, B, S);
                pictureBox1.Image = L.Display(World);
            }
            catch
            {
                OUT.Text = "Issue with the settings - try again!";
            }
        }

        private void label1_Click(object sender, EventArgs e) //OUTPUT
        {

        }

        private void button1_Click(object sender, EventArgs e) //RUN GAME
        {
            if (button.Text == "Stop")
            {
                button.Text = "Run";
                Timer1.Enabled = false;
            }
            else
            {
                button.Text = "Stop";

                Timer1.Enabled = true;
            }
        }
 
        private void button1_Click_1(object sender, EventArgs e) //SPAWN
        {
            try
            {
                int multiplier = int.Parse(textBox3.Text);
                World = L.GenerateWorld(640, 320);
                World = L.Seed(World, multiplier);
                pictureBox1.Image = L.Display(World);
            }
            catch
            {
                OUT.Text = "Issue with the settings - try again!";
            }
        }

        private void button2_Click(object sender, EventArgs e) //SPEED UP
        {
            if (counter < 100)
            {
                counter += 10;
                Timer1.Interval = counter;
            }
        }

        private void button3_Click(object sender, EventArgs e) //SLOW DOWN
        {
            if (counter > 10) {
                counter -= 10;
                Timer1.Interval = counter;
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
 
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            mousex = e.X;
            mousey = e.Y;
            try
            {
                if (clicking == true)
                {
                    World[mousey][mousex].alive = 1;
                    World[mousey][mousex].next = 1;
                    OUT.Text = mousex.ToString() + "|" + mousey.ToString();
                }
            }
            catch
            {
                OUT.Text = "Issue with the settings - try again!";
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            clicking = false;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            clicking = true;
            mousex = e.X;
            mousey = e.Y;
            try
            {
                if (clicking == true)
                {
                    World[mousey][mousex].alive = 1;
                    World[mousey][mousex].next = 1;
                    OUT.Text = mousex.ToString() + "|" + mousey.ToString();
                }
            }
            catch
            {
                OUT.Text = "Issue with the settings - try again!";
            }
        }
    }
}
