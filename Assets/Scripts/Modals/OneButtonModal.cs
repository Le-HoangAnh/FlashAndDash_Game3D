using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class OneButtonModal : Modal
{
    [SerializeField] protected Button button;
    private UnityAction callBack;

    protected override void OnEnable()
    {
        base.OnEnable();
        button.onClick.AddListener(HandleButtonClicked);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        button.onClick.RemoveListener(HandleButtonClicked);
    }

    protected virtual void HandleButtonClicked()
    {
        Events.okButtonClicked?.Invoke();
        callBack?.Invoke();
    }

    public void Init(UnityAction callBack)
    {
        this.callBack = callBack;
    }
}
