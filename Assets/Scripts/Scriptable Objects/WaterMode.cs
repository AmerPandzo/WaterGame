using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Waterhoops/Water")]
public class WaterMode : Mode
{
    public Difficulty difficulty = Difficulty.NONE;
    public float timerMax = 60f;
    public float timerBonus = 10f;
    private float timer;

    private void WaterModeEasy_OnTimerPassed()
    {
        modeController.endTime += timerBonus;
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
        modeController.startTime = Time.time;
        modeController.endTime = Time.time + timerMax;
        timer = timerMax;
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