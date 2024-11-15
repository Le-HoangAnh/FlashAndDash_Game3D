using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Button raceButton;
    [SerializeField] Button garageButton;
    [SerializeField] Button shopButton;
    [SerializeField] List<CarController> carList;

    private void Awake()
    {
        foreach (CarController car in carList)
        {
            car.DisplayCar(false);
        }
        CarController activeCar = carList[GameManager.GetCurrentActiveCarIndex()];
        activeCar.DisplayCar(true, true);
    }

    private void OnEnable()
    {
        raceButton.onClick.AddListener(HandleRaceButtonClicked);
        garageButton.onClick.AddListener(HandleGarageButtonClicked);
        shopButton.onClick.AddListener(HandleShopButtonClicked);
    }

    private void OnDisable()
    {
        raceButton.onClick.RemoveListener(HandleRaceButtonClicked);
        garageButton.onClick.RemoveListener(HandleGarageButtonClicked);
        shopButton.onClick.RemoveListener(HandleShopButtonClicked);
    }

    void HandleRaceButtonClicked()
    {
        Events.okButtonClicked?.Invoke();
        NavigationManager.LoadScene(Scenes.SELECT_A_TRACK);
    }

    void HandleGarageButtonClicked()
    {
        Events.okButtonClicked?.Invoke();
        NavigationManager.LoadScene(Scenes.GARAGE);
    }

    void HandleShopButtonClicked()
    {
        Events.okButtonClicked?.Invoke();
        NavigationManager.LoadScene(Scenes.SHOP);
    }
}
