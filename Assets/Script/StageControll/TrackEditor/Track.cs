using UnityEngine;
using System.Collections;

public class Track : MonoBehaviour {
    
    public enum MoveDestination { FORWARD, BACKWARD, WESTLINE, EASTLINE, MIDLINE, NORTHLINE, SOUTHLINE };
    public enum LineNo { EAST, SOUTH, WEST, NORTH, MID};
    
    public GameObject trackLines;
    const int totalLine = 5;
    public int trackLength = 50;
    public float radius = 1.5f;

    int[,] lines;
    GameObject[,] lineObjects;


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
        item.transform.parent = this.transform;
        bool result = moveToFirstEmptyPlace(item);
        if (!result)
        {
            GameObject.DestroyImmediate(item);
            Debug.Log("沒有空間塞下更多障礙物(收集品)");
        }
    }

    public int[,] getTrackLines()
    {
        return lines;
    }

    public GameObject[,] getTrackItems()
    {
        return lineObjects;
    }

    public void deleteItem(int lineNo, int index)
    {
        lines[lineNo, index] = 0;
        lineObjects[lineNo, index] = null;
    }
    /*public void deleteItem(GameObject item)
    {
        MovableItemOnStage mItem = item.GetComponent<MovableItemOnStage>();

        int lineNo = mItem.getLineNo();
        int index = mItem.getIndex();

        lines[lineNo, index] = 0;
        lineObjects[lineNo, index] = null;

        GameObject.DestroyImmediate(item);
    }*/

    public bool checkMove(int lineNo, int pos, MoveDestination dir)
    {
        if (lineObjects[lineNo, pos] == null)
        {
            Debug.Log("checkMove: " + lineNo + "," + pos + ". 參數位置上找不到任何物件!");
            return false;
        }
        //Debug.Log("checkMove: " + lineNo + "," + pos + ".  " + dir);
        MovableItemOnStage mItem = lineObjects[lineNo, pos].GetComponent<MovableItemOnStage>();

        int startIndex = pos;
        int checkLength = 0;

        switch (dir)
        {
            case MoveDestination.FORWARD:
                startIndex += mItem.needBlocks;
                checkLength = 1;
                break;

            case MoveDestination.BACKWARD:
                startIndex -= 1;
                checkLength = 1;
                break;

            case MoveDestination.EASTLINE:
                lineNo = 0;
                checkLength = mItem.needBlocks;
                Debug.Log("checkNext: " + lineNo + "," + checkLength);
                break;

            case MoveDestination.SOUTHLINE:
                lineNo = 1;
                checkLength = mItem.needBlocks;
                Debug.Log("checkNext: " + lineNo + "," + checkLength);
                break;

            case MoveDestination.WESTLINE:
                lineNo = 2;
                checkLength = mItem.needBlocks;
                break;


            case MoveDestination.NORTHLINE:
                lineNo = 3;
                checkLength = mItem.needBlocks;
                break;


            case MoveDestination.MIDLINE:
                lineNo = 4;
                checkLength = mItem.needBlocks;
                break;
        }

        return checkLineHasSpace(lineNo, startIndex, checkLength);
    }

    public void moveItemByDirection(int lineNo, int pos, MoveDestination dir, int blockLength)
    {
        int nextLineNo = 0;
        int nextPos = 0;

        switch (dir)
        {
            case MoveDestination.FORWARD:
                nextLineNo = lineNo;
                nextPos = pos + 1;
                break;

            case MoveDestination.BACKWARD:
                nextLineNo = lineNo;
                nextPos = pos - 1;
                break;

            case MoveDestination.EASTLINE:
                nextLineNo = 0;
                nextPos = pos;
                break;

            case MoveDestination.SOUTHLINE:
                nextLineNo = 1;
                nextPos = pos;
                break;


            case MoveDestination.WESTLINE:
                nextLineNo = 2;
                nextPos = pos;
                break;

            case MoveDestination.NORTHLINE:
                nextLineNo = 3;
                nextPos = pos;
                break;


            case MoveDestination.MIDLINE:
                nextLineNo = 4;
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

    public void moveItemTo(int srcLineNo, int srcPos, int toLineNo, int toPos)
    {
        lines[toLineNo, toPos] = lines[srcLineNo, srcPos];
        lines[srcLineNo, srcPos] = 0;

        GameObject tmp = new GameObject();
        lineObjects[toLineNo, toPos] = lineObjects[srcLineNo, srcPos];
        lineObjects[srcLineNo, srcPos] = tmp;
        GameObject.DestroyImmediate(tmp);

        MovableItemOnStage mItem = lineObjects[toLineNo, toPos].GetComponent<MovableItemOnStage>();
        mItem.setPos(toLineNo, toPos);
    }

    public void activeTrack(bool active)
    {
        if(trackLines)
            trackLines.SetActive(active);
    }

    public void printStatus()
    {
        for (int i = 0; i < totalLine; i++)
        {
            for (int j = 0; j < trackLength; j++)
            {
                if (lines[i, j] != 0)
                {
                    Debug.Log("["+i+","+j+"]");
                    Debug.Log(lineObjects[i, j]);
                }
            }
        }
    }

    //------------------private------------------------------------------

    bool moveToFirstEmptyPlace(GameObject item)
    {
        MovableItemOnStage mItem = item.GetComponent<MovableItemOnStage>();

        int startIndex = 0;
        int endIndex = trackLength - mItem.needBlocks;

        for (; startIndex < endIndex; startIndex++)
        {
            for(int lineNumber = 0; lineNumber < totalLine; lineNumber++)
            {
                if (checkLineHasSpace(lineNumber, startIndex, mItem.needBlocks))
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
        //CreatorsManager cm = GameObject.FindObjectOfType<CreatorsManager>() as CreatorsManager;

        MovableItemOnStage mItem = item.GetComponent<MovableItemOnStage>();

        lines[lineNumber, index] = mItem.needBlocks;
        lineObjects[lineNumber, index] = item;

        /*Vector3 lineOffset = getLineOffset(lineNumber);
        Vector3 lengthOffset = Vector3.forward * index * cm.blockLength;

        item.transform.position = item.transform.TransformPoint(lineOffset + lengthOffset);*/

        Debug.Log("moveToLine: ["+ lineNumber + "," + index+"]");
        mItem.setPos(lineNumber, index);
    }

    Vector3 getLineOffset(int lineNumber)
    {
        Vector3 v = Vector3.zero;
        switch(lineNumber)
        {
            case 0:
                v = Vector3.right * radius;
                break;
            case 1:
                v = Vector3.down *radius;
                break;
            case 2:
                v = Vector3.left * radius;
                break;
            case 3:
                v = Vector3.up * radius;
                break;
            case 4:
                v = Vector3.zero;
                break;
        }
        return v;
    }
}

