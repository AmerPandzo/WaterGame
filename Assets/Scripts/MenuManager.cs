using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Canvases")]
    public Canvas MainCanvas;
    public Canvas FacebookCanvas;

    [Header("Main Canvas Components")]
    public Button playButton;
    public Button quitButton;
    public Button facebbookButton;

    [Header("Facebook Canvas Components")]
    public Button backButton;

    void Awake()
    {
        playButton.onClick.AddListener(PlayClicked);
        quitButton.onClick.AddListener(QuickClicked);

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

    private void PlayClicked()
    {
        SceneManager.LoadScene(1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) QuickClicked();
    }

    private void QuickClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}