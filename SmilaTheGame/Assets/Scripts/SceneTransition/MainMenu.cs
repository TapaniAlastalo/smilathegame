using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public Button startGameButton;

    public TMP_Text text;

    private void Start()
    {
        Button btnStart = startGameButton.GetComponent<Button>();
        btnStart.onClick.AddListener(StartGame);

        AddScoreText();        
    }

    private void StartGame()
    {
        ScoreManager.Reset();
        SceneLoader.LoadNext();
    }

    private void AddScoreText()
    {
        int score = ScoreManager.score;
        if (score > 0)
        {
            text.text = "Your Score: " + score;
        }
        else
        {
            text.text = "";
        }
        
    }


}
