using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Player_Controllers : MonoBehaviour {

	//Character Controller
	private Rigidbody controller;
	
	//Movement variables
	public float playerSpeed = 3.5f;
	protected Vector3 movement = Vector3.zero;

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
    protected Vector3 currentDir = Vector3.zero;
	
	public KeyCode Up = KeyCode.UpArrow;
	public KeyCode Down = KeyCode.DownArrow;
	public KeyCode Left = KeyCode.LeftArrow;
	public KeyCode Right = KeyCode.RightArrow;
	public float keyboardSpeedAdjust = 1.5f;
	public int keyboardRotationSpeed = 5;
	//private bool keyboardRotationActive = false;
	private float movementDirection;
	
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
			
			///////////////////////////////LATERAL - CONTROLLER
            state = GamePad.GetState(playerNumber);
            movement.x = state.ThumbSticks.Left.X;
            movement.z = state.ThumbSticks.Left.Y;
            transform.position += movement * Time.deltaTime * playerSpeed;
			moveTester = transform.position;
			isMoveTesterAssigned = true;
			
			///////////////////////////////LATERAL - KEYBOARD
			if(movement.x == 0 && movement.z == 0){
				if(Input.GetKey(Up))
					movement.z += keyboardSpeedAdjust;
				else if(Input.GetKey(Down))
					movement.z -= keyboardSpeedAdjust;
				
				if(Input.GetKey(Left))
					movement.x -= keyboardSpeedAdjust;
				else if(Input.GetKey(Right))
					movement.x += keyboardSpeedAdjust;
				
				transform.position += movement*Time.deltaTime*playerSpeed;
			}
			
			///////////////////////////////LATERAL - END
			
			///////////////////////////////ROTATIONS - CONTROLLER
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
			//else if(state.ThumbSticks.Right.Y == 0 && state.ThumbSticks.Right.X == 0)
			//	keyboardRotationActive = true;
			

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
            currentDir.y = targetDir;
			currentDir.z = 0;
			

			
			///////////////////////////////ROTATIONS - KEYBOARD
			/*
			if(keyboardRotationActive){
				//Determine quadrant
	            if (movement.y >= 0 && movement.x >= 0)
	                quadrant = 1;
	            else if (movement.y >= 0 && movement.x < 0)
	                quadrant = 2;
	            else if (movement.y < 0 && movement.x < 0)
	                quadrant = 3;
	            else if (movement.y < 0 && movement.x >= 0)
	                quadrant = 4;
				}
			
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
		
			movementDirection = Vector3.Angle(Vector3.zero, movement);
		
			currentDir = transform.eulerAngles;
			currentDir.x = 0;
			
			if(Mathf.DeltaAngle(currentDir.y, movementDirection) != 0)
				Mathf.MoveTowardsAngle(currentDir.y, movementDirection, keyboardRotationSpeed*Time.deltaTime);
		
			currentDir.z = 0;
			*/
		
			currentDir.x = 0;
		
			if(Input.GetKey(Right) && Input.GetKey(Up))
				currentDir.y = 45;
			else if(Input.GetKey(Up) && Input.GetKey(Left))
				currentDir.y = 135;
			else if(Input.GetKey(Left) && Input.GetKey(Down))
				currentDir.y = 225;
			else if(Input.GetKey(Down) && Input.GetKey(Right))
				currentDir.y = 315;
			else if(Input.GetKey(Right))
				currentDir.y = 0;
			else if(Input.GetKey(Up))
				currentDir.y = 90;
			else if(Input.GetKey(Left))
				currentDir.y = 180;
			else if(Input.GetKey(Down))
				currentDir.y = 270;
		
			currentDir.y = -currentDir.y+90;
			currentDir.z = 0;
		
			if (!float.IsNaN(currentDir.y))
                transform.eulerAngles = currentDir;
	        } else if (!movementEnabled && !hasBeenTagged) {
				Debug.Log ("Runner has been tagged!");
				
				gameplayController.IncRunnersTagged();
				hasBeenTagged = true;
			}
			///////////////////////////////ROTATIONS - END
			

            
	}
}
