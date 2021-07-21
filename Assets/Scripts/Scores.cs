using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Scores : IComparable<Scores>
{

    public int score;
    public string name;

    public int CompareTo(Scores other)
    {
        if (other == null)
        {
            return 1;
        }
        if (this.score > other.score)
        {
            return 1;
        }
        if (this.score < other.score)
        {
            return -1;
        }
        return 0;
    }

    

}
