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
        String end;
        private List<Node> allNodes;
        private int end_x, end_y;
        private Queue<Node> queue;
        List<Node> path1;

        public RoverClass(String c,String s)
        {
            direction = new Dictionary<int, string>();
            direction.Add(90, "NORTH");
            direction.Add(0, "EAST");
            direction.Add(270, "SOUTH");
            direction.Add(180, "WEST");

            CoordList = new List<string>();
            allNodes = new List<Node>();
            flag = false;
            this.commands = c;
            this.start = s;
            x = 0;
            y = 0;
            dir = 0;
        }
        public RoverClass()
        {
            direction = new Dictionary<int, string>();
            direction.Add(90, "NORTH");
            direction.Add(0, "EAST");
            direction.Add(270, "SOUTH");
            direction.Add(180, "WEST");

            CoordList = new List<string>();
            allNodes = new List<Node>();
            queue = new Queue<Node>();
            path1 = new List<Node>();
            flag = false;
            x = 0;
            y = 0;
            dir = 0;
            end_x = 0;
            end_y = 0;
        }

        public String MoveRover()
        {
            Convert_String(start, ref x, ref y, ref dir);

            foreach (char command in commands) SetNewCoord(ref x,ref y,ref dir, command);
              
            return CoordList[CoordList.Count - 1];
        }

        private String MoveRover(String commands, ref int x, ref int y, ref int dir)
        {
            foreach (char command in commands) SetNewCoord(ref x, ref y, ref dir, command);

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

        public String PathAndCommand(String start,String end,List<KeyValuePair<int, int>> obs)
        {
            this.start = start;
            this.end = end;

            Convert_String(start, ref x, ref y, ref dir);
            int tempDir = dir;
            Convert_String(end, ref end_x, ref end_y, ref dir);
            dir = tempDir;
            Boolean q = is_OBS(x, y, obs);
            if (q) return "obs";
            q = is_OBS(end_x, end_y, obs);
            if (q) return "obs";

            if (x == end_x && y == end_y) return "";

            Node src = new Node(x, y);
            allNodes.Add(src);
            src.visited = true;
            queue.Enqueue(src);
            Boolean found = false;
            while (queue.Count != 0)
            {
                Node u = queue.Dequeue();
                List<Node> children = u.getAdj(allNodes);
                for (int i = 0; i < children.Count; i++)
                {
                    if (children[i].visited == false)
                    {
                        if (!is_OBS(children[i].coordinate.x, children[i].coordinate.y, obs))
                        {
                            children[i].visited = true;
                            children[i].pred = u;
                            queue.Enqueue(children[i]);

                            if (children[i].coordinate.x == end_x && children[i].coordinate.y == end_y)
                            {
                                found = true;
                                path1 = path(children[i]);
                                break;
                            }
                        }
                        else
                        {
                            children[i].visited = true;
                        }
                    }

                }
                if (found) break;
            }
            return getComm(dir, path1);
        }

        private String getComm(int dir, List<Node> path)
        {
            String command = "", c = "";
            int tempx, tempy, tempDir;
            String[] str = { "F", "B", "LB", "LF", "RB", "RF" };

            for (int i = 0; i < path.Count - 1; i++)
            {
                for (int j = 0; j < str.Length; j++)
                {
                    tempx = path[i].coordinate.x;
                    tempy = path[i].coordinate.y;
                    tempDir = dir;
                    command = MoveRover(str[j], ref tempx, ref tempy, ref dir);
                    Convert_String(command, ref tempx, ref tempy, ref dir);
                    if (path[i + 1].coordinate.x == tempx && path[i + 1].coordinate.y == tempy)
                    {
                        c += str[j];
                        tempDir = dir;
                        break;
                    }
                    dir = tempDir;

                }

            }
            return c;
        }

        private Boolean is_OBS(int x, int y, List<KeyValuePair<int, int>> o)
        {
            Boolean obs = false;
            foreach (KeyValuePair<int, int> kvp in o)//obs
            {
                if (kvp.Key == x && kvp.Value == y)
                {
                    obs = true;
                    break;
                }
            }
            return obs;
        }

        private List<Node> path(Node dest)
        {
            List<Node> path = new List<Node>();
            Node crawl = dest;
            path.Add(crawl);
            while (crawl.pred != null)
            {
                path.Add(crawl.pred);
                crawl = crawl.pred;
            }
            path.Reverse();
            return path;
        }
    }
}