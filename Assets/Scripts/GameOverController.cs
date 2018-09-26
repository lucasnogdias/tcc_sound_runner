using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOverController : MonoBehaviour {

    private UtilsPlugin utilsPlugin;
    private SpeechPlugin speechPlugin;
    private TextToSpeechPlugin textToSpeechPlugin;
    private string gameoverLine;

    int numClicks = 0;

    public Text scoreText;

    private void Awake()
    {
        // for accessing audio
        utilsPlugin = UtilsPlugin.GetInstance();
        utilsPlugin.SetDebug(0);

        speechPlugin = SpeechPlugin.GetInstance();
        speechPlugin.SetDebug(0);

        textToSpeechPlugin = TextToSpeechPlugin.GetInstance();
        textToSpeechPlugin.SetDebug(0);
        textToSpeechPlugin.Initialize();

        //textToSpeechPlugin.OnInit += this.OnInit;
        //textToSpeechPlugin.OnEndSpeech += this.OnEndSpeech;
    }

    void Start()
    {
        this.scoreText.text = GameController.instance.GetComponent<GameController>().playerScore.ToString() + " Pontos";
        this.gameoverLine = "Fim de Jogo. Você marcou " + this.scoreText.text + ". Clique na tela uma vez para retornar ao menu inicial, ou utilize um clique duplo para iniciar uma nova partida.";
        textToSpeechPlugin.SpeakOut(this.gameoverLine, "gameoverspeech");
        StartCoroutine(repeatTTS(18.0f));
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

    private IEnumerator repeatTTS(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        textToSpeechPlugin.SpeakOut(this.gameoverLine, "gameoverspeech");
        StartCoroutine(repeatTTS(18.0f));
    }

    private IEnumerator clickAction()
    {
        float waitTime = 0.35f;
        yield return new WaitForSeconds(waitTime);
        if (this.numClicks == 1)
        {
            SceneManager.LoadScene("StartScreen", LoadSceneMode.Single);
            textToSpeechPlugin.Stop();
        }
        else if (this.numClicks >= 2 )
        {
            SceneManager.LoadScene("GameStage", LoadSceneMode.Single);
            textToSpeechPlugin.Stop();
        }
        Debug.Log("Number o clicks: " + this.numClicks);
        this.numClicks = 0;
    }
}
