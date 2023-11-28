using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class UIManager : MonoBehaviour
{
    TextMeshProUGUI _tmpUI;

    public GameObject _mainMenu,
                      _pauseMenu;
    PlayerMove _playerScript;
    
    void Start()
    {
        //Time.timeScale = 0f;
    }

    /*void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(scene.name);

        if (scene == SceneManager.GetSceneByName("UI Menu")) 
        {
            Time.timeScale = 0f;
            _mainMenu.SetActive(false);
            _pauseMenu.SetActive(true);
        }
    }*/

    public void NewStart()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        Time.timeScale = 1f;
    }

    public void Continue()
    {
        SceneManager.LoadScene("SampleScene",LoadSceneMode.Single);
        Time.timeScale = 1f;
    }

    public void Options()
    {

    }

    public void Exit()
    {
        //EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
