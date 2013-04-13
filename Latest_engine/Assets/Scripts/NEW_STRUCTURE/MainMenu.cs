using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class MainMenu : MonoBehaviour {

	private int xPosition = 10;
	private int yPosition = 10;
	
	private int menuItems = 2;
	private int menuIndex = 1;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if(	Input.GetKeyDown(KeyCode.DownArrow) ||
			GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed ||
			GamePad.GetState(PlayerIndex.Two).DPad.Down == ButtonState.Pressed ||
			GamePad.GetState(PlayerIndex.Three).DPad.Down == ButtonState.Pressed ||
			GamePad.GetState(PlayerIndex.Four).DPad.Down == ButtonState.Pressed
			)
			menuIndex++;
		
		else if( Input.GetKeyDown(KeyCode.UpArrow) ||
				GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed ||
				GamePad.GetState(PlayerIndex.Two).DPad.Up == ButtonState.Pressed ||
				GamePad.GetState(PlayerIndex.Three).DPad.Up == ButtonState.Pressed ||
				GamePad.GetState(PlayerIndex.Four).DPad.Up == ButtonState.Pressed
				)
			menuIndex--;
		
		if(menuIndex <= 0)
			menuIndex = 1;
		else if(menuIndex >= 2)
			menuIndex = 2;
		
		if(Input.GetKeyDown(KeyCode.KeypadEnter) ||
			GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed ||
			GamePad.GetState(PlayerIndex.Two).Buttons.A == ButtonState.Pressed ||
			GamePad.GetState(PlayerIndex.Three).Buttons.A == ButtonState.Pressed ||
			GamePad.GetState(PlayerIndex.Four).Buttons.A == ButtonState.Pressed
			)
			if(menuIndex == 1)
				Application.LoadLevel("02");
			else if(menuIndex == 2)
				Application.Quit();
		while(GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Two).Buttons.A == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Three).Buttons.A == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Four).Buttons.A == ButtonState.Pressed);
	}
	
	void OnGUI(){
		GUI.Box (new Rect(xPosition,yPosition,100,90), "Main Menu");
		
		
		if(menuIndex == 1){
			if(GUI.Button (new Rect(xPosition+10,yPosition+60,90,20), "->New Game"))
				Application.LoadLevel("02");
		
			if(GUI.Button (new Rect(xPosition+10,yPosition+90,90,20), "Exit"))
				Application.Quit();
		}
		
		if(menuIndex == 2){
			if(GUI.Button (new Rect(xPosition+10,yPosition+60,90,20), "New Game"))
				Application.LoadLevel("02");
		
			if(GUI.Button (new Rect(xPosition+10,yPosition+90,90,20), "->Exit"))
				Application.Quit();
		}
	}
}
