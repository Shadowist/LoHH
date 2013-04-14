using UnityEngine;
using System.Collections;
using XInputDotNetPure;


[RequireComponent(typeof(AudioSource))]
public class Animatic : MonoBehaviour {

    //LOL COULD BE A FUCKING ARRAY DURRRRRRRRR
    public Texture2D[] Slides = new Texture2D[13];
	
    public float timeDelay = 3.0f;

    private float timeCurrent;
    private float timeRecorded;
    private int index = 0;
    public AudioClip animaticMusic;
    //private int timesPressed = 0;
    //private bool isPrimed = true;
	
	//private bool buttonDown = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if(GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Released ||
            GamePad.GetState(PlayerIndex.Two).Buttons.A == ButtonState.Released ||
            GamePad.GetState(PlayerIndex.Three).Buttons.A == ButtonState.Released ||
            GamePad.GetState(PlayerIndex.Four).Buttons.A == ButtonState.Released ||
			Input.GetMouseButtonUp(0) &&
			isPrimed &&
			buttonDown){
		
			buttonDown = false;
		}
		
		if(GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Two).Buttons.A == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Three).Buttons.A == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Four).Buttons.A == ButtonState.Pressed ||
			index == Slides.Length+1 ||
			Input.GetMouseButtonDown(0) &&
			isPrimed &&
			!buttonDown){
			
			renderer.material.mainTexture = Slides[index];
			index++;
			
			buttonDown = true;
		}
		*/

        if (!audio.isPlaying)
        {
            GetComponent<AudioListener>().enabled = true;
            audio.clip = animaticMusic;
            audio.Play();
        }

		if(Input.GetMouseButtonDown(0) || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Two).Buttons.A == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Three).Buttons.A == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Four).Buttons.A == ButtonState.Pressed){
			if(index >= 11){
				gameObject.SetActive(false);
                audio.Stop();
                GetComponent<AudioListener>().enabled = false;
                GameObject.Find("Monster").GetComponent<AudioListener>().enabled = true;
                GameObject.Find("Ambient").audio.Play();
                GameObject.Find("GAMEPLAY_CONTROLLER").GetComponent<Gameplay_Controller>().gameplayActive = true;
                GameObject.Find("Player 1").GetComponent<Scouts>().gameplayActive = true;
                GameObject.Find("Player 2").GetComponent<Scouts>().gameplayActive = true;
                GameObject.Find("Player 3").GetComponent<Scouts>().gameplayActive = true;
                GameObject.Find("Monster").GetComponent<Shade>().gameplayActive = true;
				//Destroy(gameObject);
			} else {
				if(index <= 10)
					renderer.material.mainTexture = Slides[index];	
				index++;
			}
			
			while(GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Two).Buttons.A == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Three).Buttons.A == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Four).Buttons.A == ButtonState.Pressed);
		}
		
		/*
        timeCurrent = Time.timeSinceLevelLoad;
		
        if (timeCurrent > timeRecorded + timeDelay) {
            renderer.material.mainTexture = Slides[index];
            timeRecorded = Time.timeSinceLevelLoad;
            index++;
        }

        if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Two).Buttons.A == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Three).Buttons.A == ButtonState.Pressed ||
            GamePad.GetState(PlayerIndex.Four).Buttons.A == ButtonState.Pressed ||
            index == Slides.Length ||
            Input.GetMouseButtonDown(0)  &&
            isPrimed){

            timesPressed++;

            if(timesPressed >=2)
                Destroy(gameObject);
        }
        */
	}
}
