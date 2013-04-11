using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	private int xPosition = 10;
	private int yPosition = 10;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		GUI.Box (new Rect(xPosition,yPosition,100,90), "Main Menu");
		
		
		if(GUI.Button (new Rect(xPosition+10,yPosition+60,90,20), "New Game"))
			Application.LoadLevel("02");
		
		if(GUI.Button (new Rect(xPosition+10,yPosition+90,90,20), "Exit"))
			Application.Quit();
	}
}
