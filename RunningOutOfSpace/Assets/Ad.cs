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

    Vector3 tempPos;

    void Start()
    {
        //if (adType == AdType.Closable)
    }
    void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 tempPos = Input.mousePosition - transform.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = Input.mousePosition - tempPos;
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
