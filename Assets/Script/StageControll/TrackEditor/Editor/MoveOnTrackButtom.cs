using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MoveButtonOnScene))]
public class MoveOnTrackButtom : Editor
{
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

        DrawButton(Track.MoveDestination.FORWARD);
        DrawButton(Track.MoveDestination.BACKWARD);
        DrawButton(Track.MoveDestination.WESTLINE);
        DrawButton(Track.MoveDestination.EASTLINE);
        DrawButton(Track.MoveDestination.NORTHLINE);
        DrawButton(Track.MoveDestination.SOUTHLINE);
        DrawButton(Track.MoveDestination.MIDLINE);
    }
    
    void DrawButton(Track.MoveDestination direction)
    {
        Color recordColor = Handles.color;
        Handles.color = Color.green;

        Vector3 offset = getOffsetFromPlace(direction);
        Vector3 buttomPos = mTarget.transform.TransformPoint(offset);

        if (offset == Vector3.zero)
            return;

        if (Handles.Button(buttomPos, mTarget.transform.rotation, btnSize, btnSize, Handles.SphereCap))
        {
            if(checkMovable(direction))
                moveObject(direction);
        }
        Handles.color = recordColor;
    }

    bool checkMovable(Track.MoveDestination direction)
    {
        int lineNo = mTarget.getLineNo();
        int index = mTarget.getIndex();

        return track.checkMove(lineNo, index, direction);
    }


    void moveObject(Track.MoveDestination direction)
    {
        /*Vector3 posNext = Vector3.zero;
        Vector3 offset = getOffsetFromPlace(direction);

        posNext = mTarget.transform.TransformPoint(offset);
        mTarget.transform.position = posNext;*/

        int lineNo = mTarget.getLineNo();
        int index = mTarget.getIndex();
        track.moveItemByDirection(lineNo, index, direction, mTarget.needBlocks);
    }

    Vector3 getOffsetFromPlace(Track.MoveDestination dir)
    {
        Vector3 returnOffset = Vector3.zero;
        CreatorsManager cm = GameObject.FindObjectOfType<CreatorsManager>() as CreatorsManager;
        
        switch (dir)
        {
            case Track.MoveDestination.FORWARD:
                returnOffset = new Vector3(0f, 0f, cm.blockLength);
                break;
            case Track.MoveDestination.BACKWARD:
                returnOffset = new Vector3(0f, 0f, cm.blockLength * -1.0f);
                break;
            case Track.MoveDestination.WESTLINE:
                returnOffset = getWestLineOffset();
                break;
            case Track.MoveDestination.EASTLINE:
                returnOffset = getEastLineOffset();
                break;
            case Track.MoveDestination.MIDLINE:
                returnOffset = getMidLineOffset();
                break;
            case Track.MoveDestination.NORTHLINE:
                returnOffset = getNorthLineOffset();
                break;
            case Track.MoveDestination.SOUTHLINE:
                returnOffset = getSouthLineOffset();
                break;

        }

        return returnOffset;
    }

    Vector3 getWestLineOffset()
    {
        Track.LineNo currentLine = mTarget.getLineNoTrackEnum();

        switch (currentLine)
        {
            case Track.LineNo.EAST:
                return new Vector3(track.radius * -2.0f, 0.0f, 0.0f);
            case Track.LineNo.SOUTH:
                return new Vector3(track.radius * -1.0f, track.radius * 1.0f, 0.0f);
            case Track.LineNo.WEST:
                return Vector3.zero;
            case Track.LineNo.NORTH:
                return new Vector3(track.radius * -1.0f, track.radius * -1.0f, 0.0f);
            case Track.LineNo.MID:
                return new Vector3(track.radius * -1.0f, 0.0f, 0.0f);
        }
        return Vector3.zero;
    }

    Vector3 getEastLineOffset()
    {
        Track.LineNo currentLine = mTarget.getLineNoTrackEnum();

        switch (currentLine)
        {
            case Track.LineNo.EAST:
                return Vector3.zero;
            case Track.LineNo.SOUTH:
                return new Vector3(track.radius * 1.0f, track.radius * 1.0f, 0.0f);
            case Track.LineNo.WEST:
                return new Vector3(track.radius * 2.0f, 0.0f, 0.0f);
            case Track.LineNo.NORTH:
                return new Vector3(track.radius * 1.0f, track.radius * -1.0f, 0.0f);
            case Track.LineNo.MID:
                return new Vector3(track.radius * 1.0f, 0.0f, 0.0f);
        }

        return Vector3.zero;
    }

    Vector3 getMidLineOffset()
    {

        Track.LineNo currentLine = mTarget.getLineNoTrackEnum();

        switch (currentLine)
        {
            case Track.LineNo.EAST:
                return new Vector3(track.radius * -1.0f, 0.0f, 0.0f);
            case Track.LineNo.SOUTH:
                return new Vector3(0.0f, track.radius * 1.0f, 0.0f);
            case Track.LineNo.WEST:
                return new Vector3(track.radius * 1.0f, 0.0f, 0.0f);
            case Track.LineNo.NORTH:
                return new Vector3(0.0f, track.radius * -1.0f, 0.0f);
            case Track.LineNo.MID:
                return Vector3.zero;
        }
        return Vector3.zero;
    }

    Vector3 getNorthLineOffset()
    {
        Track.LineNo currentLine = mTarget.getLineNoTrackEnum();

        switch (currentLine)
        {
            case Track.LineNo.EAST:
                return new Vector3(track.radius * -1.0f, track.radius * 1.0f, 0.0f);
            case Track.LineNo.SOUTH:
                return new Vector3(0.0f, track.radius * 2.0f, 0.0f);
            case Track.LineNo.WEST:
                return new Vector3(track.radius * 1.0f, track.radius * 1.0f, 0.0f);
            case Track.LineNo.NORTH:
                return Vector3.zero;
            case Track.LineNo.MID:
                return new Vector3(0.0f, track.radius * 1.0f, 0.0f);
        }
        return Vector3.zero;
    }

    Vector3 getSouthLineOffset()
    {
        Track.LineNo currentLine = mTarget.getLineNoTrackEnum();

        switch (currentLine)
        {
            case Track.LineNo.EAST:
                return new Vector3(track.radius * -1.0f, track.radius * -1.0f, 0.0f);
            case Track.LineNo.SOUTH:
                return Vector3.zero;
            case Track.LineNo.WEST:
                return new Vector3(track.radius * 1.0f, track.radius * -1.0f, 0.0f);
            case Track.LineNo.NORTH:
                return new Vector3(0.0f, track.radius * -2.0f, 0.0f);
            case Track.LineNo.MID:
                return new Vector3(0.0f, track.radius * -1.0f, 0.0f);
        }

        return Vector3.zero;
    }
}
