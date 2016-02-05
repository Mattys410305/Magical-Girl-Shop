using UnityEngine;
using System.Collections;

public class GridManager : MonoBehaviour {

    public float gridLength = 2.0f;

    StageMove stageMove;
    CreatorsManager creatorsManager;
    Mileage mileage;

    int currentGrid;
    float interval;
    float nextGrid;
    bool isMoving;
    float recordTimeWhenGameStop = 0.0f;

    void Start () {
        stageMove = GameObject.FindObjectOfType<StageMove>();
        creatorsManager = GameObject.FindObjectOfType<CreatorsManager>();
        mileage = GameObject.FindObjectOfType<Mileage>();

        isMoving = false;
        currentGrid = 0;
        nextGrid = Time.time;
        interval = gridLength / stageMove.fowardSpeed;
    }
	
	void Update () {
        if (nextGrid > Time.time || isMoving == false)
            return;

        currentGrid++;
        nextGrid += interval;

        creatorsManager.Create();
        mileage.updateStopwatch();
    }

    public void updateSpeed()
    {
        interval = gridLength / stageMove.fowardSpeed;
    }

    public float getGridLength()
    {
        return gridLength;
    }

    public int getCurrentGrid()
    {
        return currentGrid;
    }

    public bool isInMovingState()
    {
        return isMoving;
    }


    public void play()
    {
        isMoving = true;
        nextGrid = Time.time + recordTimeWhenGameStop;
    }

    public void stop()
    {
        isMoving = false;
        recordTimeWhenGameStop = nextGrid - Time.time;
    }

    public void initAndStart()
    {
		Debug.Log("initAndStart");
        isMoving = true;
        nextGrid = Time.time + interval;
        currentGrid = 0;
        mileage.makeZero();
    }
}
