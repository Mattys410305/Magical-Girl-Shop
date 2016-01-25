using UnityEngine;
using UnityEditor;
using System.Collections;

public class TrackWindow : EditorWindow {

    const string rawTrackPath = "Assets/StageControll/Block/Track.prefab";

    GameObject trackPrefab;
    GameObject currentTrack;

    string newTrackName = "newTrackName";
    int newTrackLength = 50;

    bool isShowTrack = true;
    bool isShowCurve = false;
    bool isShowObstacleSize = true;

    [MenuItem("Window/ParkourTrack")]
    public static void Open()
    {
        EditorWindow.GetWindow<TrackWindow>();
    }
    
    void OnDestroy()
    {
        if(currentTrack)
            GameObject.DestroyImmediate(currentTrack);
    }

    //------------------------UI----------------------------

    void OnGUI()
    {
        fileButtons();
        attributesText();
        stateCheckBoxes();
        itemButtons();
    }

    void attributesText()
    {
        trackNameField();
        trackLengthField();
    }

    void fileButtons()
    {
        EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
        if (GUILayout.Button("New"))
        {
            newTrack();
        }
        if (GUILayout.Button("Save"))
        {
            saveTrack();
        }
        if (GUILayout.Button("Load"))
        {
            loadTrack();
        }
        if (GUILayout.Button("EndEdit"))
        {
            endTrack();
        }
        EditorGUILayout.EndHorizontal();
    }

    void trackNameField()
    {

        EditorGUILayout.BeginHorizontal(EditorStyles.textField);

        GUILayout.Label("Name");
        if(currentTrack != null)
            currentTrack.gameObject.name = EditorGUILayout.TextField(currentTrack.name);
        else
            newTrackName = EditorGUILayout.TextField(newTrackName);

        EditorGUILayout.EndHorizontal();
    }

    void trackLengthField()
    {
        EditorGUILayout.BeginHorizontal(EditorStyles.textField);

        GUILayout.Label("Length");
        if (currentTrack != null)
            currentTrack.GetComponent<Track>().trackLength = EditorGUILayout.IntField(currentTrack.GetComponent<Track>().trackLength);
        else
            newTrackLength = EditorGUILayout.IntField(newTrackLength);
        
        EditorGUILayout.EndHorizontal();
    }

    void stateCheckBoxes()
    {
        if (isShowTrack = GUILayout.Toggle(isShowTrack, "Show track"))
        {
            isShowTrack = false;
            showTrack(!isShowTrack);
        }
        else
        {
            isShowTrack = true;
            showTrack(!isShowTrack);
        }

        if (isShowCurve = GUILayout.Toggle(isShowCurve, "Show curve"))
        {
            isShowCurve = false;
        }
        else
        {
            isShowCurve = true;
        }

        if (isShowObstacleSize = GUILayout.Toggle(isShowObstacleSize, "Show obstacle size"))
        {
            isShowObstacleSize = false;
        }
        else
        {
            isShowObstacleSize = true;
        }
    }

    void itemButtons()
    {
        if (GUILayout.Button("Add Selection Item"))
        {
            addSelectionItem();
        }
    }

    //-------------------Logic-------------------------------

    void newTrack()
    {
        Track track;
        if (currentTrack)
        {
            Debug.Log("上一個跑道尚未完成，請先EndEdit.");
            return;
        }
        trackPrefab = AssetDatabase.LoadAssetAtPath(rawTrackPath, typeof(GameObject)) as GameObject;
        currentTrack = Instantiate(trackPrefab);
        currentTrack.name = newTrackName;

        showTrack(isShowTrack);

        track = currentTrack.GetComponent<Track>() as Track;
        track.init();
    }

    void showTrack(bool show)
    {
        if (currentTrack == null)
            return;

        Track componentTrack;
        componentTrack = currentTrack.GetComponent<Track>();
        componentTrack.activeTrack(show);
    }

    void saveTrack()
    {

    }


    void loadTrack()
    {

    }


    void endTrack()
    {
        GameObject.DestroyImmediate(currentTrack);
    }

    void addSelectionItem()
    {
        
        GameObject selectOb;
        GameObject newItem;
        Track track;

        if (Selection.gameObjects.Length > 0)
            selectOb = Selection.gameObjects[0];
        else
            return;

        if (checkType(selectOb) && currentTrack)
        {
            newItem = Instantiate(selectOb, currentTrack.transform.position, currentTrack.transform.rotation) as GameObject;

            track = currentTrack.GetComponent<Track>() as Track;
            track.addNewItem(newItem);
        }
    }

    bool checkType(GameObject ob)
    {
        return ob.GetComponent<MovableItemOnStage>() != null;
    }

}
