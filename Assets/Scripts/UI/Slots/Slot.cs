using System;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public bool CanAdd = false;

    public bool Occupaied { get; private set; }

    [SerializeField] private Sprite slotSprite;
    public GameObject figure;
    public static Action FigureAdded;


    private Image form;
    private Transform animal;

    private void Start()
    {
        Occupaied = false;
        form = GetComponent<Image>();
        animal = transform.Find("Animal");
    }

    private void OnEnable()
    {
        SlotsManager.IsChacked += AddFigureToSlot;
        ResetButton.PressedOnResetButton += RemoveFigureFromList;
    }
    private void OnDisable()
    {
        SlotsManager.IsChacked -= AddFigureToSlot;
        ResetButton.PressedOnResetButton -= RemoveFigureFromList;
    }

    private void AddFigureToSlot(GameObject figure)
    {
        Debug.Log("Фигура добавлена в слот");
        if (!Occupaied && CanAdd) 
        {
            SpriteRenderer figureSparite = figure.GetComponent<SpriteRenderer>();


            form.sprite = figureSparite.sprite;
            form.color = figureSparite.color;


            animal.gameObject.SetActive(true);

            animal.GetComponent<Image>().sprite = figure.transform.Find("Animal").GetComponent<SpriteRenderer>().sprite;

            Occupaied = true;

            this.figure = figure;

            FigureAdded?.Invoke();
        }
    }

    public void RemoveFigureFromList()
    {
        form.sprite = slotSprite;
        form.color = Color.white;
        animal.gameObject?.SetActive(false);

        Occupaied = false;
        CanAdd = false;
    }
}
