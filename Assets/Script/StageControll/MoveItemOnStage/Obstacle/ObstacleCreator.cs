using UnityEngine;
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

    void Start(){
        //nextObstacle = StartTime;
    }
    
	void Update () {
	    
        /*if (Time.time > nextObstacle && !isGameStop)
        {
            int randomNum = Random.Range(0, obstacleType.Length);
            newObstacle = Instantiate(obstacleType[randomNum], transform.position, transform.rotation) as Obstacle;
            newObstacle.transform.parent = stage.transform;

            nextObstacle = Time.time + Interval;
        }*/
    }

    public void createItem(MoveItemOnStage item)
    {
        if (isGameStop)
            return;

        MoveItemOnStage tmpObject;
        tmpObject = Instantiate(item, transform.position, transform.rotation) as MoveItemOnStage;
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
