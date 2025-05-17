using UnityEngine;
using System.Collections.Generic;
using System.Linq;
//using System;

public class FigureSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> listOfForms = new();
    [SerializeField] private List<Color> listOfColors = new();
    [SerializeField] private List<Sprite> listOfAnimals = new();

    private bool firstSpawn = true;
    private List<GameObject> figuresForSpawn = new();
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
            foreach(GameObject item in figuresForSpawn)
            {
                figure.SetActive(true);

                if (figure.name.Contains(item.name))
                {
                    
                    if (firstSpawn)
                    {
                        int rnd = Random.Range(1, 11) * 3;
                        for (int i = 0; i < rnd; i++)
                        {
                            Vector3 randomPosition = new(Random.Range(-spawnArea.x / 2, spawnArea.x / 2), Random.Range(3, spawnArea.y), 5f);
                            Instantiate(item, randomPosition, Quaternion.identity);
                        }
                        
                    }
                    else 
                    {
                        Destroy(figure);
                        Vector3 randomPosition = new(Random.Range(-spawnArea.x / 2, spawnArea.x / 2), Random.Range(3, spawnArea.y), 5f);
                        Instantiate(item, randomPosition, Quaternion.identity);
                    }
                        

                }
            }
            
        }
        firstSpawn = false;
    }
}
