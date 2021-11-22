using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    // Start is called before the first frame update
    public int heightsField = 3;
    public int sizeField = 4;
    public int countGold = 12;
    public int countTries = 12;

    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
