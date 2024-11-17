using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.Playables;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] List<GameData> saveSlots;
    [SerializeField] Material defaultMaterial;
    [SerializeField] AudioManager audioManager;
    public static GameData gameData;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Events.usernameSubmitted += HandleUsernameSubmitted;
        Events.carChosen += HandleCarChosen;
        Events.backButtonPressed += PlayBackButtonAudio;
        Events.okButtonClicked += PlayOKButtonAudio;
        Events.cancelButtonClicked += PlayCancelButtonAudio;
        Events.saveSlotClicked += PlaySaveSlotSound;
        Events.leftOrRightButtonClicked += PlayRightAndLeftButtonAudio;
        Events.purchaseButtonClicked += PlayPurchaseAudio;
    }

    private void OnDestroy()
    {
        if (gameData != null)
        {
            gameData.lastPlayed = DateTime.Now.Ticks;
            SaveScriptableObject(gameData);
        }

        Events.usernameSubmitted -= HandleUsernameSubmitted;
        Events.carChosen -= HandleCarChosen;
        Events.backButtonPressed -= PlayBackButtonAudio;
        Events.okButtonClicked -= PlayOKButtonAudio;
        Events.cancelButtonClicked -= PlayCancelButtonAudio;
        Events.saveSlotClicked -= PlaySaveSlotSound;
        Events.leftOrRightButtonClicked -= PlayRightAndLeftButtonAudio;
        Events.purchaseButtonClicked -= PlayPurchaseAudio;
    }

    private void Update()
    {
        if (gameData != null)
        {
            gameData.playTime += Time.deltaTime;
        }
    }

    public static int GetCurrentActiveCarIndex()
    {
        return gameData.currentActiveCar;
    }

    public static List<bool> GetCarStates()
    {
        return gameData.unlockedCars;
    }

    public static float GetMoneyAsFloat()
    {
        return gameData.money;
    }

    public static string GetMoneyAsString()
    {
        if (gameData == null)
        {
            return "";
        }

        return gameData.money.ToString("C");
    }

    public static void UnlockCar(int index)
    {
        gameData.unlockedCars[index] = true;
        SaveScriptableObject(gameData);
    }

    public static void PurchaseCar(float price, int index)
    {
        UnlockCar(index);
        gameData.money -= price;
        SaveScriptableObject(gameData);
    }

    public static void SetActiveCar(int index)
    {
        gameData.currentActiveCar = index;
        SaveScriptableObject(gameData);
    }

    public static string GetUsername()
    {
        return gameData.username;
    }

    public static float GetPlayTime()
    {
        return gameData.playTime;
    } 

    private void HandleUsernameSubmitted(string username)
    {
        ResetGameData();
        gameData.username = username;
        SaveScriptableObject(gameData);
    }

    private void HandleCarChosen(int car)
    {
        SaveScriptableObject(gameData);
    }

    private void ResetGameData()
    {
        gameData.username = "";
        gameData.playTime = 0f;
        gameData.money = 100000f;
        gameData.lastPlayed = DateTime.Now.Ticks;
        gameData.unlockedCars = new List<bool>();
        gameData.currentActiveCar = 0;
        SaveScriptableObject(gameData);
    }

    public void PlaySaveSlotSound()
    {
        audioManager.PlaySaveSlotSound();
    }

    public void PlayBackButtonAudio()
    {
        audioManager.PlayBackButtonSound();
    }

    public void PlayOKButtonAudio()
    {
        audioManager.PlayOKButtonSound();
    }

    public void PlayChaChingButtonAudio()
    {
        audioManager.PlayChaChingButtonSound();
    }

    public void PlayRightAndLeftButtonAudio()
    {
        audioManager.PlayRightAndLeftButtonSound();
    }

    public void PlayCancelButtonAudio()
    {
        audioManager.PlayCancelButtonSound();
    }

    public void PlayPurchaseAudio()
    {
        audioManager.PlayChaChingButtonSound();
    }

    private static void SaveScriptableObject(GameData gameData)
    {
        EditorUtility.SetDirty(gameData);
        AssetDatabase.SaveAssets();
    }

}
