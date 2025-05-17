using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SlotsManager : MonoBehaviour
{
    [SerializeField] private static List<Slot> slots = new();
    public static Action<GameObject> IsChacked;
    [SerializeField] private FigureSpawner spawner;

    public static List<Slot> Slots { get { return slots; } }

    private void Start()
    {
        for (int i = 1; i <= 7; i++)
        {
            slots.Add(transform.Find($"Slot {i}").GetComponent<Slot>());
        }
    }

    private void OnEnable()
    {
        Figure.TapedOnFigure += CheckForCanAdd;
        ResetButton.PressedOnResetButton += ResetFigures;
    }

    private void OnDisable()
    {
        Figure.TapedOnFigure -= CheckForCanAdd;
        ResetButton.PressedOnResetButton -= ResetFigures;
    }

    private void CheckForCanAdd(GameObject obj)
    {
        bool allPreviousOccupied = true;
        foreach (Slot slot in slots)
        {
            slot.CanAdd = !slot.Occupaied && allPreviousOccupied;
            if (!slot.Occupaied)
            {
                allPreviousOccupied = false;
            }
            IsChacked?.Invoke(obj);
        }
    }

    private void ResetFigures()
    {
        List<GameObject> figures = new();
        foreach (var figure in slots)
        {
            if (figure.figure != null) // ƒобавл€ем только существующие фигуры
            {
                figures.Add(figure.figure);
            }
        }
        if (figures.Count > 0)
        {
            spawner.SpawnFigures(figures);
        }
    }

}
