using UnityEngine;
using UnityEngine.Audio;
using TMPro;
public class SoundManager: MonoBehaviour
{
    public static SoundManager Instance;
    public AudioMixer mixer;
    public AudioMixerGroup bgmMixer;
    public AudioMixerGroup sfxMixer;
    public TextMeshProUGUI sfxText, bgmText;
    private AudioSource bgmSource;
    private AudioSource sfxSource;
    private bool sfxMuted, bgmMuted;
    private void Awake()
    {
        Instance = this;
        bgmSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();
        bgmSource.outputAudioMixerGroup = bgmMixer;
        sfxSource.outputAudioMixerGroup = sfxMixer;
        bgmSource.loop = true;
        sfxSource.loop = false;
        bgmText.text = bgmMuted ? "Off" : "On";
        sfxText.text = sfxMuted ? "Off" : "On";
    }

    public void SetMuteBGM()
    {
        bgmMuted = !bgmMuted;
        mixer.SetFloat("BGMVol", bgmMuted ? -80 : 0);
        bgmText.text = bgmMuted ? "Off" : "On";
    }

    public void SetMuteSFX()
    {

        sfxMuted = !sfxMuted;
        mixer.SetFloat("SFXVol", sfxMuted ? -80 : 0);
        sfxText.text = sfxMuted ? "Off" : "On";
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void PlayBGM(AudioClip clip)
    {
        bgmSource.PlayOneShot(clip);
    }
}