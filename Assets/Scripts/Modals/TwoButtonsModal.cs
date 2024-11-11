using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TwoButtonsModal : Modal
{
    [SerializeField] protected Button button1;
    [SerializeField] protected Button button2;
    private UnityAction callBack1;
    private UnityAction callBack2;

    protected override void OnEnable()
    {
        base.OnEnable();
        button1.onClick.AddListener(HandleButton1Clicked);
        button2.onClick.AddListener(HandleButton2Clicked);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        button1.onClick.RemoveListener(HandleButton1Clicked);
        button2.onClick.RemoveListener(HandleButton2Clicked);
    }

    protected virtual void HandleButton1Clicked()
    {
        Events.okButtonClicked?.Invoke();
        callBack1?.Invoke();
    }

    protected virtual void HandleButton2Clicked()
    {
        Events.cancelButtonClicked?.Invoke();
        callBack2?.Invoke();
    }

    public void Init(UnityAction callBack1, UnityAction callBack2)
    {
        this.callBack1 = callBack1;
        this.callBack2 = callBack2;
    }
}
