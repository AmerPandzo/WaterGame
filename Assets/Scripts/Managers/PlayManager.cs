using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{
    [Header("UI Components")]
    public Button backButton; // GameController
    public Text timerText;
    public Text scoreText;
    public Text countdownText;
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

    private GameObject prefab;
    private int poolSize;
    private GameController.Difficulty difficulty;
    private GameController.Mode mode;

    private bool isPlaying;
    public event Action OnGameStart;
    public event Action OnGameOver;

    //public WaterLeak waterleak;
    private PoolManager poolManager;

    [Header("Gamestate")]
    public int countFrom = 3;
    public float disappearTimer = 1f; // Modes where loops disappear
    public int numberToScore; // Normal mode
    public float endTime = 60f; // Time and Water modes
    public float timeBonus = 10f; // Time and Water mode time bonus
    private float score;
    private float disappearCounter;
    private float startTime;
    private IEnumerator coroutine;

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();

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

        OnGameStart += Start;
    }

    public void ReadyUpState(GameObject prefab, int poolSize, GameController.Mode mode, GameController.Difficulty difficulty)
    {
        this.mode = mode;
        this.difficulty = difficulty;
        this.prefab = prefab;
        this.poolSize = poolSize;
        numberToScore = poolSize;

        isPlaying = false;
        InitGame();
        StartCountdown();
    }

    // TODO - work out instantiating objects with positions, rotations

    private void InitGame()
    {
        switch (mode)
        {
            case GameController.Mode.Normal:
                if (difficulty == GameController.Difficulty.Easy)
                {
                    prefab.GetComponent<PoolObject>().SetPoolObjectBehavoiur(disappearTimer, true);
                    prefab.GetComponent<PoolObject>().OnTimerPassed += () => 
                    {
                        disappearCounter++;
                        scoreText.text = disappearCounter.ToString();
                    };
                    Debug.Log("Normal easy started.");
                }
                else
                {
                    prefab.GetComponent<PoolObject>().SetPoolObjectBehavoiur(disappearTimer, false);
                    prefab.GetComponent<PoolObject>().OnEntering += () =>
                    {
                        disappearCounter++;
                        scoreText.text = disappearCounter.ToString();
                    };
                    prefab.GetComponent<PoolObject>().OnExiting += () =>
                    {
                        disappearCounter--;
                        scoreText.text = disappearCounter.ToString();
                    };
                    Debug.Log("Normal hard started.");
                }
                break;
            case GameController.Mode.Time:
                if (difficulty == GameController.Difficulty.Easy)
                {
                    prefab.GetComponent<PoolObject>().SetPoolObjectBehavoiur(disappearTimer, true);
                    prefab.GetComponent<PoolObject>().OnTimerPassed += () =>
                    {
                        endTime += timeBonus;
                        disappearCounter++;
                        scoreText.text = disappearCounter.ToString();
                        // TODO - spawning position and rotation of new objects
                        //poolManager.ReuseObject(prefab); 
                    };
                    Debug.Log("Time easy started.");
                }
                else
                {
                    prefab.GetComponent<PoolObject>().SetPoolObjectBehavoiur(disappearTimer, true);
                    prefab.GetComponent<PoolObject>().OnTimerPassed += () =>
                    {
                        disappearCounter++;
                        scoreText.text = disappearCounter.ToString();
                        // TODO - spawning position and rotation of new objects
                        //poolManager.ReuseObject(prefab); 
                    };
                    Debug.Log("Time hard started.");
                }
                break;
            case GameController.Mode.Water:
                if (difficulty == GameController.Difficulty.Easy)
                {
                    prefab.GetComponent<PoolObject>().SetPoolObjectBehavoiur(disappearTimer, true);
                    prefab.GetComponent<PoolObject>().OnTimerPassed += () =>
                    {
                        endTime += timeBonus;
                        disappearCounter++;
                        scoreText.text = disappearCounter.ToString();
                        // TODO - spawning position and rotation of new objects
                        //poolManager.ReuseObject(prefab); 
                    };
                    Debug.Log("Water easy started.");
                }
                else
                {
                    prefab.GetComponent<PoolObject>().SetPoolObjectBehavoiur(disappearTimer, true);
                    prefab.GetComponent<PoolObject>().OnTimerPassed += () =>
                    {
                        disappearCounter++;
                        scoreText.text = disappearCounter.ToString();
                        // TODO - spawning position and rotation of new objects
                        //poolManager.ReuseObject(prefab); 
                    };
                    Debug.Log("Water hard started.");
                }
                break;
        }
    }

    public void Start()
    {
        poolManager.CreatePool(prefab, poolSize);
        startTime = Time.time;
    }

    public void StartCountdown()
    {
        coroutine = CountdownSecondsFrom(countFrom);
        StartCoroutine(coroutine);
    }

    private IEnumerator CountdownSecondsFrom(int time)
    {
        countdownText.gameObject.SetActive(true);
        while (time > 0)
        {
            countdownText.text = time.ToString();
            time--;
            yield return new WaitForSecondsRealtime(1);
        }
        countdownText.gameObject.SetActive(false);
        if (OnGameStart != null) OnGameStart();
    }

    private void Update()
    {
        switch (mode)
        {
            case GameController.Mode.Normal:
                score = Time.time - startTime;
                timerText.text = FloatTimeToNiceString(score);
                if (disappearCounter == numberToScore) OnGameOver();
                break;
            case GameController.Mode.Time:
                endTime -= (Time.time - startTime);
                timerText.text = FloatTimeToNiceString(endTime);
                if (endTime < 0) OnGameOver();
                break;
            case GameController.Mode.Water:
                endTime -= (Time.time - startTime);
                // TODO - proportionally do the water level calculation
                timerText.text = FloatTimeToNiceString(endTime);
                if (endTime < 0) OnGameOver();
                break;
        }
    }

    private void FixedUpdate()
    {
        leftPushSlider.value = leftForce.currentForceMagnitude;
        rightPushSlider.value = rightForce.currentForceMagnitude;
    }

    private string FloatTimeToNiceString(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time - minutes * 60f);
        float timeInMilliseconds = time * 1000;
        int milliseconds = (int)timeInMilliseconds % 1000;
        return string.Format("{0:D2}:{1:D2}:{2:D2}", minutes, seconds, milliseconds);
    }
}