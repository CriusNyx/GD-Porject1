using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public void playGame()
    {
        SceneManager.LoadScene("PlayerTest");
    }

    public void exitGame()
    {
        Application.Quit(); 
    }




}
