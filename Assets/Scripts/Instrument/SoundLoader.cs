using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SoundLoader : MonoBehaviour
{
    public List<AudioClip> AudioClips { get; set; }
    string directoryPath;
    public string audioTypeName = "aiFf";
    int soundMax = 14;

    private void Awake()
    {
        directoryPath = Application.dataPath + "/StreamingAssets/Custom/";
        AudioClips = new List<AudioClip>();
    }

    public IEnumerator AudioLoad(System.Action CompleteCallBack)
    {
        AudioClips.Clear();
        audioTypeName = audioTypeName.ToLower();
        if (audioTypeName != "aiff" && audioTypeName != "wav")
        {
            //Debug.LogWarning($"{audioTypeName} : 현재 오디오 타입은 aiff 또는 wav가 아닙니다.");
        }
        else
        {
            AudioType audioType = audioTypeName == "aiff" ? AudioType.AIFF : AudioType.WAV;
            for (int i = 0; i < soundMax; i++)
            {
                yield return StartCoroutine(AudioLoader(i.ToString(), audioType));
            }
            //사운드가 빠른 순서대로 로드 됨. 따라서 마지막 사운드 로드되면 한번 정렬시킨다.
            if (AudioClips.Count == soundMax) ComplteLoad();
            CompleteCallBack();
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
                //Debug.LogWarning(req.error + "\n" + filePath);
                LoadErr(req.error + "\n" + filePath);
            }
            else
            {
                AudioClip audioClip = DownloadHandlerAudioClip.GetContent(req);
                audioClip.name = fileName;
                AudioClips.Add(audioClip);
            }
        }
    }

    private void ComplteLoad()
    {
        AudioClips.Sort((p1, p2) => int.Parse(p1.name).CompareTo(int.Parse(p2.name)));
    }

    private void LoadErr(string errMsg)
    {
        //에러 매니저 만들기
    }
}
