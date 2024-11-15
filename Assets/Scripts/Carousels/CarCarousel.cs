using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarCarousel : MonoBehaviour, ICarousel
{
    [SerializeField] List<CarController> cars;
    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;
    [SerializeField] bool saveActiveCar;

    public Action<int> carChanged;
    int index;
    bool goingRight;

    private void Start()
    {
        leftButton.onClick.AddListener(ShowPreviousItem);
        rightButton.onClick.AddListener(ShowNextItem);
    }

    private void OnDestroy()
    {
        leftButton.onClick.RemoveListener(ShowPreviousItem);
        rightButton.onClick.RemoveListener(ShowNextItem);
    }

    public void ShowNextItem()
    {
        if (AreAllCarsNull())
            return;
        Events.leftOrRightButtonClicked?.Invoke();
        goingRight = true;
        index = index < cars.Count - 1 ? index + 1 : 0;
        ShowCarAtItem(index);

        if (saveActiveCar)
        {
            GameManager.SetActiveCar(index);
        }
    }

    public void ShowPreviousItem()
    {
        if (AreAllCarsNull())
            return;
        Events.leftOrRightButtonClicked?.Invoke();
        goingRight = false;
        index = index > 0 ? index - 1 : cars.Count - 1;
        ShowCarAtItem(index);

        if (saveActiveCar)
        {
            GameManager.SetActiveCar(index);
        }
    }

    public void ShowCarAtItem(int index, bool applyCustomizations = false)
    {
        HideCars();

        if (cars[index] != null)
        {
            cars[index].DisplayCar(true, applyCustomizations);
        }
        else
        {
            if (goingRight == true)
            {
                ShowNextItem();
            }
            else
            {
                ShowPreviousItem();
            }
        }

        carChanged?.Invoke(index);
    }

    void HideCars()
    {
        foreach (CarController car in cars)
        {
            if (car != null)
            {
                car.DisplayCar(false);
            }
        }
    }

    public int GetCurrentItemIndex()
    {
        return index;
    }

    public CarController GetCurrentCar()
    {
        if (AreAllCarsNull())
        {
            return null;
        }
        else if (cars[index] != null)
        {
            return cars[index];
        }
        else
        {
            index = index < cars.Count - 1 ? index + 1 : 0;
            return GetCurrentCar();
        }
    }

    public void RemoveCarAtIndex(int index)
    {
        cars[index] = null;
    }

    public bool AreAllCarsNull()
    {
        foreach (CarController car in cars)
        {
            if (car != null)
            {
                return false;
            }
        }
        return true;
    }
}
