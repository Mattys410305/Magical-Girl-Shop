using UnityEngine;
using System.Collections;

public class DestroyByEnter : MonoBehaviour
{

    void Start()
    {
    }

    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Destroy: " + other.tag);
        if (other.tag == "DynamicObstacle" || other.tag == "Floor")
        {
            Destroy(other.gameObject);
        }
    }
}
