using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu;
    private string gameOverText = "";

    private void OnEnable()
    {
        GameManager.GameWin += WinText;
        GameManager.GameOver += LossText;
    }
    private void OnDisable()
    {
        GameManager.GameWin -= WinText;
        GameManager.GameOver -= LossText;
    }

    private void WinText()
    {
        gameOverText = "You Win!";
        ShowMenu();

    }

    private void LossText()
    {
        gameOverText = "You Loss!";
        ShowMenu();


    }

    private void ShowMenu()
    {
        Time.timeScale = 0f;
        gameOverMenu.GetComponentInChildren<TextMeshProUGUI>().text = gameOverText;
        gameOverMenu.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;

        gameOverMenu.SetActive(false);

        FigureSpawner.spawnedFigures.Clear();
        Figure.counter = 0;
        SlotsManager.Slots.Clear();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
