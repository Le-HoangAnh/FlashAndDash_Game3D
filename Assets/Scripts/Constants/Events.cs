using System;
using UnityEngine;

public static class Events
{
    public static Action<string> usernameSubmitted;
    public static Action<int> carChosen;
    public static Action backButtonPressed;
    public static Action okButtonClicked;
    public static Action cancelButtonClicked;
    public static Action purchaseButtonClicked;
    public static Action leftOrRightButtonClicked;
    public static Action saveSlotClicked;
    public static Action raceStarted;
    public static Action<int> lapCompleted;
    public static Action raceCompleted;
    public static Action<CarController> carSpawnedToTrack;
}
