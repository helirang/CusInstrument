using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerController : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider volumeSlider;

    public readonly string MIXER_VOLUME= "Volume";

    private void Awake()
    {
        volumeSlider.value = PlayerPrefs.GetFloat(MIXER_VOLUME, 1f);
        audioMixer.SetFloat("Volume", Mathf.Log10(volumeSlider.value) * 20);
    }

    private void OnEnable()
    {
        volumeSlider.onValueChanged.AddListener(SetVoulme);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(MIXER_VOLUME, volumeSlider.value);
        volumeSlider.onValueChanged.RemoveListener(SetVoulme);
    }

    public void SetVoulme(float value)
    {
        audioMixer.SetFloat("Volume",Mathf.Log10(value)*20);
    }

    
}
