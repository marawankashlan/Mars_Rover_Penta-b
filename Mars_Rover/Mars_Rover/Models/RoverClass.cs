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
        private Boolean ex;
        //this constructor will be called before the first two problems giving them the start point and the commands to follow.
        public RoverClass(String c,String s)
        {
            direction = new Dictionary<int, string>();
            direction.Add(90, "NORTH");
            direction.Add(0, "EAST");
            direction.Add(270, "SOUTH");
            direction.Add(180, "WEST");
            ex = false;
            CoordList = new List<string>();
            allNodes = new List<Node>();
            flag = false;
            this.commands = c;
            this.start = s;
            x = 0;
            y = 0;
            dir = 0;
        }

        //this is an override of the constructor that will be called before the third problem as it doesn't need a commands to follow because this problem produce the commands from the start and endpoint.
        public RoverClass()
        {
            direction = new Dictionary<int, string>();
            direction.Add(90, "NORTH");
            direction.Add(0, "EAST");
            direction.Add(270, "SOUTH");
            direction.Add(180, "WEST");
            ex = false;
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

        //this function is for solving the first problem
        public String MoveRover()
        {
            //take the start string and split it into x,y coordinates and the heading of the rover. 
            Convert_String(start, ref x, ref y, ref dir);
            if (ex)
                return ("Incorrect input format");
            //for each command in the string of commands SetNewCoord function takes the current position and heading of the rover and apply the command on them.
            foreach (char command in commands) SetNewCoord(ref x,ref y,ref dir, command);
              
            return CoordList[CoordList.Count - 1];
        }

        //override of MoveRover function that will help to produce the commands of the given path in the third problem.
        //this function takes a command, current x and y, and the heading of the rover.
        private String MoveRover(String commands, ref int x, ref int y, ref int dir)
        {
            //for each command in the string of commands SetNewCoord function takes the current position and heading of the rover and apply the command on them.
            foreach (char command in commands) SetNewCoord(ref x, ref y, ref dir, command);

            return CoordList[CoordList.Count - 1];
        }

        //this function takes the current x,y,heading of the rover with a command then apply this command on the rover heading and position.
        private void SetNewCoord(ref int x,ref int y,ref int dir,char comm)
        {
            //check on the coming command and rover heading then make the suitable change on the rover position and heading according to this command. 
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

            //save the rover path in Coordlist list.
            CoordList.Add("(" + x + ", " + y + ")" + direction[dir]);
        }

        //this function is used to solve the second problem.
        //this function calls the first problem functions to make the rover follow the commands and save its position and heading of each command in a list
        public String Check_Obstacles(List<KeyValuePair<int, int>> obs)
        {
            String exp=MoveRover();
            if (exp == "Incorrect input format")
                return exp;

            //call obs function that will return if the rover path includes an obstacle or not
            else
                return Obs(x, y, dir, CoordList, obs);
        }

        //this function takes the rover position,heading,list of rover path, and the list of obstacles.
        private String Obs(int xx,int yy,int dirr, List<String> coordinate, List<KeyValuePair<int, int>> test)
        {
            Boolean check = false;
            int counter = 0;

            //for each coordinate in the rover path check if this coordinate is an obstacle or not
            foreach (String coord in coordinate)
            {
                Convert_String(coord, ref xx, ref yy, ref dirr);
                foreach (KeyValuePair<int, int> kvp in test)
                    if (kvp.Key == xx && kvp.Value == yy)
                    {
                        check = true;
                        counter = coordinate.IndexOf(coord);
                        break;
                    }
                if (check) break;
            }
            //if rover path contain an obstacle then return the step before the obs else return the final coordinate in the rover path.
            return ( check ? (coordinate[counter - 1] + " " + "STOPPED") : (coordinate[coordinate.Count - 1]));
        }

        //this function takes a string and convert it into the rover position(x,y) and heading
        private void Convert_String(String s,ref int x,ref int y,ref int dir)
        {
            flag = false;
            String tempp,qq,ww,t;
            int counter;
            if (s[0] != '(')
            {
                ex = true;
            }
            else
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == '(' && s[i + 1] != '-')
                    {
                        counter = i;
                        t = "";
                        while (true)
                        {
                            if (Char.IsDigit(s[counter + 1]))
                            {
                                t += s[counter + 1];
                            }
                            else
                                break;
                            counter++;
                        }                       
                        if (s[counter + 1] != ',' || s[counter + 2] != ' ')
                        { ex = true; }
                        try
                        {
                            x = int.Parse(t);
                            flag = true;
                        }
                        catch (Exception e)
                        {
                            ex = true;
                            break;
                        }
                    }
                    if (s[i] == '(' && s[i + 1] == '-')
                    {
                        ww = s[i + 1].ToString();
                        counter = i + 2;
                        t = "";
                        while (true)
                        {
                            if (Char.IsDigit(s[counter]))
                            {
                                t += s[counter];
                            }
                            else
                                break;
                            counter++;
                        }
                        if (s[counter] != ',' || s[counter + 1] != ' ')
                        { ex = true; }
                        try
                        {
                            t = ww + t;
                            x = int.Parse(t);
                            flag = true;
                        }
                        catch(Exception e)
                        {
                            ex = true;
                            break;
                        }
                    }
                    if (s[i] == ' ' && flag == true && s[i + 1] != '-')
                    {
                        counter = i;
                        t = "";
                        while (true)
                        {
                            if (Char.IsDigit(s[counter + 1]))
                            {
                                t += s[counter + 1];
                            }
                            else
                                break;
                            counter++;
                        }                       
                        if (s[counter + 1] != ',' && s[counter + 1] != ')')
                            ex = true;
                        if (s[i - 1] != ',')
                            ex = true;

                        try
                        {
                            y = int.Parse(t);
                            break;
                        }
                        catch (Exception e)
                        {
                            ex = true;
                            break;
                        }
                    }
                    if (s[i] == ' ' && flag == true && s[i + 1] == '-')
                    {
                        ww = s[i + 1].ToString();
                        counter = i + 2;
                        t = "";
                        while (true)
                        {
                            if (Char.IsDigit(s[counter]))
                            {
                                t += s[counter];
                            }
                            else
                                break;
                            counter++;
                        }
                        if(s[i-1]!=',')
                            ex = true;
                        if (s[counter] != ','&& s[counter] != ')')
                            ex = true;
                        t = ww + t;
                        try
                        {
                            y = int.Parse(t);
                            break;
                        }
                        catch (Exception e)
                        {
                            ex = true;
                            break;
                        }

                    }
                }
            }
            if (s.Contains("NORTH"))
                dir = 90;
            else if (s.Contains("EAST"))
                dir = 0;
            else if (s.Contains("SOUTH"))
                dir = 270;
            else if (s.Contains("WEST"))
                dir = 180;
        }

        //this function is used to solve the third problem, It takes the start and endpoint with the list of obstacles
        public String PathAndCommand(String start,String end,List<KeyValuePair<int, int>> obs)
        {
            this.start = start;
            this.end = end;
            //convert the start and endpoint to coordinate and heading 
            Convert_String(start, ref x, ref y, ref dir);
            if(ex)
                return "Incorrect input format";

            int tempDir = dir;
            Convert_String(end, ref end_x, ref end_y, ref dir);
            if (ex)
                return "Incorrect input format";

            dir = tempDir;
            //check if the start or the endpoint are obstacles 
            Boolean q = is_OBS(x, y, obs);
            if (q) return "obs";
            q = is_OBS(end_x, end_y, obs);
            if (q) return "obs";

            //check if the start point is the endpoint
            if (x == end_x && y == end_y) return "";

            //by using the BFS algorithm the path from the start point to the endpoint will be built avoiding all the obstacles
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
            //after building the path from the start to the endpoint getComm function is called to get the commands used to move the rover in this path
            return getComm(dir, path1);
        }

        //this function is used to get the commands used to move from the start point to the endpoint.
        private String getComm(int dir, List<Node> path)
        {
            String command = "", c = "";
            int tempx, tempy, tempDir;
            //string array containg every possible command
            String[] str = { "F", "B", "LB", "LF", "RB", "RF" };

            //moving on the path from start to end get the coordinate then apply a command on it then check if the output of this command equals to the next coordinate or not
            //if it matches the next coordinate then add this command to the string if not try another command
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
            //after getting the correct commands to produce the path return it.
            return c;
        }

        //this function is used to check if the passed coordinates is an obstacle or not
        private Boolean is_OBS(int x, int y, List<KeyValuePair<int, int>> o)
        {
            Boolean obs = false;
            foreach (KeyValuePair<int, int> kvp in o)
            {
                if (kvp.Key == x && kvp.Value == y)
                {
                    obs = true;
                    break;
                }
            }
            return obs;
        }

        //this function is used to build the path from the start to the endpoint by moving on the predecessor of every point from the endpoint to the start point the reverse it.
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