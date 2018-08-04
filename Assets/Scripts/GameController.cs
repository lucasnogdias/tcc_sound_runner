using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameObject instance = null;

    public int playerScore = 0;
    public int scoreMultiplier = 0;
    public int currentLevel = 0;
    public int pointsToNextLevel = 100;
    public int pointStreak = 0;
    public int basePointValue = 5;

    // Use this for initialization
    void Start() {
        if (GameController.instance == null)
        {
            GameController.instance = this.gameObject;
        }

        else
        {
            Destroy(this.gameObject);
        }
    }

    void scorePoints(){
        this.playerScore += this.basePointValue * this.scoreMultiplier;
        this.pointStreak++;
        if (this.playerScore > this.pointsToNextLevel)
        {
            this.currentLevel += 1;
        }
        if (this.pointStreak%10 == 0 && scoreMultiplier<10)
        {
            this.scoreMultiplier++;
        }
    }

}
