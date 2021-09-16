using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int _totalScore = 0;

    public int TotalScore {
        get { return _totalScore; }
        set { _totalScore += value; }
    }
}
