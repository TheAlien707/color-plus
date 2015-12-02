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
    public bool isActive = false;
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
        }
        else if (whatColor == "black")
        {
            meshRend.material = black;
        }
        else if (whatColor == "blue")
        {
            meshRend.material = blue;
        }
        else if (whatColor == "yellow")
        {
            meshRend.material = yellow;
        }
        else if (whatColor == "red")
        {
            meshRend.material = red;
        }
        else if (whatColor == "magenta")
        {
            meshRend.material = magenta;
        }
        else if (whatColor == "green")
        {
            meshRend.material = green;
        }
    }

    public void OnMouseDown()
    {
        aGameController.PlayerClickedCubeGrid(this.gameObject);
    }

	// Use this for initialization
	void Start ()
    {
        aGameController = GameObject.Find("GameControlObject").GetComponent<gameCode>();

    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
