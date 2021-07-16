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
    }
}
