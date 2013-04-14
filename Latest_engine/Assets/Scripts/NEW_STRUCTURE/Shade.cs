using UnityEngine;
using System.Collections;
using XInputDotNetPure;

[RequireComponent (typeof(BoxCollider))]
[RequireComponent (typeof(AudioSource))]
public class Shade : Player_Controllers {
	//Character Controller
	
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
	
	public bool vibrationActive = true;
	
	private Scouts target;
    private FlashlightScript targetFlashlight;
	private AudioSource targetAudioSource;
	public AudioClip killSound;
	public float killSoundVolume = 1.0f;
	public AudioClip boundaryCue;
	public float boundaryCueVolume = 1.0f;
	
	//Sensor stuff
	private string[] scoutList = new string[3]{"Player 1","Player 2","Player 3"};
	public float coneSensorAngle = 15f;
	public float coneMaxDistance = 50f;
	public float coneClosest = 0.25f;
	public float cone2ndClosest = 0.50f;
	public float cone3rdClosest = 0.75f;
	public float coneClosestVolume = 1.0f;
	public float cone2ndClosestVolume = 1.0f;
	public float cone3rdClosestVolume = 1.0f;
	public float cone4thClosestVolume = 1.0f;
	public bool controlVibeClosest = true;
	public bool controlVibe2ndClosest = false;
	public bool controlVibe3rdClosest = false;
	public bool controlVibe4thClosest = false;

    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (gameplayActive)
        {
            //////////////////////////////////////MOVEMENT
            //Vibration!
            //distance1 = Mathf.Abs(target1.transform.position.magnitude - transform.position.magnitude);
            //distance2 = Mathf.Abs(target2.transform.position.magnitude - transform.position.magnitude);
            //distance3 = Mathf.Abs(target3.transform.position.magnitude - transform.position.magnitude);
            //distance4 = Mathf.Abs(target4.transform.position.magnitude - transform.position.magnitude);
            /*
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
                */
            /*
            if(heartBeatVibrate){
                controllerVib = Mathf.Sin(sinTime);
            }
            sinTime++;
            if(controllerVib == 0)
                heartBeatVibrate = false;
            */
            /*if (controllerVib < 0.5) {
                controllerVib = 0.25f;
                //controllerVib = Mathf.Sin(currentTime * 10);
            } else if (controllerVib >= 0.5) {
                controllerVib = 100;
            }else if(controllerVib <= -0.4)
				controllerVib = 0;
			*/


            //Debug.Log (controllerVib);

            //GamePad.SetVibration(0,controllerVib,controllerVib);
            /*if(vibrationActive)
                GamePad.SetVibration(0,controllerVib, controllerVib);
            else
                GamePad.SetVibration(0,0,0);*/
            //}

            //GamePad.SetVibration(0,0,0);
            //////////////////////////////////////MOVEMENT-END



            //////////////////////////////////////SENSORS
            //currentDir.y holds current directional info
            for (int i = 0; i < scoutList.Length; i++)
            {
                GameObject scout = GameObject.Find(scoutList[i]);
                Scouts scoutController = scout.GetComponent("Scouts") as Scouts;

                if (scoutController.movementEnabled)
                {
                    Vector3 targetDir = scout.transform.position - transform.position;
                    float deltaAngle = Vector3.Angle(transform.forward, targetDir); //deltaAngle between shade and scouts
                    float distanceToScout = Vector3.Distance(transform.position, scout.transform.position);

                    //Debug.Log (distanceToScout);

                    float distanceClosest = coneMaxDistance * coneClosest;
                    float distance2ndClosest = coneMaxDistance * cone2ndClosest;
                    float distance3rdClosest = coneMaxDistance * cone3rdClosest;

                    if (deltaAngle <= coneSensorAngle)
                    {
                        if (distanceToScout <= distanceClosest)
                        {
                            if (!scout.audio.isPlaying)
                            {
                                scout.audio.volume = coneClosestVolume;
                                scout.audio.clip = scoutController.heartbeatClosest;
                                scout.audio.Play();
                            }
                            Debug.Log("Scout Closest!");
                        }
                        else if (distanceToScout <= distance2ndClosest)
                        {
                            if (!scout.audio.isPlaying)
                            {
                                scout.audio.volume = cone2ndClosestVolume;
                                scout.audio.clip = scoutController.heartbeat2ndClosest;
                                scout.audio.Play();
                            }
                            Debug.Log("Scout 2nd Closest!");
                        }
                        else if (distanceToScout <= distance3rdClosest)
                        {
                            if (!scout.audio.isPlaying)
                            {
                                scout.audio.volume = cone3rdClosestVolume;
                                scout.audio.clip = scoutController.heartbeat3rdClosest;
                                scout.audio.Play();
                            }
                            Debug.Log("Scout 3rd Closest!");
                        }
                        else if (distanceToScout <= maxDistance)
                        {
                            if (!scout.audio.isPlaying)
                            {
                                scout.audio.volume = cone4thClosestVolume;
                                scout.audio.clip = scoutController.heartbeat4thClosest;
                                scout.audio.Play();
                            }
                            Debug.Log("Scout Furthest!");
                        }

                    }
                    else
                    {
                        Debug.Log("No Scout sensed!");
                    }

                    if (distanceToScout <= distanceClosest)
                        if (controlVibeClosest)
                        {
                            GamePad.SetVibration(playerNumber, 1.0f, 1.0f);
                            scout.animation.Play("startle", PlayMode.StopAll);
                            if (!scout.audio.isPlaying)
                            {
                                if (Random.Range(0, 1) == 1)
                                    scout.audio.clip = scoutController.startledAudio1;
                                else
                                    scout.audio.clip = scoutController.startledAudio2;
                                scout.audio.Play();
                            }
                        }
                        else
                            GamePad.SetVibration(playerNumber, 0f, 0f);
                    else if (distanceToScout <= distance2ndClosest)
                        if (controlVibe2ndClosest)
                        {
                            GamePad.SetVibration(playerNumber, 1.0f, 1.0f);
                            scout.animation.Play("startle", PlayMode.StopAll);
                            if (!scout.audio.isPlaying)
                            {
                                if (Random.Range(0, 1) == 1)
                                    scout.audio.clip = scoutController.startledAudio1;
                                else
                                    scout.audio.clip = scoutController.startledAudio2;
                                scout.audio.Play();
                            }
                        }
                        else
                            GamePad.SetVibration(playerNumber, 0f, 0f);
                    else if (distanceToScout <= distance3rdClosest)
                        if (controlVibe3rdClosest)
                            GamePad.SetVibration(playerNumber, 1.0f, 1.0f);
                        else
                            GamePad.SetVibration(playerNumber, 0f, 0f);
                    else if (distanceToScout <= maxDistance)
                        if (controlVibe4thClosest)
                            GamePad.SetVibration(playerNumber, 1.0f, 1.0f);
                        else
                            GamePad.SetVibration(playerNumber, 0f, 0f);
                    else
                        GamePad.SetVibration(playerNumber, 0f, 0f);
                }
            }
            //////////////////////////////////////SENSORS-END
        }
	}
	
	//COLLISION CHECKS
	void OnTriggerEnter(Collider targetCollider) {
				
		//When shade touches scouts
		if(targetCollider.gameObject.tag == "Scout"){
	        target = targetCollider.gameObject.GetComponent("Scouts") as Scouts;
	        targetFlashlight = targetCollider.gameObject.GetComponent("FlashlightScript") as FlashlightScript;
			targetAudioSource = targetCollider.gameObject.GetComponent("AudioSource") as AudioSource;
			

	        if(target.movementEnabled){ //FUUUUUUUUUU
				target.animation.Play("faint",PlayMode.StopAll);
				target.audio.PlayOneShot(target.killSound);
				audio.PlayOneShot(killSound);
			}
			
			//targetFlashlight.isActivated = false;
			//target.flashLight.enabled = false;
			target.movementEnabled = false;
			//targetAudioSource.enabled = false;
			
	        GamePad.SetVibration(0, 1, 1);

		}
		//Scout/Shade interaction END
		
		if(targetCollider.gameObject.tag == "GameplayArea"){
			
			audio.PlayOneShot(boundaryCue, boundaryCueVolume);
			
			GamePad.SetVibration(playerNumber, 1, 1);
			
			Debug.Log ("LOOOOOL OUT OF BOUNDS");
			if(movement.x > 0)
				movement.x = -5;
			else if(movement.x < 0)
				movement.x = 5;
			if(movement.z > 0)
				movement.z = -5;
			else if(movement.z < 0)
				movement.z = 5;
			
			movement.y = 0;
			
			transform.position += movement;
			
			GamePad.SetVibration(playerNumber, 0 ,0);
		}
    }
	
	void OnApplicationQuit () {
		GamePad.SetVibration(0,0,0);
	}
}
