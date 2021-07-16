using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mars_Rover.Models;

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
            String start = "(4,2,EAST)";
            String expected = "(6,4)North";
            RoverClass rover = new RoverClass(commands, start);

            //Act
            String output= rover.MoveRover();

            //Assert
            Assert.AreEqual(expected, output, "Rover didn't follow the commands correctly");
        }
    }
}
