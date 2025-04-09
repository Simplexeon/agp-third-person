using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class scr_RecordedGameObject : MonoBehaviour, int_RecordedObject
{
    [SerializeField]
    private UnityEvent<bool> PlaybackChanged;

    [SerializeField]
    private UnityEvent<Transform> PlaybackDataUpdated;


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
        // No need to know if recording for this.
    }

    public void SetPlayback(bool value)
    {
        PlaybackChanged.Invoke(value);
    }

    public void FromJSON(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }

    public string ToJSON()
    {
        return JsonUtility.ToJson(this);
    }

    /* --- END INTERFACE --- */
}
