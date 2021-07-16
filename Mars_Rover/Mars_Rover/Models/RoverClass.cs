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
        public List<String> CoordList;
        private int x, y, dir;
        private Boolean flag;

        public RoverClass(String c,String s)
        {
            direction = new Dictionary<int, string>();
            direction.Add(0, "NORTH");
            direction.Add(90, "EAST");
            direction.Add(180, "SOUTH");
            direction.Add(270, "WEST");

            CoordList = new List<string>();
            flag = false;
            this.commands = c;
            this.start = s;
            x = 0;
            y = 0;
            dir = 0;
        }

        public String MoveRover()
        {
            String finalpos = "";
            Convert_String(start, ref x, ref y, ref dir);

            foreach (char command in commands) SetNewCoord(ref x,ref y,ref dir, command);

            start = direction[dir];
            finalpos= "(" + x + "," + y + ")" + start;
            return finalpos;
        }

        private void SetNewCoord(ref int x,ref int y,ref int dir, char comm)
        {

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

            start = direction[dir];
            CoordList.Add("(" + x + "," + y + ")" + start);
        }

        public String Check_Obstacles(List<KeyValuePair<int, int>> obs)
        {
            String finalpos = MoveRover();
            Boolean obst = false;

            finalpos= Obs(x,y,dir,CoordList, obs);

            return finalpos;
        }

        private String Obs(int xx,int yy,int dirr, List<String> coordinate, List<KeyValuePair<int, int>> test)
        {
            String finalpos = "";
            Boolean check = false;
            int counter = 0;
            foreach (String coord in coordinate)
            {
                Convert_String(coord, ref xx, ref yy, ref dirr);
                counter++;
                foreach (KeyValuePair<int, int> kvp in test)
                    if (kvp.Key == xx && kvp.Value == yy)
                    {
                        check = true;
                    }
            }
            if (check== true)
            {
                finalpos = coordinate[counter-1]+ " " + "STOPPED";
            }
            else
                finalpos = coordinate[coordinate.Count-1];

            return finalpos;
        }

        private void Convert_String(String s,ref int x,ref int y,ref int dir)
        {
            //reading the start point
            Boolean flag = false;
            for (int i = 0; i < s.Length; i++)
            {
                if (start[i] == '(')
                {
                    x = int.Parse(s[i + 1].ToString());
                    flag = true;
                }
                if (s[i] == ',' && flag == true)
                {
                    y = int.Parse(s[i + 1].ToString());
                    flag = false;
                }
            }
            if (s.Contains("NORTH"))
                dir = 0;
            else if (s.Contains("EAST"))
                dir = 90;
            else if (s.Contains("SOUTH"))
                dir = 180;
            else
                dir = 270;
        }
    }
}