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
        var x = this.transform.position.x;
        if (x < -0.5f) //Left lane
            obstacleAudioSource.panStereo = -1;
        else if (x > 0.5f) //Right Lane
            obstacleAudioSource.panStereo = 1;
        else //Middle Lane
            obstacleAudioSource.panStereo = 0;
        //set random pitch for audio clip.
        obstacleAudioSource.pitch = Random.Range(0.5f, 0.9f);
        //set initial volume
        obstacleAudioSource.volume = 0.0f;
    }

    public void reset ()
    {
        //Get a AudioClip from obstacle Spawner
        this.obstacleAudioSource.clip = ObstacleSpawner.obstacleAudio.Dequeue();
        this.obstacleAudioSource.Play();

        //Set stereo pan based on position
        var x = this.transform.position.x;
        if (x < -0.5f) //Left lane
            obstacleAudioSource.panStereo = -1;
        else if (x > 0.5f) //Right Lane
            obstacleAudioSource.panStereo = 1;
        else //Middle Lane
            obstacleAudioSource.panStereo = 0;
        //set random pitch for audio clip.
        obstacleAudioSource.pitch = Random.Range(0.5f, 0.9f);
        //set initial volume
        obstacleAudioSource.volume = 0.0f;
    }

    void FixedUpdate()
    {
        //Adjusts volume according to obstacle vertical distance to listener
        var y = this.transform.position.y;
        var verticalDistance = y - (-4.0f);

        if (verticalDistance > 0 /*&& obstacleAudioSource.volume < 1*/)
        {
            obstacleAudioSource.volume = 1.0f - (0.0833333f * verticalDistance);
        }


        if (verticalDistance < -0.1f && obstacleAudioSource.volume > 0)
        {
            obstacleAudioSource.volume = 1.0f + (0.3833333f * verticalDistance);
            if (verticalDistance > -0.2f)
            {
                obstacleAudioSource.pitch -= 0.0022f;
            }
        }
    }
	
	public void returnClip()
    {
        ObstacleSpawner.obstacleAudio.Enqueue(this.obstacleAudioSource.clip);
    }
}
