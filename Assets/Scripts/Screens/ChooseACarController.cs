using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseACarController : MonoBehaviour
{
    [SerializeField] CarCarousel carCarousel;
    [SerializeField] Button selectCarButton;
    //[SerializeField] TextMeshProUGUI chooseACarText;

    private void Awake()
    {
        //chooseACarText.SetText("Choose a car, " + NavigationManager.sceneData["username"] + "!");
    }

    private void OnEnable()
    {
        selectCarButton.onClick.AddListener(HandleSelectCarButtonClicked);
    }

    private void OnDisable()
    {
        selectCarButton.onClick.RemoveListener(HandleSelectCarButtonClicked);
    }

    void HandleSelectCarButtonClicked()
    {
        Events.carChosen?.Invoke(carCarousel.GetCurrentItemIndex());
        NavigationManager.LoadScene(Scenes.MAIN_MENU);
    }
}
