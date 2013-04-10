using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Player_Controllers : MonoBehaviour {

	//Character Controller
	private Rigidbody controller;
	
	//Movement variables
	public float playerSpeed = 3.5f;
	private Vector3 movement = Vector3.zero;

    public bool movementEnabled = true;
	
	//Rotation variables
	//private Quaternion currentDir;
	private float targetDir = 0.0f;
	
	//Physics settings
	public float mass = 1.0f;
	/*
	//Setting up for custom controls
	public KeyCode moveUp = KeyCode.W;
	public KeyCode moveDown = KeyCode.S;
	public KeyCode moveLeft = KeyCode.A;
	public KeyCode moveRight = KeyCode.D;
    */

    public PlayerIndex playerNumber = PlayerIndex.Two;
    private GamePadState state;
    private int quadrant;
    private Vector3 currentDir = Vector3.zero;
	
	public KeyCode Up = KeyCode.UpArrow;
	public KeyCode Down = KeyCode.DownArrow;
	public KeyCode Left = KeyCode.LeftArrow;
	public KeyCode Right = KeyCode.RightArrow;
	
	private Vector3 moveTester;
	private bool isMoveTesterAssigned = false;
	
	public Gameplay_Controller gameplayController;
	
	private bool hasBeenTagged = false;
	// Use this for initialization
	void Start () {
		if(!GetComponent("Rigidbody"))
			gameObject.AddComponent("Rigidbody");
		if(!GetComponent("BoxCollider"))
			gameObject.AddComponent("BoxCollider");
	} 

	void FixedUpdate () {
        if (movementEnabled) {
            movement = Vector3.zero;
			
			if(gameObject.tag == "Scout"){
				if((moveTester != transform.position)&& (isMoveTesterAssigned)){
					animation.CrossFade ("walk");
				}
				else{
					animation.Play("idle", PlayMode.StopAll);
				}
			}
			
			//LATERAL - CONTROLLER
            state = GamePad.GetState(playerNumber);
            movement.x = state.ThumbSticks.Left.X;
            movement.z = state.ThumbSticks.Left.Y;
            transform.position += movement * Time.deltaTime * playerSpeed;
			moveTester = transform.position;
			isMoveTesterAssigned = true;
			
			//LATERAL - KEYBOARD
			if(movement.x == 0 && movement.z == 0){
				if(Input.GetKeyDown(Up))
					movement.z += 5;
				else if(Input.GetKeyDown(Down))
					movement.z -= 5;
				
				if(Input.GetKeyDown(Left))
					movement.x -= 5;
				else if(Input.GetKeyDown(Right))
					movement.x += 5;
				
				transform.position += movement*Time.deltaTime*playerSpeed;
			}
			
			//LATERAL - END

            targetDir = Mathf.Atan(state.ThumbSticks.Right.Y / state.ThumbSticks.Right.X);
            targetDir *= Mathf.Rad2Deg;

            //Determine quadrant
            if (state.ThumbSticks.Right.Y >= 0 && state.ThumbSticks.Right.X >= 0)
                quadrant = 1;
            else if (state.ThumbSticks.Right.Y >= 0 && state.ThumbSticks.Right.X < 0)
                quadrant = 2;
            else if (state.ThumbSticks.Right.Y < 0 && state.ThumbSticks.Right.X < 0)
                quadrant = 3;
            else if (state.ThumbSticks.Right.Y < 0 && state.ThumbSticks.Right.X >= 0)
                quadrant = 4;

            //Convert to 0-360 degrees
            switch (quadrant) {
                case 2:
                    targetDir += 180;
                    break;
                case 3:
                    targetDir += 180;
                    break;
                case 4:
                    targetDir += 360;
                    break;
                default:
                    break;
            }

            currentDir = transform.eulerAngles;
			currentDir.x = 0;
            currentDir.y = -targetDir+90;
			currentDir.z = 0;

            if (!float.IsNaN(currentDir.y))
                transform.eulerAngles = currentDir;
        } else if (!movementEnabled && !hasBeenTagged) {
			Debug.Log ("Runner has been tagged!");
			
			gameplayController.IncRunnersTagged();
			hasBeenTagged = true;
		}
	}
}
