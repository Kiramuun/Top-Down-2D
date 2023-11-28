using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEchap : MonoBehaviour
{
    UIManager _uiManag;
    
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(scene.name);

        if (scene == SceneManager.GetSceneByName("UI Menu"))
        {
            _uiManag._mainMenu.SetActive(false);
            _uiManag._pauseMenu.SetActive(true);
        }
    }
}
