﻿using UnityEngine;
using System.Collections;

public class Mileage : MonoBehaviour {

    
    public int AddMile = 10; 
    /*public float BaseInterval = 5.0f;

    bool isMove = true;
    float interval = 0.5f;
    float nextMile = 0.0f;
    float stopTime = 0.0f;

    StageMove stageMove;*/
    ParkourData data;
    
    void Start()
    {
        data = GameObject.FindObjectOfType<ParkourData>();
        if(!data)
        {
            Debug.Log("ParkourData not Found!");
        }
        /*stageMove = GameObject.FindObjectOfType<StageMove>();
        if (!stageMove)
        {
            Debug.Log("StageMove not Found!");
        }

        interval = BaseInterval / stageMove.getFowardSpeed();
        nextMile = Time.time + interval;*/
    }

	/*void Update ()
    {
	    if( Time.time > nextMile && isMove)
        {
            nextMile += interval;
            
        }
	}*/

    public void updateStopwatch()
    {
        data.addMileage(AddMile);
    }

    /*public void updateSpeed()
    {
        interval = BaseInterval / stageMove.getFowardSpeed();
    }

    public void play()
    {
        isMove = true;
        nextMile += (Time.time - stopTime);
    }

    public void stop()
    {
        isMove = false;
        stopTime = Time.time;
    }*/
}




