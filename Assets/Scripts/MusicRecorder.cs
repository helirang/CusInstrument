using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicRecorder
{
    /// <summary>
    /// 유저가 누른 건반 번호
    /// </summary>
    List<int> inputData = new List<int>();
    /// <summary>
    /// 유저가 건반을 누른 시간
    /// </summary>
    List<float> inputTimeData = new List<float>();

    /// <summary>
    /// 깊은 복사를 하는 백업용 데이터들
    /// </summary>
    List<int> backUpInputData = new List<int>();
    List<float> backUpinputTimeData = new List<float>();

    char[] delimiterChars = {'/',','};
    float startTime;

    /// <summary>
    /// 건반 번호를 기록한 List<int>형식의 데이터 반환
    /// </summary>
    /// <returns> 건반 값 List<int> </returns>
    public List<int> GetInputData()
    {
        return inputData.Count > 0 ? 
                inputData : backUpInputData;
    }

    /// <summary>
    /// 건반 번호를 기록한 List<int>형식의 데이터 반환
    /// </summary>
    /// <returns> 건반 값 List<int> </returns>
    public List<float> GetInputTimeData()
    {
        return inputTimeData.Count > 0 ?
            inputTimeData : backUpinputTimeData;
    }

    public bool SetData(string data)
    {
        string[] datas = data.Split(delimiterChars[0]);
        string[] stringInputData = datas[0].Split(delimiterChars[1]);
        string[] stringTimeData = datas[1].Split(delimiterChars[1]);
        this.inputData = stringInputData.Select(s => int.Parse(s)).ToList();
        this.inputTimeData = stringTimeData.Select(s => float.Parse(s)).ToList();
        return this.inputData.Count > 0 && 
            this.inputTimeData.Count == this.inputData.Count ? true : false;
    }

    public bool SaveData()
    {
        string sInputData = string.Join(',',inputData);
        string sInputTimeData = string.Join(',', inputTimeData);
        string saveData = sInputData + '/' + sInputTimeData;
        return true;
    }
    /// <summary>
    /// 매개변수 값을 inputData에 추가, 입력 시간 간격을 inputTimeData에 기록한다.
    /// </summary>
    /// <param name="audioNum">건반 번호</param>
    public void Record(int audioNum) 
    {
        inputData.Add(audioNum);
        if (startTime == 0f) inputTimeData.Add(0f);
        else inputTimeData.Add(startTime = Time.time - startTime);

        startTime = Time.time;
    }

    public void Initialize()
    {
        if(inputData.Count > 0)
        {
            backUpInputData = inputData.ToList<int>();
            backUpinputTimeData = inputTimeData.ToList<float>();
        }
        inputData.Clear();
        inputTimeData.Clear();
        startTime = 0f;
    }
}
