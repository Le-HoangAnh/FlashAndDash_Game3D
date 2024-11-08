using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class Modal : MonoBehaviour
{
    [SerializeField] Button scrimButton;

    protected virtual void OnEnable()
    {
        if (scrimButton != null)
        {
            scrimButton.onClick.AddListener(HideModal);
        }
    }

    protected virtual void OnDisable()
    {
        if (scrimButton != null)
        {
            scrimButton.onClick.RemoveListener(HideModal);
        }
    }

    public void ShowModal()
    {
        gameObject.SetActive(true);
    }

    public void HideModal()
    {
        gameObject.SetActive(false);
    }
}
