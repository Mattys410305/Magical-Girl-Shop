using UnityEngine;
using UnityEditor;
using System.Collections;

public class TrackWindow : EditorWindow {

    TrackDataFormat trackData;
    const string trackPath = "Assets/Resources/Track/";
    const string rawBlockPath = "Assets/StageControll/Block/Track.prefab";
    const string itemPath = "Assets/StageControll/Obstacles/";

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
            setShowObstacleSize(true);
        }
        else
        {
            isShowObstacleSize = true;
            setShowObstacleSize(false);
        }
    }

    void itemButtons()
    {
        EditorGUILayout.BeginHorizontal(EditorStyles.textField);
        if (GUILayout.Button("Add Selection Item"))
        {
            addSelectionItem();
        }
        EditorGUILayout.EndHorizontal();
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
        trackPrefab = AssetDatabase.LoadAssetAtPath(rawBlockPath, typeof(GameObject)) as GameObject;
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
        Track track;
        track = currentTrack.GetComponent<Track>() as Track;
        int[,] lines = track.getTrackLines();
        GameObject[,] lineItems = track.getTrackItems();

        trackData = ScriptableObject.CreateInstance<TrackDataFormat>();
        trackData.initLength(newTrackLength);
        trackData.setLines(lines, lineItems);
        AssetDatabase.CreateAsset(trackData, trackPath + currentTrack.name + ".asset");
    }


    void loadTrack()
    {
        Object selectAsset;
        if (checkLoadStatue() == false)
            return;
        
        selectAsset = Selection.objects[0];
        
        trackData = AssetDatabase.LoadAssetAtPath<TrackDataFormat>(trackPath + selectAsset.name + ".asset");
		newTrackName = trackData.name;
        newTrack();
        setAssetOnTrack();
    }

    void setAssetOnTrack()
    {
        Track track = currentTrack.GetComponent<Track>() as Track;
        int[,] lines = trackData.getLineNo();
        string[,] lineItemNames = trackData.getLineItemNames();
        GameObject itemType, loadedItem;

        for(int lineNo = 4; lineNo >= 0; lineNo--)
        {
            for (int index = trackData.trackLength - 1; index >= 0; index--)
            {
                if (lines[lineNo, index] != 0)
                {
                    itemType = AssetDatabase.LoadAssetAtPath<GameObject>(itemPath + lineItemNames[lineNo, index] + ".prefab");
                    Debug.Log(itemPath + lineItemNames[lineNo, index] + ": " + itemType);
                    loadedItem = Instantiate(itemType, currentTrack.transform.position, currentTrack.transform.rotation) as GameObject;

                    track.addNewItem(loadedItem);

                    MovableItemOnStage mItem = loadedItem.GetComponent<MovableItemOnStage>();

                    track.moveItemTo(mItem.getLineNo(), mItem.getIndex(), lineNo, index);
                }
            }
        }
    }

    bool checkLoadStatue()
    {
        if (currentTrack != null)
        {
            Debug.Log("請先編輯好上一個跑道, 完成按EndEdit後再Load.");
            return false;
        }

        if (Selection.objects.Length == 0)
        {
            Debug.Log("請點選跑道的asset再Load (Assets/StageControll/Track/檔名.asset).");
            Debug.Log(Selection.objects.Length);
            return false;
        }

        return true;
    }


    void endTrack()
    {
        GameObject.DestroyImmediate(currentTrack);
        trackData = null;
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

    void setShowObstacleSize(bool isShow)
    {
        NeedSpaceOnScene[] needSpaceOnScenes = GameObject.FindObjectsOfType<NeedSpaceOnScene>();
        foreach(NeedSpaceOnScene nsos in needSpaceOnScenes)
        {
            nsos.showGizmos = isShow;
        }
    }

}
