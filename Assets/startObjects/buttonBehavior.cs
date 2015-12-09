using UnityEngine;
using System.Collections;

public class buttonBehavior : MonoBehaviour {
    menuCode menuController;
    public string title;

    public void OnMouseDown()
    {
        menuCode.buttonWasClicked(title);
    }

	// Use this for initialization
	void Start () {
	    menuController = GameObject.Find("menuControllerObject").GetComponent<menuCode>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
