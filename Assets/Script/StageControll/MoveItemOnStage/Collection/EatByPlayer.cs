using UnityEngine;
using System.Collections;

public class EatByPlayer : MonoBehaviour {
    Collection item;
    ParkourData data;

    void Start () {
        item = transform.parent.GetComponent<Collection>();
        data = GameObject.FindObjectOfType<ParkourData>();
    }
	
	void Update () {
	
	}
    
    void OnTriggerEnter(Collider other)
    {
        if (!item)
        {
            Debug.Log("no collection!");
            return;
        }

        if (other.tag == "Player")
        {
            data.addCoin(item.getValue());
            other.SendMessage("eatItem", item.getCollectionType());
            Destroy(transform.parent.gameObject);
        }
    }
}

