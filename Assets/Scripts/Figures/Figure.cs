using System;
using UnityEngine;

public class Figure : MonoBehaviour
{
    public static Action<GameObject> TapedOnFigure;
    public static int counter = 0;

    private void OnEnable()
    {
        ResetButton.PressedOnResetButton += ClearCounter;
    }
    private void OnDisable()
    {
        ResetButton.PressedOnResetButton -= ClearCounter;
    }

    private void OnMouseDown()
    {
        counter = 0;
        foreach (Slot slot in SlotsManager.Slots)
        {
            if (slot.Occupaied) counter++;
        }
        if (counter < 7) 
        {
            TapedOnFigure?.Invoke(gameObject);
            gameObject.SetActive(false);
        }
            
    }

    private void ClearCounter()
    {
        counter = 0;
    }

}
