using UnityEngine;
using System.Collections;

public class newCubeBehavior : cubeBehavior {
    public int randomNumber;

    public int ChooseRandomNumber()
    {
        return Random.Range(0, howManyColorsCanBe);
    }

    //selects the color to become
    public void ChooseARandomColor()
    {
        randomNumber = ChooseRandomNumber();
        if (randomNumber == 0)
        {
            whatColor = "red";
        }
        if (randomNumber == 1)
        {
            whatColor = "blue";
        }
        if (randomNumber == 2)
        {
            whatColor = "yellow";
        }
        if (randomNumber == 3)
        {
            whatColor = "green";
        }
        if (randomNumber == 4)
        {
            whatColor = "magenta";
        }
        base.ColorSelf();
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
