using UnityEngine;
using System.Collections;

public class UICollectionItems : MonoBehaviour {
    
    public GUISkin gSkinParkour;
    public float endDelay = 1.2f;

    ParkourData data;
    ResultDialog resultDialog;

    float endTime = -1.0f;
    bool isEnd = false;

    void Start ()
    {
        data = GameObject.FindObjectOfType<ParkourData>();
        if (!data)
            Debug.Log("ParkourData not Found!");

        resultDialog = GameObject.FindObjectOfType<ResultDialog>();
        if (!resultDialog)
            Debug.Log("ResultDialog not Found!");
    }
	
	void Update () {
        if (endTime < 0.0f)
            return;

        if(Time.time > endTime)
        {
            isEnd = true;
            resultDialog.showResult();
        }
	}

    void OnGUI()
    {
        if (isEnd)
            return;

        GUI.skin = gSkinParkour;

        int recieveCoins = data.getCoin();
        string coinText = "Coins:  " + recieveCoins.ToString();
        int mileage = data.getMileage();
        string mileageText = "Mileage:  " + mileage.ToString() + "m";

        GUI.TextArea(new Rect(Screen.width - 90.0f,
                                0,
                                90.0f,
                                25.0f),
                      coinText);

        GUI.TextArea(new Rect(  0.0f,
                                0.0f,
                                140.0f,
                                25.0f),
                      mileageText);
    }

    public void endgame()
    {
        endTime = Time.time + endDelay;
    }
}
