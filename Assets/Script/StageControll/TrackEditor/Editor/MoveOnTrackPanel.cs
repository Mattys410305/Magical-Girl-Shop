using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MovePanelOnScene))]
public class MoveOnTrackPanel : Editor
{
    Track track;
    MovePanelOnScene moveTarget;
    MovableItemOnStage mItem;

    Rect infoPanelRect = new Rect(5.0f, 5.0f, 340.0f, 70.0f);

    Track.LineNo originLineNo;
    int originIndex;

    void OnSceneGUI()
    {
        initData();
        setPanel();
    }

    void initData()
    {
        track = GameObject.FindObjectOfType<Track>();
        moveTarget = target as MovePanelOnScene;
        mItem = moveTarget.getMovableItem();

        originLineNo = mItem.getLineNoTrackEnum();
        originIndex = mItem.getIndex();
    }

    void setPanel()
    {
        Track.LineNo lineNo;
        int index;
        
        Handles.BeginGUI();
        {
            GUILayout.BeginArea(infoPanelRect, "", "Window");
            {
                EditorGUILayout.BeginHorizontal(EditorStyles.numberField);
                EditorGUILayout.LabelField("Line");
                lineNo = (Track.LineNo)EditorGUILayout.EnumPopup(mItem.getLineNoTrackEnum());
                EditorGUILayout.EndHorizontal();
                
                index = EditorGUILayout.IntSlider("Index", mItem.getIndex(), 0, track.trackLength - 1);
            }
            GUILayout.EndArea();
        }
        Handles.EndGUI();

        if(lineNo != originLineNo || index != originIndex)
            setPanelValueToItem(lineNo, index);
    }

    void setPanelValueToItem(Track.LineNo lineNo, int index)
    {
        if (track.checkMoveByPanel((int)lineNo, index))
        {
            track.moveItemTo((int)originLineNo, originIndex, (int)lineNo, index);
        }
        else
        {
            Debug.Log("setPanelValueToItem -> checkMoveByPanel() fail.");
            lineNo = originLineNo;
            index = originIndex;
        }
    }
}
