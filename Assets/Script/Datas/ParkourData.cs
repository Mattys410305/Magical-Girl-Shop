using UnityEngine;
using System.Collections;

public class ParkourData : MonoBehaviour {
    
    int recieveCoinNum = 0;
    int mileage = 0;


    void Awake () {
	    
	}


    void update()
    {
    }

    public void addCoin(int num)
    {
        recieveCoinNum += num;
    }

    public int getCoin()
    {
        return recieveCoinNum;
    }

    public void addMileage(int num)
    {
        mileage += num;
    }

    public int getMileage()
    {
        return mileage;
    }


}
