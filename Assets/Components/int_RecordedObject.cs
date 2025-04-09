using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface int_RecordedObject
{
    // A recorded object needs to be able to be turned into a JSON object and back.

    public void SetRecording(bool value);

    public void SetPlayback(bool value);

    public void FromJSON(string json);

    public string ToJSON();
}
