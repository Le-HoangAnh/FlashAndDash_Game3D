using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class FormattedText : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI formattedText;
    [SerializeField] protected string prefix;

    public string GetValue()
    {
        return formattedText.text;
    }

    protected abstract void FormatText();
}
