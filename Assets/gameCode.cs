using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameCode : MonoBehaviour {
    public GameObject baseCube;
    public GameObject newCube;
	public Material clickedColor;
    public int newCubeX = 7;
    public int newCubeY = 0;
    //^^ the X & Y coordinates of the newCube
    public int chosenRow;
    public int randomCol;
    public int randomRow;
    public bool thereIsAnActiveCube = false;
    //tracks if there is an active cube
    public bool foundACube = false;
    public int activeCubeX;
    public int activeCubeY;
    public string activeCube;
    //holds the x&y & color values of the most active cube
    public GameObject[,] availablePos;
    public int randomPositionInList;
    public bool isThereAnOpenCol = true;
    public int numColsofCubes = 8;
    public int numRowsofCubes = 5;
    public int maxAvailableCubes;
    public int occupiedCubes = 0;
    public GameObject [,] cubeGrid;
    public float gameLength = 60f;
    //^^ how long should the game last (currently 60 secs)
    public bool gameOn = true;
    public float newCubeColorFreq = 2f;
    //^^ how often should the newCube get a color (currently 2 secs)
    public float turnStart = 1f;
    //^^ when should the next turn start (initially set to 1 sec after game starts)
    public int sameColorCrossPoints = 10;
    //^^ how many points a same-color cross is worth
    public int rainbowCrossPoints = 5;
    //^^ how many points a rainbow cross is worth
    public int playerPoints = 0;
    //^^ how many points the player currently has
    public int rainbowsToAward = 0;
    public int sameColorsToAward = 0;
    public int pointsToAward = 0;

    //figure out what the center cube's bool 
    public void WhatCenterBool(GameObject centerCube)
    {
        if (centerCube.GetComponent<cubeBehavior>().whatColor == "red")
        {
            centerCube.GetComponent<cubeBehavior>().redTouch = true;
        }
        else if (centerCube.GetComponent<cubeBehavior>().whatColor == "blue")
        {
            centerCube.GetComponent<cubeBehavior>().blueTouch = true;
        }
        else if (centerCube.GetComponent<cubeBehavior>().whatColor == "yellow")
        {
            centerCube.GetComponent<cubeBehavior>().yellowTouch = true; 
        }
        else if (centerCube.GetComponent<cubeBehavior>().whatColor == "magenta")
        {
            centerCube.GetComponent<cubeBehavior>().magentaTouch = true;
        }
        else if (centerCube.GetComponent<cubeBehavior>().whatColor == "green")
        {
            centerCube.GetComponent<cubeBehavior>().greenTouch = true;
        }
    }

    //figure out what the other colorTouch bools are, around the centerCube
    public void WhatColorTouchBools(GameObject centerCube, GameObject cubeToCheck)
    {
        if (cubeToCheck.GetComponent<cubeBehavior>().whatColor == "red")
        {
            centerCube.GetComponent<cubeBehavior>().redTouch = true;
        }
        else if (cubeToCheck.GetComponent<cubeBehavior>().whatColor == "blue")
        {
            centerCube.GetComponent<cubeBehavior>().blueTouch = true;
        }
        else if (cubeToCheck.GetComponent<cubeBehavior>().whatColor == "yellow")
        {
            centerCube.GetComponent<cubeBehavior>().yellowTouch = true;
        }
        else if (cubeToCheck.GetComponent<cubeBehavior>().whatColor == "magenta")
        {
            centerCube.GetComponent<cubeBehavior>().magentaTouch = true;
        }
        else if (cubeToCheck.GetComponent<cubeBehavior>().whatColor == "green")
        {
            centerCube.GetComponent<cubeBehavior>().greenTouch = true;
        }
    }


    //checks cubeGrid for rainbowCrosses
    public void RainbowCrossChecker()
    {
        //don't bother checking the outer rim because they can't be the center of a cross
        for (int rowToCheck = 1; rowToCheck < numRowsofCubes - 1; rowToCheck++)
        {
            for (int colToCheck = 1; colToCheck < numColsofCubes - 1; colToCheck++)
            {
                //only run if the cube is not white/black
                if (cubeGrid[colToCheck, rowToCheck].GetComponent<cubeBehavior>().whatColor != "white" || cubeGrid[colToCheck, rowToCheck].GetComponent<cubeBehavior>().whatColor != "black")
                {
                    //only run if the surrounding cubs are not white/black
                    if (cubeGrid[colToCheck + 1, rowToCheck].GetComponent<cubeBehavior>().whatColor != "white" || cubeGrid[colToCheck + 1, rowToCheck].GetComponent<cubeBehavior>().whatColor != "black" ||
                        cubeGrid[colToCheck - 1, rowToCheck].GetComponent<cubeBehavior>().whatColor != "white" || cubeGrid[colToCheck - 1, rowToCheck].GetComponent<cubeBehavior>().whatColor != "black" ||
                        cubeGrid[colToCheck, rowToCheck + 1].GetComponent<cubeBehavior>().whatColor != "white" || cubeGrid[colToCheck, rowToCheck + 1].GetComponent<cubeBehavior>().whatColor != "black" ||
                        cubeGrid[colToCheck, rowToCheck - 1].GetComponent<cubeBehavior>().whatColor != "white" || cubeGrid[colToCheck, rowToCheck - 1].GetComponent<cubeBehavior>().whatColor != "black")
                    {
                        WhatCenterBool(cubeGrid[colToCheck, rowToCheck]);
                        //for the cube to the right
                        WhatColorTouchBools(cubeGrid[colToCheck, rowToCheck], cubeGrid[colToCheck + 1, rowToCheck]);
                        //the cube to the left
                        WhatColorTouchBools(cubeGrid[colToCheck, rowToCheck], cubeGrid[colToCheck - 1, rowToCheck]);
                        //the cube above
                        WhatColorTouchBools(cubeGrid[colToCheck, rowToCheck], cubeGrid[colToCheck, rowToCheck + 1]);
                        //the cube below
                        WhatColorTouchBools(cubeGrid[colToCheck, rowToCheck], cubeGrid[colToCheck, rowToCheck - 1]);

                        //now check to see if all five bools are true; if they are, then there is a rainbow cross. 
                        if (cubeGrid[colToCheck, rowToCheck].GetComponent<cubeBehavior>().redTouch && cubeGrid[colToCheck, rowToCheck].GetComponent<cubeBehavior>().blueTouch &&
                            cubeGrid[colToCheck, rowToCheck].GetComponent<cubeBehavior>().greenTouch && cubeGrid[colToCheck, rowToCheck].GetComponent<cubeBehavior>().magentaTouch &&
                            cubeGrid[colToCheck, rowToCheck].GetComponent<cubeBehavior>().yellowTouch)
                        {
                            //award points
                            rainbowsToAward++;
                            //the cross should also turn black
                            cubeGrid[colToCheck, rowToCheck].GetComponent<cubeBehavior>().whatColor = "black";
                            cubeGrid[colToCheck + 1, rowToCheck].GetComponent<cubeBehavior>().whatColor = "black";
                            cubeGrid[colToCheck - 1, rowToCheck].GetComponent<cubeBehavior>().whatColor = "black";
                            cubeGrid[colToCheck, rowToCheck + 1].GetComponent<cubeBehavior>().whatColor = "black";
                            cubeGrid[colToCheck, rowToCheck - 1].GetComponent<cubeBehavior>().whatColor = "black";
                            //and then reset them all to false because they are now not true
                            cubeGrid[colToCheck, rowToCheck].GetComponent<cubeBehavior>().yellowTouch = false;
                            cubeGrid[colToCheck, rowToCheck].GetComponent<cubeBehavior>().redTouch = false;
                            cubeGrid[colToCheck, rowToCheck].GetComponent<cubeBehavior>().blueTouch = false;
                            cubeGrid[colToCheck, rowToCheck].GetComponent<cubeBehavior>().magentaTouch = false;
                            cubeGrid[colToCheck, rowToCheck].GetComponent<cubeBehavior>().greenTouch = false;
                        }
                    }
                }
            }
        }
        if (rainbowsToAward > 0)
        {
            AwardPoints();
        }
    }

    public bool ColorChecker(int centerCubeX, int centerCubeY, string cubeColor)
    {
        if (cubeGrid[centerCubeX + 1,centerCubeY].GetComponent<cubeBehavior>().whatColor == cubeColor &&
            cubeGrid[centerCubeX - 1, centerCubeY].GetComponent<cubeBehavior>().whatColor == cubeColor &&
            cubeGrid[centerCubeX, centerCubeY + 1].GetComponent<cubeBehavior>().whatColor == cubeColor &&
            cubeGrid[centerCubeX, centerCubeY - 1].GetComponent<cubeBehavior>().whatColor == cubeColor)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    //checks cubeGrid for sameColorCrosses
    /*public void SameColorCrossChecker()
    {
        //don't bother checking the outer rim because they can't be the center of a cross
        for (int rowToCheck = 1; rowToCheck < numRowsofCubes - 1; rowToCheck++)
        {
            for (int colToCheck = 1; colToCheck < numColsofCubes - 1; colToCheck++)
            {
                //only run if the cube is not white/black
                if (cubeGrid[colToCheck, rowToCheck].GetComponent<cubeBehavior>().whatColor != "white" || cubeGrid[colToCheck, rowToCheck].GetComponent<cubeBehavior>().whatColor != "black")
                {
                    //only run if the surrounding cubs are also not white/black
                    if (cubeGrid[colToCheck + 1, rowToCheck].GetComponent<cubeBehavior>().whatColor != "white" || cubeGrid[colToCheck + 1, rowToCheck].GetComponent<cubeBehavior>().whatColor != "black" ||
                        cubeGrid[colToCheck - 1, rowToCheck].GetComponent<cubeBehavior>().whatColor != "white" || cubeGrid[colToCheck - 1, rowToCheck].GetComponent<cubeBehavior>().whatColor != "black" ||
                        cubeGrid[colToCheck, rowToCheck + 1].GetComponent<cubeBehavior>().whatColor != "white" || cubeGrid[colToCheck, rowToCheck + 1].GetComponent<cubeBehavior>().whatColor != "black" ||
                        cubeGrid[colToCheck, rowToCheck - 1].GetComponent<cubeBehavior>().whatColor != "white" || cubeGrid[colToCheck, rowToCheck - 1].GetComponent<cubeBehavior>().whatColor != "black")
                    {
                        if (ColorChecker (colToCheck, rowToCheck, cubeGrid[colToCheck, rowToCheck].GetComponent<cubeBehavior>().whatColor) == true)
                        {
                            //award points
                            sameColorsToAward++;
                            //the cross should also turn black
                            cubeGrid[colToCheck, rowToCheck].GetComponent<cubeBehavior>().whatColor = "black";
                            cubeGrid[colToCheck + 1, rowToCheck].GetComponent<cubeBehavior>().whatColor = "black";
                            cubeGrid[colToCheck - 1, rowToCheck].GetComponent<cubeBehavior>().whatColor = "black";
                            cubeGrid[colToCheck, rowToCheck + 1].GetComponent<cubeBehavior>().whatColor = "black";
                            cubeGrid[colToCheck, rowToCheck - 1].GetComponent<cubeBehavior>().whatColor = "black";
                        }
                    }
                }
            }
        }
        if (sameColorsToAward > 0)
        {
            AwardPoints();
        }
    }*/

    public void AwardPoints()
    {
        if (rainbowsToAward > 0)
        {
            //calculate how many points player has earned
            pointsToAward = rainbowsToAward * rainbowCrossPoints;
            playerPoints += pointsToAward;
            //reset rainbowsToAward
            rainbowsToAward = 0;
        }
        if (sameColorsToAward > 0)
        {
            pointsToAward = sameColorsToAward * sameColorCrossPoints;
            playerPoints += pointsToAward;
            sameColorsToAward = 0;
        }
        //update score
        print(playerPoints);
    }
    public bool WasAProperKeyPressed()
    {
        if (Input.GetKeyDown("1") || Input.GetKeyDown("2") || Input.GetKeyDown("3") || Input.GetKeyDown("4") || Input.GetKeyDown("5"))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void ColorTheCubes()
    {
        for (int rowOfCubes = 0; rowOfCubes < numRowsofCubes; rowOfCubes++)
        {
            for (int CubesInRow = 0; CubesInRow < numColsofCubes; CubesInRow++)
            {
                cubeGrid[CubesInRow, rowOfCubes].GetComponent<cubeBehavior>().ColorSelf();
				if (cubeGrid[CubesInRow, rowOfCubes].GetComponent<cubeBehavior>().isActive == true) 
				{
					cubeGrid[CubesInRow, rowOfCubes].GetComponent<cubeBehavior>().meshRend.material = clickedColor;
				}
            }
        }
        if (newCube.GetComponent<newCubeBehavior>().whatColor == "white")
        {
            //newCube should never actually be white and should be invisible
            newCube.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            newCube.GetComponent<Renderer>().enabled = true;
            newCube.GetComponent<newCubeBehavior>().BeAColor();
        }
    }

    //method that determines when it is time for the newCube to be colored
    public bool TimeForNewCubeColor()
    {
        if (Time.time >= turnStart)
        {
            turnStart += newCubeColorFreq;
            return true;
        }
        else
        {
            return false;
        }
    }

    //method that checks to make sure that the previous newCube has been moved
    public bool NewCubeIsEmpty()
    {
        if (newCube.GetComponent<newCubeBehavior>().whatColor == "white")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //chooses a randomCol for newCube to be placed in
    public void ChooseARandomColumn()
    {
        List<int> availableCol = new List<int>();
        //creates a list of available columns
        for (int cubeInRow = 0; cubeInRow < numColsofCubes; cubeInRow++)
        {
            if (cubeGrid[cubeInRow, chosenRow].GetComponent<cubeBehavior>().whatColor == "white")
            {
                availableCol.Add(cubeInRow);
            }
        }
        if (availableCol.Count == 0)
        {
            isThereAnOpenCol = false;
        }
        else if (availableCol.Count > 0)
        {
            isThereAnOpenCol = true;
            randomPositionInList = Random.Range(0, availableCol.Count);
            randomCol  = availableCol[randomPositionInList];
        }

    }

    //looks for all rows that currently have an empty position
    public void ChooseARandomRow()
    {
        List<int> availableRows = new List<int>();
        for (int rowOfCubes = 0; rowOfCubes < numRowsofCubes; rowOfCubes++)
        {
            foundACube = false;
            if (foundACube == false)
            {
                for (int cubesInRow = 0; cubesInRow < numColsofCubes; cubesInRow++)
                {
                    if (cubeGrid[cubesInRow, chosenRow].GetComponent<cubeBehavior>().whatColor == "white")
                    {
                        //only rows that have available cubes should be added to the list
                        availableRows.Add(rowOfCubes);
                        foundACube = true;
                    }

                }
            }
        }
        randomPositionInList = Random.Range(0, availableRows.Count);
        randomRow = availableRows[randomPositionInList];
    }

    public void PlayerChoseARow()
    {
        if (Input.GetKeyDown("1"))
        {
            chosenRow = 0;
            //place cube in what appears to be the 1st row
        }
        if (Input.GetKeyDown("2"))
        {
            chosenRow = 1;
            //place cube in what appears to be the 2nd row
        }
        if (Input.GetKeyDown("3"))
        {
            chosenRow = 2;
            //place cube in what appears to be the 3rd row
        }
        if (Input.GetKeyDown("4"))
        {
            chosenRow = 3;
            //place cube in what appears to be the 4th row
        }
        if (Input.GetKeyDown("5"))
        {
            chosenRow = 4;
            //place cube in what appears to be the 5th row
        }
    }

    //keeps track of whether or not there is available cubes
    public bool isThereRoomIncubeGrid()
    {
        maxAvailableCubes = numColsofCubes * numRowsofCubes;
        if (occupiedCubes < maxAvailableCubes)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PlaceThenewCube()
    {
        PlayerChoseARow();
        ChooseARandomColumn();
        if (isThereAnOpenCol == true)
        {
            //set the cube in the grid as the same color as the newCube
            cubeGrid[randomCol, chosenRow].GetComponent<cubeBehavior>().whatColor = newCube.GetComponent<newCubeBehavior>().whatColor;
            //reset the newCube
            newCube.GetComponent<newCubeBehavior>().whatColor = "white";
            //color the cubes again
            cubeGrid[randomCol, chosenRow].GetComponent<cubeBehavior>().ColorSelf();
            //reset that variable so it works again
            isThereAnOpenCol = false;
            //add to the occupiedCubes list
            occupiedCubes++;
        }
        //if player attempts to place a cube in a full row, then game over
        else
        {
            gameOn = false;
        }
    }

    //Turn a random cube in cubeGrid black
    public void TurnACubeBlack()
    {
        //if there is not any room to place a black cube, then game over
        if (isThereRoomIncubeGrid() == false)
        {
            gameOn = false;
        }
        else
        {
            ChooseARandomRow();
            chosenRow = randomRow;
            ChooseARandomColumn();
            cubeGrid[randomCol, randomRow].GetComponent<cubeBehavior>().whatColor = "black";
            //add to the occupiedCube count
            occupiedCubes++;
        }
        //destroy the newCube
        newCube.GetComponent<newCubeBehavior>().whatColor = "white";
    }

    public void PlayerClickedCubeGrid(GameObject clickedCube)
    {
		//self note: white cubes CANNOT become active
		if (clickedCube.GetComponent<cubeBehavior>().isActive == true)
		{
            //player has clicked on an already-active cube, so it should deactivate
            clickedCube.GetComponent<cubeBehavior>().isActive = false;
			clickedCube.GetComponent<cubeBehavior>().ColorSelf();
			thereIsAnActiveCube = false;
        }
		else if (clickedCube.GetComponent<cubeBehavior>().whatColor != "white" && clickedCube.GetComponent<cubeBehavior>().whatColor != "black")
		{
			//Player clicked on a non-white & non-black cube
			//so this cube should activate & color itself
			clickedCube.GetComponent<cubeBehavior>().isActive = true;
            clickedCube.GetComponent<cubeBehavior>().meshRend.material = clickedColor;
            //the previously active cube (if there is one) should deactivate
            cubeGrid[activeCubeX, activeCubeY].GetComponent<cubeBehavior>().isActive = false;
            cubeGrid[activeCubeX, activeCubeY].GetComponent<cubeBehavior>().ColorSelf();
            //information about the newly active cube is taken
            activeCubeX = clickedCube.GetComponent<cubeBehavior>().x;
			activeCubeY = clickedCube.GetComponent<cubeBehavior>().y;
			activeCube = clickedCube.GetComponent<cubeBehavior>().whatColor;
			thereIsAnActiveCube = true;
		}
		else if (thereIsAnActiveCube == true && clickedCube.GetComponent<cubeBehavior>().whatColor == "white")
        {
			//player clicked an inactive cube with an active cube
            //if the clicked cube is white AND touching the active cube, then they swap places
            //diagonals are allowed
            if (clickedCube.GetComponent<cubeBehavior>().x + 1 == activeCubeX && clickedCube.GetComponent<cubeBehavior>().y == activeCubeY ||
			    clickedCube.GetComponent<cubeBehavior>().x - 1 == activeCubeX && clickedCube.GetComponent<cubeBehavior>().y == activeCubeY ||
			    clickedCube.GetComponent<cubeBehavior>().y + 1 == activeCubeY && clickedCube.GetComponent<cubeBehavior>().x == activeCubeX ||
			    clickedCube.GetComponent<cubeBehavior>().y - 1 == activeCubeY && clickedCube.GetComponent<cubeBehavior>().x == activeCubeX ||
                clickedCube.GetComponent<cubeBehavior>().x + 1 == activeCubeX && clickedCube.GetComponent<cubeBehavior>().y + 1 == activeCubeY ||
                clickedCube.GetComponent<cubeBehavior>().x - 1 == activeCubeX && clickedCube.GetComponent<cubeBehavior>().y - 1 == activeCubeY ||
                clickedCube.GetComponent<cubeBehavior>().x - 1 == activeCubeX && clickedCube.GetComponent<cubeBehavior>().y + 1 == activeCubeY ||
                clickedCube.GetComponent<cubeBehavior>().x + 1 == activeCubeX && clickedCube.GetComponent<cubeBehavior>().y - 1 == activeCubeY)
            {
                //the active cube "moves" to the clicked cube (the color moves over) and STAYS active
                clickedCube.GetComponent<cubeBehavior>().whatColor = activeCube;
			    clickedCube.GetComponent<cubeBehavior>().isActive = true;
                clickedCube.GetComponent<cubeBehavior>().meshRend.material = clickedColor;
                //the formerly active cube should become white and deactivates
                cubeGrid[activeCubeX, activeCubeY].GetComponent<cubeBehavior>().whatColor = "white";
                cubeGrid[activeCubeX, activeCubeY].GetComponent<cubeBehavior>().ColorSelf();
                cubeGrid[activeCubeX, activeCubeY].GetComponent<cubeBehavior>().isActive = false;
                //the data about the new position is taken
                activeCubeX = clickedCube.GetComponent<cubeBehavior>().x;
                activeCubeY = clickedCube.GetComponent<cubeBehavior>().y;
                activeCube = clickedCube.GetComponent<cubeBehavior>().whatColor;
            }
        }
        //nothing happens if the player clicks a white cube w/o an active cube
        //nothing happens if the player clicks a white cube that isn't touching an active cube
        
    }

	// Use this for initialization
	void Start () {
        // numColsofCubes is how many cubes should be in each row (how many columns there are)
        // numRowsofCubes is how many rows of cubes should there be
        // rowOfCubes and CubesInRow are how many has Unity made so far
        cubeGrid = new GameObject[numColsofCubes, numRowsofCubes];
        for (int rowOfCubes = 0; rowOfCubes < numRowsofCubes; rowOfCubes++)
        {
            for (int CubesInRow = 0; CubesInRow < numColsofCubes; CubesInRow++)
            {
                cubeGrid[CubesInRow, rowOfCubes] = (GameObject)Instantiate(baseCube, new Vector3(CubesInRow * 2 - 10, rowOfCubes * 2 - 4, 10), Quaternion.identity) as GameObject;
                cubeGrid[CubesInRow, rowOfCubes].GetComponent<cubeBehavior>().x = CubesInRow;
                cubeGrid[CubesInRow, rowOfCubes].GetComponent<cubeBehavior>().y = rowOfCubes;
            }
        }
        newCube = (GameObject)Instantiate(newCube, new Vector3(newCubeX, newCubeY, 10), Quaternion.identity) as GameObject;
        //initialize newCube ((placed at bottom of screen))
        /*newCube = new newCube();
        newCubePos = (GameObject)Instantiate(baseCube, new Vector3(newCube.row, newCube.col, 10), Quaternion.identity) as GameObject;
        newCubePos.GetComponent<Renderer>().material = white; */

    }
	
	// Update is called once per frame
	void Update ()
	{
        if (Time.time > gameLength)
        {
            gameOn = false;
        }
        if (gameOn == true)
        {
            if (TimeForNewCubeColor())
            {
                if (NewCubeIsEmpty())
                {
                    newCube.GetComponent<newCubeBehavior>().ChooseARandomColor();
                }
                else
                {
                    //player didn't place newCube in time, so they are penalyzed with a black cube
                    TurnACubeBlack();
                    //then newCube is destroyed
                }
                newCube.GetComponent<newCubeBehavior>().BeAColor();
                RainbowCrossChecker();
                //SameColorCrossChecker();
                ColorTheCubes();
            }
            if (WasAProperKeyPressed() == true)
            {
                PlaceThenewCube();
            }
        }
        else
        {
            print("game over");
            //game over state
            //if player score is > 0, they win
            if (playerPoints > 0)
            {
                print("you win!");
                print(playerPoints);
            }
            //if player score == 0, they lose
            else
            {
                print("you lose :(");
                print(playerPoints);
            }
        }
	
	}
}
