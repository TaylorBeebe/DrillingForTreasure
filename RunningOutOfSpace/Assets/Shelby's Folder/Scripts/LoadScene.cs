using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class LoadScene : MonoBehaviour, IPointerDownHandler
{
    public string SceneToLoad;

    void Start()
    {
        PlayerPrefs.SetInt("FloorPers", 1);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (SceneToLoad == "Final Mock Up")
        {
            PlayerPrefs.SetInt("FloorPers", 1);
            
        }
        SceneManager.LoadScene(SceneToLoad);
    }
}
