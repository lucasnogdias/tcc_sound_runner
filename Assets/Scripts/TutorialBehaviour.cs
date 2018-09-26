using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBehaviour : MonoBehaviour {

    private UtilsPlugin utilsPlugin;
    private SpeechPlugin speechPlugin;
    private TextToSpeechPlugin textToSpeechPlugin;
    //private Dispatcher dispatcher;
    private float waitingInterval = 2f;

    private string tutorialLine = "Capitão, conseguimos despitar a frota inimiga desviando por este campo de asteróides. No entanto nosso sistema visual de navegação foi danificado e teremos de utilizar os dados do sonar para pilotar a nave! Estarei transmitindo os planos inimigos que conseguimos obter para nossa central, por isso é importante que você nos mantenha à salvo pelo máximo de tempo que conseguir.";

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

    // Use this for initialization
    void Start()
    {
        this.tutorialLine += " Para facilitar nossa navegação dividi o espaço manobrável à nossa frente em três regiões: Esquerda, Direita e Centro. A aproximação dos asteroides será representada por sons, um asteroide se aproximando pela região esquerda será representado por um som à sua esquerda, um asteroide que se aproxima pela região da direita será representado por um som à sua direita e um asteroide vindo diretamente de frente será representado por um som nos dois canais simultâneamente.";
        this.tutorialLine += " Para mover a nave para a esquerda ou para a direita basta tocar na sua tela na metade esquerda ou direita, respectivamente. Se a tela não registrar nenhum toque a nave irá automaticamente retornar para a posição central. Para ajuda-lo a manter a referência de nosso posicionamento os sons são sempre reproduzidos independentemente da posição atual da nave, portanto um som à direita sempre representará um asteroide se aproximando pela região da direita e respectivamente para as demais regiões.";
        this.tutorialLine += " Nossos escudos são capazes de absorver quatro impactos de asteróides, mas qualquer impacto depois disso destruirá a nave. Os escudos são capazes de se recuperar com o tempo, então procure manter a calma depois de atingido para evitar impactos consecutivos que podem acarretar na destruição da nave";
        this.tutorialLine += " Por quanto mais tempo você nos mantiver voando, mais informações serei capaz de enviar aos nossos aliados, por outro lado o campo de asteróides se torna mais denso e mais perigoso a medida que viajamos dentro dele. Boa sorte capitão, pilote como nunca, a rebelião depende de você!";
        textToSpeechPlugin.SpeakOut(this.tutorialLine, "tutorialspeech");
        StartCoroutine(endTutorial(150.0f));
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

    private IEnumerator endTutorial(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("GameStage", LoadSceneMode.Single);
    }

    private IEnumerator clickAction()
    {
        float waitTime = 0.35f;
        yield return new WaitForSeconds(waitTime);
        if (this.numClicks == 1)
        {
            //SceneManager.LoadScene("GameStage", LoadSceneMode.Single);
        }
        else if (this.numClicks >= 2)
        {
            //SceneManager.LoadScene("TutorialScreen", LoadSceneMode.Single);
            SceneManager.LoadScene("GameStage", LoadSceneMode.Single);
            textToSpeechPlugin.Stop();
        }
        this.numClicks = 0;
    }

}
