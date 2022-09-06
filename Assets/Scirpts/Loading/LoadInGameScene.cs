using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadInGameScene : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadSceneAsync("InGameScene",LoadSceneMode.Single);
    }

}
