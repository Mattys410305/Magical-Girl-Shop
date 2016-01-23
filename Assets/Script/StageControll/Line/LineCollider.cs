using UnityEngine;
using System.Collections;

public class LineCollider : MonoBehaviour {

    public bool isLeftBoundary;
    public bool isRightBoundary;
    public bool isLeftEnd;
    public bool isRightEnd;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (isLeftBoundary)
            {
                other.SendMessage("touchLeftBoundaryAndStop");
                if(isLeftEnd)
                    other.SendMessage("touchLeftEnd");
            }
            else if(isRightBoundary)
            {
                other.SendMessage("touchRightBoundaryAndStop");
                if (isRightEnd)
                    other.SendMessage("touchRightEnd");
            }
        }
    }
}
