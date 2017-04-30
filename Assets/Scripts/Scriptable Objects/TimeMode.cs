using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Waterhoops/Time")]
public class TimeMode : Mode
{
    public Difficulty difficulty = Difficulty.NONE;
    public float timerMax = 60f;
    public float timerBonus = 10f;
    private float timer;

    private void TimeEasyMode_OnTimerPassed()
    {
        modeController.endTime += timerBonus;
        modeController.disappearCounter++;
        modeController.Reuse();
    }

    private void TimeHardMode_OnTimerPassed()
    {
        modeController.disappearCounter++;
        modeController.Reuse();
    }

    public override void Init(ModeController modeController)
    {
        this.modeController = modeController;
        modeController.startTime = Time.time;
        modeController.endTime = Time.time + timerMax;
        timer = timerMax;
        modeController.dataHolder.disappearTimer = disappearTimer;
        modeController.dataHolder.isDisappearing = loopsDisappearing;

        if (difficulty == Difficulty.Easy)
        {
            modeController.dataHolder.OnTimerPassed += TimeEasyMode_OnTimerPassed;
            Debug.Log("Time easy started.");
        }
        else
        {
            modeController.dataHolder.OnTimerPassed += TimeHardMode_OnTimerPassed;
            Debug.Log("Time hard started.");
        }
    }

    public override void UpdateMode()
    {
        timer = modeController.endTime - Time.time;
        modeController.scoreValue = modeController.disappearCounter;
        modeController.timerValue = timer;
        if (timer < 0)
        {
            modeController.OnGameOver();
            modeController.timerValue = 0f;
        }
    }
}