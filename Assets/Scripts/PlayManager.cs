using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour {

    [Header("UI Components")]
    public Button backButton;

    void Awake()
    {
        backButton.onClick.AddListener(Back);
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
