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
            direction.Add(90, "NORTH");
            direction.Add(0, "EAST");
            direction.Add(270, "SOUTH");
            direction.Add(180, "WEST");

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
            Convert_String(start, ref x, ref y, ref dir);

            foreach (char command in commands) SetNewCoord(ref x,ref y,ref dir, command);
              
            return CoordList[CoordList.Count - 1];
        }

        private void SetNewCoord(ref int x,ref int y,ref int dir,char comm)
        {
            if ((comm == 'F' && dir == 0) || (comm == 'B' && dir == 180))
                x += 1;
           else if ((comm == 'B' && dir == 0) || (comm == 'F' && dir == 180))
                x -= 1;
           else if ((comm == 'F' && dir == 90) || (comm == 'B' && dir == 270))
                y += 1;
           else  if ((comm == 'B' && dir == 90) || (comm == 'F' && dir == 270))
                y -= 1;
            else if (comm == 'L')
                dir = ((dir + 90) == 360) ? (360 - (dir + 90)) : (dir + 90);
            else if (comm == 'R')
                dir = ((dir - 90) == -90) ? (360 + (dir - 90)) : (dir - 90);

            CoordList.Add("(" + x + ", " + y + ")" + direction[dir]);
        }

        public String Check_Obstacles(List<KeyValuePair<int, int>> obs)
        {
            MoveRover();

            return Obs(x, y, dir, CoordList, obs);
        }

        private String Obs(int xx,int yy,int dirr, List<String> coordinate, List<KeyValuePair<int, int>> test)
        {
            Boolean check = false;
            int counter = 0;

            foreach (String coord in coordinate)//string
            {
                Convert_String(coord, ref xx, ref yy, ref dirr);
                foreach (KeyValuePair<int, int> kvp in test)//obs
                    if (kvp.Key == xx && kvp.Value == yy)
                    {
                        check = true;
                        counter = coordinate.IndexOf(coord);
                        break;
                    }
                if (check) break;
            }

           return ( check ? (coordinate[counter - 1] + " " + "STOPPED") : (coordinate[coordinate.Count - 1]));
        }

        private void Convert_String(String s,ref int x,ref int y,ref int dir)
        {
            //reading the start point
            flag = false;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                {
                    x = int.Parse(s[i + 1].ToString());
                    flag = true;
                }
                if (s[i] == ' ' && flag == true)
                {
                    y = int.Parse(s[i + 1].ToString());
                    break;
                }
            }
            if (s.Contains("NORTH"))
                dir = 90;
            else if (s.Contains("EAST"))
                dir = 0;
            else if (s.Contains("SOUTH"))
                dir = 270;
            else
                dir = 180;
        }
    }
}