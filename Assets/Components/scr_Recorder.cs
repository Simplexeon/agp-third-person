using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Recorder : MonoBehaviour
{

    // Data

    private Dictionary<string, scr_Recordable> recordables;

    /// <summary>
    /// Stores recording information by RecordID with a list of each
    /// new data change. 
    /// </summary>
    private Dictionary<string, List<str_FrameData>> recording;

    /// <summary>
    /// Stores the current list entry index for all of the recordables.
    /// Used during playback to track which times have been reached.
    /// </summary>
    private Dictionary<string, int> playbackPositions;

    private bool inRecording;

    private bool inPlayback;

    private float recordingTime;



    // Processes


    public scr_Recorder()
    {
        recordables = new Dictionary<string, scr_Recordable>();
    }


    // Update is called once per frame
    void Update()
    {
        if (inRecording)
        {
            recordingTime += Time.deltaTime;
        }

        if (inPlayback)
        {
            recordingTime += Time.deltaTime;
            int finishedEntries = 0;

            // Update each recordable that needs to be
            foreach(KeyValuePair<string, List<str_FrameData>> entry in recording)
            {
                if(ProcessPlaybackEntry(entry))
                {
                    finishedEntries++;
                }
            }

            // Everything finished playback, so end playback.
            if(finishedEntries >= recording.Count)
            {
                inPlayback = false;
            }
        }
    }


    /// <summary>
    /// Playback one of the recordable objects this recorder is tracking.
    /// If the recording is finished, returns true.
    /// </summary>
    /// <param name="entry">
    /// RecordableID and recording data.
    /// </param>
    /// <returns>
    /// If the playback is finished.
    /// </returns>
    private bool ProcessPlaybackEntry(KeyValuePair<string, List<str_FrameData>> entry)
    {
        int pos = playbackPositions[entry.Key];

        if (pos < entry.Value.Count)
        {
            while (recordingTime >= entry.Value[pos].time)
            {
                // assign the data to the object
                recordables[entry.Key].UpdateValue(entry.Value[pos].data);

                playbackPositions[entry.Key]++;
                pos++;

                // Stop if reached the end of the object's tape.
                if(pos >= entry.Value.Count)
                {
                    return true;
                }
            }
        } 
        else
        {
            return true;
        }

        return false;
    }


    // Functions

    public void BeginRecording()
    {
        inRecording = true;
        recordingTime = 0;
    }

    public void EndRecording()
    {
        inRecording = false;
    }

    public void StartPlayback()
    {
        inPlayback = true;
        recordingTime = 0;
    }


    public void RegisterRecordable(string name, scr_Recordable recordable)
    {
        recordables[name] = recordable;
        recording[name] = new List<str_FrameData>();
        playbackPositions[name] = 0;
    }

    public void RecordValue(string recordID, string data)
    {
        if (!recording.ContainsKey(recordID)) return;

        recording[recordID].Add(new str_FrameData(recordingTime, data));
    }
}
