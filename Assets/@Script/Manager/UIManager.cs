using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject UI_mainMenu;
    public GameObject UI_ingameEndless;
    public GameObject UI_ingameTutorial;
    public GameObject UI_loading;
    public GameObject UI_achivement;
    public GameObject moreContainer;
    public GameObject chooseType;
    public GameObject chooseMode;
    public Slider s_loading;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    Time.timeScale = 1;
        //    SpawnerZombie.instance.ReturnAllZombiesToPool();
        //}
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Tombol Aktif");
    }
    public void BackToMenu()
    {
        UI_mainMenu.SetActive(false);
        UI_ingameEndless.SetActive(false);
        UI_ingameTutorial.SetActive(false);
        UI_achivement.SetActive(false);
        moreContainer.SetActive(false);
        chooseType.SetActive(false);
        chooseMode.SetActive(false);
        Time.timeScale = 1;
        GameManager.Instance.envi.SetActive(false);
        StartCoroutine(OpenNext(UI_mainMenu));
    }
    public void PlayEndlessGame()
    {
        UI_ingameEndless.SetActive(false);
        //GameManager.Instance.mode = GAME_MODE.ENDLESS;
        StartCoroutine( OpenNext(UI_ingameEndless));
    }
    
    public void PlayTutorialsGame()
    {
        UI_mainMenu.SetActive(false);
        UI_ingameEndless.SetActive(false);
        UI_ingameTutorial.SetActive(false);
        UI_achivement.SetActive(false);
        moreContainer.SetActive(false);
        chooseType.SetActive(false);
        chooseMode.SetActive(false);
        GameManager.Instance.mode = GAME_MODE.TUTORIAL;
        GameManager.Instance.envi.SetActive(false);
        StartCoroutine(OpenNext(UI_ingameTutorial));
    }

    public void OpenMore()
    {
        moreContainer.SetActive(!moreContainer.activeInHierarchy);
    }


   public IEnumerator OpenNext(GameObject _panelTarget)
    {
        yield return null;
        UI_loading.SetActive(true);

        s_loading.value = 0;
        for (; ; )
        {
            if (s_loading.value > .95) break;
            s_loading.value += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        _panelTarget.SetActive(true);
        UI_loading.SetActive(false);
    }
}
