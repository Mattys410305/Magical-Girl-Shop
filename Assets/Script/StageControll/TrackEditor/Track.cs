using UnityEngine;
using System.Collections;

public class Track : MonoBehaviour {

    public GameObject trackLines;
    public int trackLength = 50;


    void Start() {

    }

    void Update() {

    }

    public void activeTrack(bool active)
    {
        trackLines.SetActive(active);
    }
}

