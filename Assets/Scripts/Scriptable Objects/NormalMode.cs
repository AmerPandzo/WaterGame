using UnityEngine;

[CreateAssetMenu(menuName = "Waterhoops/Normal")]
public class NormalMode : Mode
{
    public int numberToScore = 10;
    public Difficulty difficulty = Difficulty.NONE;

    private void NormalMode_OnTimerPassed()
    {
        modeController.disappearCounter++;
    }

    private void NormalMode_OnEntering()
    {
        modeController.disappearCounter++;
    }

    private void NormalMode_OnExiting()
    {
        modeController.disappearCounter--;
    }

    public override void Init(ModeController modeController)
    {
        this.modeController = modeController;
        modeController.dataHolder.disappearTimer = disappearTimer;
        modeController.dataHolder.isDisappearing = loopsDisappearing;

        if (difficulty == Difficulty.Easy)
        {
            modeController.dataHolder.OnTimerPassed += NormalMode_OnTimerPassed;
            Debug.Log("Normal easy started.");
        }
        else
        {
            modeController.dataHolder.OnEntering += NormalMode_OnEntering;
            modeController.dataHolder.OnExiting += NormalMode_OnExiting;
            Debug.Log("Normal hard started.");
        }
    }

    public override void UpdateMode()
    {
        if (modeController.isPlaying)
        {
            modeController.scoreValue = modeController.disappearCounter;
            modeController.timerValue = Time.time - modeController.startTime;
            if (modeController.disappearCounter == numberToScore) modeController.OnGameOver();
        }
    }
}