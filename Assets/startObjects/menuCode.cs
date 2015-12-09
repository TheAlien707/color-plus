using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class menuCode : MonoBehaviour {
    public GameObject startButton;
    public GameObject instructButton;
    public Text titleUI;
    public Text gameStartUI;
    public Text instructUI;

    public static void buttonWasClicked(string title)
    {
        if (title == "start")
        {
            Application.LoadLevel("gameScene");
        }
        if (title == "instruct")
        {
            Application.LoadLevel("instructionsMenuScene");
        }
    }

	// Use this for initialization
	void Start () {
        startButton = Instantiate (startButton) as GameObject;
        startButton.GetComponent<buttonBehavior>().title = "start";
        instructButton = Instantiate(instructButton) as GameObject;
        instructButton.GetComponent<buttonBehavior>().title = "instruct";
        titleUI.text = ("Color Plus: Shannon's Version");
        gameStartUI.text = "Start Game";
        instructUI.text = "Instructions";


	}
	
	// Update is called once per frame
	void Update () {

	
	}
}
