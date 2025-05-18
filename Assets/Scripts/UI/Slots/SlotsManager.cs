using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SlotsManager : MonoBehaviour
{
    [SerializeField] private static List<Slot> slots = new();
    public static Action<GameObject> IsChacked;
    [SerializeField] private FigureSpawner spawner;

    public static List<Slot> Slots { get { return slots; }}

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
        Slot.FigureAdded += CheckForMatches;
    }

    private void OnDisable()
    {
        Figure.TapedOnFigure -= CheckForCanAdd;
        ResetButton.PressedOnResetButton -= ResetFigures;
        Slot.FigureAdded -= CheckForMatches;
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
        }
        IsChacked?.Invoke(obj);
    }

    private void ResetFigures()
    {
        List<GameObject> figures = new();
        foreach (var slot in slots)
        {
            if (slot.figure != null)
            {

                figures.Add(slot.figure);
            }
        }
        if (figures.Count > 0)
        {
            spawner.SpawnFigures(figures);
        }
    }

    private void CheckForMatches()
    {
        List<Slot> matchedSlots = new List<Slot>();

        for (int i = 0; i <= slots.Count - 3; i++)
        {
            if (AreSlotsFiguresMatching(slots[i], slots[i + 1], slots[i + 2]))
            {
                if (!matchedSlots.Contains(slots[i]))
                    matchedSlots.Add(slots[i]);
                if (!matchedSlots.Contains(slots[i + 1]))
                    matchedSlots.Add(slots[i + 1]);
                if (!matchedSlots.Contains(slots[i + 2]))
                    matchedSlots.Add(slots[i + 2]);
            }
        }
        if (matchedSlots.Count > 0)
        {
            RemoveMatchedFigures(matchedSlots);
            Debug.Log($"Количество фигур: {FigureSpawner.spawnedFigures.Count()}");
        }
    }

    private bool AreSlotsFiguresMatching(Slot slot1, Slot slot2, Slot slot3)
    {
        if (!slot1.Occupaied || !slot2.Occupaied || !slot3.Occupaied)
            return false;

        Image form1 = slot1.GetComponent<Image>();
        Image form2 = slot2.GetComponent<Image>();
        Image form3 = slot3.GetComponent<Image>();

        Image animal1 = slot1.transform.Find("Animal").GetComponent<Image>();
        Image animal2 = slot2.transform.Find("Animal").GetComponent<Image>();
        Image animal3 = slot3.transform.Find("Animal").GetComponent<Image>();

        bool formsMatch = form1.sprite == form2.sprite && form2.sprite == form3.sprite;

        bool colorsMatch = form1.color == form2.color && form2.color == form3.color;

        bool animalsMatch = animal1.sprite == animal2.sprite && animal2.sprite == animal3.sprite;

        return formsMatch && colorsMatch && animalsMatch;
    }

    private void RemoveMatchedFigures(List<Slot> matchedSlots)
    {

        foreach (Slot slot in matchedSlots)
        {
            FigureSpawner.spawnedFigures.Remove(slot.figure);
            Destroy(slot.figure);
            slot.RemoveFigureFromList();
        }

    }
}
