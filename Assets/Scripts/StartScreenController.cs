using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using AUP;

public class StartScreenController : MonoBehaviour
{

    private UtilsPlugin utilsPlugin;
    private SpeechPlugin speechPlugin;
    private TextToSpeechPlugin textToSpeechPlugin;
    private Dispatcher dispatcher;
    private float waitingInterval = 2f;

    private string introLine = "Bem vindo ao jogo Fuga do Campo de Asteroides. Para Ouvir o tutorial clique na tela uma vez. Para ir direto ao jogo dê um clique duplo na tela";

    int numClicks = 0;

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

    private void OnInit(int status)
    {
        dispatcher.InvokeAction(
            () =>
            {
                if (status == 1)
                {
                    Debug.Log("init speech service successful!");

                    //get available locale on android device
                    //textToSpeechPlugin.GetAvailableLocale();

                    //set default locale
                    textToSpeechPlugin.SetLocaleByCountry(textToSpeechPlugin.GetCountryISO2Alpha(TTSLocaleCountry.PORTUGAL));
                    textToSpeechPlugin.SetPitch(1f);
                    textToSpeechPlugin.SetSpeechRate(1f);
                    CancelInvoke("WaitingMode");
                    Invoke("WaitingMode", waitingInterval);
                }
                else
                {
                    Debug.Log("init speech service failed!");

                    CancelInvoke("WaitingMode");
                    Invoke("WaitingMode", waitingInterval);
                }
            }
        );
    }

    private void OnEndSpeech(string utteranceId)
    {
        dispatcher.InvokeAction(
            () =>
            {
                SceneManager.LoadScene("GameStage", LoadSceneMode.Single);
                CancelInvoke("WaitingMode");
                Invoke("WaitingMode", waitingInterval);
            }
        );
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(repeatTTS(0.3f));
    }

    // Update is called once per frame
    void Update()
    {
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
        textToSpeechPlugin.SpeakOut(this.introLine, "introspeech");
        StartCoroutine(repeatTTS(15.0f));
    }

    private IEnumerator clickAction()
    {
        float waitTime = 0.35f;
        yield return new WaitForSeconds(waitTime);
        if (this.numClicks == 1)
        {
            SceneManager.LoadScene("TutorialScreen", LoadSceneMode.Single);
            textToSpeechPlugin.Stop();
        }
        else if (this.numClicks >= 2)
        {
            //SceneManager.LoadScene("TutorialScreen", LoadSceneMode.Single);
            SceneManager.LoadScene("GameStage", LoadSceneMode.Single);
            textToSpeechPlugin.Stop();
        }
        Debug.Log("Number o clicks: " + this.numClicks);
        this.numClicks = 0;
    }
}

