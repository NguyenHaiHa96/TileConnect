using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UICanvas : UIElement
{
    public UIID ID;
    public bool IsDestroyOnClose;

    protected RectTransform rectTransform;
    protected Animator animator;
    protected UnityAction closeCallback = null;

    public UnityAction CloseCallback { get => closeCallback; set => closeCallback = value; }

    public override void OnClose()
    {
        if (CloseCallback != null)
        {
            CloseCallback();
        }
        base.OnClose();
        if (IsDestroyOnClose)
        {
            Destroy(gameObject);
        }
    }

    public virtual void Setup()
    {

    }
}
