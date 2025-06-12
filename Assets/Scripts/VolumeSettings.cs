using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    [Header("Audio")]
    public AudioMixer audioMixer;

    [Header("Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider ambienceSlider;

    private const string MasterVolumeKey = "Volume_Master";
    private const string MusicVolumeKey = "Volume_Music";
    private const string SFXVolumeKey = "Volume_SFX";
    private const string AmbienceVolumeKey = "Volume_Ambience";

    void Start()
    {
        float masterVol = PlayerPrefs.GetFloat(MasterVolumeKey, 0.75f);
        float musicVol = PlayerPrefs.GetFloat(MusicVolumeKey, 0.75f);
        float sfxVol = PlayerPrefs.GetFloat(SFXVolumeKey, 0.75f);
        float ambienceVol = PlayerPrefs.GetFloat(AmbienceVolumeKey, 0.75f);

        masterSlider.value = masterVol;
        musicSlider.value = musicVol;
        sfxSlider.value = sfxVol;
        ambienceSlider.value = ambienceVol;

        ApplyVolume("MasterVolume", masterVol);
        ApplyVolume("MusicVolume", musicVol);
        ApplyVolume("SFXVolume", sfxVol);
        ApplyVolume("AmbienceVolume", ambienceVol);

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        ambienceSlider.onValueChanged.AddListener(SetAmbienceVolume);
    }

    public void SetMasterVolume(float value)
    {
        ApplyVolume("MasterVolume", value);
        PlayerPrefs.SetFloat(MasterVolumeKey, value);
    }

    public void SetMusicVolume(float value)
    {
        ApplyVolume("MusicVolume", value);
        PlayerPrefs.SetFloat(MusicVolumeKey, value);
    }

    public void SetSFXVolume(float value)
    {
        ApplyVolume("SFXVolume", value);
        PlayerPrefs.SetFloat(SFXVolumeKey, value);
    }

    public void SetAmbienceVolume(float value)
    {
        ApplyVolume("AmbienceVolume", value);
        PlayerPrefs.SetFloat(AmbienceVolumeKey, value);
    }

    private void ApplyVolume(string parameter, float sliderValue)
    {
        float dB = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat(parameter, dB);
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }
}
