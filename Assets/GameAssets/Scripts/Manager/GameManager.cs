using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GameManager : Singleton<GameManager>
{
    [Button(Name = "Clear Player Data")]
    public void ClearPlayerData()
    {
        PlayerPrefs.DeleteAll();
    }
    public GameState GameState;
    public bool IsPauseGame;

    public override void OnInit()
    {
        base.OnInit();
        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }
        ChangeState(GameState.Gameplay);
    }

    public void ChangeState(GameState state)
    {
        GameState = state;
        switch (GameState)
        {
            case GameState.Shop:
                break;
            case GameState.MainMenu:
                HandleStartGame();
                break;
            case GameState.Pause:
                break;
            case GameState.Gameplay:
                HandleGameplay();
                break;
            case GameState.Quit:
                HandleQuitGame();
                break;
            default:
                break;
        }
    }

    private void HandleStartGame()
    {

    }

    private void HandleGameplay()
    {
        UIManager.Instance.OpenUI(UIID.UI_Gameplay);
        LevelManager.Instance.LevelGenerator.GenerateLevel(TileTheme.Fish);
    }

    private void HandleQuitGame()
    {

    }
}
