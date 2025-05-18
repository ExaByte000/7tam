using System;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action GameOver;
    public static Action GameWin;

    private bool gameOver;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    private void GameOverConditions()
    {
        int counter = 0;
        foreach (var slot in SlotsManager.Slots)
        {
            if (slot.Occupaied)
            {
                counter++;
            }
        }
        if (counter == 7 && !gameOver)
        {
            GameOver?.Invoke();
            gameOver = true;
        }
    }

    private void GameWinConditions()
    {
        if(FigureSpawner.spawnedFigures.Count() == 0 && !gameOver)
        {
            GameWin?.Invoke();
            gameOver = true;
        }
    }

    private void Update()
    {
        GameWinConditions();
        GameOverConditions();
    }
}
