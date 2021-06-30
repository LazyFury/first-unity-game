using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreText;
    public GameManager gameManager;

    public int score;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        score = gameManager.archive.score;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();

    }
}
