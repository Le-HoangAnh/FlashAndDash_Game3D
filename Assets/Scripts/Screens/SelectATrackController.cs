using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectATrackController : MonoBehaviour
{
    [SerializeField] List<GameObject> tracks;
    [SerializeField] Button selectTrackButton;
    [SerializeField] TrackCarousel trackCarousel;

    private void Start()
    {
        selectTrackButton.onClick.AddListener(HandleSelectTrackButtonClicked);
    }

    private void OnDestroy()
    {
        selectTrackButton.onClick.RemoveListener(HandleSelectTrackButtonClicked);
    }

    private void Update()
    {
        TurnTracksToFaceCamera();
    }

    void TurnTracksToFaceCamera()
    {
        foreach (GameObject track in tracks)
        {
            track.transform.LookAt(Camera.main.transform);
        }
    }

    public void HandleSelectTrackButtonClicked()
    {
        Dictionary<string, string> sceneData = new Dictionary<string, string>();
        switch (trackCarousel.GetCurrentItemIndex())
        {
            case 0:
                sceneData.Add("track", "1");
                NavigationManager.LoadScene(Scenes.TRACK1, sceneData);
                break;
            case 1:
                sceneData.Add("track", "2");
                NavigationManager.LoadScene(Scenes.TRACK2, sceneData);
                break;
            case 2:
                sceneData.Add("track", "3");
                NavigationManager.LoadScene(Scenes.TRACK3, sceneData);
                break;
        }
    }
}
