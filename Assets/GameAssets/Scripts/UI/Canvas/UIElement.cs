using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement : CacheComponent
{
    public RectTransform RectTransform;
    public Vector3 AnchoredPosition { get => RectTransform.anchoredPosition; set => RectTransform.anchoredPosition = value; }
    
    public virtual void OnCloseCallBack()
    {

    }

    public virtual void OnOpen()
    {
        gameObject.SetActive(true);
    }

    public virtual void OnClose()
    {
        gameObject.SetActive(false);
    }

    public virtual void FetchData()
    {

    }
}
