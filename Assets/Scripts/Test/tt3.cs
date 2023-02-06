using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class tt3 : MonoBehaviour
{
    char[] dd = {'/',','};
    string data = "0,1,2,3/4.2,1.2,1.1,1.5";
    // Start is called before the first frame update
    void Start()
    {
        string[] datas = data.Split(dd[0]);
        string[] d1 = datas[0].Split(dd[1]);
        string[] d2 = datas[1].Split(dd[1]);
        List<int> i1 = d1.Select(s => int.Parse(s)).ToList();
        List<float> f1 = d2.Select(s => float.Parse(s)).ToList();
        foreach (var i in f1)
        {
            Debug.Log(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
