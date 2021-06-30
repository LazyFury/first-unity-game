using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class RewardsController : MonoBehaviour
{
    public ScoreManager scoreManager;
    public int RewardsScore = 5;
    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(nameof(GameManager.Tag.Player)))
        {
            scoreManager.score += RewardsScore;
            Destroy(gameObject, .2f);
        }
    }
}
