﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

    private static List<Vector3> initPositions = new List<Vector3>(new Vector3[] {
            new Vector3(-2.5f, 8.0f, 0.0f),
            new Vector3(2.5f, 8.0f, 0.0f),
            new Vector3(0.0f, 8.0f, 0.0f),
        });
    private int consecutiveLeft;
    private int consecutiveRight;
    private int consecutiveMiddle;

    private ObstaclePool pool;

    public static Queue<AudioClip> obstacleAudio = new Queue<AudioClip>();
    public AudioClip[] audioSourcesList;

    static void reshuffle(AudioClip[] clips)
    {
        // Knuth shuffle algorithm
        for (int t = 0; t < clips.Length; t++)
        {
            AudioClip tmp = clips[t];
            int r = Random.Range(t, clips.Length);
            clips[t] = clips[r];
            clips[r] = tmp;
        }
    }

    // Use this for initialization
    void Start () {
        //Set the list of audioClips in a stack used by the Obstacles
        reshuffle(audioSourcesList);
        ObstacleSpawner.obstacleAudio = new Queue<AudioClip>(this.audioSourcesList);

        //Get pool
        this.pool = GameObject.FindObjectOfType<ObstaclePool>().GetComponent<ObstaclePool>();

        //Start the coountdown for the spawns.
        StartCoroutine(waitAndSpawn(0.5f));
	}

    private IEnumerator waitAndSpawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Spawn object here
        bool validSpawn = false;
        int pos = 0;
        while (!validSpawn)
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            pos = Random.Range(0, ObstacleSpawner.initPositions.Count);
            if (pos == 0)
            { //spawning on the left
                if (this.consecutiveLeft < 2)
                {
                    validSpawn = true;
                    this.consecutiveLeft += 1;
                    this.consecutiveRight = 0;
                    this.consecutiveMiddle = 0;
                }
            }
            else if (pos == 1)
            { //spawning on the right
                if (this.consecutiveRight < 2)
                {
                    validSpawn = true;
                    this.consecutiveRight += 1;
                    this.consecutiveLeft = 0;
                    this.consecutiveMiddle = 0;
                }
            }
            else
            { //Spawning on center
                if (this.consecutiveMiddle < 2)
                {
                    validSpawn = true;
                    this.consecutiveMiddle += 1;
                    this.consecutiveLeft = 0;
                    this.consecutiveRight = 0;
                }
            }
        }
        float spawnInterval = GameController.instance.GetComponent<GameController>().GetSpawnInterval();
        float spawnSpeed = GameController.instance.GetComponent<GameController>().GetSpawnSpeed();
        GameObject obstacle = this.pool.takeObstacle();
        obstacle.transform.position = ObstacleSpawner.initPositions[pos];
        obstacle.GetComponent<ObstacleBehaviour>().reset();
        obstacle.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, -spawnSpeed);
        StartCoroutine(waitAndSpawn(spawnInterval));
    }
}
