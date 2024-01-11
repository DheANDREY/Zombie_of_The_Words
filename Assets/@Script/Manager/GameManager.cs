using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GAME_MODE mode;
    public GameObject envi;
    public AudioClip bgmClip;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UIManager.Instance.BackToMenu();
    }

}

public enum GAME_SCENE
{
    MAIN_MENU,
    SETTING,
    ACHIEVEMENT,
    INGAME
}

public enum GAME_MODE
{
    TUTORIAL,
    ENDLESS
}

public enum WORD_SELECTION
{
    COMMAND,
    UPPER,
    MID,
    BOT
}