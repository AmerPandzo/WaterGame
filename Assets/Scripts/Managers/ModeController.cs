using System;
using UnityEngine;
using System.Collections.Generic;

public class ModeController : MonoBehaviour
{
    public Mode mode;
    public DataHolder dataHolder;

    [HideInInspector] public GameObject prefab;
    private int poolSize;

    [HideInInspector] public bool isPlaying;
    [HideInInspector] public float scoreValue;
    [HideInInspector] public float timerValue;
    [HideInInspector] public float startTime;

    [Header("Mode state info")]
    public int disappearCounter;
    public float leftTime;

    public Action OnGameStart;
    public Action OnGameOver;

    //public WaterLeak waterleak;
    public Transform[] spawnPositions;
    private Queue<Transform> spawningQueue;
    private PoolManager poolManager;

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();
        spawningQueue = new Queue<Transform>();

        // TODO - GameOver, Pause
        OnGameOver += () => { Debug.Log("OnGameOver event triggered."); isPlaying = false; };
    }

    public void Reuse()
    {
        poolManager.ReuseObject(prefab, GetSpawnPosition(), UnityEngine.Random.rotation);
    }

    public void StartGame()
    {
        for (int i = 0; i < poolSize; i++)
        {
            poolManager.ReuseObject(prefab, GetSpawnPosition(), UnityEngine.Random.rotation);
        }
        startTime = Time.time;
        isPlaying = true;
    }

    public void SetMode(GameObject prefab, int poolSize, Mode mode)
    {
        isPlaying = false;
        this.mode = mode;
        this.prefab = prefab;
        this.poolSize = poolSize;
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            spawningQueue.Enqueue(spawnPositions[i]);
        }
        poolManager.CreatePool(prefab, poolSize);
        this.mode.Init(this);
    }

    private void Update()
    {
        if (isPlaying)
        {
            mode.UpdateMode();
        }
    }

    private Vector3 GetSpawnPosition()
    {
        Transform transform = spawningQueue.Dequeue();
        spawningQueue.Enqueue(transform);
        return transform.position;
    }
}