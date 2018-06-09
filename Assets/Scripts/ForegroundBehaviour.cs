using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundBehaviour : MonoBehaviour {

    public float xInc = 0.0f;
    public float yInc = 0.0f;

	// Use this for initialization
	void Start () {
        xInc = Random.Range(-0.008f, 0.008f);
        yInc = Random.Range(-0.008f, 0.008f);

        if (xInc == 0.0f && yInc == 0.0f)
        {
            xInc += 0.005f;
        }
	}
	
	// Update is called once per frame
	void Update () {
        float cX = this.transform.position.x;
        float cY = this.transform.position.y;
        this.transform.position = new Vector3((cX + xInc), (cY + yInc), -5.0f);

        if (this.transform.position.x >= 11.0f)
        {
            this.xInc = Random.Range(-0.008f, 0.0f);
        } else if (this.transform.position.x <= -11.0f){
            this.xInc = Random.Range(0.0f, 0.008f);
        }

        if (this.transform.position.y >= 4.5f)
        {
            this.yInc = Random.Range(-0.008f, 0.0f);
        }else if (this.transform.position.y <= -4.5f)
        {
            this.yInc = Random.Range(0.0f, 0.008f);
        }
	}
}
