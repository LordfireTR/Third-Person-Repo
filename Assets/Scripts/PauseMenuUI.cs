using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] PlayerController PlayerController;
    public void ResumeGame()
    {
        Time.timeScale = 1;
        PlayerController.isPaused = false;
        PlayerController.pauseScreen.gameObject.SetActive(false);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif

    }
}
