﻿using UnityEngine;
using System.Collections;

public class NeedSpaceOnScene : MonoBehaviour {

    public bool showGizmos = false;

    void OnDrawGizmosSelected()
    {
        if (!showGizmos)
            return;

        GridManager gm = GameObject.FindObjectOfType<GridManager>();
        int needBlocks = getNeedBlocks();

        Transform parent = transform.parent;
        Vector3 offset = new Vector3(0, 0, needBlocks * gm.gridLength / 2);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(parent.TransformPoint(offset), new Vector3(2.0f, 2.0f, needBlocks * gm.gridLength));
    }

    public int getNeedBlocks()
    {
        GameObject parent = transform.parent.gameObject;
        if (!parent)
            return 0;

        MovableItemOnStage mItem = parent.GetComponent<MovableItemOnStage>();
        if (!mItem)
            return 0;

        return mItem.needBlocks;
    }
}
