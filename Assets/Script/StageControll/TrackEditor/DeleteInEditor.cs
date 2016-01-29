using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DeleteInEditor : MonoBehaviour {
    
    void OnDestroy()
    {
        if (Application.isEditor)
        {
            /*Track track = GameObject.FindObjectOfType<Track>();
            GameObject parent = gameObject.transform.parent.gameObject;
            MovableItemOnStage mItem = parent.GetComponent<MovableItemOnStage>();

            int posLineNo = mItem.getLineNo();
            int posIndex = mItem.getIndex();

            if (parent && track)
            {
                track.deleteItem(posLineNo, posIndex);
                Destroy(parent);
            }*/
        }
    }
}
