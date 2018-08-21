using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameObject instance = null;

    public float playerScore = 0;
    public int scoreMultiplier = 1;
    public int currentLevel = 0;
    public int bonusLevel = 0;
    public int pointStreak = 0;
    private int[] pointsToNextLevel = { 6, 15, 27, 42, 60, 75, 95, 120, 150, 187, 218, 257, 305, 364, 446 };
    private float[] basePointValue = {1.0f, 1.2f, 1.4f, 1.6f, 1.8f, 2.1f, 2.4f, 2.7f, 3.0f, 3.3f, 3.8f, 4.3f, 4.8f, 5.3f, 6.8f};
    private float[] spawnSpeed = {4.0f, 4.1f, 4.2f, 4.3f, 4.4f, 4.6f, 4.8f, 5.0f, 5.2f, 5.4f, 5.7f, 5.0f, 6.3f, 6.6f, 6.9f};
    private float[] spawnInterval = { 6.0f, 5.9f, 5.8f, 5.7f, 5.6f, 5.35f, 5.2f, 5.05f, 4.9f, 4.75f, 4.55f, 4.35f, 4.15f, 3.95f, 3.75f};

    // Use this for initialization
    void Start() {
        if (GameController.instance == null)
        {
            GameController.instance = this.gameObject;
            DontDestroyOnLoad(this.gameObject);
        }

        else
        {
            Destroy(this.gameObject);
        }
    }

    public void scorePoints(){
        this.playerScore += (this.basePointValue[currentLevel]+0.7f*bonusLevel) * this.scoreMultiplier;
        this.pointStreak++;
        if (this.playerScore > (this.pointsToNextLevel[currentLevel]+(8.0f*bonusLevel*basePointValue[currentLevel])) )
        {
            if (this.currentLevel < 14)
            {
                this.currentLevel += 1;
                FindObjectOfType<PlayerBehavior>().recoverLives();
            } else
            {
                this.bonusLevel += 1;
            }
        }
        int maxMulti;
        if (currentLevel < 5)
        {
            maxMulti = 3;
        } else if (currentLevel < 10)
        {
            maxMulti = 5;
        } else if (currentLevel <= 14 && bonusLevel == 0)
        {
            maxMulti = 7;
        } else
        {
            maxMulti = 10;
        }
        if (scoreMultiplier<maxMulti)
        {
            this.scoreMultiplier++;
        }
    }

    public float GetSpawnInterval()
    {
        float spI = spawnInterval[currentLevel] - 0.3f * bonusLevel;
        if (spI < 1.0f)
        {
            spI = 1.0f;
        }
        return spI;
    }

    public float GetSpawnSpeed()
    {
        float spS = spawnSpeed[currentLevel] + 0.5f * bonusLevel;
        return spS;
    }

}
