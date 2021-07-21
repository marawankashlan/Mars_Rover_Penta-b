using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Mars_Rover.Models;

namespace Mars_Rover.Controllers
{
    [RoutePrefix("api/Rover")]
    public class RoverClassController : ApiController
    {

        //this is the api link of the first problem 
        [AcceptVerbs("GET", "POST")]
        [Route("prob1/{start}/{command}")]
        [ResponseType(typeof(String))]
        public String problem1(String start, String command)
        {
            RoverClass rover = new RoverClass(command, start);

            return rover.MoveRover();
        }


        //this is the api link of the second problem
        [AcceptVerbs("GET", "POST")]
        [Route("prob2/{start}/{command}/{obs}")]
        [ResponseType(typeof(String))]
        public String problem2(String start, String command,String obs)
        {
            List<KeyValuePair<int, int>> o = new List<KeyValuePair<int, int>>();
            RoverClass rover = new RoverClass(command, start);
            String[] sp = obs.Split(',');
            
            foreach(String obstacle in sp)
            {
                String[] n = obstacle.Split(' ');

                o.Add(new KeyValuePair<int, int>(int.Parse(n[0]), int.Parse(n[1])));
            }
            return rover.Check_Obstacles(o);
        }

        //this is the api link of the third problem
        [AcceptVerbs("GET", "POST")]
        [Route("prob3/{start}/{end}/{obs}")]
        [ResponseType(typeof(String))]
        public String problem3(String start, String end, String obs)
        {
            List<KeyValuePair<int, int>> o = new List<KeyValuePair<int, int>>();
            RoverClass rover = new RoverClass();
            String[] sp = obs.Split(',');

            foreach (String obstacle in sp)
            {
                String[] n = obstacle.Split(' ');

                o.Add(new KeyValuePair<int, int>(int.Parse(n[0]), int.Parse(n[1])));
            }
            return rover.PathAndCommand(start,end,o);
        }

    }
}
