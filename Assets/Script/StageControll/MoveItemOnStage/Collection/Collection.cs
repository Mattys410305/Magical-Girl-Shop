using UnityEngine;
using System.Collections;

public class Collection : MonoBehaviour
{

    public enum CollectionType { coin };
    public int blockLength;
    public CollectionType type = CollectionType.coin;
    public int value = 1;

    void Start()
    {

    }

    void Update()
    {

    }

    public CollectionType getCollectionType()
    {
        return type;
    }

    public int getValue()
    {
        return value;
    }
}



