using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterChange : MonoBehaviour {

    public Sprite[] characterSprites;

    string playerName;
    int characterSpriteID = 0;

    Image characterSprite;

    void Start()
    {
        characterSprite = GameObject.Find("CharacterSprite").GetComponent<Image>();
    }

    void Update()
    {
        playerName = GameObject.Find("NameEnter").GetComponent<InputField>().text;
    }

    public string returnName()
    {
        return playerName;
    }

    public int returnSpriteID()
    {
        return characterSpriteID;
    }

    public void NextCharacter()
    {
        if (characterSpriteID == characterSprites.Length - 1)
            characterSpriteID = 0;
        else
            characterSpriteID++;

        characterSprite.sprite = characterSprites[characterSpriteID];
    }
    public void PreviousCharacter()
    {
        if (characterSpriteID == 0)
            characterSpriteID = characterSprites.Length - 1;
        else
            characterSpriteID--;

        characterSprite.sprite = characterSprites[characterSpriteID];
    }
}
