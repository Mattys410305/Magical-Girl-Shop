﻿using UnityEngine;
using System.Collections;

public class ObstacleCreator : MonoBehaviour {

    public GameObject stage;

    public float StartTime = 1.0f;
    public float Interval = 5.0f;
    public Obstacle[] obstacleType;
    public int[] obstacleWeight;

    private bool isGameStop = false;

    float nextObstacle;
    float tmpNextObstacleTime;
    Obstacle newObstacle;
    
    public void createItem(MovableItemOnStage item)
    {
        if (isGameStop)
            return;

        MovableItemOnStage tmpObject;
        tmpObject = Instantiate(item, transform.position, transform.rotation) as MovableItemOnStage;
        tmpObject.transform.parent = stage.transform;
    }

    public void start()
    {
        isGameStop = false;
        tmpNextObstacleTime = Time.time - nextObstacle;
    }

    public void stop()
    {
        isGameStop = true;
        nextObstacle = Time.time + tmpNextObstacleTime;
    }
}
