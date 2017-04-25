using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Waterhoops/Time")]
public class TimeMode : Mode
{
    public Difficulty difficulty = Difficulty.NONE;
    public float endTime = 60f;
    public float timeBonus = 10f;

    private void TimeEasyMode_OnTimerPassed()
    {
        endTime += timeBonus;
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
        modeController.dataHolder.disappearTimer = disappearTimer;
        modeController.dataHolder.isDisappearing = loopsDisappearing;
        modeController.leftTime = endTime;

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
        modeController.scoreValue = modeController.disappearCounter;
        modeController.leftTime -= (Time.time - modeController.startTime);
        modeController.timerValue = endTime;
        if (modeController.leftTime < 0) modeController.OnGameOver();
    }
}