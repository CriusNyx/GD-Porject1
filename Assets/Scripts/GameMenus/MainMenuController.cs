using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    public void playGame()
    {
        var diff = GameObject.Find("DifficultyDropdown");
        if (diff != null)
        {
            var dropdown = diff.GetComponent<Dropdown>();

            switch (dropdown.value)
            {
                case 0:
                    Difficulty.SetBaseDifficultyLevel(Difficulty.DifficultyLevel.VeryEasy);
                    break;
                case 1:
                    Difficulty.SetBaseDifficultyLevel(Difficulty.DifficultyLevel.Easy);
                    break;
                case 2:
                    Difficulty.SetBaseDifficultyLevel(Difficulty.DifficultyLevel.Normal);
                    break;
                case 3:
                    Difficulty.SetBaseDifficultyLevel(Difficulty.DifficultyLevel.Hard);
                    break;
                case 4:
                    Difficulty.SetBaseDifficultyLevel(Difficulty.DifficultyLevel.VeryHard);
                    break;
            }
        }

        SceneManager.LoadScene("EnemyBehaviourDebug");
    }

    public void exitGame()
    {
        Application.Quit(); 
    }


    public void goToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
