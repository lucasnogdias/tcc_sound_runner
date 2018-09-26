using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : MonoBehaviour {

    public List<GameObject> obstaclePool;

    public GameObject obstaclePremade;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 5; i++) {
            GameObject obstacle = GameObject.Instantiate(this.obstaclePremade);
            obstacle.SetActive(false);
            this.obstaclePool.Add(obstacle);
        }
    }
	
	public GameObject takeObstacle()
    {
        if (this.obstaclePool.Count > 0)
        {
            GameObject obstacle = this.obstaclePool[this.obstaclePool.Count - 1];
            this.obstaclePool.RemoveAt(this.obstaclePool.Count - 1);
            obstacle.SetActive(true);
            return obstacle;
        }
        else {
            GameObject obstacle = GameObject.Instantiate(this.obstaclePremade);
            return obstacle;
        }
    }

    public void storeObstacle(GameObject obs)
    {
        obs.SetActive(false);
        this.obstaclePool.Add(obs);
    }
}
