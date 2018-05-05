using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

    public static Queue<AudioClip> obstacleAudio = new Queue<AudioClip>();

    private static List<Vector3> initPositions = new List<Vector3>(new Vector3[] {
            new Vector3(-2.5f, 8.0f, 0.0f),
            new Vector3(2.5f, 8.0f, 0.0f),
            new Vector3(0.0f, 8.0f, 0.0f),
        });

    public GameObject obstaclePremade;
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

        //Start the coountdown for the spawns.
        StartCoroutine(waitAndSpawn(0.5f));
	}

    private IEnumerator waitAndSpawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Spawn object here
        Random.InitState(System.DateTime.Now.Millisecond);
        int pos = Random.Range(0, ObstacleSpawner.initPositions.Count);
        GameObject obstacle = GameObject.Instantiate(this.obstaclePremade);
        obstacle.transform.position = ObstacleSpawner.initPositions[pos];
        obstacle.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, -1.5f);
        StartCoroutine(waitAndSpawn(5.5f));
    }
}
