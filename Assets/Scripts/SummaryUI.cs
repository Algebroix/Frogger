using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SummaryUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI summary;

    [SerializeField]
    private TextMeshProUGUI scoreSource;

    [SerializeField]
    private TextMeshProUGUI scoreShown;

    private string wonText = "You won! Congratulations!";
    private string lostText = "You lost.";

    public void Show(bool won = true)
    {
        if (won)
        {
            FrogController.currentLevel = (FrogController.currentLevel + 1) % 3;
            summary.text = wonText;
        }
        else
        {
            FrogController.currentLevel = 0;
            summary.text = lostText;
        }

        scoreShown.text = scoreSource.text;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(FrogController.currentLevel);
        }
    }
}