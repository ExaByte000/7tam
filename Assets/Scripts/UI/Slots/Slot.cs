using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public bool CanAdd = false;

    public bool Occupaied { get; private set; }

    [SerializeField] private Sprite slotSprite;
    public GameObject figure;

    
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
        if (!Occupaied && CanAdd) 
        {
            SpriteRenderer figureSparite = figure.GetComponent<SpriteRenderer>();


            form.sprite = figureSparite.sprite;
            form.color = figureSparite.color;


            animal.gameObject.SetActive(true);

            animal.GetComponent<Image>().sprite = figure.transform.Find("Animal").GetComponentInChildren<SpriteRenderer>().sprite;

            Occupaied = true;

            this.figure = figure;
        }
    }

    private void RemoveFigureFromList()
    {
        form.sprite = slotSprite;
        form.color = Color.white;
        animal.gameObject.SetActive(false);

        Occupaied = false;
        CanAdd = false;
    }
}
