using System;
using UnityEngine;

public abstract class Mode : ScriptableObject
{
    public enum Difficulty { NONE, Easy, Hard }
    public bool loopsDisappearing;
    public int disappearTimer = 1;
    [HideInInspector] public ModeController modeController;
    public abstract void Init(ModeController modeController);
    public abstract void UpdateMode();
}