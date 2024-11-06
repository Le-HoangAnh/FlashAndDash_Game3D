using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : Car
{
    [SerializeField] CarModel carModel;
    [SerializeField] CarView carView;
    [SerializeField] GameObject car;
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] bool isAIControllerCar;
    [SerializeField] Ai agent;
    [SerializeField] SelfDrivingCar pathFollower;
    [SerializeField] GameObject racingCameraObject;
    [SerializeField] GameObject turboFlash; //nitrous
    [SerializeField] AudioSource gasAudio;
    [SerializeField] AudioSource brakeAudio;
    [SerializeField] AudioSource turboFlashAudio;
    [SerializeField] AudioSource crashAudio;
    [SerializeField] GameObject turboFlashUI; //nitrous UI
    [SerializeField] List<MeshRenderer> bodyMeshRenderers;
    [SerializeField] List<GameObject> bodyGameObjects;
    [SerializeField] MeshCollider bodyCollider;
    bool turboFlashReady; //nitrous ready
    [SerializeField] bool raceStarted;
    float turboFlashTimer; //nitrous timer

    [System.Serializable]
    public class AxleInfo
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool motor;
        public bool steering;
    }

    public override void Start()
    {
        base.Start();

        if (isAIControllerCar)
        {
            Events.carSpawnedToTrack?.Invoke(this);
        }
        Events.raceStarted += HandleRaceStarted;
        
        if (!isAIControllerCar)
        {
            //carModel.SetCarLabel(GameManager.SetUsername());
        }
        
        if (turboFlash != null)
        {
            turboFlash.SetActive(false);
        }
    }

    public override void Update()
    {
        base.Update();

        if (isAIControllerCar || !raceStarted)
        {
            return;
        }

        Quaternion tempRotation = transform.rotation;
        tempRotation.x = 0;
        tempRotation.z = 0;
        transform.rotation = tempRotation;

        if (turboFlashReady && Input.GetKeyDown(KeyCode.N))
        {
            UserTurboFlash();
        }
        turboFlashUI.SetActive(turboFlashReady);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            HandleGasPedal();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            HandleBrakePedal();
        }

        if (!turboFlashReady && turboFlashTimer >= 30f)
        {
            turboFlashReady = true;
        }
        else if (!turboFlashReady)
        {
            turboFlashTimer += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {
        Events.raceStarted -= HandleRaceStarted;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (crashAudio != null && crashAudio.isPlaying)
        {
            crashAudio.Play();
        }
    }

    private void UserTurboFlash()
    {
        turboFlashReady = false;
        turboFlashUI.SetActive(false);
        ToggleTurboFlash(true);
        IncreaseSpeed();
        Invoke("HideTurboFlashEffect", 5f);
        turboFlashTimer = 0;
    }

    private void HideTurboFlashEffect()
    {
        DecreaseSpeed();
        ToggleTurboFlash(false);
    }

    public void DisplayCar(bool shouldShow, bool applyCustomizations = false)
    {
        car.SetActive(shouldShow);

        if (applyCustomizations == true)
        {
            SetCarRims();
        }
    }

    public GameObject GetCar()
    {
        return car;
    }

    public string GetCarName()
    {
        return carModel.GetCarName();
    }

    public float GetCarPrice()
    {
        return carModel.GetCarPrice();
    }

    public void SetCarRims()
    {
        carView.SetCarRims();
    }

    public void SetRimMaterial(Material material)
    {
        carView.SetRimMaterial(material);
    }

    public Material[] GetCarBodyMaterials()
    {
        return carView.GetCarBodyMaterials();
    }

    public Material GetRimMaterial()
    {
        return carView.GetRimMaterial();
    }

    public float GetSpeed()
    {
        return rigidbody.velocity.magnitude;
    }

    public void HandleCheckpointWasHit()
    {
        carModel.CheckpointWasHit();
    }

    public void SetCarLabel(string label)
    {
        carModel.SetCarLabel(label);
    }

    public string GetCarLable()
    {
        return carModel.GetCarLabel();
    }

    public void SetDistanceToNextCheckpoint(float distance)
    {
        carModel.SetDistanceToNextCheckpoint(distance);
    }

    public float GetDistanceToNextCheckpoint()
    {
        return carModel.GetDistanceToNextCheckpoint();
    }

    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public float motor;

    private void FixedUpdate()
    {
        if (isAIControllerCar || !raceStarted)
        {
            return;
        }

        motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }

            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
        }
    }

    public void EnableAICar()
    {
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
        agent.enabled = true;
        pathFollower.enabled = true;
    }

    public GameObject GetRacingCameraObject()
    {
        return racingCameraObject;
    }

    public void ToggleTurboFlash(bool shouldShow)
    {
        turboFlash.SetActive(shouldShow);

        if (shouldShow == true)
        {
            turboFlashAudio.Play();
        }
        else
        {
            turboFlashAudio.Stop();
        }
    }

    public void IncreaseSpeed()
    {
        maxMotorTorque *= 4;
    }

    public void DecreaseSpeed()
    {
        maxMotorTorque /= 4;
    }

    void HandleRaceStarted()
    {
        raceStarted = true;
    }

    void HandleGasPedal()
    {
        if (gasAudio.isPlaying)
        {
            return;
        }
        gasAudio.Play();
    }

    void HandleBrakePedal()
    {
        if (brakeAudio.isPlaying)
        {
            return;
        }
        brakeAudio.Play();
    }
}
