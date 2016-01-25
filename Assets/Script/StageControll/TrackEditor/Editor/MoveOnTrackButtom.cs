using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MoveButtonOnScene))]
public class MoveOnTrackButtom : Editor
{
    enum ButtomPlace { FOREWARD, BACKWARD, LEFTLINE, RIGHTLINE, MIDLINE};

    MovableItemOnStage mTarget;
    Track track;

    float btnSize = 0.3f;

    void OnSceneGUI()
    {
        MoveButtonOnScene tmp = target as MoveButtonOnScene;
        mTarget = tmp.transform.parent.GetComponent<MovableItemOnStage>(); ;

        if (mTarget.transform.parent == null)
            return;

        track = mTarget.transform.parent.GetComponent<Track>();

        DrawButton(Track.MoveDirection.FORWARD);
        DrawButton(Track.MoveDirection.BACKWARD);
    }
    
    void DrawButton(Track.MoveDirection direction)
    {
        Color recordColor = Handles.color;
        Handles.color = Color.red;

        Vector3 offset = getOffsetFromPlace(direction);
        Vector3 buttomPos = mTarget.transform.TransformPoint(offset*2.0f);

        if (Handles.Button(buttomPos, mTarget.transform.rotation, btnSize, btnSize, Handles.SphereCap))
        {
            if(checkMovable(direction))
                moveObject(direction);
        }
        Handles.color = recordColor;
    }

    bool checkMovable(Track.MoveDirection direction)
    {
        int lineNo = mTarget.getLineNo();
        int index = mTarget.getIndex();

        return track.checkMove(lineNo, index, direction);
    }


    void moveObject(Track.MoveDirection direction)
    {
        Vector3 posNext = Vector3.zero;
        Vector3 offset = getOffsetFromPlace(direction);

        if (direction == Track.MoveDirection.FORWARD)
        {
            posNext = mTarget.transform.TransformPoint(offset);
        }
        else if (direction == Track.MoveDirection.BACKWARD)
        {
            posNext = mTarget.transform.TransformPoint(offset);
        }
        mTarget.transform.position = posNext;

        int lineNo = mTarget.getLineNo();
        int index = mTarget.getIndex();
        track.moveItem(lineNo, index, direction, mTarget.blockLength);
    }

    Vector3 getOffsetFromPlace(Track.MoveDirection place)
    {
        Vector3 returnOffset = Vector3.zero;
        CreatorsManager cm = GameObject.FindObjectOfType<CreatorsManager>() as CreatorsManager;

        if (place == Track.MoveDirection.FORWARD)
        {
            returnOffset = new Vector3(0f, 0f, cm.blockLength);
        }
        else if (place == Track.MoveDirection.BACKWARD)
        {
            returnOffset = new Vector3(0f, 0f, cm.blockLength * -1.0f);
        }

        return returnOffset;
    }
}
