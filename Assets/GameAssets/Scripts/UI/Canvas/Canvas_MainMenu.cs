using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Canvas_MainMenu : UICanvas
{
    [SerializeField] private Button userProfile;
    [SerializeField] private Button purchaseGold;
    [SerializeField] private Button openSettings;
    [SerializeField] private Button startGame;
    [SerializeField] private TextMeshProUGUI userName;
    [SerializeField] private TextMeshProUGUI numberOfGolds;
    [SerializeField] private TextMeshProUGUI Level;
    [SerializeField] private TextMeshProUGUI MapName;

    public override void FetchData()
    {
        purchaseGold.onClick.AddListener(() => UIManager.Instance.OpenUI(UIID.UI_Store));
        openSettings.onClick.AddListener(() => UIManager.Instance.OpenUI(UIID.UI_Settings));
        startGame.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Gameplay));
    }

    public void ShowCurrentMap()
    {

    }
}
