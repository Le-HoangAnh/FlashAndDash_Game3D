using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedRaceModal : OneButtonModal
{
    [SerializeField] Leaderboard finishRaceModalLeaderboard;
    [SerializeField] GameObject confettiImageObject;
    [SerializeField] GameObject confettiParticlesObject;
    [SerializeField] GameObject trophyObject;
    [SerializeField] AudioSource congratulationsAudio;
    [SerializeField] AudioSource achievementAudio;

    public void UpdateFinalLeaderboard(List<CarController> listOfCarsInRace)
    {
        finishRaceModalLeaderboard.UpdateLeaderboard(listOfCarsInRace);
    }

    public void ShowFirstPlaceCelebration(bool shouldShow)
    {
        if (shouldShow == true)
        {
            confettiImageObject.SetActive(true);
            confettiParticlesObject.SetActive(true);
            trophyObject.SetActive(true);
            Invoke("StopTrophySpinning", 1.5f);

            if (achievementAudio != null)
            {
                achievementAudio.Play();
            }
        }
        else
        {
            confettiImageObject.SetActive(true);
            confettiParticlesObject.SetActive(true);
            trophyObject.SetActive(true);
        }
    }

    private void StopTrophySpinning()
    {
        Rotate360 rotate360Script = trophyObject.GetComponent<Rotate360>();
        rotate360Script.enabled = false;

        if (congratulationsAudio != null)
        {
            congratulationsAudio.Play();
        }
    }
}
