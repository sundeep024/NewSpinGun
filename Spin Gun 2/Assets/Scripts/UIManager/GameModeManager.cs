using UnityEngine;
using System;

public enum GameMode
{
    MainMenu,
    Play,
    shoot,
    Over
}

public class GameModeManager : MonoBehaviour
{
    public static GameMode selectedMode = GameMode.MainMenu;

    public static Action OnGameMainMenu;
    public static Action OnGamePlay;
    public static Action OnGameOver;
    public static Action OnGameShoot;
    public static Action<int> OnScoreChange;
}