using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class instructCode : MonoBehaviour {
    public Text mouseCommandsUI;
    public Text keyCommandsUI;
    public Text scoringUI;
    public Text buttonUI;
    public Text gameOverUI;
    public GameObject startButton;

	// Use this for initialization
	void Start () {
        startButton = Instantiate(startButton) as GameObject;
        keyCommandsUI.text = "Use the buttons 1-5 to select a row to place the randomly colored cube. The column will be chosen for you! Do it quick though, or you'll get a black cube!";
        mouseCommandsUI.text = "Click on a colored, but not black or grey, cube to activate it. While active, you can move it to any adjacent white cube!";
        scoringUI.text = "Your goal is to make a + shape with five of the same colored cube or one of each color. You'll earn more points if you get multiple +s in one turn!";
        buttonUI.text = "Start Game";
        gameOverUI.text = "The game is over when one of two things happen: a cube is told to go in a full row or time runs out.";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
