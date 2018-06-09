using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour {

    private Vector3 leftLanePos = new Vector3(-2.0f, -4.0f, 0.0f);
    private Vector3 rightLanePos = new Vector3(2.0f, -4.0f, 0.0f);
    private Vector3 middleLanePos = new Vector3(0.0f, -4.0f, 0.0f);

    private Vector2 startPos = new Vector2();
    private Vector2 direction = new Vector2();
    private bool directionChosen = false;

    // Use this for initialization
    void Start () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //Initial Position
        this.transform.position = middleLanePos;
	}
	
	// Update is called once per frame
	void Update () {
        //Gets keyboard input. Usefull for testing on the computer.
        if (Input.GetKeyUp("left")){
            moveCharacter("left");
        }
        if (Input.GetKeyUp("right"))
        {
            moveCharacter("right");
        }
        if (Input.GetKeyUp("up")||Input.GetKeyUp("down"))
        {
            moveCharacter("center");
        }

        /*
        //Get Phone or Tablet Orientation
        DeviceOrientation orientation = Input.deviceOrientation;
        //Sets Player position based on the orientation value.
        if (orientation != DeviceOrientation.Unknown) //Check if we know device's orientation
        {
            if (orientation == DeviceOrientation.LandscapeLeft)
            {
                moveCharacter("left");
            }
            else if (orientation == DeviceOrientation.LandscapeRight)
            {
                moveCharacter("right");
            }
            else if (orientation == DeviceOrientation.Portrait || orientation == DeviceOrientation.PortraitUpsideDown)
            {
                moveCharacter("center");
            }
        }*/

        
        // Track a single touch as a direction control.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on touch phase.
            switch (touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    startPos = touch.position;
                    directionChosen = false;
                    break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                    direction = touch.position - startPos;
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    directionChosen = true;
                    break;
            }
        }
        if (directionChosen)
        {
            directionChosen = false;
            // Something that uses the chosen direction...
            if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
            {
                //Movement was more vertical than horizontal, move player to central lane.
                moveCharacter("center");
            }
            else if (direction.x>0)
            {
                //Movement to the right, move player to right lane.
                moveCharacter("right");
            }
            else if (direction.x<0)
            {
                //Movement to the left, move player to left lane.
                moveCharacter("left");
            }
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Obstacle")
        {
            Destroy(coll.gameObject);
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
    }

    void moveCharacter(string direction){
        switch (direction){
            case "left":
                transform.position = this.leftLanePos;
                break;
            case "right":
                transform.position = this.rightLanePos;
                break;
            default :
                transform.position = this.middleLanePos;
                break;
        }
    }
    
}
