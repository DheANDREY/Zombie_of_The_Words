using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
public class IngameTutorial : MonoBehaviour
{
    public static IngameTutorial Instance;
    public GameObject achievement;
    public float timer;
    public TextMeshProUGUI t_timer, bestWPM, bestEPM, bestAccuracy; 
    public WordDisplay t_text; 
    public FingerClassificators FingerClassificators;
    public Image i_lPinky, i_lRing, i_lMid, i_lPoint, i_lThumb, i_rPinky, i_rRing, i_rMid, i_rPoint, i_rThumb;
    public List<char> pressed= new List<char>();
    [DllImport("user32.dll")]
    public static extern short GetKeyState(int keyCode);
    bool isCapsLockOn = false;

    private float curTimer,curStopwatch;
    private bool playable;
    private void Awake()
    {
        Instance = this;
        curTimer = timer;
        playable = true;
    }

    private void OnEnable()
    {

        WordManager.wordTypedCount = 0;
        WordManager.hitCount = 0;
        curStopwatch = 0;
        curTimer = timer;
        WordManager.Instance.words.Clear();
        WordManager.Instance.AddWordTutor(t_text);
        WordManager.Instance.WordInit();
    }
    private void OnDisable()
    {

        achievement.SetActive(false);
    }
    private void Update()
    {
        if (!playable) return;
        if (curTimer<0)
        {
            playable = false;
            achievement.SetActive(true);
            string[] wordCount = WordManager.Instance.words[0].word.Remove(WordManager.Instance.words[0].typeIndex).Split(" ");
            WordManager.wordTypedCount = wordCount.Length;
            bestWPM.text = WordManager.Instance.GetBestWPM(curStopwatch).ToString("00");
            bestEPM.text = WordManager.Instance.GetBestEPM(curStopwatch).ToString("00");
            bestAccuracy.text = WordManager.Instance.GetAccuracy().ToString("00");
            return;
        }
        foreach (char letter in Input.inputString)
        {
            if (Input.anyKeyDown)
            {
                WordManager.hitCount++;
                isCapsLockOn = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;//init stat
                switch (FingerClassificators.GetFinger(letter.ToString().ToLower()[0]))
                {
                    case FINGER.None:
                        break;
                    case FINGER.L_PINKY:
                        i_lPinky.color = Color.red;
                        pressed.Add(letter);
                        break;
                    case FINGER.L_RING:
                        i_lRing.color = Color.red;
                        pressed.Add(letter);
                        break;
                    case FINGER.L_MID:
                        i_lMid.color = Color.red;
                        pressed.Add(letter);
                        break;
                    case FINGER.L_POINT:
                        i_lPoint.color = Color.red;
                        pressed.Add(letter);
                        break;
                    case FINGER.L_THUMB:
                        i_lThumb.color = Color.red;
                        pressed.Add(letter);
                        break;
                    case FINGER.R_PINKY:
                        i_rPinky.color = Color.red;
                        pressed.Add(letter);
                        break;
                    case FINGER.R_RING:
                        i_rRing.color = Color.red;
                        pressed.Add(letter);
                        break;
                    case FINGER.R_MID:
                        i_rMid.color = Color.red;
                        pressed.Add(letter);
                        break;
                    case FINGER.R_POINT:
                        i_rPoint.color = Color.red;
                        pressed.Add(letter);
                        break;
                    case FINGER.R_THUMB:
                        i_rThumb.color = Color.red;
                        pressed.Add(letter);
                        break;
                    default:
                        i_lPinky.color = Color.white;
                        i_lRing.color = Color.white;
                        i_lMid.color = Color.white;
                        i_lPoint.color = Color.white;
                        i_lThumb.color = Color.white;
                        i_rPinky.color = Color.white;
                        i_rRing.color = Color.white;
                        i_rMid.color = Color.white;
                        i_rPoint.color = Color.white;
                        i_rThumb.color = Color.white;
                        break;
                }
                WordManager.Instance.TypeLetter(letter);
            }
        }
        for (int i = 0; i < pressed.Count; i++)
        {
            string _key = string.IsNullOrWhiteSpace(pressed[i].ToString()) ? "space" : pressed[i].ToString().ToLower();
            if (Input.GetKeyUp(_key))
            {
                switch (FingerClassificators.GetFinger(pressed[i].ToString().ToLower()[0]))
                {
                    case FINGER.None:
                        break;
                    case FINGER.L_PINKY:
                        i_lPinky.color = Color.white;
                        break;
                    case FINGER.L_RING:
                        i_lRing.color = Color.white;
                        break;
                    case FINGER.L_MID:
                        i_lMid.color = Color.white;
                        break;
                    case FINGER.L_POINT:
                        i_lPoint.color = Color.white;
                        break;
                    case FINGER.L_THUMB:
                        i_lThumb.color = Color.white;
                        break;
                    case FINGER.R_PINKY:
                        i_rPinky.color = Color.white;
                        break;
                    case FINGER.R_RING:
                        i_rRing.color = Color.white;
                        break;
                    case FINGER.R_MID:
                        i_rMid.color = Color.white;
                        break;
                    case FINGER.R_POINT:
                        i_rPoint.color = Color.white;
                        break;
                    case FINGER.R_THUMB:
                        i_rThumb.color = Color.white;
                        break;
                    default:
                        i_lPinky.color = Color.white;
                        i_lRing.color = Color.white;
                        i_lMid.color = Color.white;
                        i_lPoint.color = Color.white;
                        i_lThumb.color = Color.white;
                        i_rPinky.color = Color.white;
                        i_rRing.color = Color.white;
                        i_rMid.color = Color.white;
                        i_rPoint.color = Color.white;
                        i_rThumb.color = Color.white;
                        break;
                }
                pressed.Remove(pressed[i]);
            }
        }
        curTimer -= Time.deltaTime;
        curStopwatch += Time.deltaTime;
        float minutes = Mathf.FloorToInt(curTimer / 60);
        float seconds = Mathf.FloorToInt(curTimer % 60);

        t_timer.text = string.Format("{0:00}:{1:00}", minutes, seconds) ;
    }
}
[System.Serializable]
public class FingerClassificators
{
    public FingerKey[] fingerClassificators;

    public FINGER GetFinger(char letter)
    {
        for (int i = 0; i < fingerClassificators.Length; i++)
        {
            if (fingerClassificators[i].characters.Contains(letter)) return fingerClassificators[i].finger;
        }
        return FINGER.None;
    }
}
