using UnityEngine;
using System.Collections;

public class MainParkour : MonoBehaviour {
	
	public string[] tracks;

	GridManager gridManager;
	CreatorsManager creatorsManager;
	TrackDataFormat track;
	int totalLength;
	int currentLength;

	CreatorsManager.CreateMode createMode = CreatorsManager.CreateMode.Edited;

	const string trackPath = "Track/";

	int currentTrackNo;
	bool init;

	void Start () {
		currentTrackNo = 0;
		totalLength = 0;

		gridManager = GameObject.FindObjectOfType<GridManager>();
		if(!gridManager)
			Debug.Log("MainParkour: GridManager not Found.");
			
		creatorsManager = GameObject.FindObjectOfType<CreatorsManager>();
		init = false;
	}
	
	void Update () {
		if((totalLength - 1) > gridManager.getCurrentGrid() && init == true)
			return;

		if(createMode == CreatorsManager.CreateMode.Edited)
		{
			loadNewTrack();
			creatorsManager.setToEditedMode();
			creatorsManager.setCurrentTrack(track);
		}
		else if(createMode == CreatorsManager.CreateMode.Random)
		{
			creatorsManager.setToRandomMode();
		}

		if(init == false)
		{
			init = true;
			gridManager.initAndStart();
		}
	}

	void loadNewTrack()
	{
		string trackName = tracks[currentTrackNo];

		track = Resources.Load<TrackDataFormat>(trackPath + trackName);
		if(!track)
		{
			Debug.Log("MainParkour: Can't load resource: " + trackPath + tracks);
			return;
		}
		currentLength = track.trackLength;
		totalLength += currentLength;

		currentTrackNo++;
		if(currentTrackNo >= tracks.Length)
			currentTrackNo = 0;
	}

	public void restart()
	{
		currentLength = 0;
		totalLength = 0;
	}
}
