using UnityEngine;
using UnityEngine.Events;

public class InteractionManager : MonoBehaviour {
    //Written by Cedric.

    [SerializeField] Sprite leverFlipped;
    [SerializeField] Sprite leverUnFlipped;

    public UnityEvent OnInteract;

    public SpriteRenderer sr;
    public bool isFlipped = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void LoadRoom(int room)
    {
        GameObject.Find("Player").SendMessage("CanMove");
    }
    
    public void FlipLever(GameObject lever)
    {
        if (isFlipped)
            sr.sprite = leverFlipped;
        else
            sr.sprite = leverUnFlipped;

        isFlipped = !isFlipped;

        GameObject.Find("Player").SendMessage("CanMove");
    }
}


