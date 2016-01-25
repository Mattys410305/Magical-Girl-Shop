using UnityEngine;
using System.Collections;

public class MovableItemOnStage : MonoBehaviour {

    public enum ItemType { coin, obstacle };
    public int blockLength;
    public ItemType type = ItemType.coin;

    int posLineNo;
    int posIndex;

    void Start () {
	
	}
	
	void Update () {

    }

    public ItemType getItemType()
    {
        return type;
    }

    public int getLineNo()
    {
        return posLineNo;
    }

    public int getIndex()
    {
        return posIndex;
    }

    public void setPos(int lineNo, int depth)
    {
        posLineNo = lineNo;
        posIndex = depth;
    }
}
