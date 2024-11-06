using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    [SerializeField] Button backButton;

    private void Start()
    {
        backButton.onClick.AddListener(HandleBackButtonPressed);
    }

    private void OnDestroy()
    {
        backButton.onClick.RemoveListener(HandleBackButtonPressed);
    }

    private void HandleBackButtonPressed()
    {
        Events.backButtonPressed?.Invoke();
    }
}
