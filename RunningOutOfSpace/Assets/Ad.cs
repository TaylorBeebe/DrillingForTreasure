using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Ad : MonoBehaviour, IDragHandler, IEndDragHandler {

    [System.Serializable]
    public enum AdType
    {
        Closable,
        Test,
        Test2,
        Test3
    }

    public AdType adType;

    Vector3 tempMousePos;
    void Start()
    {
        //if (adType == AdType.Closable)
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            tempMousePos = Input.mousePosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //
    }

    public void CloseAd()
    {
        Destroy(gameObject);
    }
}
