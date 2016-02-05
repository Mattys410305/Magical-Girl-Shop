using UnityEngine;
using System.Collections;

public class MovableItemOnStage : MonoBehaviour {

    public enum ItemType { coin, obstacle };
    public int needBlocks = 10;
    public ItemType type = ItemType.coin;
    public string realName;

    Track.LineNo posLineNoTrackEnum;
    int posLineNo = 4;
    int posIndex = 0;

    public ItemType getItemType()
    {
        return type;
    }

    public Track.LineNo getLineNoTrackEnum()
    {
        return posLineNoTrackEnum;
    }

    public int getLineNo()
    {
        return posLineNo;
    }

    public int getIndex()
    {
        return posIndex;
    }

    public void setPos(int lineNo, int depth)
    {
        moveItem(lineNo, depth);
        setTrackLineCoordinate(lineNo, depth);
    }
    
    void moveItem(int toLineNo, int toLineIndex)
    {
        Vector3 beginng = TranslateTrackLineToWorldCoordinate(posLineNo, posIndex);
        Vector3 destination = TranslateTrackLineToWorldCoordinate(toLineNo, toLineIndex);

        transform.position = transform.TransformPoint(destination - beginng);

    }

    Vector3 TranslateTrackLineToWorldCoordinate(int lineNo, int index)
    {
        Vector3 lineOffset = getLineOffset(lineNo);
        Vector3 indexOffset = getIndexOffset(index);

        return lineOffset + indexOffset;
    }

    Vector3 getLineOffset(int lineNumber)
    {
        Track track = GameObject.FindObjectOfType<Track>();
        Vector3 v = Vector3.zero;
        switch (lineNumber)
        {
            case 0:
                v = Vector3.right * track.radius;
                break;
            case 1:
                v = Vector3.down * track.radius;
                break;
            case 2:
                v = Vector3.left * track.radius;
                break;
            case 3:
                v = Vector3.up * track.radius;
                break;
            case 4:
                v = Vector3.zero;
                break;
        }
        return v;
    }

    Vector3 getIndexOffset(int index)
    {
        GridManager gm = GameObject.FindObjectOfType<GridManager>();

        return new Vector3(0, 0, index * gm.gridLength);
    }

    void setTrackLineCoordinate(int toLineNo, int toLineIndex)
    {
        posLineNo = toLineNo;
        posIndex = toLineIndex;
        switch (toLineNo)
        {
            case 0:
                posLineNoTrackEnum = Track.LineNo.EAST;
                break;
            case 1:
                posLineNoTrackEnum = Track.LineNo.SOUTH;
                break;
            case 2:
                posLineNoTrackEnum = Track.LineNo.WEST;
                break;
            case 3:
                posLineNoTrackEnum = Track.LineNo.NORTH;
                break;
            case 4:
                posLineNoTrackEnum = Track.LineNo.MID;
                break;
        }
    }
}
