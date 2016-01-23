using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour {

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
        float currentX = transform.position.x;

        if (other.tag == "Player")
        {
            other.SendMessage("touchLineAndStop", currentX);
            if (isLeftEnd)
                other.SendMessage("touchLeftEnd");
            else if (isRightEnd)
                other.SendMessage("touchRightEnd");
        }
    }
}
