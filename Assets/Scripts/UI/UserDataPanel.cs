using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserDataPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI username;
    [SerializeField] TextMeshProUGUI money;

    private void Start()
    {
        username.SetText(GameManager.GetUsername());
        money.SetText(GameManager.GetMoneyAsString());
    }
}
