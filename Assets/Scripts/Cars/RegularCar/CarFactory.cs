using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarFactory : MonoBehaviour
{
    [SerializeField] List<GameObject> carMeshes;
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] List<Path> paths;
    [SerializeField] GameObject carStartingDirectionObject;
    static List<CarController> cars;
    int counter;
    int currentTrack;
    //bool showHeadlights;

    private void Start()
    {
        cars = new List<CarController>();
        currentTrack = int.Parse(NavigationManager.sceneData["track"]);
        //showHeadlights = currentTrack % 2 == 0;
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            SpawnRandomCar(spawnPoints[i], i);
        }
    }

    public GameObject SpawnRandomCar(Transform spawnPoint, int index)
    {
        int randomBodyIndex = Random.Range(0, carMeshes.Count - 1);
        GameObject car = Instantiate(carMeshes[randomBodyIndex], spawnPoint);
        car.transform.parent = null;
        CarController carController = car.GetComponent<CarController>();
        carController.SetCarLabel("Car " + counter);
        counter++;
        SelfDrivingCar pf = car.GetComponent<SelfDrivingCar>();
        pf.path = index <= 2 ? paths[0] : paths[1];
        carController.gameObject.transform.rotation = carStartingDirectionObject.transform.rotation;
        cars.Add(carController);
        return car;
    }

    public static void EnableAICars()
    {
        foreach (CarController car in cars)
        {
            car.EnableAICar();
        }
    }
}
