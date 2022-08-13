using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    // Start is called before the first frame update
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    public void ExitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif

    }
}
