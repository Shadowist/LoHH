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
			
			if(GUI.Button (new Rect(20,40,90,20), "Close Menu"))
				ShowGameplayMenu = false;
			
			if(GUI.Button (new Rect(20,70,90,20), "Restart Level"))
				Application.LoadLevel("02");
			
			if(GUI.Button (new Rect(20,100,90,20), "Main Menu"))
				Application.LoadLevel("MainMenu");
			
			if(GUI.Button (new Rect(20,130,90,20), "Exit"))
				Application.Quit();
		}
	}
}
