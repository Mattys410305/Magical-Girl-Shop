using UnityEngine;
using UnityEditor;
using System.Collections;

public class PrintTestMenu : Editor
{
    [MenuItem("PrintTest/Track")]
    public static void printTrack()
    {
        Track tmp = GameObject.FindObjectOfType<Track>();
        tmp.printStatus();
    }
}
