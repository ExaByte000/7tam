using System;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    public static Action PressedOnResetButton;

    public void PressOnButton()
    {
        PressedOnResetButton?.Invoke();
    }
}
