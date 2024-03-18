using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using TMPro;

public enum SoundEnum
{
    click,shot,reload,hitted,miss,zomDead
}
public class SoundManager: MonoBehaviour
{
    public static Dictionary<SoundEnum, AudioClip> soundLibrary = new Dictionary<SoundEnum, AudioClip>();

    public static SoundManager instance;
    public AudioMixer mixer;
    //public AudioMixerGroup bgmMixer;
    //public AudioMixerGroup sfxMixer;
    public TextMeshProUGUI sfxText, bgmText;
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioClip[] bgm;
    public AudioClip[] sfx;
    private bool sfxMuted, bgmMuted;
    private void Awake()
    {              
            instance = this;
            DontDestroyOnLoad(gameObject);
            //bgmSource.outputAudioMixerGroup = bgmMixer;
            //sfxSource.outputAudioMixerGroup = sfxMixer;
            //bgmText.text = bgmMuted ? "Off" : "On";
            //sfxText.text = sfxMuted ? "Off" : "On";
        
    }

    private void Start()
    {
        soundLibrary[SoundEnum.shot] = sfx[0];
        soundLibrary[SoundEnum.reload] = sfx[1];
        soundLibrary[SoundEnum.hitted] = sfx[2];
        soundLibrary[SoundEnum.miss] = sfx[3];
        soundLibrary[SoundEnum.zomDead] = sfx[4];
        soundLibrary[SoundEnum.click] = sfx[5];
    }
    public WordDisplay wd;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            //PlaySound(SoundEnum.zomDead);
            //Debug.Log(wd.isZomDead);
        }
    }

    public void SetMuteBGM()
    {
        bgmMuted = !bgmMuted;
        mixer.SetFloat("BGMVol", bgmMuted ? -80 : 0);
        //bgmText.text = bgmMuted ? "Off" : "On";
    }

    public void SetMuteSFX()
    {

        sfxMuted = !sfxMuted;
        mixer.SetFloat("SFXVol", sfxMuted ? -80 : 0);
        //sfxText.text = sfxMuted ? "Off" : "On";
    }

    public static bool isSFXOn = true;
    public void PlaySFX()
    {
        isSFXOn = true;
        sfxSource.Play();
    }
    public void StopSFX()
    {
        sfxSource.Stop();
        isSFXOn = false;
    }
    public void PlayBGM(AudioClip clip)
    {
        StopBGM();

        bgmSource.clip = clip;
        bgmSource.Play();
    }
    public void StopBGM()
    {
        bgmSource.Stop();
    }
    // Metode untuk memainkan sound effect berdasarkan nama
    public void PlaySFX2(string name)
    {
        AudioClip clip = FindSoundEffectByName(name);
        if (clip != null && isSFXOn)
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Sound effect '" + name + "' is not found!");
        }
    }

    // Metode untuk mencari sound effect berdasarkan nama
    private AudioClip FindSoundEffectByName(string name)
    {
        foreach (var clip in sfx)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }
        return null;
    }

    public void PlaySound(SoundEnum sound)
    {
        if (soundLibrary.ContainsKey(sound) && isSFXOn)
        {
            AudioClip clip = soundLibrary[sound];
            sfxSource.PlayOneShot(clip);
            //AudioSource.PlayClipAtPoint(clip, transform.position, sfxVolume);
            //Debug.Log("SoundEnum="+sound.ToString());
        }
        else
        {
            Debug.LogError("Sound not found in the library.");
        }
    }

    public void PlayClick(AudioClip sound)
    {
        sfxSource.PlayOneShot(sound);
    }
}