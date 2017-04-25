using UnityEngine;
using UnityEngine.UI;

public class PlayUI : MonoBehaviour
{
    public Button backButton;
    public Text timerText;
    public Text scoreText;
    public Text countdownText;
    public Button leftPushButton;
    public Button rightPushButton;
    [HideInInspector] public Slider leftPushSlider;
    [HideInInspector] public Slider rightPushSlider;

    private void Awake()
    {
        leftPushSlider = leftPushButton.gameObject.GetComponentInChildren<Slider>();
        rightPushSlider = rightPushButton.gameObject.GetComponentInChildren<Slider>();
    }
}