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
                             Screen.width,
                             Screen.height),
                   ""
            );
        
		GUI.TextArea(new Rect( Screen.width/2 - Screen.width/2.5f/2,
								Screen.height/2 - Screen.height/48*8,
                                Screen.width/2.5f,
								Screen.height/24),
                      coinText);

		GUI.TextArea(new Rect( Screen.width/2 - Screen.width/2.5f/2,
								Screen.height/2 - Screen.height/48*5,
								Screen.width/2.5f,
								Screen.height/24),
                      mileageText);

        if( GUI.Button(new Rect(0.0f,
                                0.0f,
                                Screen.width,
								Screen.height),
                   "Click to Restart."
                                ))
        {
            restart();
        }
    }

    public void showResult()
    {
        isEnd = true;
    }

    void restart()
    {
        isEnd = false;
        EndControll endControll = GameObject.FindObjectOfType<EndControll>();
        endControll.restartGame();
    }
}

