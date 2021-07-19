using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mars_Rover.Models;
using System.Collections.Generic;

namespace Mars_Rover_UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void MoveRover_CorrectCommit_FollowCommand()
        {
            //Arrange
            String commands = "FLFFFRFLB";
            String start = "(4, 2,EAST)";
            String expected = "(6, 4)NORTH";
            RoverClass rover = new RoverClass(commands, start);

            //Act
            String output= rover.MoveRover();

            //Assert
            Assert.AreEqual(expected, output, "Rover didn't follow the commands correctly");
        }
        [TestMethod]
        public void MoveRover_CorrectCommit2_FollowCommand()
        {
            //Arrange
            String commands = "FLFFRFLB";
            String start = "(4, 2,EAST)";
            String expected = "(6, 3)NORTH";
            RoverClass rover = new RoverClass(commands, start);

            //Act
            String output = rover.MoveRover();

            //Assert
            Assert.AreEqual(expected, output, "Rover didn't follow the commands correctly");
        }
        [TestMethod]
        public void MoveRover_ErrorInCommand_FollowCommand()
        {
            //Arrange
            String commands = "FLWFFRFLBS";
            String start = "(0, 0,NORTH)";
            String expected = "(-1, 2)WEST";
            RoverClass rover = new RoverClass(commands, start);

            //Act
            String output = rover.MoveRover();

            //Assert
            Assert.AreEqual(expected, output, "Rover didn't follow the commands correctly");
        }
        [TestMethod]
        public void MoveRover_ErrorInCommand2_FollowCommand()
        {
            //Arrange
            String commands = "CTFLPPFRBVX";
            String start = "(-1, 5,SOUTH)";
            String expected = "(0, 5)SOUTH";
            RoverClass rover = new RoverClass(commands, start);

            //Act
            String output = rover.MoveRover();

            //Assert
            Assert.AreEqual(expected, output, "Rover didn't follow the commands correctly");
        }
        [TestMethod]
        public void Check_Obstacles_CorrectCommit_FollowCommand()
        {
            //Arrange
            List<KeyValuePair<int, int>> obs = new List<KeyValuePair<int, int>>();

            String commands = "FLFFFRFLB";
            String start = "(4, 2,EAST)";
            String expected = "(5, 5)EAST STOPPED";
            RoverClass rover = new RoverClass(commands, start);
            obs.Add(new KeyValuePair<int, int>(1, 3));
            obs.Add(new KeyValuePair<int, int>(6, 5));
            obs.Add(new KeyValuePair<int, int>(7, 4));

            //Act
            String output = rover.Check_Obstacles(obs);

            //Assert
            Assert.AreEqual(expected, output, "Rover didn't follow the commands correctly");
        }

        /// <summary>
        /// Problem 3 test cases
        /// First one is a correct flow test case where a start and an end with the list of obs added and the rover will move from the start to the end avoiding all obs
        /// </summary>
        [TestMethod]
        public void PathAndCommand_CorrectFlow_CorrectCommands()
        {
            //Arrange
            List<KeyValuePair<int, int>> obs = new List<KeyValuePair<int, int>>();
            String end = "(6, 7)";
            String start = "(4, 2, EAST)";
            String expected = "BLFFFFLBBBLB";
            RoverClass rover = new RoverClass();
            obs.Add(new KeyValuePair<int, int>(1, 3));
            obs.Add(new KeyValuePair<int, int>(5, 2));
            obs.Add(new KeyValuePair<int, int>(6, 5));
            obs.Add(new KeyValuePair<int, int>(7, 4));
            obs.Add(new KeyValuePair<int, int>(5, 5));
            obs.Add(new KeyValuePair<int, int>(4, 3));
            obs.Add(new KeyValuePair<int, int>(5, 7));

            //Act
            String output = rover.PathAndCommand(start,end,obs);

            //Assert
            Assert.AreEqual(expected, output, "Rover didn't reach the endpoint safely");
        }

        /// <summary>
        /// second test case where the start point is also the endpoint
        /// </summary>
        [TestMethod]
        public void PathAndCommand_StartIsEnd_CorrectCommands()
        {
            //Arrange
            List<KeyValuePair<int, int>> obs = new List<KeyValuePair<int, int>>();
            String end = "(9, 1)";
            String start = "(9, 1, EAST)";
            String expected = "";
            RoverClass rover = new RoverClass();
            obs.Add(new KeyValuePair<int, int>(1, 3));
            obs.Add(new KeyValuePair<int, int>(5, 2));
            obs.Add(new KeyValuePair<int, int>(6, 5));
            obs.Add(new KeyValuePair<int, int>(7, 4));
            obs.Add(new KeyValuePair<int, int>(5, 5));
            obs.Add(new KeyValuePair<int, int>(4, 3));
            obs.Add(new KeyValuePair<int, int>(5, 7));

            //Act
            String output = rover.PathAndCommand(start, end, obs);

            //Assert
            Assert.AreEqual(expected, output, "Rover didn't reach the endpoint safely");
        }

        /// <summary>
        /// third test case where the start point is also an obs
        /// </summary>
        [TestMethod]
        public void PathAndCommand_StartIsObs_CorrectCommands()
        {
            //Arrange
            List<KeyValuePair<int, int>> obs = new List<KeyValuePair<int, int>>();
            String end = "(2, 7)";
            String start = "(5, 5, SOUTH)";
            String expected = "obs";
            RoverClass rover = new RoverClass();
            obs.Add(new KeyValuePair<int, int>(1, 3));
            obs.Add(new KeyValuePair<int, int>(5, 2));
            obs.Add(new KeyValuePair<int, int>(6, 5));
            obs.Add(new KeyValuePair<int, int>(7, 4));
            obs.Add(new KeyValuePair<int, int>(5, 5));
            obs.Add(new KeyValuePair<int, int>(4, 3));
            obs.Add(new KeyValuePair<int, int>(5, 7));

            //Act
            String output = rover.PathAndCommand(start, end, obs);

            //Assert
            Assert.AreEqual(expected, output, "Rover didn't reach the endpoint safely");
        }

        /// <summary>
        /// fourth test case where the endpoint is also an obs
        /// </summary>
        [TestMethod]
        public void PathAndCommand_EndIsObs_CorrectCommands()
        {
            //Arrange
            List<KeyValuePair<int, int>> obs = new List<KeyValuePair<int, int>>();
            String end = "(6, 5)";
            String start = "(1, 0, SOUTH)";
            String expected = "obs";
            RoverClass rover = new RoverClass();
            obs.Add(new KeyValuePair<int, int>(1, 3));
            obs.Add(new KeyValuePair<int, int>(5, 2));
            obs.Add(new KeyValuePair<int, int>(6, 5));
            obs.Add(new KeyValuePair<int, int>(7, 4));
            obs.Add(new KeyValuePair<int, int>(5, 5));
            obs.Add(new KeyValuePair<int, int>(4, 3));
            obs.Add(new KeyValuePair<int, int>(5, 7));

            //Act
            String output = rover.PathAndCommand(start, end, obs);

            //Assert
            Assert.AreEqual(expected, output, "Rover didn't reach the endpoint safely");
        }
    }
}
