using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicRecorder
{
    /// <summary>
    /// 유저가 누른 건반 번호
    /// </summary>
    List<int> inputData;
    /// <summary>
    /// 유저가 건반을 누른 시간
    /// </summary>
    List<float> inputTimeData;
    float startTime;

    /// <summary>
    /// 기록 번호를 요청하면 해당 번호의 건반 값을 리턴해 준다.
    /// </summary>
    /// <param name="num">기록 번호</param>
    /// <returns>건반 값 리턴</returns>
    public int GetInputData(int num)
    {
        int result = -1;

        if (num < inputData.Count)
            result = inputData[num];

        return result;
    }

    /// <summary>
    /// 기록 번호를 요청하면 해당 번호의 건반 입력 시간 을 리턴해 준다.
    /// </summary>
    /// <param name="num">기록 번호</param>
    /// <returns>건반 입력 시간 리턴</returns>
    public float GetInputTimeData(int num)
    {
        float result = -1f;

        if (num < inputTimeData.Count)
            result = inputTimeData[num];

        return result;
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
        inputData.Clear();
        inputTimeData.Clear();
        startTime = 0f;
    }
}
