using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class coffin : MonoBehaviour {
	
	public bool hasBeenPlayed = false;
	public AudioClip coffinOpenTrack;
	public AudioClip coffinGhostEscapeTrack;
	
	private bool audio1HasBeenPlayed = false;
	private bool audio2HasBeenPlayed = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//For when players get individual audio
		/*
		if(hasBeenPlayed){
			if(!audio1HasBeenPlayed && !audio.isPlaying){
				audio.clip = coffinOpenTrack;
				audio.Play();
				audio1HasBeenPlayed = true;
			}
			
			if(!audio2HasBeenPlayed && !audio.isPlaying){
				audio.clip = coffinGhostEscapeTrack;
				audio.Play();
				audio2HasBeenPlayed = true;
			}
		}
		*/
		
		if(hasBeenPlayed){
			GameObject monster = GameObject.Find("Monster");
			if(!audio1HasBeenPlayed && !monster.audio.isPlaying){
				monster.audio.clip = coffinOpenTrack;
				monster.audio.Play();
				audio1HasBeenPlayed = true;
			}
			
			if(!audio2HasBeenPlayed && !monster.audio.isPlaying){
				monster.audio.clip = coffinGhostEscapeTrack;
				monster.audio.Play();
				audio2HasBeenPlayed = true;
			}
		}
	}
}
