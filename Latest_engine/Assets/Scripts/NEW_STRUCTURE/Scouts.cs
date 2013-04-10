using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Scouts : Player_Controllers {

    private GameObject flashLight;
	private GameObject areaLight;
	private float duration = 4.0f;
	public int maxSpotRange = 35;
	public int minSpotRange = 14;
	public int maxAreaRange = 2;
	public int minAreaRange = 10;
	public float minSpotIntensity = 8f;
	public float maxSpotIntensity = 8f;
	public float minAreaIntensity = 1.5f;
	public float maxAreaIntensity = 1.5f;
	private float flickerSpeed = 4f;
	private float rechargeTime = 10f;
	private float flickerLength = 0.5f;
	//public KeyCode activateKey = KeyCode.E;
	
	
	//FROM KILLBOX.CS
	private bool isLightCharged = true;
	private bool isLightOn = false;
	private float tempTime;
	private bool isTimeAssigned = false;
	//ENDENDEND
	
    public bool isActivated = true; // for debugging
	
	private coffin targetCoffin;

	void Start()
	{
        flashLight = new GameObject("flashLight");
        flashLight.AddComponent<Light>();
        flashLight.light.type = LightType.Spot;
        flashLight.transform.localPosition = new Vector3(0, 0, 0);
        flashLight.transform.localRotation = new Quaternion(0, 0, 0, 0);
        flashLight.light.range = minSpotRange;
        flashLight.light.intensity = minSpotIntensity;
        flashLight.light.spotAngle = 40;
        flashLight.light.color = new Color32(242, 211, 128, 255);

        areaLight = new GameObject("areaLight");
        areaLight.AddComponent<Light>();
        areaLight.light.type = LightType.Point;
        areaLight.transform.localPosition = new Vector3(0, 0, 0);
        areaLight.transform.localRotation = new Quaternion(0,0,0,0);
        areaLight.light.range = minAreaRange;
        areaLight.light.intensity = minAreaIntensity;
        areaLight.light.color = new Color32(242, 211, 128, 255);
	}
	void Update ()
	{
        if (isActivated) {

            if (!flashLight.light.enabled)
                flashLight.light.enabled = true;
            if (!areaLight.light.enabled)
                areaLight.light.enabled = true;

            if (GamePad.GetState(playerNumber).Buttons.A == ButtonState.Pressed) {
                if (isLightCharged) {
                    Debug.Log("Flashlight On!");
                    isLightOn = true;
                    flashLight.light.range = maxSpotRange;
                    flashLight.light.intensity = maxSpotIntensity;
                    flashLight.light.spotAngle = 45;
                    flashLight.light.color = (Color.white * new Color32(242, 211, 128, 255));
                    areaLight.light.range = maxAreaRange;
                    areaLight.light.intensity = maxAreaIntensity;
                    areaLight.light.color = new Color32(242, 211, 128, 255);
                } else {
                    Debug.Log("You're flashlight is not charged!");
                }
            }
            if (isLightOn) {
                ChargeFlashlight();
            }
        } else {
            if (flashLight.light.enabled)
                flashLight.light.enabled = false;
            //if (areaLight.enabled)
            //    areaLight.enabled = false;
        }
	}
	
	void ChargeFlashlight()
	{
		isLightCharged = false;
		//bool isLightReady = false;
		if(!isTimeAssigned)
		{
			tempTime = Time.timeSinceLevelLoad;
			// Debug.Log (tempTime);
			isTimeAssigned = true;
		}
		if((Time.timeSinceLevelLoad >= (tempTime + duration)) && Time.timeSinceLevelLoad < (tempTime + (duration + 2.0f)))
		{	
			// Debug.Log("Light Flicker");
			Color a = (Color.white*new Color32(242,211,128,255));
			Color b = Color.clear;
			float t = (Mathf.PingPong(Time.time, (flickerSpeed*0.1f))/(flickerSpeed*flickerLength)) * (flickerLength*10f);
            flashLight.light.color = Color.Lerp(b, a, t);
            flashLight.light.range = Mathf.Lerp(maxSpotRange, minSpotRange, t);
            flashLight.light.intensity = Mathf.Lerp(maxSpotIntensity, minSpotIntensity, t);
            areaLight.light.intensity = minAreaIntensity;
            areaLight.light.range = Mathf.Lerp(maxAreaRange, minAreaRange, (t * 2));
            areaLight.light.color = new Color32(242, 211, 128, 255);
		}

		if((Time.timeSinceLevelLoad >= (tempTime + (duration + 2.0f)))
			&& (Time.timeSinceLevelLoad < (tempTime + (duration + rechargeTime))))
		{
			// Debug.Log ("Light off");
            flashLight.light.range = minSpotRange;
            flashLight.light.intensity = minSpotIntensity;
            flashLight.light.spotAngle = 40;
            flashLight.light.color = new Color32(242, 211, 128, 255);
            areaLight.light.range = minAreaRange;
            areaLight.light.intensity = minAreaIntensity;
            areaLight.light.color = new Color32(242, 211, 128, 255);
		}
		if((Time.timeSinceLevelLoad >= (tempTime +(duration + rechargeTime)))
			&& (Time.timeSinceLevelLoad < (tempTime +(duration + (rechargeTime+2f)))))
		{
			// Debug.Log ("areaLight flicker");
			// Debug.Log (tempTime +(duration + (rechargeTime+2f)));
			Color a = Color.white;
			Color b = Color.green;
			float t = (Mathf.PingPong(Time.time, (flickerSpeed*0.1f))/(flickerSpeed*flickerLength)) * (flickerLength*10f);
            areaLight.light.color = Color.Lerp(a, b, t);
		}
		if(Time.timeSinceLevelLoad >= (tempTime +(duration + (rechargeTime+2f))))
		{
			// Debug.Log ("Light Charged");
            areaLight.light.color = new Color32(242, 211, 128, 255);
			isLightCharged = true;
			isLightOn = false;
			isTimeAssigned = false;
		}
	}
	
	void OnTriggerEnter (Collider targetCollider) {
		if(targetCollider != null && targetCollider.name == "coffin"){
			targetCoffin = targetCollider.GetComponent("coffin") as coffin;
			
			if(!targetCoffin.hasBeenPlayed){
				targetCoffin.animation.Play("open", PlayMode.StopAll);
				targetCoffin.hasBeenPlayed = true;
				
				gameplayController.IncCoffinsTagged();
				
				Debug.Log("Coffin Tagged!");
			}
		}
	}
}
