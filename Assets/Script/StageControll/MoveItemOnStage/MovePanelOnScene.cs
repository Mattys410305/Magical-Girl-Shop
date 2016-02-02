using UnityEngine;
using System.Collections;

public class MovePanelOnScene : MonoBehaviour {
    
    public MovableItemOnStage getMovableItem()
    {
        GameObject parent = transform.parent.gameObject;

        if (parent)
            return parent.GetComponent<MovableItemOnStage>();
        else
            return null;
    }
}
