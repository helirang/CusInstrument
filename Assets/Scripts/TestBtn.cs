using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBtn : MonoBehaviour
{
    public FloatVariable hp;
    public void MHP()
    {
        hp.runtimeValue -= 1;
    }
}
