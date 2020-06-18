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
        int counter = 250;
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
            if (counter < 1000)
            {
                counter += 200;
                Timer1.Interval = counter;
            }
        }

        private void button3_Click(object sender, EventArgs e) //SLOW DOWN
        {
            if (counter > 100)
            {
                counter -= 200;
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

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"RULES         B and S numbers           Description
2x2 - 3, 6 / 1, 2, 5                 Similar to Conway's Life in character, but totally different patterns

Amoeba - 3, 5, 7 / 1, 3, 5, 8             Forms large random areas that mimic amoebas, sometimes

Life - 3 / 2, 3                     Conway's game of life, very chaotic but beautiful to behold

Coral - 3 / 4, 5, 6, 7, 8               Creates pockets of life that slowly grow and take over like Coral in the ocean

Day & Night - 3, 6, 7, 8 / 3, 4, 6, 7, 8         Creates very organic masses of life that tend to slowly vanish

Flakes - 3 / 0, 1, 2, 3, 4, 5, 6, 7, 8       Produces beautiful flakes, starting from simple groups of cells

Gnarl - 1 / 1                       Start with a single dot and explodes into lots of squares

Maze - 3 / 1, 2, 3, 4, 5               Creates maze - like patterns.

Maze - 5 - 3 / 1, 2, 3, 4                 Creates slightly different maze - like patterns

Maze + 7 - 3, 7 / 1, 2, 3, 4               Adds 'mice' that run around the mazes sometimes

Move - 3, 6, 8 / 2, 4, 5               Very calm world

I've done some exploring as well and here's a few I've come across:

A - 4, 5, 6, 7 / 2, 7               This one tends to spawn spaceships that travel across the map

B - 1, 3, 7 / 2, 8                 This one at a certain level(around 200 randomly spawned) creates lines of replicators

C - 1 / 1, 8                     Similar to Gnarl in that it creates squares but self-destructs leading to beautiful repetition

Lastly - you can edit the rules during the game!", @"RULES");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (clicking == true)
            {
                mousex = e.X;
                mousey = e.Y;
                World[mousey][mousex].alive = 1;
                World[mousey][mousex].next = 1;
            }
        }
    }
}
