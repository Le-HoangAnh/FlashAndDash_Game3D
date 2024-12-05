using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] List<CarController> playerCarList;
    [SerializeField] TimeText timeText;
    [SerializeField] LapText lapText;
    [SerializeField] List<CheckPointTrigger> checkPointTriggers;
    [SerializeField] PlaceText placeText;
    [SerializeField] Leaderboard leaderboard;
    [SerializeField] FinishedRaceModal finishedRaceModal;
    List<CarController> listOfCarsInRace;
    float raceTime = 0;
    bool raceStarted = false;
    CarController playerCar;
    int place;

    private void Awake()
    {
        listOfCarsInRace = new List<CarController>();
        timeText.SetValue(0f);
        lapText.SetValue(0);
        playerCar = playerCarList[GameManager.GetCurrentActiveCarIndex()];
        int currentTrack = int.Parse(NavigationManager.sceneData["track"]);
        bool showHeadLights = currentTrack % 2 == 0;
        playerCar.DisplayCar(true, showHeadLights);
        listOfCarsInRace.Add(playerCar);
        Events.raceStarted += HandleRaceStarted;
        Events.lapCompleted += HandleLapUpdated;
        Events.carSpawnedToTrack += HandleCarAddedToRace;
        Events.raceCompleted += HandleRaceCompleted;
    }

    private void OnDestroy()
    {
        Events.raceStarted -= HandleRaceStarted;
        Events.lapCompleted -= HandleLapUpdated;
        Events.carSpawnedToTrack -= HandleCarAddedToRace;
        Events.raceCompleted -= HandleRaceCompleted;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        }

        UpdateCarList();
        UpdatePlace();
        leaderboard.UpdateLeaderboard(listOfCarsInRace);
    }

    private void FixedUpdate()
    {
        if (raceStarted)
        {
            raceTime += Time.fixedDeltaTime;
            timeText.SetValue(raceTime);
        }
    }

    void HandleRaceStarted()
    {
        raceStarted = true;
    }

    void HandleLapUpdated(int lapNumber)
    {
        lapText.SetValue(lapNumber);
    }

    int SortListOfCarsInRace(CarController c1, CarController c2)
    {
        CarModal c1Modal = c1.GetComponent<CarModal>();
        CarModal c2Modal = c2.GetComponent<CarModal>();
        int compareValue = c1Modal.GetNumberOfCheckpointsHit().CompareTo(c2Modal.GetNumberOfCheckpointsHit());
        if (compareValue != 0)
        {
            return compareValue;
        }

        float distanceOfCar1FromNextCheckpoint = Vector3.Distance(c1Modal.gameObject.transform.position,
            checkPointTriggers[c1Modal.GetNumberOfCheckpointsHit() + 1].gameObject.transform.position);
        float distanceOfCar2FromNextCheckpoint = Vector3.Distance(c2Modal.gameObject.transform.position,
            checkPointTriggers[c2Modal.GetNumberOfCheckpointsHit() + 1].gameObject.transform.position);
        int returnValue = distanceOfCar1FromNextCheckpoint > distanceOfCar2FromNextCheckpoint ? -1 : 1;
        return returnValue;
    }

    private void UpdateCarList()
    {
        listOfCarsInRace.Sort(SortListOfCarsInRace);
        listOfCarsInRace.Reverse();
    }

    private void UpdatePlace()
    {
        place = listOfCarsInRace.IndexOf(playerCar) + 1;
        placeText.SetValue(place);
    }

    void HandleCarAddedToRace(CarController car)
    {
        listOfCarsInRace.Add(car);
    }

    void HandleRaceCompleted()
    {
        finishedRaceModal.Init(HandleMainMenuButtonClicked);
        finishedRaceModal.UpdateFinalLeaderboard(listOfCarsInRace);
        finishedRaceModal.ShowModal();
        finishedRaceModal.ShowFirstPlaceCelebration(place == 1);
    }

    private void HandleMainMenuButtonClicked()
    {
        NavigationManager.LoadScene(Scenes.MAIN_MENU);
    }
}
