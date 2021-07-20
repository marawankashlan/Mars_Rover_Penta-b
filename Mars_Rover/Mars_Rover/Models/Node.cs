using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mars_Rover.Models
{
    //this class is used as a helper class in the BFS algorithm of the third problem where we save the coordinate of every node, its predecessor, its adjacent nodes, amd if it is visited or not.
    public class Node
    {
        public Coordinates coordinate;
        public Boolean visited;
        public Node pred;

        int[] dx;
        int[] dy;
        public Node(int x, int y)
        {
            coordinate = new Coordinates(x, y);
            visited = false;
            pred = null;
            dx = new int[4] { 0, -1, 0, 1 };
            dy = new int[4] { -1, 0, 1, 0 };

        }
        //get the adjacent nodes of our current node
        public List<Node> getAdj(List<Node> allNodes)
        {
            List<Node> adj = new List<Node>();
            for (int i = 0; i < 4; i++)
            {
                Node a = new Node(this.coordinate.x + dx[i], this.coordinate.y + dy[i]);
                a = a.checkNode(allNodes);
                adj.Add(a);
            }
            return adj;
        }

        //this function is used to check if one of the adjacent nodes where called or used before or not if yes then called it again if not build a new node with its coordinates
        private Node checkNode(List<Node> allNodes)
        {
            Boolean contain = false;
            int index = 0;
            for (int i = 0; i < allNodes.Count; i++)
            {
                if (allNodes[i].coordinate.x == this.coordinate.x && allNodes[i].coordinate.y == this.coordinate.y)
                {
                    contain = true;
                    index = i;
                    break;
                }

            }
            if (contain)
                return allNodes.ElementAt(index);
            else
            {
                allNodes.Add(this);
                return this;
            }

        }
    }

    //class used to save the coordinate(x,y) of every node
    public class Coordinates
    {
        public int x;
        public int y;
        public Coordinates(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}