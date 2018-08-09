using UnityEngine;
using UnityEngine.Events;

public class InteractionManager : MonoBehaviour {
    //Written by Cedric.

    [SerializeField] Sprite leverFlipped;
    [SerializeField] Sprite leverUnFlipped;

    public UnityEvent OnInteract;

    private SpriteRenderer sr;
    private bool isFlipped = false;

    void Start()
    {
        sr.GetComponent<SpriteRenderer>();
    }

    public void LoadRoom(int room)
    {
        GameObject.Find("Player").SendMessage("CanMove");
    }
    
    public void FlipLever(GameObject lever)
    {
        isFlipped = !isFlipped;
        if (isFlipped)
            sr.sprite = leverFlipped;
        else
            sr.sprite = leverUnFlipped;

        GameObject.Find("Player").SendMessage("CanMove");
    }
}


