using UnityEngine;
using System.Collections;

public class RotateTrackDataReader : MonoBehaviour {

    int length = 50;
    float[] rotateDatas;
    

	void Start () {
        rotateDatas = new float[length];
        for(int i=0; i< length; i++)
        {
            rotateDatas[i] = 0.0f;
        }
    }

    public void changeStage(string stageName)
    {

    }

    public float read(int index)
    {
        index = index % length;
        return rotateDatas[index];
    }
}
