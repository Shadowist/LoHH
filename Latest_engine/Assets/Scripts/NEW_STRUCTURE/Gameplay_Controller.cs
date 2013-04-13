//Gameplay Controller

using UnityEngine;
using System.Collections;
using XInputDotNetPure;

[RequireComponent(typeof(AudioSource))]
public class Gameplay_Controller : MonoBehaviour {
	private int coffinsTagged = 0;
	private int runnersTagged = 0;
	private bool winReported = false;
	private GameObject monster;
	
	public GameObject mainAnimatic;
	public Texture2D runnersWin;
	public Texture2D monsterWins;
	
	public float winnerClipVolume = 0.75f;
	public AudioClip runnersWinAudio;
	public AudioClip monsterWinsAudio;
	
	//GUI!
	private bool ShowGameplayMenu = true;
	private int menuIndex = 1;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(coffinsTagged == 4){
			Debug.Log("Runners win!");
			winReported = true;
			mainAnimatic.renderer.material.mainTexture = runnersWin;
			mainAnimatic.SetActive(true);
			audio.PlayOneShot(runnersWinAudio, winnerClipVolume);
		} else if (runnersTagged == 3) {
			Debug.Log("Monster wins!");
			winReported = true;
			mainAnimatic.renderer.material.mainTexture = monsterWins;
			mainAnimatic.SetActive(true);
			audio.PlayOneShot(monsterWinsAudio, winnerClipVolume);
		}
		
		if(winReported == true)
			monster = GameObject.Find("Monster");
			Destroy(monster);
			GamePad.SetVibration(0,0,0);
		
		if((Input.GetKeyDown(KeyCode.Escape) || 
			GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed ||
			GamePad.GetState(PlayerIndex.Two).Buttons.Start == ButtonState.Pressed ||
			GamePad.GetState(PlayerIndex.Three).Buttons.Start == ButtonState.Pressed ||
			GamePad.GetState(PlayerIndex.Four).Buttons.Start == ButtonState.Pressed) &&
			ShowGameplayMenu == false)
			ShowGameplayMenu = true;
		else if((Input.GetKeyDown(KeyCode.Escape) || 
			GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed ||
			GamePad.GetState(PlayerIndex.Two).Buttons.Start == ButtonState.Pressed ||
			GamePad.GetState(PlayerIndex.Three).Buttons.Start == ButtonState.Pressed ||
			GamePad.GetState(PlayerIndex.Four).Buttons.Start == ButtonState.Pressed) &&
			ShowGameplayMenu == true)
			ShowGameplayMenu = false;
		
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
		else if(menuIndex >= 4)
			menuIndex = 4;
		
		if(Input.GetKeyDown(KeyCode.KeypadEnter) ||
			GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed ||
			GamePad.GetState(PlayerIndex.Two).Buttons.A == ButtonState.Pressed ||
			GamePad.GetState(PlayerIndex.Three).Buttons.A == ButtonState.Pressed ||
			GamePad.GetState(PlayerIndex.Four).Buttons.A == ButtonState.Pressed
			)
			if(menuIndex == 1) //Bah...could've just used a switch statement
				ShowGameplayMenu = false;
			else if(menuIndex == 2)
				Application.LoadLevel("02");
			else if(menuIndex == 3)
				Application.LoadLevel("MainMenu");
			else if(menuIndex == 4)
				Application.Quit();
		
		while(GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Two).DPad.Down == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Three).DPad.Down == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Four).DPad.Down == ButtonState.Pressed ||
			GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Two).DPad.Up == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Three).DPad.Up == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Four).DPad.Up == ButtonState.Pressed ||
			GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Two).Buttons.Start == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Three).Buttons.Start == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Four).Buttons.Start == ButtonState.Pressed ||
			GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Two).Buttons.A == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Three).Buttons.A == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Four).Buttons.A == ButtonState.Pressed);
	}
	
	public void IncCoffinsTagged(){
		coffinsTagged++;
		print ("Coffins tagged: " + coffinsTagged);
	}
	
	public void DecCoffinsTagged(){
		coffinsTagged--;
	}

	public void ResetCoffinsTagged(){
		coffinsTagged = 0;
	}
	
	public void IncRunnersTagged(){
		runnersTagged++;
		print ("Scouts tagged: " + runnersTagged);
	}
	
	public void DecRunnersTagged(){
		runnersTagged--;
	}
	
	public void ResetRunnersTagged(){
		runnersTagged = 0;
	}
	
	void OnGUI(){
		if(ShowGameplayMenu){
			GUI.Box (new Rect(10,10,110,150), "Game Menu");
			
			
			if(menuIndex == 1){
				if(GUI.Button (new Rect(20,40,90,20), "->Close Menu"))
					ShowGameplayMenu = false;
				
				if(GUI.Button (new Rect(20,70,90,20), "Restart Level"))
					Application.LoadLevel("02");
				
				if(GUI.Button (new Rect(20,100,90,20), "Main Menu"))
					Application.LoadLevel("MainMenu");
				
				if(GUI.Button (new Rect(20,130,90,20), "Exit"))
					Application.Quit();
			} else if(menuIndex == 2){
				if(GUI.Button (new Rect(20,40,90,20), "Close Menu"))
					ShowGameplayMenu = false;
				
				if(GUI.Button (new Rect(20,70,90,20), "->Restart Level"))
					Application.LoadLevel("02");
				
				if(GUI.Button (new Rect(20,100,90,20), "Main Menu"))
					Application.LoadLevel("MainMenu");
				
				if(GUI.Button (new Rect(20,130,90,20), "Exit"))
					Application.Quit();
			} else if(menuIndex == 3){
				if(GUI.Button (new Rect(20,40,90,20), "Close Menu"))
					ShowGameplayMenu = false;
				
				if(GUI.Button (new Rect(20,70,90,20), "Restart Level"))
					Application.LoadLevel("02");
				
				if(GUI.Button (new Rect(20,100,90,20), "->Main Menu"))
					Application.LoadLevel("MainMenu");
				
				if(GUI.Button (new Rect(20,130,90,20), "Exit"))
					Application.Quit();
			} else if(menuIndex == 4){
				if(GUI.Button (new Rect(20,40,90,20), "Close Menu"))
					ShowGameplayMenu = false;
				
				if(GUI.Button (new Rect(20,70,90,20), "Restart Level"))
					Application.LoadLevel("02");
				
				if(GUI.Button (new Rect(20,100,90,20), "Main Menu"))
					Application.LoadLevel("MainMenu");
				
				if(GUI.Button (new Rect(20,130,90,20), "->Exit"))
					Application.Quit();
			}
		}
	}
}
