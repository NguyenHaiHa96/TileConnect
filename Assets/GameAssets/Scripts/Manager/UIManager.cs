using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Sirenix.OdinInspector;

public class UIManager : Singleton<UIManager>
{
    [ShowInInspector] private Dictionary<UIID, UICanvas> UICanvasPrefabs = new Dictionary<UIID, UICanvas>();
    [ShowInInspector] public Dictionary<UIID, UICanvas> UICanvas = new Dictionary<UIID, UICanvas>();

    public Transform CanvasParent;
    public Canvas_MainMenu Canvas_MainMenu;
    public Canvas_Gameplay Canvas_Gameplay;

    private string path = "UI/Canvas/";

    public override void OnInit()
    {
        base.OnInit();
        UICanvas[] canvases = Resources.LoadAll<UICanvas>(path);
        for (int i = 0; i < canvases.Length; i++)
        {
            UICanvasPrefabs.Add(canvases[i].ID, canvases[i]);
        }

        Canvas_MainMenu = GetUI(UIID.UI_MainMenu) as Canvas_MainMenu;
        Canvas_Gameplay = GetUI(UIID.UI_Gameplay) as Canvas_Gameplay;
        OpenUI(UIID.UI_Gameplay);
    }

    public bool IsOpenedUI(UIID ID)
    {
        return UICanvas.ContainsKey(ID) && UICanvas[ID] != null && UICanvas[ID].gameObject.activeInHierarchy;
    }

    public UICanvas GetUI(UIID ID)
    {
        UICanvas canvas = null;
        if (!UICanvas.ContainsKey(ID) || UICanvas[ID] == null)
        {
            canvas = Instantiate(UICanvasPrefabs[ID], CanvasParent);
            canvas.gameObject.SetActive(false);
            UICanvas.Add(ID, canvas);
        }
        else
        {
            canvas = UICanvas[ID];
        }

        return UICanvas[ID];
    }
    public T GetUI<T>(UIID ID) where T : UICanvas
    {
        return GetUI(ID) as T;
    }
    public T OpenUI<T>(UIID ID) where T : UICanvas
    {
        return OpenUI(ID) as T;
    }

    public UICanvas OpenUI(UIID ID)
    {
        UICanvas canvas = GetUI(ID);

        canvas.Setup();
        canvas.OnOpen();

        return canvas;
    }

    public void CloseUI(UIID ID)
    {
        if (IsOpened(ID))
        {
            GetUI(ID).OnClose();
        }
    }

    public bool IsOpened(UIID ID)
    {
        return UICanvas.ContainsKey(ID) && UICanvas[ID] != null;
    }

}



