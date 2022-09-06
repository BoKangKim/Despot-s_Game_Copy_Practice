using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    public void StartButtonEvent()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public void ExitButtonEvent()
    {
        Application.Quit();
    }

}
