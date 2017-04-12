using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{
    [Header("UI Components")]
    public Button backButton; // GameController
    public Button leftPushButton;
    public Button rightPushButton;
    [HideInInspector] public Slider leftPushSlider;
    [HideInInspector] public Slider rightPushSlider;

    [Header("GameObject Pushers")]
    public GameObject leftPusher;
    public GameObject rightPusher;

    private ButtonHelper leftPushHelper;
    private ButtonHelper rightPushHelper;
    private ApplyForce leftForce;
    private ApplyForce rightForce;

    private void Awake()
    {
        leftPushSlider = leftPushButton.gameObject.GetComponentInChildren<Slider>();
        rightPushSlider = rightPushButton.gameObject.GetComponentInChildren<Slider>();

        leftForce = leftPusher.GetComponentInChildren<ApplyForce>();
        rightForce = rightPusher.GetComponentInChildren<ApplyForce>();

        leftPushSlider.maxValue = leftForce.maxForceMagnitude;
        rightPushSlider.maxValue = rightForce.maxForceMagnitude;

        leftPushHelper = leftPushButton.GetComponent<ButtonHelper>();
        rightPushHelper = rightPushButton.GetComponent<ButtonHelper>();

        leftPushHelper.OnPointerDownAction += leftForce.Toggle;
        leftPushHelper.OnPointerUpAction += leftForce.Toggle;
        rightPushHelper.OnPointerDownAction += rightForce.Toggle;
        rightPushHelper.OnPointerUpAction += rightForce.Toggle;

    }

    private void FixedUpdate()
    {
        leftPushSlider.value = leftForce.currentForceMagnitude;
        rightPushSlider.value = rightForce.currentForceMagnitude;
    }
}