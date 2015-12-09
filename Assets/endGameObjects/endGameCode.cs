using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class endGameCode : MonoBehaviour {
    public Text totalSameColorsAwardedUI;
    public Text finalScoreUI;
    public Text totalPunishmentCubesUI;
    public Text totalRainbowCrossesAwardedUI;
    public Text totalBonusesAwardedUI;
    public Text loseWinUI;
    public Text playAgainUI;
    public Text gameOverUI;
    //dataToSave savedData;

    public int totalSameColor;
    public int totalPunish;
    public int totalScore;
    public int totalRainbows;
    public int totalBonus;

	// Use this for initialization
	void Start ()
    {

        //import all of the data from the game scene
        /*totalScore = savedData.score;
        totalPunish = savedData.numPunishCubes;
        totalSameColor = savedData.sameColorsAwarded;
        totalRainbows = savedData.rainbowsAwarded;
        totalBonus = savedData.bonusesEarned; */

        totalPunish = gameCode.totalBlackPunishmentCubes;
        totalSameColor = gameCode.totalSameColorsAwarded;
        totalScore = gameCode.playerPoints;
        totalRainbows = gameCode.totalRainbowsAwarded;
        totalBonus = gameCode.totalBonusesEarned;
    }
	
	// Update is called once per frame
	void Update () {
        gameOverUI.text = "Game Over! Let's see how you did.";
        totalSameColorsAwardedUI.text = "You made " + totalSameColor + " same-color crosses!";
        totalPunishmentCubesUI.text = "You got " + totalPunish + " black cube penalties.";
        totalBonusesAwardedUI.text = "You earned " + totalBonus + " streak bonuses!";
        totalRainbowCrossesAwardedUI.text = "You made " + totalRainbows + " rainbow crosses!";
        finalScoreUI.text = "Your final score was " + totalScore;
        playAgainUI.text = "Menu";
        if (totalScore > 0)
        {
            loseWinUI.text = "You win! Nice job!";
        }
        else
        {
            loseWinUI.text = "You lose. Better luck next time!";
        }
    }
}
