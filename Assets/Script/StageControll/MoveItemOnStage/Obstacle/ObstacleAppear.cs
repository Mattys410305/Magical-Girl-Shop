using UnityEngine;
using System.Collections;

public class ObstacleAppear : MonoBehaviour {

    public enum ObstacleAppearType { non, floatUp };
    public ObstacleAppearType appearType = ObstacleAppearType.non;
    public float speed = 6.0f;
    public float appearTime = 0.5f;

    float stopTime;

	void Start () {
        if (appearType == ObstacleAppearType.floatUp)
        {
            initFloatUp();
        }
    }
	
	void Update () {
	    if(appearType == ObstacleAppearType.floatUp)
        {
            floatUp();
        }
	}

    void initFloatUp()
    {
        transform.position = transform.TransformPoint(Vector3.down * (speed * appearTime));
        stopTime = Time.time + appearTime;
    }

    void floatUp()
    {
        if (Time.time < stopTime)
        {
            transform.position = transform.TransformPoint(Vector3.up * Time.deltaTime * speed);
        }
        else
        {
            transform.Rotate(Vector3.forward, speed);
        }
    }
}

