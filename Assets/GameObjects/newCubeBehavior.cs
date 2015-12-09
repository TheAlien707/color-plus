using UnityEngine;
using System.Collections;

public class newCubeBehavior : cubeBehavior {
    public int randomNumber;
    public int previousNumber;

    public int ChooseRandomNumber()
    {
        return Random.Range(0, howManyColorsCanBe);
    }

    //makes sure that a random color doesn't repeat
    //true makes the RNG generate a new number
    public bool SameColorAsPrevious()
    {
        if (previousNumber == randomNumber)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //selects the color to become
    public void ChooseARandomColor()
    {
        randomNumber = ChooseRandomNumber();
        while (SameColorAsPrevious() == true)
        {
            randomNumber = ChooseRandomNumber();
        }
        if (randomNumber == 0)
        {
            whatColor = "red";
        }
        else if (randomNumber == 1)
        {
            whatColor = "blue";
        }
        else if (randomNumber == 2)
        {
            whatColor = "yellow";
        }
        else if (randomNumber == 3)
        {
            whatColor = "green";
        }
        else if (randomNumber == 4)
        {
            whatColor = "magenta";
        }
        base.ColorSelf();
        previousNumber = randomNumber;
    }

    //calls the base-class's self-coloring method
    public void BeAColor()
    {
        //"color self" was already set up in the derived class (cubeBehavior)
        base.ColorSelf();
    }
   
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
