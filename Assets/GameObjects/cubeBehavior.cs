using UnityEngine;
using System.Collections;

public class cubeBehavior : MonoBehaviour {
    public int x, y;
    public int howManyColorsCanBe = 5;
    public string whatColor = "white";
    public Material white;
    public Material black;
    public Material blue;
    public Material yellow;
    public Material red;
    public Material magenta;
    public Material green;
    public Material grey;
    public bool isActive = false;
    public bool hasAColor = false;
    public bool gotBigger = false;
    public bool activeCubeInCubeGrid = false;
    gameCode aGameController;
    public MeshRenderer meshRend;

    public bool redTouch, blueTouch, greenTouch, magentaTouch, yellowTouch;
    public int howManySameColorTouchingMe;

    //sets self color
    public void ColorSelf()
    {
        if (whatColor == "white")
        {
            meshRend.material = white;
            hasAColor = false;
        }
        else if (whatColor == "black")
        {
            meshRend.material = black;
            hasAColor = false;
        }
        else if (whatColor == "blue")
        {
            meshRend.material = blue;
            hasAColor = true;
        }
        else if (whatColor == "yellow")
        {
            meshRend.material = yellow;
            hasAColor = true;
        }
        else if (whatColor == "red")
        {
            meshRend.material = red;
            hasAColor = true;
        }
        else if (whatColor == "magenta")
        {
            meshRend.material = magenta;
            hasAColor = true;
        }
        else if (whatColor == "green")
        {
            meshRend.material = green;
            hasAColor = true;
        }
        else if (whatColor == "grey")
        {
            meshRend.material = grey;
            hasAColor = false;
        }
    }

    public void OnMouseDown()
    {
        aGameController.PlayerClickedCubeGrid(this.gameObject);
    }

    //makes clickable cubes obvious
    public void OnMouseEnter()
    {
        if (hasAColor == true)
        {
            //if the cube is colored, then it is clickable
            transform.localScale += new Vector3(0.3f, 0.3f, 0);
            gotBigger = true;
        }
        if (aGameController.thereIsAnActiveCube == true && whatColor == "white")
        {
            //if there is an active cube, then white cubes are clickable too 
            transform.localScale += new Vector3(0.3f, 0.3f, 0);
            gotBigger = true;
        }
    }

    //resizes the clickable cubes once mouse leaves
    public void OnMouseExit()
    {
        if (gotBigger == true)
        {
            transform.localScale -= new Vector3(0.3f, 0.3f, 0);
            gotBigger = false;
        }
    }

	// Use this for initialization
	void Start ()
    {
        aGameController = GameObject.Find("GameControlObject").GetComponent<gameCode>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        //activeCubeInCubeGrid = aGameController.thereIsAnActiveCube;
    }
}
