using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(){
        GameController.gameRunning = true;
        SceneManager.LoadScene("Gioco");
    }


    public void QuitGame(){
        Application.Quit();
    }
}
