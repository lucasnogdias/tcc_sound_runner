using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour {

    public AudioSource obstacleAudioSource;

	// Use this for initialization
	void Start () {
        //Get a AudioClip from obstacle Spawner
        this.obstacleAudioSource.clip = ObstacleSpawner.obstacleAudio.Dequeue();
        this.obstacleAudioSource.Play();
        
        //Set stereo pan based on position
        //TODO: test if this makes any difference on audio
        var x = this.transform.position.x;
        if (x < -0.5f) //Left lane
            obstacleAudioSource.panStereo = -1;
        else if (x > 0.5f) //Right Lane
            obstacleAudioSource.panStereo = 1;
        else //Middle Lane
            obstacleAudioSource.panStereo = 0;
        //set random pitch for audio clip.
        obstacleAudioSource.pitch = Random.Range(0.3f, 0.7f);
	}

    void Update()
    {
        //rolls of volume faster after player is behind
        var y = this.transform.position.y;
        if (y < -5.5f && obstacleAudioSource.volume > 0)
        {
            obstacleAudioSource.volume -= 0.01f;
        }
    }
	
	public void returnClip()
    {
        ObstacleSpawner.obstacleAudio.Enqueue(this.obstacleAudioSource.clip);
    }
}
