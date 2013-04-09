//Gameplay Controller

using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Gameplay_Controller : MonoBehaviour {
	private int coffinsTagged = 0;
	private int runnersTagged = 0;
	private bool winReported = false;
	private GameObject monster;
	
	public GameObject mainAnimatic;
	public Texture2D runnersWin;
	public Texture2D monsterWins;
	
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
		} else if (runnersTagged == 3) {
			Debug.Log("Monster wins!");
			winReported = true;
			mainAnimatic.renderer.material.mainTexture = monsterWins;
			mainAnimatic.SetActive(true);
		}
		
		if(winReported == true)
			monster = GameObject.Find("Monster");
			Destroy(monster);
			GamePad.SetVibration(0,0,0);
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
}
