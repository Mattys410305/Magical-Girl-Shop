using UnityEngine;
using System.Collections;

public class CreatorsManager : MonoBehaviour {

	const string itemPath = "Item/";

    public enum CreateMode { Random, Edited };

    public ObstacleCreator[] lines;

    public MovableItemOnStage[] midObstacleTypes;
    public int[] midObstacleWeights;
    public MovableItemOnStage[] obstacleTypes;
    public int[] obstacleWeights;
    public MovableItemOnStage[] collectionTypes;
    public int[] collectionWeights;

    GridManager gridManager;

    CreateMode currentMode = CreateMode.Random;
    int[] currentLines;
    MovableItemOnStage[] currentObjects;
    bool[] dirtyFlags;

    TrackDataFormat currentTrack;
	int trackStartGrid = 0;

    void Start () {

        currentLines = new int[lines.Length];
        currentObjects = new MovableItemOnStage[lines.Length];
        dirtyFlags = new bool[lines.Length];

        gridManager = GameObject.FindObjectOfType<GridManager>();

        for (int i = 0; i < currentLines.Length; i++)
            currentLines[i] = 0;
    }

    public void Create()
    {
        cleanFlags();

        if (currentMode == CreateMode.Random)
        {
            produceInRandomMode();
        }
        else if (currentMode == CreateMode.Edited)
        {
            produceInEditedMode();
        }

        countDownCurrentLines();
    }

    void cleanFlags()
    {
        for (int i = 0; i < dirtyFlags.Length; i++)
            dirtyFlags[i] = false;
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
                currentObjects[i] = chooseItemToRoundLineInRandom(i);
            }
        }
        if(checkMidLine())
            currentObjects[lines.Length - 1] = chooseItemToMidLineInRandom();
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

    MovableItemOnStage chooseItemToRoundLineInRandom(int LineNo)
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
                dirtyFlags[LineNo] = true;
                currentLines[LineNo] = obstacleTypes[index].needBlocks;
                return obstacleTypes[index];
            }
        }
        for (index = 0; index < collectionWeights.Length; index++)
        {
            tmpWeight += collectionWeights[index];
            if (tmpWeight >= random)
            {
                dirtyFlags[LineNo] = true;
                currentLines[LineNo] = collectionTypes[index].needBlocks;
                return collectionTypes[index];
            }
        }

        return currentObjects[LineNo];

    }

    MovableItemOnStage chooseItemToMidLineInRandom()
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
                dirtyFlags[lines.Length - 1] = true;
                return midObstacleTypes[index];
            }
        }

        return currentObjects[lines.Length - 1];
    }

    //---------------------------------------------------------------
    
    void produceInEditedMode()
    {
        setCurrentObjectsInEdited();

        midLine();
        roundLines();
    }

    void setCurrentObjectsInEdited()
    {
		int currentGrid = gridManager.getCurrentGrid();
		int currentTrackIndex = currentGrid - trackStartGrid;
		string tmpItemName;

		for(int i=0; i < 5; i++)
		{
			if(currentTrack.getLineNo()[i, currentTrackIndex] != 0)
			{
				currentLines[i] = currentTrack.getLineNo()[i, currentTrackIndex];
				tmpItemName = currentTrack.getLineItemNames()[i, currentTrackIndex];
				currentObjects[i] = Resources.Load<MovableItemOnStage>(itemPath + tmpItemName);
				if(!currentObjects[i])
					Debug.Log("CM: can't load: " + tmpItemName);
				dirtyFlags[i] = true;
			}
		}
    }

    //---------------------------------------------------------------

    void midLine()
    {
        if (dirtyFlags[lines.Length - 1] && currentObjects[lines.Length - 1])
		{
            lines[lines.Length - 1].createItem(currentObjects[lines.Length - 1]);
		}
    }

    void roundLines()
    {
        for (int i = 0; i < lines.Length - 1; i++)
		{
            if(dirtyFlags[i] && currentObjects[i])
				lines[i].createItem(currentObjects[i]);
        }
    }


    //---------------------------------------------------------------

    public void setToRandomMode()
    {
        currentMode = CreateMode.Random;
    }

    public void setToEditedMode()
    {
        currentMode = CreateMode.Edited;
    }

	public void setCurrentTrack(TrackDataFormat track)
    {
		currentTrack = track;
		trackStartGrid = gridManager.getCurrentGrid()+1;
		Debug.Log("SetCTrack: " + trackStartGrid);
    }
}

