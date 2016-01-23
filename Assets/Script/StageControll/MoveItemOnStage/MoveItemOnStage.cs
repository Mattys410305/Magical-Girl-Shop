using UnityEngine;
using System.Collections;

public class MoveItemOnStage : MonoBehaviour {

    public enum ItemType { coin, obstacle };
    public int blockLength;
    public ItemType type = ItemType.coin;
    
    void Start () {
	
	}
	
	void Update () {

    }

    public ItemType getItemType()
    {
        return type;
    }
}
