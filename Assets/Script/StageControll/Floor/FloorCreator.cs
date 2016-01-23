using UnityEngine;
using System.Collections;

public class FloorCreator : MonoBehaviour {

    public float startTime;
    public float interval;
    public GameObject stage;
    public Floor[] floorType;
    public int[] floorWeight;

    private bool isGameStop = false;

    Floor newfloor;
    float nextFloor;
    float tmpNextFloorTime;

    void Start () {
        nextFloor = startTime;
    }

	void Update () {

        if (Time.time > nextFloor && !isGameStop)
        {
            int randomNum = Random.Range(0, floorType.Length);
            newfloor = Instantiate(floorType[randomNum], transform.position, transform.rotation) as Floor;
            newfloor.transform.parent = stage.transform;

            nextFloor += interval;
        }

    }
    
    public void start()
    {
        isGameStop = false;
        tmpNextFloorTime = Time.time - nextFloor;
    }

    public void stop()
    {
        isGameStop = true;
        nextFloor = Time.time + tmpNextFloorTime;
    }
}


