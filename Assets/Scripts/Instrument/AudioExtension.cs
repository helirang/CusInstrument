using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioExtension :MonoBehaviour
{
    private List<IEnumerator> fadeCoroutines = new List<IEnumerator>();
    private List<bool> isFadeOuts = new List<bool>();

    void Initialized()
    {
        fadeCoroutines.Clear();
        isFadeOuts.Clear();
    }
    public void FadeOutSetting(int max)
    {
        Initialized();
        for (int i = 0; i < max; i++)
        {
            isFadeOuts.Add(false);
            fadeCoroutines.Add(null);
        }
    }
    public void FadeOutStart(int audioNum, AudioSource audioSource)
    {
        float duration = 1.5f;
        float targetVolume = 0.01f;
        isFadeOuts[audioNum] = true;
        fadeCoroutines[audioNum] = FadeOut(audioNum, duration, targetVolume, audioSource);
        StartCoroutine(fadeCoroutines[audioNum]);
    }
    public void FadeOutStop(int audioNum, AudioSource audioSource)
    {
        if (isFadeOuts[audioNum])
        {
            StopCoroutine(fadeCoroutines[audioNum]);
            audioSource.Stop();
            audioSource.volume = 1f;
            isFadeOuts[audioNum] = false;
        }
    }
    IEnumerator FadeOut(int audioNum, float duration, float targetVolume, AudioSource audioSource)
    {
        isFadeOuts[audioNum] = true;
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        audioSource.Stop();
        audioSource.volume = 1f;
        isFadeOuts[audioNum] = false;
    }
}
