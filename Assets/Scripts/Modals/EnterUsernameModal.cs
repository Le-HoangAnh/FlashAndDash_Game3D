using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnterUsernameModal : OneButtonModal
{
    [SerializeField] TMP_InputField inputField;

    protected override void HandleButtonClicked()
    {
        base.HandleButtonClicked();
        string username = inputField.text;
        Events.usernameSubmitted(username);
    }
}
