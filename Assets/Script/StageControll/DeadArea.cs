using UnityEngine;
using System.Collections;

public class DeadArea : MonoBehaviour {

    EndControll endControll;
    GameObject stageController;


    void Start () {
        stageController = GameObject.FindGameObjectWithTag("StageController");
        endControll = stageController.GetComponent<EndControll>();
        if (!endControll)
            Debug.Log("endController not Found!");

    }
	
	void Update () {
	    
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            endControll.SendMessage("endGame");
        }
    }
}
