using UnityEngine;
using System.Collections;

public class EndControll : MonoBehaviour {

    public PlayerController player;

    /*StageMove stageMove;
    CreatorsManager creatorsManager;*/
    //Mileage mileage;
    GridManager gridManager;
    DestroyByTime[] destrotBTs;
    UICollectionItems ui;

    bool isEndGame = false;
    
    void Start () {
        /*creatorsManager = gameObject.GetComponentInChildren<CreatorsManager>();
        if (!creatorsManager)
            Debug.Log("CreatorsManager not Found, please set it as stageControll's child!");

        stageMove = gameObject.GetComponentInChildren<StageMove>();
        if (!stageMove)
            Debug.Log("StageMove not Found!, please set it as stageControll's child!");*/

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (!player)
            Debug.Log("EndControll.cs: Player not Found!");

        gridManager = GameObject.FindObjectOfType<GridManager>();
        if (!gridManager)
            Debug.Log("EndControll.cs: Grid Manager not Found!"); 

        /*mileage = GameObject.FindObjectOfType<Mileage>();
        if (!mileage)
            Debug.Log("Mileage Data not Found!");*/

        ui = GameObject.FindObjectOfType<UICollectionItems>();
        if (!ui)
            Debug.Log("EndControll.cs: ui not Found!");

    }

    public void endGame()
    {
        isEndGame = true;
        stopGame();
        ui.endgame();
    }

    public void stopGame()
    {
        //stageMove.stop();
        //creatorsManager.stop();
        //mileage.stop();
        gridManager.stop();
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

        //stageMove.play();
        //creatorsManager.play();
        //mileage.play();
        gridManager.play();
        player.setControllable(true);
        
        destrotBTs = GameObject.FindObjectsOfType<DestroyByTime>();
        foreach (DestroyByTime dbt in destrotBTs)
        {
            dbt.play();
        }
    }
}
