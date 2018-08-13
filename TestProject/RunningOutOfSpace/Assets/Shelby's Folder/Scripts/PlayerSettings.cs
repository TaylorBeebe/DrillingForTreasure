using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSettings : MonoBehaviour {

    public string playerName;
    public int characterSpriteID = 0;

    [SerializeField] int gameSceneBuildIndex;


    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void ready()
    {
        //SceneManager.LoadScene("Level");

        var canv = GameObject.Find("Canvas").GetComponent<CharacterChange>();
        playerName = canv.returnName();
        characterSpriteID = canv.returnSpriteID();
        SceneManager.LoadScene(gameSceneBuildIndex);
    }
}
