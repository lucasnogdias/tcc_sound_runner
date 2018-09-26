using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

    private ObstaclePool pool;

    void Start()
    {
        this.pool = GameObject.FindObjectOfType<ObstaclePool>().GetComponent<ObstaclePool>();    
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameController.instance.GetComponent<GameController>().scorePoints();
        coll.gameObject.GetComponent<ObstacleBehaviour>().returnClip();
        this.pool.storeObstacle(coll.gameObject);
    }
}
