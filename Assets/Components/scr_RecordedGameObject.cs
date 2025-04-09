using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class scr_RecordedGameObject : MonoBehaviour, int_RecordedObject
{
    [SerializeField]
    private UnityEvent<bool> RecordingChanged;

    [SerializeField]
    private UnityEvent<bool> PlaybackChanged;

    [SerializeField]
    private UnityEvent<Vector3, Vector3, Vector3> PlaybackDataUpdated;


    // Data

    public float posx;
    public float posy;
    public float posz;

    public float rotx;
    public float roty;
    public float rotz;

    public float scalex;
    public float scaley;
    public float scalez;

    // Functions

    /* --- Recorded Object Interface --- */

    public void SetRecording(bool value)
    {
        RecordingChanged.Invoke(value);
    }

    public void SetPlayback(bool value)
    {
        PlaybackChanged.Invoke(value);
    }

    public void FromJSON(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);

        PlaybackDataUpdated.Invoke(new Vector3(posx, posy, posz), new Vector3(rotx, roty, rotx), 
            new Vector3(scalex, scaley, scalez));
    }

    public string ToJSON()
    {
        return JsonUtility.ToJson(this);
    }

    /* --- END INTERFACE --- */
}
