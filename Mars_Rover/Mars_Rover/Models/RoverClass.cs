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

            return finalpos;
        }
    }
}