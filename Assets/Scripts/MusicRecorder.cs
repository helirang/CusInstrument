using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicRecorder
{
    /// <summary>
    /// ������ ���� �ǹ� ��ȣ
    /// </summary>
    List<int> inputData = new List<int>();
    /// <summary>
    /// ������ �ǹ��� ���� �ð�
    /// </summary>
    List<float> inputTimeData = new List<float>();

    /// <summary>
    /// ���� ���縦 �ϴ� ����� �����͵�
    /// </summary>
    List<int> backUpInputData = new List<int>();
    List<float> backUpinputTimeData = new List<float>();

    char[] delimiterChars = {'/',','};
    float startTime;

    /// <summary>
    /// �ǹ� ��ȣ�� ����� List<int>������ ������ ��ȯ
    /// </summary>
    /// <returns> �ǹ� �� List<int> </returns>
    public List<int> GetInputData()
    {
        return inputData.Count > 0 ? 
                inputData : backUpInputData;
    }

    /// <summary>
    /// �ǹ� ��ȣ�� ����� List<int>������ ������ ��ȯ
    /// </summary>
    /// <returns> �ǹ� �� List<int> </returns>
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
