using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {

    public float lifeTime = 20.0f;
    float destoryTime;
    float remainTime;

    private bool isGameStop;

    void Start()
    {
        isGameStop = false;
        destoryTime = Time.time + lifeTime;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > destoryTime && !isGameStop)
        {
            Destroy(gameObject);
        }
    }
    
    public void stop()
    {
        isGameStop = true;
        remainTime = Time.time - destoryTime;
    }

    public void play()
    {
        isGameStop = false;
        destoryTime = Time.time + remainTime;
    }
}

