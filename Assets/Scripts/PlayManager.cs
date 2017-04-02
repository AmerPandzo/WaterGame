using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
    [Header("UI Components")]
    public Button backButton;
    public Button leftPushButton;
    public Button rightPushButton;
    public Slider leftPushSlider;
    public Slider rightPushSlider;

    [Header("GameObject Pushers")]
    public GameObject leftPusher;
    public GameObject rightPusher;

    private ButtonHelper leftPushHelper;
    private ButtonHelper rightPushHelper;
    private ApplyForce leftForce;
    private ApplyForce rightForce;

    void Awake()
    {
        backButton.onClick.AddListener(Back);

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Back();
    }

    void FixedUpdate()
    {
        leftPushSlider.value = leftForce.currentForceMagnitude;
        rightPushSlider.value = rightForce.currentForceMagnitude;
    }

    private void Back()
    {
        SceneManager.LoadScene(0);
    }
}