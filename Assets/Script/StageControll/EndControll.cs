using UnityEngine;
using System.Collections;

public class EndControll : MonoBehaviour
{

    public PlayerController player;
    GridManager gridManager;
    DestroyByTime[] destrotBTs;
    UICollectionItems ui;
	MainParkour mainParkour;

    ParkourData data;

    bool isEndGame = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (!player)
            Debug.Log("EndControll.cs: Player not Found!");

        gridManager = GameObject.FindObjectOfType<GridManager>();
        if (!gridManager)
            Debug.Log("EndControll.cs: Grid Manager not Found!");

        ui = GameObject.FindObjectOfType<UICollectionItems>();
        if (!ui)
            Debug.Log("EndControll.cs: ui not Found!");

        data =  GameObject.FindObjectOfType<ParkourData>();
        if (!data)
            Debug.Log("EndControll.cs: ParkourData not Found!");

		mainParkour = GameObject.FindObjectOfType<MainParkour>();
		if (!mainParkour)
			Debug.Log("EndControll.cs: MainParkour not Found!");
    }

    public void endGame()
    {
        isEndGame = true;
        stopGame();
        ui.endgame();
    }

    public void stopGame()
    {
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

        gridManager.play();
        player.setControllable(true);

        destrotBTs = GameObject.FindObjectsOfType<DestroyByTime>();
        foreach (DestroyByTime dbt in destrotBTs)
        {
            dbt.play();
        }
    }

    public void restartGame()
    {
        player.setControllable(true);
        destrotBTs = GameObject.FindObjectsOfType<DestroyByTime>();
        foreach (DestroyByTime dbt in destrotBTs)
        {
            GameObject.Destroy(dbt.gameObject);
        }
        isEndGame = false;
        ui.restart();
        data.cleanCoins();
		mainParkour.restart();
        gridManager.initAndStart();
    }
}

