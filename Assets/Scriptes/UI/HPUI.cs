using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPUI : MonoBehaviour
{
    public TextMeshProUGUI HpCounter;
    public TextMeshProUGUI StamCounter;
    public void ChangeHP(int change)
    {
        HpCounter.text = change.ToString();
    }
    public void ChangeStam(int change)
    {
        StamCounter.text = change.ToString();
    }
}