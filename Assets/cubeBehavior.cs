using UnityEngine;
using System.Collections;

public class cubeBehavior : MonoBehaviour {
    public int x, y;
    public string whatColor = "white";
    public Material white;
    public Material black;
    public Material blue;
    public Material yellow;
    public Material red;
    public Material magenta;
    public Material green;
    public bool hasBeenClicked = false;
    public bool isNotDead = true;
    gameCode aGameController;

    //sets self color
    public void ColorSelf()
    {
        if (whatColor == "white")
        {
            GetComponent<Renderer>().material = white;
        }
        if (whatColor == "black")
        {
            GetComponent<Renderer>().material = black;
        }
        if (whatColor == "blue")
        {
            GetComponent<Renderer>().material = blue;
        }
        if (whatColor == "yellow")
        {
            GetComponent<Renderer>().material = yellow;
        }
        if (whatColor == "red")
        {
            GetComponent<Renderer>().material = red;
        }
        if (whatColor == "magenta")
        {
            GetComponent<Renderer>().material = magenta;
        }
        if (whatColor == "green")
        {
            GetComponent<Renderer>().material = green;
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
