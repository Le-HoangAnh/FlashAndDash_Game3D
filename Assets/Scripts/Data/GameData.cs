using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObject", order = 1)]
public class GameData : ScriptableObject
{
    public string username;
    public float playTime;
    public float money;
    public long lastPlayed;
    public List<bool> unlockedCars;
    public int currentActiveCar;
}
