using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour {

    public AudioSource obstacleAudioSource;

	// Use this for initialization
	void Start () {
        this.obstacleAudioSource.clip = ObstacleSpawner.obstacleAudio.Pop();
        obstacleAudioSource.pitch = Random.Range(0.3f, 0.7f);
	}
	
	public void returnClip()
    {
        ObstacleSpawner.obstacleAudio.Push(this.obstacleAudioSource.clip);
    }
}
