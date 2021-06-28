using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum Tag
    {
        Player,
        Enemy
    }

    public HealthyManager healthyManager;
    public LevelManager levelManager;


    // Start is called before the first frame update
    private void Start()
    {
        healthyManager = GameObject.FindObjectOfType<HealthyManager>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
