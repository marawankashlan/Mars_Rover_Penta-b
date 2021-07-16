using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mars_Rover.Models
{
    public class RoverClass
    {
        public String commands;
        public String start;
        public IDictionary<int, string> direction;
        private int x, y, dir;
        private Boolean flag;
        RoverClass(String c,String s)
        {
            direction = new Dictionary<int, string>();
            direction.Add(0, "North");
            direction.Add(90, "East");
            direction.Add(180, "South");
            direction.Add(270, "West");

            flag = false;
            this.commands = c;
            this.start = s;
            x = 0;
            y = 0;
            dir = 0;
        }
        public String part1()
        {
            String finalpos = "";
            Queue<char> comm = new Queue<char>();
            char c;

            //reading the start point
            int x = 0, y = 0, dir = 0;
            Boolean flag = false;
            for (int i = 0; i < start.Length; i++)
            {
                if (start[i] == '(')
                {
                    x = int.Parse(start[i + 1].ToString());
                    flag = true;
                }
                if (start[i] == ',' && flag == true)
                {
                    y = int.Parse(start[i + 1].ToString());
                    flag = false;
                }
            }
            if (start.Contains("NORTH"))
                dir = 0;
            else if (start.Contains("EAST"))
                dir = 90;
            else if (start.Contains("SOUTH"))
                dir = 180;
            else
                dir = 270;


            //reading the commands
            for (int i = 0; i < commands.Length; i++)
            {
                comm.Enqueue(commands[i]);
            }

            String temp;
            string[] news;
            int counter = comm.Count;
            for (int j = 0; j < counter; j++)
            {
                c = comm.Dequeue();
                temp = SetNewCoord(x, y, dir, c);
                news = temp.Split(' ');
                x = int.Parse(news[0].ToString());
                y = int.Parse(news[1].ToString());
                dir = int.Parse(news[2].ToString());
            }
            start = direction[dir];
            Console.WriteLine("(" + x + "," + y + "," + start + ")");
            return finalpos;
        }
        public String SetNewCoord(int x,int y,int dir, char comm)
        {

            String newcoord = "";
            if (dir == 0)//north
            {
                if (comm == 'F')
                {
                    y += 1;
                }
                else if (comm == 'B')
                {
                    y -= 1;
                }
                else if (comm == 'L')
                {
                    dir = 270;
                }
                else
                {
                    dir = 90;
                }
            }
            else if (dir == 90)//east
            {
                if (comm == 'F')
                {
                    x += 1;
                }
                else if (comm == 'B')
                {
                    x -= 1;
                }
                else if (comm == 'L')
                {
                    dir = 0;
                }
                else
                {
                    dir = 180;
                }
            }
            else if (dir == 180)//south
            {
                if (comm == 'F')
                {
                    y -= 1;
                }
                else if (comm == 'B')
                {
                    y += 1;
                }
                else if (comm == 'L')
                {
                    dir = 90;
                }
                else
                {
                    dir = 270;
                }
            }
            else if (dir == 270)//west
            {
                if (comm == 'F')
                {
                    x -= 1;
                }
                else if (comm == 'B')
                {
                    x += 1;
                }
                else if (comm == 'L')
                {
                    dir = 180;
                }
                else
                {
                    dir = 0;
                }
            }
            newcoord = x + " " + y + " " + dir;
            return newcoord;

        }
    }
}