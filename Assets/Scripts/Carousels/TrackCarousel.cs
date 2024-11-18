using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackCarousel : MonoBehaviour, ICarousel
{
    [SerializeField] List<GameObject> tracks;
    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;
    [SerializeField] Transform pivot;
    [SerializeField] float carouselRotateSpeed;
    int currentActiveIndex;
    bool rotateLeft;
    bool rotateRight;
    Quaternion leftRotateEnd;
    Quaternion rightRotateEnd;

    private void Start()
    {
        leftButton.onClick.AddListener(ShowPreviousItem);
        rightButton.onClick.AddListener(ShowNextItem);
    }

    private void OnDestroy()
    {
        leftButton.onClick.RemoveListener(ShowPreviousItem);
        rightButton.onClick.RemoveListener(ShowNextItem);
    }

    public int GetCurrentItemIndex()
    {
        return currentActiveIndex;
    }

    public void ShowNextItem()
    {
        if (rotateLeft || rotateRight)
            return;
        Events.leftOrRightButtonClicked?.Invoke();
        currentActiveIndex = currentActiveIndex < tracks.Count - 1 ? currentActiveIndex + 1 : 0;
        rightRotateEnd = pivot.transform.rotation * Quaternion.Euler(0, -120, 0);
        rotateRight = true;
    }

    public void ShowPreviousItem()
    {
        if (rotateLeft || rotateRight)
            return;
        Events.leftOrRightButtonClicked?.Invoke();
        currentActiveIndex = currentActiveIndex > 0 ? currentActiveIndex - 1 : tracks.Count - 1;
        leftRotateEnd = pivot.transform.rotation * Quaternion.Euler(0, 120, 0);
        rotateLeft = true;
    }

    private void Update()
    {
        if (rotateLeft)
        {
            pivot.transform.rotation = Quaternion.Lerp(pivot.transform.rotation, leftRotateEnd, 
                                                       carouselRotateSpeed * Time.deltaTime);
        }

        if (rotateRight)
        {
            pivot.transform.rotation = Quaternion.Lerp(pivot.transform.rotation, rightRotateEnd,
                                                       carouselRotateSpeed * Time.deltaTime);
        }

        if (rotateLeft && pivot.transform.rotation == leftRotateEnd)
        {
            rotateLeft = false;
        }

        if (rotateRight && pivot.transform.rotation == rightRotateEnd)
        {
            rotateRight = false;
        }
    }
}
