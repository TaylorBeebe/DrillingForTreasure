using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour {

    string sceneToLoad;

    public void LoadScene(string scene)
    {
        sceneToLoad = scene;
        Invoke("StartScene", 0.25f);
        GetComponent<Animator>().SetTrigger("fade");
    }

    void StartScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    public void CloseGame()
    {
        GetComponent<Animator>().SetTrigger("fade");
        Invoke("CloseGameEnd", 0.25f);
    }

    void CloseGameEnd()
    {
        Application.Quit();
    }
}
