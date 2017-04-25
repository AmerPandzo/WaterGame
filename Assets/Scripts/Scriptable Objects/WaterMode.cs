using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Waterhoops/Water")]
public class WaterMode : Mode
{
    public Difficulty difficulty = Difficulty.NONE;
    public float endTime = 60f;
    public float timeBonus = 10f;

    private void WaterModeEasy_OnTimerPassed()
    {
        modeController.leftTime += timeBonus;
        modeController.disappearCounter++;
        modeController.Reuse();
    }

    private void WaterHardMode_OnTimerPassed()
    {
        modeController.disappearCounter++;
        modeController.Reuse();
    }

    public override void Init(ModeController modeController)
    {
        this.modeController = modeController;
        modeController.leftTime = endTime;
        modeController.dataHolder.disappearTimer = disappearTimer;
        modeController.dataHolder.isDisappearing = loopsDisappearing;

        if (difficulty == Difficulty.Easy)
        {
            modeController.dataHolder.OnTimerPassed += WaterModeEasy_OnTimerPassed;
            Debug.Log("Water easy started.");
        }
        else
        {
            modeController.dataHolder.OnTimerPassed += WaterHardMode_OnTimerPassed;
            Debug.Log("Water hard started.");
        }
    }

    public override void UpdateMode()
    {
        // TODO - proportionally do the water level calculation
        modeController.scoreValue = modeController.disappearCounter;
        modeController.leftTime -= (Time.time - modeController.startTime);
        modeController.timerValue = modeController.leftTime;
        if (endTime < 0) modeController.OnGameOver();
    }

}
