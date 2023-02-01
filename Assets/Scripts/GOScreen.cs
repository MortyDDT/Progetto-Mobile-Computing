using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GOScreen : MonoBehaviour
{
    public void ClickRestart() {
        GameController.gameRunning = true;
        SceneManager.LoadScene("Gioco");
        ScoreSystem.theScore = 0;
    }

    public void ClickMenu() {
        SceneManager.LoadScene("Main Menu");
        ScoreSystem.theScore = 0;
    }

}
