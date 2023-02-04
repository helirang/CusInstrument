using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SoundLoader : MonoBehaviour
{
    [SerializeField]List<AudioClip> audioClips;
    string directoryPath;
    public string audioTypeName = "aiFf";
    int soundMax = 12;

    private void Awake()
    {
        directoryPath = Application.dataPath + "/StreamingAssets/Custom/";
    }

    public void AudioLoad()
    {
        audioClips.Clear();
        audioTypeName = audioTypeName.ToLower();
        if (audioTypeName != "aiff" && audioTypeName != "wav")
        {
            Debug.LogWarning($"{audioTypeName} : 현재 오디오 타입은 aiff 또는 wav가 아닙니다.");
        }
        else
        {
            AudioType audioType = audioTypeName == "aiff" ? AudioType.AIFF : AudioType.WAV;
            for (int i = 0; i < soundMax; i++)
            {
                StartCoroutine(AudioLoader(i.ToString(), audioType));
            }
        }
    }

    private IEnumerator AudioLoader(string fileName , AudioType audioType)
    {
        string filePath = $"{directoryPath}{fileName}.{audioType}";
        using (UnityWebRequest req = UnityWebRequestMultimedia.GetAudioClip(filePath, audioType))
        {
            yield return req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning(req.error + "\n" + filePath);
            }
            else
            {
                AudioClip audioClip = DownloadHandlerAudioClip.GetContent(req);
                audioClip.name = fileName;
                audioClips.Add(audioClip);

                //비동기로딩으로 사운드가 빠른 순서대로 로드 됨. 따라서 마지막 사운드 로드되면 한번 정렬시킨다.
                if (audioClips.Count == soundMax) audioClips.Sort((p1,p2)=>int.Parse(p1.name).CompareTo(int.Parse(p2.name)));
            }
        }
    }
}
