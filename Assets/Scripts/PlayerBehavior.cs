using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
