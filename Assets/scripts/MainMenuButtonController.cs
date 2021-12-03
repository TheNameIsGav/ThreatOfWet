using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtonController : MonoBehaviour
{
    public Toggle tutorialToggle;
    public void PlayGame()
    {
        if (tutorialToggle.isOn)
        {
            SceneManager.LoadScene("WrightTutorialLevel");
        }
        else
        {
            SceneManager.LoadScene("TestLevel");
        }
    }

    public void ExitGame()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
    }

    public void toggleChecker()
    {
        if (tutorialToggle.isOn)
        {
            tutorialToggle.isOn = false;
        }
        else
        {
            tutorialToggle.isOn = true;
        }
    }
}
