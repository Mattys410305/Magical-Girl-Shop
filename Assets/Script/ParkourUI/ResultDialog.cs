using UnityEngine;
using System.Collections;

public class ResultDialog : MonoBehaviour
{
    public GUISkin gSkinParkour;

    ParkourData data;

    bool isEnd = false;

    void Start()
    {
        data = GameObject.FindObjectOfType<ParkourData>();
        if (!data)
            Debug.Log("ParkourData not Found!");
    }

    void Update()
    {

    }

    void OnGUI()
    {
        if (!isEnd)
            return;

        GUI.skin = gSkinParkour;

        int recieveCoins = data.getCoin();
        string coinText = "Coins:  " + recieveCoins.ToString();
        int mileage = data.getMileage();
        string mileageText = "Mileage:  " + mileage.ToString() + "m";

        GUI.Label(new Rect(0.0f,
                             0.0f,
                             Screen.width - 0.0f,
                             Screen.height - 0.0f),
                   ""
            );
        
        GUI.TextArea(new Rect( 90.0f,
                                200.0f,
                                Screen.width - 180.0f,
                                25.0f),
                      coinText);

        GUI.TextArea(new Rect( 90.0f,
                                230.0f,
                                Screen.width - 180.0f,
                                25.0f),
                      mileageText);

        GUI.Button(new Rect(90.0f,
                                Screen.height - 120.0f,
                                Screen.width - 180.0f,
                                25.0f),
                   "Button"
                                );
    }

    public void showResult()
    {
        isEnd = true;
    }
}

