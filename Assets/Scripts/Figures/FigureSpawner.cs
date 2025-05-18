using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class FigureSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> listOfForms = new();
    [SerializeField] private List<Color> listOfColors = new();
    [SerializeField] private List<Sprite> listOfAnimals = new();

    public static List<GameObject> spawnedFigures = new();

    private bool firstSpawn = true;
    private readonly List<GameObject> figuresForSpawn = new();
    private Vector2 spawnArea = new(5.5f, 50f);

    private void Start()
    {
        GenerateFigures();
        SpawnFigures(figuresForSpawn);
    }

    public void GenerateFigures()
    {
        foreach (var form in listOfForms)
        {
            form.GetComponent<SpriteRenderer>().color = listOfColors[Random.Range(0, listOfColors.Count())];
            form.transform.Find("Animal").GetComponentInChildren<SpriteRenderer>().sprite = listOfAnimals[Random.Range(0, listOfAnimals.Count())];
            figuresForSpawn.Add(form);

        }
    }

    public void SpawnFigures(List<GameObject> figures)
    {
        foreach (GameObject figure in figures)
        {
            foreach (GameObject item in figuresForSpawn)
            {
                figure.SetActive(true);

                if (figure.name.Contains(item.name))
                {
                   
                    if (firstSpawn)
                    {
                        int rnd = Random.Range(1, 10) * 3;
                        for (int i = 0; i < rnd; i++)
                        {
                            AreaForSpawn(item);
                        }

                    }
                    else
                    {
                        
                        if (spawnedFigures.Contains(figure)) spawnedFigures.Remove(figure);
                        Destroy(figure);
                        AreaForSpawn(item);
                        
                    }
                }
            }

        }
        firstSpawn = false;
    }

    private void AreaForSpawn(GameObject item)
    {
        Vector3 randomPosition = new(Random.Range(-spawnArea.x/2, spawnArea.x/2), Random.Range(5, spawnArea.y), 0);
        
        spawnedFigures.Add(Instantiate(item, randomPosition, Quaternion.identity));
       
    }
    
}
