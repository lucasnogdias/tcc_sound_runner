using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {
    void OnCollisionEnter2D(Collision2D coll)
    {
        coll.gameObject.GetComponent<ObstacleBehaviour>().returnClip();
        Destroy(coll.gameObject);
    }
}
