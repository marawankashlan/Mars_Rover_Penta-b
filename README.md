# Mars_Rover_Penta-b
 
This is the solution of Mars Rover assessment of penta-b.

To run the project from visual studio go to controls file then choose the RoverClassControl file from it, then press on run the project.

After the browser starts you will add /api/Rover/prob1/Start point/command to the URL to run the first problem API.     
the start point should be in this format (x, y,DIRECTION) example: (4, 2,EAST)   
Example for the URL:( http://localhost:61043//api/Rover/prob1/(4, 2,EAST)/FFLBBRFF)

After the browser starts you will add /api/Rover/prob2/Start point/command/obstacles to the URL to run the second prolem API.     
the start point should be in this format (x, y,DIRECTION) example (4, 2,EAST)  
the obstacles should be in this format x1 y1,x2 y2,x3 y3 ex 1 2,4 4,7 5   
Example for the URL:( http://localhost:61043//api/Rover/prob2/(4, 2,EAST)/FFLBBRFF/1 2,4 4,7 5)

After the browser starts you will add /api/Rover/prob3/Start point/endpoint/obstacles to the URL to run the third prolem API.     
the start point should be in this format (x, y,DIRECTION) example (4, 2,EAST)  
the endpoint should be in this format (x, y) example: (5, 6)   
the obstacles should be in this format x1 y1,x2 y2,x3 y3 example: 1 2,4 4,7 5  
Example for the URL:( http://localhost:61043//api/Rover/prob3/(4, 2,EAST)/(5, 6)/1 2,4 4,7 5) 

to run the unit test of this assessment you will choose UnitTest1 file click on test from toolbox choose windows then test explorer, the test cases will appear in the test explorer click on run all.
