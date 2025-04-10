using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class scr_Recordable : MonoBehaviour
{
    // Properties

    [SerializeField]
    private scr_Recorder Recorder;

    [SerializeField]
    public int_RecordedObject RecordedObject;

    // RecordID, Object JSON
    [SerializeField]
    private UnityEvent<string, string> RecordValue;

    // The string used by the recorder to store data
    [SerializeField]
    private string RecordID;



    // Processes


    private void Start()
    {
        // Setup this object with the recorder
        if (Recorder != null)
        {
            Recorder.RegisterRecordable(RecordID, this);
        }

    }



    // Functions


    public void SetRecording(bool value)
    {
        RecordedObject.SetRecording(value);
    }


    public void SetPlayback(bool value)
    {
        RecordedObject.SetPlayback(value);
    }

    /// <summary>
    /// Save the value of the passed object to the recorder.
    /// </summary>
    /// <param name="recordedObject">
    /// Object to save data of.
    /// </param>
    public void SaveValue(int_RecordedObject recordedObject)
    {
        RecordValue.Invoke(RecordID, recordedObject.ToJSON());
    }

    /// <summary>
    /// Update the data of the object being recorded.
    /// Commonly used during playback of recordings.
    /// </summary>
    /// <param name="data">
    /// JSON data of the recorded object.
    /// </param>
    public void UpdateValue(string data)
    {
        RecordedObject.FromJSON(data);
    }

}
