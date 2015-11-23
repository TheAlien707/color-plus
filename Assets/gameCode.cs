using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameCode : MonoBehaviour {
    public GameObject baseCube;
    public GameObject newCube;
    public int newCubeX = 7;
    public int newCubeY = 0;
    //^^ the X & Y coordinates of the newCube
    public int chosenRow;
    public int randomCol;
    public int toBeBlackRow;
    public int toBeBlackCol;
    //^^ these are randomly generated x&y coordinates to turn an available cube black
    public bool thereIsAnActiveCube = false;
    //tracks if there is an active cube
    public int formerlyClickedCubeX;
    public int formerlyClickedCubeY;
    public string formerlyClickedCube;
    //holds the x&y & color values of the most recent clicked cube
    public GameObject[,] availablePos;
    public int randomPositionInList;
    public bool isThereAnOpenCol = true;
    public int numColsofCubes = 8;
    public int numRowsofCubes = 5;
    public GameObject [,] cubeGrid;
    public float gameLength = 60f;
    //^^ how long should the game last (currently 60 secs)
    public float newCubeColorFreq = 2f;
    //^^ how often should the newCube get a color (currently 2 secs)
    public float turnStart = 1f;
    //^^ when should the next turn start (initially set to 1 sec after game starts)

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
            }
        }
        newCube.GetComponent<newCubeBehavior>().BeAColor();
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

    public void PlaceThenewCube()
    {
        PlayerChoseARow();
        ChooseARandomColumn();
        if (isThereAnOpenCol == true)
        {
            cubeGrid[randomCol, chosenRow].GetComponent<cubeBehavior>().whatColor = newCube.GetComponent<newCubeBehavior>().whatColor;
            //set the cube in the grid as the same color as the newCube
            newCube.GetComponent<newCubeBehavior>().whatColor = "white";
            //reset the newCube
            ColorTheCubes();
            //color the cubes again
        }
    }

    //Not yet fully implemented; will turn a random cube in cubeGrid black
    public void TurnACubeBlack()
    {
        for (int cubeInRow = 0; cubeInRow < numColsofCubes; cubeInRow++)
        {
            for (int rowOfCubes = 0; rowOfCubes < numRowsofCubes; rowOfCubes++)
            {
                if (cubeGrid[cubeInRow, rowOfCubes].GetComponent<cubeBehavior>().whatColor == "white")
                {
                    

                }
            }
        }
    }

    public void PlayerClickedCubeGrid(GameObject clickedCube)
    {
        if (clickedCube.GetComponent<cubeBehavior>().hasBeenClicked == true)
        {
            //player has clicked on an already-active cube, so it should deactivate
            clickedCube.GetComponent<cubeBehavior>().hasBeenClicked = false;
        }
        else if (clickedCube.GetComponent<cubeBehavior>().hasBeenClicked == false && thereIsAnActiveCube == true)
        {
            //player has clicked on an inactive cube while there is an active cube
            //clickedCube becomes active
            clickedCube.GetComponent<cubeBehavior>().hasBeenClicked = true;
            //if the clicked cube is white AND touching the active cube, then they swap places
            if (clickedCube.GetComponent<cubeBehavior>().whatColor == "white")
            {
                if (clickedCube.GetComponent<cubeBehavior>().x + 1 == formerlyClickedCubeX && clickedCube.GetComponent<cubeBehavior>().y == formerlyClickedCubeY)
                {
                    //if clicked cube is directly to the right of the active cube
                    //the active cube "moves" to the clicked cube (the color moves over)
                    clickedCube.GetComponent<cubeBehavior>().whatColor = formerlyClickedCube;
                    //the formerly active cube should become white
                    cubeGrid[formerlyClickedCubeX, formerlyClickedCubeY].GetComponent<cubeBehavior>().whatColor = "white";
                }
                if (clickedCube.GetComponent<cubeBehavior>().x - 1 == formerlyClickedCubeX && clickedCube.GetComponent<cubeBehavior>().y == formerlyClickedCubeY)
                {
                    //if clicked cube is directly to the left of the active cube
                    //the active cube "moves" to the clicked cube (the color moves over)
                    clickedCube.GetComponent<cubeBehavior>().whatColor = formerlyClickedCube;
                    //the formerly active cube should become white
                    cubeGrid[formerlyClickedCubeX, formerlyClickedCubeY].GetComponent<cubeBehavior>().whatColor = "white";
                }
                if (clickedCube.GetComponent<cubeBehavior>().y + 1 == formerlyClickedCubeY && clickedCube.GetComponent<cubeBehavior>().x == formerlyClickedCubeX)
                {
                    //if clicked cube is directly above active cube
                    //the active cube "moves" to the clicked cube (the color moves over)
                    clickedCube.GetComponent<cubeBehavior>().whatColor = formerlyClickedCube;
                    //the formerly active cube should become white
                    cubeGrid[formerlyClickedCubeX, formerlyClickedCubeY].GetComponent<cubeBehavior>().whatColor = "white";
                }
                if (clickedCube.GetComponent<cubeBehavior>().y - 1 == formerlyClickedCubeY && clickedCube.GetComponent<cubeBehavior>().x == formerlyClickedCubeX)
                {
                    //if clicked cube is directly below active cube
                    //the active cube "moves" to the clicked cube (the color moves over)
                    clickedCube.GetComponent<cubeBehavior>().whatColor = formerlyClickedCube;
                    //the formerly active cube should become white
                    cubeGrid[formerlyClickedCubeX, formerlyClickedCubeY].GetComponent<cubeBehavior>().whatColor = "white";
                }
            }
            //if the clickedCube isn't white, then active cube just teleports over (unless its black, in which case nothing happens)
            else if (clickedCube.GetComponent<cubeBehavior>().whatColor == "red" || clickedCube.GetComponent<cubeBehavior>().whatColor == "blue" || clickedCube.GetComponent<cubeBehavior>().whatColor == "green" || clickedCube.GetComponent<cubeBehavior>().whatColor == "magenta" || clickedCube.GetComponent<cubeBehavior>().whatColor == "yellow")
            {
            //player clicked on a colored (not black) cube with an active cube
            //active cube should deactivate
            cubeGrid[formerlyClickedCubeX, formerlyClickedCubeY].GetComponent<cubeBehavior>().hasBeenClicked = false;
            //clicked cube activates
            clickedCube.GetComponent<cubeBehavior>().hasBeenClicked = true;
            }
        }
        else if (clickedCube.GetComponent<cubeBehavior>().hasBeenClicked == false && thereIsAnActiveCube == false)
        {
            //player clicked on an inactive cube but there is no other active cube
            //so this cube should activate
            clickedCube.GetComponent<cubeBehavior>().hasBeenClicked = true;
            //but nothing else should happen
            formerlyClickedCubeX = clickedCube.GetComponent<cubeBehavior>().x;
            formerlyClickedCubeY = clickedCube.GetComponent<cubeBehavior>().y;
            formerlyClickedCube = clickedCube.GetComponent<cubeBehavior>().whatColor;
            thereIsAnActiveCube = true;
        }
        
        ColorTheCubes();
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
        ColorTheCubes();
        if (TimeForNewCubeColor() == true)
        {
            if (NewCubeIsEmpty() == true)
            {
                newCube.GetComponent<newCubeBehavior>().ChooseARandomColor();
            }
            else
            {
                //TurnACubeBlack();
                //a random white cube in the cubeGrid[] should turn black
                // haven't figured out how yet though--> 2 dimensional list? 
                print("game over");
            }
        }
        if (WasAProperKeyPressed() == true)
        {
            PlaceThenewCube();
        }
	
	}
}
