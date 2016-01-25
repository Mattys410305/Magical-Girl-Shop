using UnityEngine;
using System.Collections;

public class Track : MonoBehaviour {
    
    public enum MoveDirection { FORWARD, BACKWARD, LEFTLINE, RIGHTLINE, MIDLINE };

    public GameObject trackLines;
    public int totalLine = 5;
    public int trackLength = 50;

    int[,] lines;
    GameObject[,] lineObjects;

    float radius = 1.5f;

    public void init()
    {
        lines = new int[totalLine, trackLength];
        lineObjects = new GameObject[totalLine, trackLength];
        for(int i=0; i< totalLine; i++)
        {
            for (int j = 0; j < trackLength; j++)
            {
                lines[i,j] = 0;
                lineObjects[i, j] = null;
            }

        }
    }

    public void addNewItem(GameObject item)
    {
        bool result = moveToFirstEmptyPlace(item);
        if (result)
        {
            item.transform.parent = this.transform;
        }
        else
        {
            GameObject.DestroyImmediate(item);
            Debug.Log("沒有空間塞下更多障礙物(收集品)");
        }

    }

    public bool checkMove(int lineNo, int pos, MoveDirection dir)
    {
        if (lineObjects[lineNo, pos] == null)
        {
            Debug.Log("checkMove: " + lineNo + "," + pos + ". 參數位置上找不到任何物件!");
            return false;
        }
        Debug.Log("checkMove" + lineNo + pos);
        MovableItemOnStage mItem = lineObjects[lineNo, pos].GetComponent<MovableItemOnStage>();

        int startIndex = pos;
        int checkLength = 0;

        switch (dir)
        {
            case MoveDirection.FORWARD:
                startIndex += mItem.blockLength;
                checkLength = 1;
                break;

            case MoveDirection.BACKWARD:
                startIndex -= 1;
                checkLength = 1;
                break;

            case MoveDirection.LEFTLINE:
                lineNo = (lineNo + 1) % (totalLine - 1);
                checkLength = mItem.blockLength;
                break;

            case MoveDirection.RIGHTLINE:
                lineNo = (lineNo + totalLine - 1) % (totalLine - 1);
                checkLength = mItem.blockLength;
                break;

            case MoveDirection.MIDLINE:
                lineNo = totalLine - 1;
                checkLength = mItem.blockLength;
                break;
        }

        return checkLineHasSpace(lineNo, startIndex, checkLength);
    }

    public void moveItem(int lineNo, int pos, MoveDirection dir, int blockLength)
    {
        int nextLineNo = 0;
        int nextPos = 0;

        switch (dir)
        {
            case MoveDirection.FORWARD:
                nextLineNo = lineNo;
                nextPos = pos + 1;
                break;

            case MoveDirection.BACKWARD:
                nextLineNo = lineNo;
                nextPos = pos - 1;
                break;

            case MoveDirection.LEFTLINE:
                nextLineNo = (lineNo + 1) % (totalLine - 1);
                nextPos = pos;
                break;

            case MoveDirection.RIGHTLINE:
                nextLineNo = (lineNo + totalLine - 1) % (totalLine - 1);
                nextPos = pos;
                break;

            case MoveDirection.MIDLINE:
                nextLineNo = totalLine - 1;
                nextPos = pos;
                break;
        }

        lines[lineNo, pos] = 0;
        lines[nextLineNo, nextPos] = blockLength;

        GameObject tmp = new GameObject();
        lineObjects[nextLineNo, nextPos] = lineObjects[lineNo, pos];
        lineObjects[lineNo, pos] = tmp;
        GameObject.DestroyImmediate(tmp);

        MovableItemOnStage mItem = lineObjects[nextLineNo, nextPos].GetComponent<MovableItemOnStage>();
        mItem.setPos(nextLineNo, nextPos);
    }

    public void activeTrack(bool active)
    {
        trackLines.SetActive(active);
    }


    //------------------private------------------------------------------

    bool moveToFirstEmptyPlace(GameObject item)
    {
        MovableItemOnStage mItem = item.GetComponent<MovableItemOnStage>();

        int startIndex = 0;
        int endIndex = trackLength - mItem.blockLength;

        for (; startIndex < endIndex; startIndex++)
        {
            for(int lineNumber = 0; lineNumber < this.totalLine; lineNumber++)
            {
                if (checkLineHasSpace(lineNumber, startIndex, mItem.blockLength))
                {
                    moveToLine(item, lineNumber, startIndex);
                    return true;
                }
            }
        }
        return false;
    }

    bool checkLineHasSpace( int lineNumber, int startIndex, int blockLength)
    {
        if(startIndex < 0)
        {
            return false;
        }

        for(int i = startIndex; i < (startIndex + blockLength); i++)
        {
            if (lines[lineNumber, i] != 0)
                return false; 
        }
        return true;
    }

    void moveToLine(GameObject item, int lineNumber, int index)
    {
        CreatorsManager cm = GameObject.FindObjectOfType<CreatorsManager>() as CreatorsManager;

        MovableItemOnStage mItem = item.GetComponent<MovableItemOnStage>();

        lines[lineNumber, index] = mItem.blockLength;
        lineObjects[lineNumber, index] = item;

        Vector3 lineOffset = getLineOffset(lineNumber);
        Vector3 lengthOffset = Vector3.forward * index * cm.blockLength;

        item.transform.position = item.transform.TransformPoint(lineOffset + lengthOffset);

        Debug.Log("moveToLine: ["+ lineNumber + "," + index+"]");
        mItem.setPos(lineNumber, index);
    }

    Vector3 getLineOffset(int lineNumber)
    {
        Vector3 v = Vector3.zero;
        switch(lineNumber)
        {
            case 0:
                v = Vector3.down * radius;
                break;
            case 1:
                v = Vector3.right *radius;
                break;
            case 2:
                v = Vector3.up * radius;
                break;
            case 3:
                v = Vector3.left * radius;
                break;
            case 4:
                v = Vector3.zero;
                break;
        }
        return v;
    }
}

