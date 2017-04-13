using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Canvases")]
    public Canvas MainCanvas;
    public Canvas FacebookCanvas;

    [Header("Main Canvas Components")]
    public Button playButton; // GameController
    public Button quitButton; // GameController
    public Button facebbookButton;

    [Header("Facebook Canvas Components")]
    public Button backButton;

    private void Awake()
    {
        facebbookButton.onClick.AddListener(ToggleCanvas);
        backButton.onClick.AddListener(ToggleCanvas);
    }

    private void ToggleCanvas()
    {
        MainCanvas.enabled = !MainCanvas.enabled;
        MainCanvas.GetComponent<GraphicRaycaster>().enabled = MainCanvas.enabled;
        FacebookCanvas.enabled = !FacebookCanvas.enabled;
        FacebookCanvas.GetComponent<GraphicRaycaster>().enabled = FacebookCanvas.enabled;
    }
}