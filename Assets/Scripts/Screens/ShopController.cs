using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI carNameText;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] Button buyButton;
    [SerializeField] CarCarousel carCarousel;
    [SerializeField] GameObject noCarsAvailableMessage;
    [SerializeField] TwoButtonsModal confirmPurchaseModal;
    [SerializeField] TwoButtonsModal congratulationsModal;
    [SerializeField] OneButtonModal notEnoughMoneyModal;

    private void OnEnable()
    {
        SetupCarsForSaleList();
        DisplayShopContent();
        buyButton.onClick.AddListener(HandleBuyButtonPressed);
        confirmPurchaseModal.Init(HandleConfirmPurchaseModalConfirm, HideConfirmPurchaseModal);
        congratulationsModal.Init(HandleMainMenuButtonClicked, HandleGarageButtonClicked);
        notEnoughMoneyModal.Init(HandleNotEnoughMoneyModalCloseButtonClicked);
        carCarousel.carChanged += UpdateScreen;
    }

    private void OnDisable()
    {
        buyButton.onClick.RemoveListener(HandleBuyButtonPressed);
        carCarousel.carChanged -= UpdateScreen;
    }

    void SetupCarsForSaleList()
    {
        List<bool> carStates = GameManager.GetCarStates();
        for (int i = 0; i < carStates.Count; i++)
        {
            bool doesPlayerHaveCar = carStates[i];
            if (doesPlayerHaveCar)
            {
                carCarousel.RemoveCarAtIndex(i);
            }
        }
    }

    void DisplayShopContent()
    {
        if (carCarousel.AreAllCarsNull())
        {
            noCarsAvailableMessage.SetActive(true);
        }
        else
        {
            carCarousel.ShowCarAtItem(0);
            UpdateScreen(carCarousel.GetCurrentItemIndex());
        }
    }

    void HandleBuyButtonPressed()
    {
        confirmPurchaseModal.ShowModal();
    }

    void UpdateScreen(int index)
    {
        carNameText.SetText(carCarousel.GetCurrentCar().GetCarName());
        priceText.SetText(carCarousel.GetCurrentCar().GetCarPrice().ToString("C"));
    }

    void HandleConfirmPurchaseModalConfirm()
    {
        float playerMoney = GameManager.GetMoneyAsFloat();
        float carPrice = carCarousel.GetCurrentCar().GetCarPrice();
        if (playerMoney >= carPrice)
        {
            Events.purchaseButtonClicked?.Invoke();
            GameManager.PurchaseCar(carPrice, carCarousel.GetCurrentItemIndex());
            congratulationsModal.ShowModal();
            confirmPurchaseModal.HideModal();
        }
        else
        {
            notEnoughMoneyModal.ShowModal();
        }
    }

    void HideConfirmPurchaseModal()
    {
        confirmPurchaseModal.HideModal();
    }

    void HandleMainMenuButtonClicked()
    {
        NavigationManager.LoadScene(Scenes.MAIN_MENU);
    }

    void HandleGarageButtonClicked()
    {
        NavigationManager.LoadScene(Scenes.GARAGE);
    }

    void HandleNotEnoughMoneyModalCloseButtonClicked()
    {
        confirmPurchaseModal.HideModal();
        notEnoughMoneyModal.HideModal();
    }
}
