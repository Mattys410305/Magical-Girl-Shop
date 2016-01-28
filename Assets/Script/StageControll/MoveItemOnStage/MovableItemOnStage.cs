using UnityEngine;
using System.Collections;

public class MovableItemOnStage : MonoBehaviour {

    public enum ItemType { coin, obstacle };
    public int needBlocks = 10;
    public ItemType type = ItemType.coin;
    public string realName;

    Track.LineNo posLineNoTrackEnum;
    int posLineNo;
    int posIndex;

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
        posLineNo = lineNo;
        posIndex = depth;
        switch(lineNo)
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
