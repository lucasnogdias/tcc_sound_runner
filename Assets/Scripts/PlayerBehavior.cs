using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

    private Vector3 leftLanePos = new Vector3(-2.0f, -4.0f, 0.0f);
    private Vector3 rightLanePos = new Vector3(2.0f, -4.0f, 0.0f);
    private Vector3 middleLanePos = new Vector3(0.0f, -4.0f, 0.0f);

	// Use this for initialization
	void Start () {
        //Initial Position
        this.transform.position = middleLanePos;
	}
	
	// Update is called once per frame
	void Update () {
        //Gets keyboard input. Usefull for testing.
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
