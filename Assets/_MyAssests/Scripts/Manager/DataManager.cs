using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    [SerializeField] TMP_Text[] currentScoreText;
    [SerializeField] TMP_Text HeighestScoreText;
    [SerializeField] GameObject gameEndPanel;
    [SerializeField] int _currentScore = 0;

    void Start()
    {
        _currentScore = 0;
    }

    // Update is called once per frame
    public void UpdateScore()
    {
        _currentScore++;
        foreach (var score in currentScoreText)
        {
            score.text = _currentScore.ToString();
        }
    }

    public void GameEndMenu()
    {
        int lastScore = PlayerPrefs.GetInt("Score");
        if (lastScore < _currentScore)
        {
            PlayerPrefs.SetInt("Score", _currentScore);
            HeighestScoreText.text = _currentScore.ToString();
        }
        else
        {
            HeighestScoreText.text = lastScore.ToString();
        }
        foreach (var score in currentScoreText)
        {
            score.text = _currentScore.ToString();
        }

        gameEndPanel.SetActive(true);
    }

    public void ReStartMenu()
    {
        SceneManager.LoadScene(0);
    }
}
