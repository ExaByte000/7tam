using System;
using UnityEngine;

public class Figure : MonoBehaviour
{
    public static Action<GameObject> TapedOnFigure;
    private static int counter = 0;

    private void OnEnable()
    {
        ResetButton.PressedOnResetButton += ClearCounter;
    }

    private void OnMouseDown()
    {
        

        foreach (Slot slot in SlotsManager.Slots)
        {
            if (slot.CanAdd) counter++;
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
