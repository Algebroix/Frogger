using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverlayUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI score;

    [SerializeField]
    private RectTransform timeBar;

    [SerializeField]
    private SummaryUI summaryUI;

    [SerializeField]
    private Transform livesContainer;

    private Image[] lives;

    public void ShowSummary(bool won)
    {
        summaryUI.Show(won);
    }

    public void SetTime(float percentage)
    {
        timeBar.localScale = new Vector3(1.0f, percentage, 1.0f);
    }

    public void SetScore(int value)
    {
        score.text = value.ToString();
    }

    public void RemoveLife(int lifeIndex)
    {
        lifeIndex = Mathf.Clamp(lifeIndex, 0, 2);
        lives[lifeIndex].enabled = false;
    }

    private void Awake()
    {
        lives = livesContainer.GetComponentsInChildren<Image>();
    }
}