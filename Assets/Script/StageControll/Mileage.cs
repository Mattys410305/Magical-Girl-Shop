using UnityEngine;
using System.Collections;

public class Mileage : MonoBehaviour {
    
    public int AddMile = 10; 
    ParkourData data;
    
    void Start()
    {
        data = GameObject.FindObjectOfType<ParkourData>();
        if(!data)
        {
            Debug.Log("ParkourData not Found!");
        }
    }
    
    public void updateStopwatch()
    {
        data.addMileage(AddMile);
    }

    public void makeZero()
    {
        data.makeZeroMileage();
    }
}




