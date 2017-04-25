using UnityEngine;
using System.Collections;

public class PlayManager : MonoBehaviour
{
    [HideInInspector] public PlayUI playUi;
    [HideInInspector] public ModeController modeController;

    [Header("Push")]
    public GameObject leftPusher;
    public GameObject rightPusher;
    private ButtonHelper leftPushHelper;
    private ButtonHelper rightPushHelper;
    private ApplyForce leftForce;
    private ApplyForce rightForce;

    [Space(5)]
    public int countdownTime = 3;
    private IEnumerator coroutine;

    private void Awake()
    {
        GetComponents();

        leftPushHelper.OnPointerDownAction += leftForce.Toggle;
        leftPushHelper.OnPointerUpAction += leftForce.Toggle;
        rightPushHelper.OnPointerDownAction += rightForce.Toggle;
        rightPushHelper.OnPointerUpAction += rightForce.Toggle;
    }

    private void Start()
    {
        playUi.leftPushSlider.maxValue = leftForce.ForceMagnitude;
        playUi.rightPushSlider.maxValue = rightForce.ForceMagnitude;
        StartCountdown();
    }

    public void StartCountdown()
    {
        coroutine = CountdownSecondsFrom(countdownTime);
        StartCoroutine(coroutine);
    }

    private IEnumerator CountdownSecondsFrom(int time)
    {
        playUi.countdownText.gameObject.SetActive(true);
        playUi.timerText.gameObject.SetActive(false);
        while (time > 0)
        {
            playUi.countdownText.text = time.ToString();
            time--;
            yield return new WaitForSecondsRealtime(1);
        }
        playUi.countdownText.gameObject.SetActive(false);
        playUi.timerText.gameObject.SetActive(true);
        modeController.StartGame();
    }

    private void FixedUpdate()
    {
        playUi.leftPushSlider.value = leftForce.currentForceMagnitude;
        playUi.rightPushSlider.value = rightForce.currentForceMagnitude;
        playUi.scoreText.text = modeController.scoreValue.ToString();
        playUi.timerText.text = FloatTimeToNiceString(modeController.timerValue);
    }

    private string FloatTimeToNiceString(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time - minutes * 60f);
        float timeInMilliseconds = time * 1000;
        int milliseconds = (int)timeInMilliseconds % 1000;
        return string.Format("{0:D2}:{1:D2}:{2:D2}", minutes, seconds, milliseconds);
    }

    private void GetComponents()
    {
        playUi = GetComponent<PlayUI>();
        modeController = GetComponent<ModeController>();
        leftForce = leftPusher.GetComponentInChildren<ApplyForce>();
        rightForce = rightPusher.GetComponentInChildren<ApplyForce>();
        leftPushHelper = playUi.leftPushButton.GetComponent<ButtonHelper>();
        rightPushHelper = playUi.rightPushButton.GetComponent<ButtonHelper>();
    }
}