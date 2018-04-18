using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour {

    private Vector3 leftLanePos = new Vector3(-2.0f, -4.0f, 0.0f);
    private Vector3 rightLanePos = new Vector3(2.0f, -4.0f, 0.0f);
    private Vector3 middleLanePos = new Vector3(0.0f, -4.0f, 0.0f);
    private Gyroscope gyro;

	// Use this for initialization
	void Start () {
        //Initial Position
        this.transform.position = middleLanePos;

        //Set gyroscope
        gyro = Input.gyro;
        if (!gyro.enabled)
        {
            gyro.enabled = true;
        }
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

        //Gets Gyroscope value
        //TODO: Test on the phone if  the X is really the axis i have to get for the effect i want to get.
        float gyroX = this.gyro.attitude.x;
        print(gyroX);
        //Sets Player position based on the gyroscope value. Comment to enable Keyboard Control for testing.
        //TODO: check this value, it must be empyrical and be confortable for the player
        /*if (gyroX < -30.0f)
        {
            moveCharacter("left");
        }
        else if (gyroX > 30.0f)
        {
            moveCharacter("right");
        }
        else
        {
            moveCharacter("center");
        }*/
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
