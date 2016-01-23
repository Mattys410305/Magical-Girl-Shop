using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MoveItemOnStage))]
public class MoveOnTrackButtom : Editor
{
    enum ButtomPlace { FOREWARD, BACKWARD, LEFTLINE, RIGHTLINE, MIDLINE};
    

    int currentPlace = 0;
    int currentLine = 0;

    float btnSize = 0.3f;

    void OnSceneGUI()
    {
        MoveItemOnStage tar = target as MoveItemOnStage;

        DrawButton(tar, ButtomPlace.FOREWARD);
        DrawButton(tar, ButtomPlace.BACKWARD);
    }
    
    void DrawButton(MoveItemOnStage tar, ButtomPlace place)
    {
        Color recordColor = Handles.color;
        Handles.color = Color.red;

        Vector3 offset = getOffsetFromPlace(place);
        Vector3 buttomPos = tar.transform.TransformPoint(offset*2.0f);

        if (Handles.Button(buttomPos, tar.transform.rotation, btnSize, btnSize, Handles.SphereCap))
        {
            moveObject(tar, place);
        }
        Handles.color = recordColor;
    }

    void moveObject(MoveItemOnStage tar, ButtomPlace place)
    {
        Vector3 posNext = Vector3.zero;
        Vector3 offset = getOffsetFromPlace(place);

        if (place == ButtomPlace.FOREWARD)
        {
            posNext = tar.transform.TransformPoint(offset);
        }
        else if (place == ButtomPlace.BACKWARD)
        {
            posNext = tar.transform.TransformPoint(offset);
        }
        tar.transform.position = posNext;
    }

    Vector3 getOffsetFromPlace(ButtomPlace place)
    {
        Vector3 returnOffset = Vector3.zero;
        CreatorsManager cm = GameObject.FindObjectOfType<CreatorsManager>() as CreatorsManager;

        if (place == ButtomPlace.FOREWARD)
        {
            returnOffset = new Vector3(0f, 0f, cm.blockLength);
        }
        else if (place == ButtomPlace.BACKWARD)
        {
            returnOffset = new Vector3(0f, 0f, cm.blockLength * -1.0f);
        }

        return returnOffset;
    }
}
