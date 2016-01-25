using UnityEngine;
using System.Collections;

public class TrackDataFormat : ScriptableObject, ISerializationCallbackReceiver
{
    int trackLength = 0;

    [SerializeField]
    int[,] lines;

    [SerializeField]
    GameObject[,] lineObjects;

    public void initLength(int length)
    {
        trackLength = length;
        lines = new int[5, trackLength];
        lineObjects = new GameObject[5, trackLength];
    }

    public void setLines(int index, int[,] lines, GameObject[,] lineObjects)
    {
        this.lines = lines;
        this.lineObjects = lineObjects;
    }

    public void setLine(int index, int[] line, GameObject[] lineObjects)
    {
        for(int i=0; i < trackLength; i++)
        {
            this.lines[index,i] = line[i];
            this.lineObjects[index,i] = lineObjects[i];
        }
    }


    public void OnBeforeSerialize()
    {

    }
    public void OnAfterDeserialize()
    {

    }
}

