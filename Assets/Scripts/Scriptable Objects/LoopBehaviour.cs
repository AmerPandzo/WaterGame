using UnityEngine;
using System;

[CreateAssetMenu(fileName = "LoopBehaviour")]
public class LoopBehaviour : ScriptableObject
{
    public bool isDisappearing;
    public float disappearTimer;

    public Action OnTimerPassed;
    public Action OnEntering;
    public Action OnExiting;
}