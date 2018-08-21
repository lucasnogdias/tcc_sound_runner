using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

    int numClicks = 0;

    public Text scoreText;

    void Start()
    {
        this.scoreText.text = GameController.instance.GetComponent<GameController>().playerScore.ToString() + " Pontos";
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            if (this.numClicks <= 0)
            {
                StartCoroutine(clickAction());
            }
            this.numClicks++;
        }
    }

    private IEnumerator clickAction()
    {
        float waitTime = 0.35f;
        yield return new WaitForSeconds(waitTime);
        if (this.numClicks == 1)
        {
            SceneManager.LoadScene("StartScreen", LoadSceneMode.Single);
        }
        else if (this.numClicks >= 2 )
        {
            SceneManager.LoadScene("GameStage", LoadSceneMode.Single);
        }
        Debug.Log("Number o clicks: " + this.numClicks);
        this.numClicks = 0;
    }
}
