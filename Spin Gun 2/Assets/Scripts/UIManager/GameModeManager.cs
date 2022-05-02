using UnityEngine;
using System;

public enum GameMode
{
    MainMenu,
    Play,
    Over
}

public class GameModeManager : MonoBehaviour
{
    public GameMode selectedMode = GameMode.MainMenu;

    public Action OnGameMainMenu;
    public Action OnGamePlay;
    public Action OnGameOver;
    public static Action<int> OnScoreChange;
}
