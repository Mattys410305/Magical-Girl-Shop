using UnityEngine;
using System.Collections;

public class EndControll : MonoBehaviour {

    public PlayerController player;

    StageMove stage;
    CreatorsControll creatorsControll;
    DestroyByTime[] destrotBTs;
    Mileage mileage;
    UICollectionItems ui;

    bool isEndGame = false;

    // Use this for initialization
    void Start () {
        creatorsControll = gameObject.GetComponentInChildren<CreatorsControll>();
        if (!creatorsControll)
            Debug.Log("CreatorsControll not Found, please set it as stageControll's child!");

        stage = gameObject.GetComponentInChildren<StageMove>();
        if (!stage)
            Debug.Log("StageMove not Found!, please set it as stageControll's child!");

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (!player)
            Debug.Log("Player not Found!");

        mileage = GameObject.FindObjectOfType<Mileage>();
        if (!mileage)
            Debug.Log("Mileage Data not Found!");

        ui = GameObject.FindObjectOfType<UICollectionItems>();
        if (!ui)
            Debug.Log("ui not Found!");

    }
	
	void Update () {
	    
	}

    public void endGame()
    {
        isEndGame = true;
        stopGame();
        ui.endgame();
    }

    public void stopGame()
    {
        stage.stop();
        creatorsControll.stop();
        mileage.stop();
        player.setControllable(false);

        destrotBTs = GameObject.FindObjectsOfType<DestroyByTime>();
        foreach (DestroyByTime dbt in destrotBTs)
        {
            dbt.stop();
        }
    }

    public void startGame()
    {
        if (isEndGame)
            return;
        
        stage.start();
        creatorsControll.start();
        mileage.start();
        player.setControllable(true);
        
        destrotBTs = GameObject.FindObjectsOfType<DestroyByTime>();
        foreach (DestroyByTime dbt in destrotBTs)
        {
            dbt.start();
        }
    }
}
