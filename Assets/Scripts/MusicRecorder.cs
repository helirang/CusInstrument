using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicRecorder
{
    /// <summary>
    /// ������ ���� �ǹ� ��ȣ
    /// </summary>
    List<int> inputData;
    /// <summary>
    /// ������ �ǹ��� ���� �ð�
    /// </summary>
    List<float> inputTimeData;
    float startTime;

    /// <summary>
    /// ��� ��ȣ�� ��û�ϸ� �ش� ��ȣ�� �ǹ� ���� ������ �ش�.
    /// </summary>
    /// <param name="num">��� ��ȣ</param>
    /// <returns>�ǹ� �� ����</returns>
    public int GetInputData(int num)
    {
        int result = -1;

        if (num < inputData.Count)
            result = inputData[num];

        return result;
    }

    /// <summary>
    /// ��� ��ȣ�� ��û�ϸ� �ش� ��ȣ�� �ǹ� �Է� �ð� �� ������ �ش�.
    /// </summary>
    /// <param name="num">��� ��ȣ</param>
    /// <returns>�ǹ� �Է� �ð� ����</returns>
    public float GetInputTimeData(int num)
    {
        float result = -1f;

        if (num < inputTimeData.Count)
            result = inputTimeData[num];

        return result;
    }

    /// <summary>
    /// �Ű����� ���� inputData�� �߰�, �Է� �ð� ������ inputTimeData�� ����Ѵ�.
    /// </summary>
    /// <param name="audioNum">�ǹ� ��ȣ</param>
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
