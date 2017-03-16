using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
    [Header("UI Components")]
    public Button backButton;
    public Button leftPushButton;
    public Button rightPushButton;

    [Header("GameObject Pushers")]
    public GameObject leftPusher;
    public GameObject rightPusher;

    private ButtonHelper leftPushHelper;
    private ButtonHelper rightPushHelper;

    void Awake()
    {
        backButton.onClick.AddListener(Back);

        leftPushHelper = leftPushButton.GetComponent<ButtonHelper>();
        rightPushHelper = rightPushButton.GetComponent<ButtonHelper>();

        leftPushHelper.OnPointerDownAction += leftPusher.GetComponentInChildren<ApplyForce>().Toggle;
        leftPushHelper.OnPointerUpAction += leftPusher.GetComponentInChildren<ApplyForce>().Toggle;
        rightPushHelper.OnPointerDownAction += rightPusher.GetComponentInChildren<ApplyForce>().Toggle;
        rightPushHelper.OnPointerUpAction += rightPusher.GetComponentInChildren<ApplyForce>().Toggle;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Back();
    }

    private void Back()
    {
        SceneManager.LoadScene(0);
    }
}