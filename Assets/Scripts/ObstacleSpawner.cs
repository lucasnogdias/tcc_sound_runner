using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

    public GameObject obstaclePremade;

    private List<Vector3> initPositions = new List<Vector3>(new Vector3[] {
            new Vector3(-2.0f, 8.0f, 0.0f),
            new Vector3(2.0f, 8.0f, 0.0f),
            new Vector3(0.0f, 8.0f, 0.0f),
        });

    // Use this for initialization
    void Start () {
        //Start the coountdown for the spawns.
        StartCoroutine(waitAndSpawn(0.5f));
	}
	
	// Update is called once per frame
	void Update () {
	}

    private IEnumerator waitAndSpawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Spawn object here
        Random.InitState(System.DateTime.Now.Millisecond);
        int pos = Random.Range(0, this.initPositions.Count);
        GameObject obstacle = GameObject.Instantiate(this.obstaclePremade);
        obstacle.transform.position = this.initPositions[pos];
        obstacle.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, -1.5f);
        StartCoroutine(waitAndSpawn(5.5f));
    }
}
