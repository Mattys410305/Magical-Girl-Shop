using UnityEngine;
using System.Collections;

public class CreatorsControll : MonoBehaviour {

    ObstacleCreator[] oCreators;
    FloorCreator[] fCreators;

    void Start () {
        oCreators = gameObject.GetComponentsInChildren<ObstacleCreator>();
        fCreators = gameObject.GetComponentsInChildren<FloorCreator>();

    }
	
	void Update () {
	
	}
    
    public void start()
    {
        foreach (ObstacleCreator oCreator in oCreators)
        {
            oCreator.SendMessage("start");
        }

        foreach (FloorCreator fCreator in fCreators)
        {
            fCreator.SendMessage("start");
        }
    }

    public void stop()
    {
        foreach (ObstacleCreator oCreator in oCreators)
        {
            oCreator.SendMessage("stop");
        }

        foreach (FloorCreator fCreator in fCreators)
        {
            fCreator.SendMessage("stop");
        }
    }
}

