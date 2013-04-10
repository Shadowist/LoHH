using UnityEngine;
using System.Collections;
using XInputDotNetPure;

[RequireComponent (typeof(BoxCollider))]
public class Shade : Player_Controllers {
	//Character Controller
	//private Rigidbody controller;
	
	public float maxDistance = 100.0f;	
	public float controllerVib = 0.01f;
	public GameObject target1;
	public GameObject target2;
	public GameObject target3;
	public float playSpaceX = 25.0f;
	public float playSpaceZ = 25.0f;
	
	private float distance1;
	private float distance2;
	private float distance3;
	
	
	public float rotationSpeed = 300.0f;
	
	private GamePadState state;
	
	private int closestTarget = 0;

	//private int quadrant;
	public bool vibrationActive = true;
	
	private Scouts target;
    private FlashlightScript targetFlashlight;
	private AudioSource targetAudioSource;
	public AudioClip killSound;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
		//Vibration!
		distance1 = Mathf.Abs(target1.transform.position.magnitude - transform.position.magnitude);
		distance2 = Mathf.Abs(target2.transform.position.magnitude - transform.position.magnitude);
		distance3 = Mathf.Abs(target3.transform.position.magnitude - transform.position.magnitude);
		//distance4 = Mathf.Abs(target4.transform.position.magnitude - transform.position.magnitude);
		
		if((distance1 < maxDistance) || (distance2 < maxDistance) || (distance3 < maxDistance)) {
			if((distance1 < distance2) && (distance1 < distance3))
				closestTarget = 1;
			if((distance2 < distance1) && (distance2 < distance3))
				closestTarget = 2;
			if((distance3 < distance1) && (distance3 < distance2))
				closestTarget = 3;
			
			//Debug.Log (distance1 + "\t" + distance2 + "\t" + distance3 + "\t" + distance4 + "\t" + closestTarget);
			
			switch(closestTarget){
			case 1:
				controllerVib = (maxDistance - distance1)/maxDistance;
				break;
			case 2:
				controllerVib = (maxDistance - distance2)/maxDistance;
				break;
			case 3:
				controllerVib = (maxDistance - distance3)/maxDistance;
				break;
			}
			
			/*
			if(heartBeatVibrate){
				controllerVib = Mathf.Sin(sinTime);
			}
			sinTime++;
			if(controllerVib == 0)
				heartBeatVibrate = false;
			*/
            if (controllerVib < 0.5) {
                controllerVib = 0.25f;
                //controllerVib = Mathf.Sin(currentTime * 10);
            } else if (controllerVib >= 0.5) {
                controllerVib = 100;
            }else if(controllerVib <= -0.4)
				controllerVib = 0;
			
			
			
			//Debug.Log (controllerVib);
			
			//GamePad.SetVibration(0,controllerVib,controllerVib);
			if(vibrationActive)
				GamePad.SetVibration(0,controllerVib, controllerVib);
			else
				GamePad.SetVibration(0,0,0);
		}
		
		GamePad.SetVibration(0,0,0);
	}
	
	void OnTriggerEnter(Collider targetCollider) {
		if(targetCollider != null){
	        //Debug.Log(targetCollider.name + " killed");
	        target = targetCollider.gameObject.GetComponent("Scouts") as Scouts;
	        targetFlashlight = targetCollider.gameObject.GetComponent("FlashlightScript") as FlashlightScript;
			targetAudioSource = targetCollider.gameObject.GetComponent("AudioSource") as AudioSource;
			
			//if(target == null)
			//	Debug.Log ("Broken =(");
			
			if(target != null && targetFlashlight != null && targetAudioSource != null){
		        if(target.movementEnabled){ //FUUUUUUUUUU
					target.animation.Play("faint",PlayMode.StopAll);
					
				}
				
				target.movementEnabled = false;
		        targetFlashlight.isActivated = false;
				targetAudioSource.enabled = false;
				
		        GamePad.SetVibration(0, 1, 1);
			}
		}
    }
	
	void OnApplicationQuit () {
		GamePad.SetVibration(0,0,0);
	}
}
