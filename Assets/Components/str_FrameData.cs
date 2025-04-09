using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores information about a recorded object at a specific time.
/// </summary>
public struct str_FrameData
{
    public float time;
    public string data;

    public str_FrameData(float time, string data)
    {
        this.time = time;
        this.data = data;
    }
}
