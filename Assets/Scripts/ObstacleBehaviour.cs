using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour {

    public AudioSource obstacleAudio;

	// Use this for initialization
	void Start () {
        obstacleAudio.pitch = Random.Range(0.3f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
