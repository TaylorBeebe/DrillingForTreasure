using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class LoadScene : MonoBehaviour, IPointerDownHandler
{
    public string SceneToLoad;

    public void OnPointerDown(PointerEventData eventData)
    {
        SceneManager.LoadScene("SceneToLoad");
    }
}
