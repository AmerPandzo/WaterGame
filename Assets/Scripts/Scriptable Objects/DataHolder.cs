using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Waterhoops/Game data")]
public class DataHolder : ScriptableObject
{
    [HideInInspector] public bool isDisappearing;
    [HideInInspector] public float disappearTimer;

    public Action OnTimerPassed;
    public Action OnEntering;
    public Action OnExiting;
}