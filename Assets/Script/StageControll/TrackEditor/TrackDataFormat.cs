using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackDataFormat : ScriptableObject, ISerializationCallbackReceiver
{
    int[,] lines;
    string[,] lineItemNames;

    // lineIndex*10 + lineNo = keyLine
    public int trackLength = 0;
    public List<int> keyLine = new List<int>();
    public List<int> valueNeedBlocks = new List<int>();
    public List<string> valueItemNames = new List<string>();
    
    public void initLength(int length)
    {
        trackLength = length;
        lines = new int[5, trackLength];
        lineItemNames = new string[5, trackLength];
    }

    public void setLines(int[,] inputLines, GameObject[,] inputLineItems)
    {
        MovableItemOnStage mItem;
        lines = inputLines;

        for (int lineNo = 0; lineNo < 5; lineNo++)
        {
            for (int lineIndex = 0; lineIndex < trackLength; lineIndex++)
            {
                if (inputLineItems[lineNo, lineIndex])
                {
                    mItem = inputLineItems[lineNo, lineIndex].GetComponent<MovableItemOnStage>();
                    lineItemNames[lineNo, lineIndex] = mItem.realName;
                }
            }
        }
    }

    public int[,] getLineNo()
    {
        return lines;
    }

    public string[,] getLineItemNames()
    {
        return lineItemNames;
    }

    public void OnBeforeSerialize()
    {
        keyLine.Clear();
        valueNeedBlocks.Clear();
        valueItemNames.Clear();
        
        int lineNoAndIndex;
        for (int lineNo = 0; lineNo < 5; lineNo++)
        {
            for (int lineIndex = 0; lineIndex < trackLength; lineIndex++)
            {
                if (lines[lineNo, lineIndex] != 0)
                {
                    lineNoAndIndex = lineIndex * 10 + lineNo;
                    keyLine.Add(lineNoAndIndex);
                    valueNeedBlocks.Add(lines[lineNo, lineIndex]);
                    valueItemNames.Add(lineItemNames[lineNo,lineIndex]);
                }
            }
        }
    }

    public void OnAfterDeserialize()
    {
        int lineNo;
        int lineIndex;

        lines = new int[5, trackLength];
        lineItemNames = new string[5, trackLength];

        for (int i = 0; i < 5; i++)
            for (int j = 0; j < trackLength; j++)
                lines[i, j] = 0;

        for (int i = 0; i < keyLine.Count; i++)
        {
            lineNo = keyLine[i] % 10;
            lineIndex = keyLine[i] / 10;

            lines[lineNo, lineIndex] = valueNeedBlocks[i];
            lineItemNames[lineNo, lineIndex] = valueItemNames[i];
        }
    }
}

