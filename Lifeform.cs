using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lifeform
{
    public class Life
    {
        public int alive = new int();
        public int next = new int();
        public int still = new int();
        public Life()
        {
            this.alive = 0;
            this.next = 0;
            this.still = 0;
        }
        public Lifeform.Life GenerateOne()
        {
            var output = new Lifeform.Life();
            return output;
        }
        public List<Lifeform.Life> GenerateSlice(int x)
        {
            List<Lifeform.Life> Output = new List<Lifeform.Life>();
            for (var index = 0; index < x; index++)
            {
                Output.Add(this.GenerateOne());
            }
            return Output;
        }
        public List<List<Lifeform.Life>> GenerateWorld(int x, int y)
        {
            List<List<Lifeform.Life>> Output = new List<List<Lifeform.Life>>();
            var index = 0;
            for (index = 0; index < y; index++)
            {
                Output.Add(this.GenerateSlice(x));
            }
            return Output;
        }
        public Bitmap Display(List<List<Lifeform.Life>> world)
        {
            Bitmap bmp = new Bitmap(640, 320);
            for (var yindex = 0; yindex < world.Count(); yindex++)
            {
                for (var xindex = 0; xindex < world[yindex].Count(); xindex++)
                {
                    if (world[yindex][xindex].alive == 0)
                    {
                        bmp.SetPixel(xindex, yindex, Color.FromArgb(255, 0, 0, 0));
                    }
                    else
                    {
                        if (world[yindex][xindex].still < 4)
                        {
                            bmp.SetPixel(xindex, yindex, Color.FromArgb(255, 255, 255, 255));
                        }
                        else if (world[yindex][xindex].still < 30)
                        {
                            bmp.SetPixel(xindex, yindex, Color.FromArgb(255, 255, 0, 0));
                        }
                        else
                        {
                            bmp.SetPixel(xindex, yindex, Color.FromArgb(255, 0, 255, 0));
                        }
                    }
                    if (world[yindex][xindex].next == 0)
                    {
                        world[yindex][xindex].alive = 0;
                        if (world[yindex][xindex].still > 0)
                        {
                            world[yindex][xindex].still--;
                        }
                    }
                    else
                    {
                        world[yindex][xindex].alive = 1;
                        if (world[yindex][xindex].still <= 31)
                        {
                            world[yindex][xindex].still++;
                        }
                    }

                }
                
            }
            return bmp;
        }
        public List<List<Lifeform.Life>> Seed(List<List<Lifeform.Life>> world, int multiplier)
        {
            Random r = new Random();
            if (world.Count() != 0) {
                for (var i = 0; i < multiplier; i++)
                {
                    int yint = r.Next(0, world.Count());
                    int xint = r.Next(0, world[0].Count());
                    world[yint][xint].alive = 1;
                    world[yint][xint].next = 1;
                }
            }
            return world;
        }

        public int Check(int x, int y, List<List<Lifeform.Life>> world)
        {
            var switcher = "";
            var xaxis = x == world[0].Count()-1;
            var yaxis = y == world.Count()-1;
            var isxzero = x == 0;
            var isyzero = y == 0;
            switch (xaxis) {
                case true:
                    switcher += "y";
                    break;

                case false:
                    switcher += "n";
                    break;
            }
            switch (yaxis) {
                case true:
                    switcher += "y";
                    break;
                case false:
                    switcher += "n";
                    break;
            }
            switch (isxzero) {
                case true:
                    switcher += "y";
                    break;
                case false:
                    switcher += "n";
                    break;
            }
            switch (isyzero) {
                case true:
                    switcher += "y";
                    break;
                case false:
                    switcher += "n";
                    break;
            }
            switch (switcher) {
                case "nnnn":
                    return 0;
                case "nnny":
                    return 1;

                case "nnyn":
                    return 2;

                case "nnyy":
                    return 3;

                case "nynn":
                    return 4;

                case "nyyn":
                    return 5;

                case "ynnn":
                    return 6;

                case "ynny":
                    return 7;

                case "yynn":
                    return 8;

            }
            return 9; //ignore
        }
        public List<List<Lifeform.Life>> Adjust(List<List<Lifeform.Life>> i, List<int> l, List<int> d)
        {
            for (var y = 0; y < i.Count(); y++)
            {
                for (var x = 0; x < i[y].Count(); x++)
                {
                    switch (Check(x, y, i)) {
                        case 0:
                            i[y][x].next = State(i[y][x],
                                i[y - 1][x - 1],
                                i[y - 1][x],
                                i[y - 1][x + 1],
                                i[y + 1][x - 1],
                                i[y + 1][x],
                                i[y + 1][x + 1],
                                i[y][x - 1],
                                i[y][x + 1], l, d);
                            break;

                        case 1:
                            i[y][x].next = State(i[y][x],
                                i[i.Count() -1][x - 1],
                                i[i.Count() -1][x],
                                i[i.Count() - 1][x + 1],
                                i[y + 1][x - 1],
                                i[y + 1][x],
                                i[y + 1][x + 1],
                                i[y][x - 1],
                                i[y][x + 1], l, d);
                            break;

                        case 2:
                            i[y][x].next = State(i[y][x],
                                i[y - 1][i[y].Count()-1],
                                i[y - 1][x],
                                i[y - 1][x + 1],
                                i[y + 1][i[y].Count() - 1],
                                i[y + 1][x],
                                i[y + 1][x + 1],
                                i[y][i[y].Count() - 1],
                                i[y][x + 1], l, d);
                            break;

                        case 3:
                            i[y][x].next = State(i[y][x],
                                i[i.Count()-1][i[y].Count()-1],
                                i[i.Count()-1][x],
                                i[i.Count()-1][x + 1],
                                i[y + 1][i[y].Count()-1],
                                i[y + 1][x],
                                i[y + 1][x + 1],
                                i[y][i[y].Count()-1],
                                i[y][x + 1], l, d);
                            break;

                        case 4:
                            i[y][x].next = State(i[y][x],
                                i[y - 1][x - 1],
                                i[y - 1][x],
                                i[y - 1][x + 1],
                                i[0][x - 1],
                                i[0][x],
                                i[0][x + 1],
                                i[y][x - 1],
                                i[y][x + 1], l, d);
                            break;

                        case 5:
                            i[y][x].next = State(i[y][x],
                                i[y - 1][i[y].Count() - 1],
                                i[y - 1][x],
                                i[y - 1][x + 1],
                                i[0][i[y].Count() - 1],
                                i[0][x],
                                i[0][x + 1],
                                i[y][i[y].Count() - 1],
                                i[y][x + 1], l, d);
                            break;

                        case 6:
                            i[y][x].next = State(i[y][x],
                                i[y - 1][x - 1],
                                i[y - 1][x],
                                i[y - 1][0],
                                i[y + 1][x - 1],
                                i[y + 1][x],
                                i[y + 1][0],
                                i[y][x - 1],
                                i[y][0], l, d);
                            break;

                        case 7:
                            i[y][x].next = State(i[y][x],
                                i[i.Count() - 1][x - 1],
                                i[i.Count() - 1][x],
                                i[i.Count() - 1][0],
                                i[y + 1][x - 1],
                                i[y + 1][x],
                                i[y + 1][0],
                                i[y][x - 1],
                                i[y][0], l, d);
                            break;

                        case 8:
                            i[y][x].next = State(i[y][x],
                                i[y - 1][x - 1],
                                i[y - 1][x],
                                i[y - 1][0],
                                i[0][x - 1],
                                i[0][x],
                                i[0][0],
                                i[y][x - 1],
                                i[y][0], l, d);
                            break;
                    }
                }
            }
            return i;
        }
        public int State(Life a, Life b, Life c, Life d, Life e, Life f, Life g, Life h, Life i, List<int> live, List<int> dead)
        {
            var total = b.alive + c.alive + d.alive + e.alive + f.alive + g.alive + h.alive + i.alive;

            switch (a.alive) {
                case 1:
                    for (var index = 0; index < live.Count(); index++) { 
                        if (live[index] == total)
                        {
                            return 1;
                        }
                    }
                    break;

                case 0:
                       for (var index = 0; index < dead.Count(); index++) { 
                        if (dead[index] == total)
                        {
                            return 1;
                        }
                    }
                    break;
            }
            return 0;
        }
    }
}
 