using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameData))]
public class GameDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GameData gameDataScripts = (GameData)target;

        if (GUILayout.Button("Reset"))
        {
            gameDataScripts.username = "";
            gameDataScripts.playTime = 0;
            gameDataScripts.money = 0;
            gameDataScripts.lastPlayed = 0;
            gameDataScripts.currentActiveCar = 0;
            gameDataScripts.unlockedCars = new List<bool>();
        }
    }
}
