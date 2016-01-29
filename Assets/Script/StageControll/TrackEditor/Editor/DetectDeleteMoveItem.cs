using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(DeleteInEditor))]
public class DetectDeleteMoveItem : Editor {
    
    GameObject parent;

    void OnEnable()
    {
        parent = (target as DeleteInEditor).gameObject.transform.parent.gameObject;
    }

    void OnDestroy()
    {
        if (Application.isEditor)
        {
            if (((DeleteInEditor)target) == null && parent)
            {
                Track track = GameObject.FindObjectOfType<Track>();
                //GameObject parent = (target as DeleteInEditor).gameObject.transform.parent.gameObject;
                MovableItemOnStage mItem = parent.GetComponent<MovableItemOnStage>();

                int posLineNo = mItem.getLineNo();
                int posIndex = mItem.getIndex();

                Debug.Log("OnDestroy: " + posLineNo + ", " + posIndex);

                if (track)
                {
                    track.deleteItem(posLineNo, posIndex);
                    GameObject.DestroyImmediate(parent);
                }
            }

        }
    }
}
