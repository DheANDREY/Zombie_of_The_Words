using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject MainMenu, TutorMenu, EndlessMenu, SettingMenu, CreditMenu, ExitMenu;
    public GameObject PauseButt;
    public SoundManager SM; public AudioClip bgmSound;

    // Update is called once per frame
    void Start()
    {
        SM.PlayBGM(bgmSound);
    }

    public void OpenTutorial(bool isOpen)
    {
        if (isOpen)
        {
            TutorMenu.SetActive(true);
        }
        else
        {
            TutorMenu.SetActive(false);
        }
    }
    public UIManager uiManager;
    public void OpenEndless(bool isOpen)
    {
        if (isOpen)
        {
            MainMenu.SetActive(false);  
            GameManager.Instance.mode = GAME_MODE.ENDLESS;
            WordDisplay.zomDeadCounter = 0;
            uiManager.PlayEndlessGame(); PauseButt.SetActive(true);
        }
        else
        {
            EndlessMenu.SetActive(false);
        }
    }
    public void OpenSetting(bool isOpen)
    {
        if (isOpen)
        {
            SettingMenu.SetActive(true);
        }
        else
        {
            SettingMenu.SetActive(false);
        }
    }
    public void OpenCredit(bool isOpen)
    {
        if (isOpen)
        {
            CreditMenu.SetActive(true);
        }
        else
        {
            CreditMenu.SetActive(false);
        }
    }
    public void OpenExit(bool isOpen)
    {
        if (isOpen)
        {
            ExitMenu.SetActive(true);
        }
        else
        {
            ExitMenu.SetActive(false);
        }
    }

    public GameObject PanelPause;
    public void OpenPauseMenu(bool isOpen)
    {
        if (isOpen)
        {
            Time.timeScale = 0;
            PanelPause.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            PanelPause.SetActive(false);
        }
    }

    public Transform parentAllZombies;
    public void RestartEndless()
    {
        OpenPauseMenu(false);
        for (int i = 0; i < parentAllZombies.childCount; i++)
        {
            Destroy(parentAllZombies.GetChild(i).gameObject);
        }
        
        OpenEndless(false);     OpenEndless(true);
    }
}
