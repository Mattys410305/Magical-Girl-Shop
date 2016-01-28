using UnityEngine;
using System.Collections;

public class CreatorsManager : MonoBehaviour {


    public enum CreateMode { Random, Block };

    public ObstacleCreator[] lines;

    public MovableItemOnStage[] midObstacleTypes;
    public int[] midObstacleWeights;
    public MovableItemOnStage[] obstacleTypes;
    public int[] obstacleWeights;
    public MovableItemOnStage[] collectionTypes;
    public int[] collectionWeights;

    public float blockLength = 2.0f;

    CreateMode currentMode = CreateMode.Random;
    int[] currentLines;
    MovableItemOnStage[] currentObjects;
    bool[] changeFlags;

    float nextBlock = 0.0f;
    float interval;

    void Start () {

        currentLines = new int[lines.Length];
        currentObjects = new MovableItemOnStage[lines.Length];
        changeFlags = new bool[lines.Length];

        nextBlock = Time.time;
        interval =  blockLength / 10.0f;

        for (int i = 0; i < currentLines.Length; i++)
            currentLines[i] = 0;
    }
	
	void Update () {

        if (nextBlock > Time.time)
            return;

        nextBlock += interval;
        cleanFlags();
        
        if (currentMode == CreateMode.Random)
        {
            produceInRandomMode();
        }
        else if(currentMode == CreateMode.Block)
        {
            produceInBlockMode();
        }

        countDownCurrentLines();
	}

    void cleanFlags()
    {
        for (int i = 0; i < changeFlags.Length; i++)
            changeFlags[i] = false;
    }

    void countDownCurrentLines()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            if (currentLines[i] > 0)
            {
                currentLines[i]--;
            }
            else if (currentLines[i] == 0)
            {
                currentLines[i] = -1;
                currentObjects[i] = null;
            }
        }
    }

    void produceInRandomMode ()
    {
        setCurrentObjectsInRandom();

        midLine();
        roundLines();
        
    }

    void setCurrentObjectsInRandom()
    {
        for (int i = 0; i < lines.Length -1 ; i++)
        {
            if(checkNeiborLines(i))
            {
                currentObjects[i] = getRandomToRoundLine(i);
            }
        }
        if(checkMidLine())
            currentObjects[lines.Length - 1] = getRandomToMidLine();
    }

    bool checkNeiborLines(int lineNo)
    {
        int lineNum = lines.Length - 1;
        int rightLineNo = (lineNo + 1) % lineNum;
        int leftLineNo = (lineNo + lineNum - 1) % lineNum;
        
        if (currentLines[lineNo] > 0)
            return false;

        if (currentLines[rightLineNo] > 0 && currentLines[leftLineNo] > 0)
        {
            return false;
        }

        return true;
    }

    bool checkMidLine()
    {
        if (currentLines[lines.Length - 1] > 0)
            return false;
        else
            return true;
    }

    MovableItemOnStage getRandomToRoundLine(int LineNo)
    {

        int totalWeight = 0;
        int random;
        int index;

        for (int i = 0; i < obstacleWeights.Length; i++)
            totalWeight += obstacleWeights[i];
        for (int i = 0; i < collectionWeights.Length; i++)
            totalWeight += collectionWeights[i];

        if (totalWeight > 100)
        {
            random = Random.Range(0, totalWeight);
        }
        else
        {
            random = Random.Range(0, 100);
        }

        int tmpWeight = 0;
        for (index = 0; index < obstacleWeights.Length; index++)
        {
            tmpWeight += obstacleWeights[index];
            if (tmpWeight >= random)
            {
                changeFlags[LineNo] = true;
                currentLines[LineNo] = obstacleTypes[index].needBlocks;
                return obstacleTypes[index];
            }
        }
        for (index = 0; index < collectionWeights.Length; index++)
        {
            tmpWeight += collectionWeights[index];
            if (tmpWeight >= random)
            {
                changeFlags[LineNo] = true;
                currentLines[LineNo] = collectionTypes[index].needBlocks;
                return collectionTypes[index];
            }
        }

        return currentObjects[LineNo];

    }

    MovableItemOnStage getRandomToMidLine()
    {
        int totalWeight = 0;
        int random;
        int index;

        for (int i = 0; i < midObstacleWeights.Length; i++)
            totalWeight += midObstacleWeights[i];

        if(totalWeight > 100)
        {
            random = Random.Range(0, totalWeight);
        }
        else
        {
            random = Random.Range(0, 100);
        }

        int tmpWeight = 0;
        for(index = 0; index < midObstacleWeights.Length; index++)
        {
            tmpWeight += midObstacleWeights[index];
            if (tmpWeight >= random)
            {
                currentLines[lines.Length - 1] = midObstacleTypes[index].needBlocks;
                changeFlags[lines.Length - 1] = true;
                return midObstacleTypes[index];
            }
        }

        return currentObjects[lines.Length - 1];
    }

    void produceInBlockMode()
    {

    }

    void midLine()
    {
        if (changeFlags[lines.Length - 1] && currentObjects[lines.Length - 1])
            lines[lines.Length - 1].SendMessage("createItem", currentObjects[lines.Length - 1]);
    }

    void roundLines()
    {
        for (int i = 0; i < lines.Length - 1; i++)
        {
            if(changeFlags[i] && currentObjects[i])
                lines[i].SendMessage("createItem", currentObjects[i]);
        }
    }


}

