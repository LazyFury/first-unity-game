using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

public class GameManager : MonoBehaviour
{

    public enum Tag
    {
        Player,
        Enemy,
        rewards
    }
    public static ArchivesList archives = new ArchivesList();
    public Archives defArchive;

    public Archives archive;
    public HealthyManager healthyManager;
    public LevelManager levelManager;
    public ScoreManager scoreManager;
    public PlayerController player;
    public bool init = false;
    public UnityEvent DeathEvent;

    // Start is called before the first frame update
    private void Start()
    {
        healthyManager = GameObject.FindObjectOfType<HealthyManager>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        scoreManager = GameManager.FindObjectOfType<ScoreManager>();
        player = GameManager.FindObjectOfType<PlayerController>();
        DeathEvent.AddListener(onDeath);
        if (init)
        {
            archive = archives.archive(defArchive);
            getHealthyAndUpdate();
            if (levelManager != null)
            {
                getLevelAntUpdate();
                levelManager.levels.onLevelChange += updateLevel;
            }
        }
    }

    private void getLevelAntUpdate()
    {
        levelManager.levels.currentLevel = archive.level;
    }

    private void getHealthyAndUpdate()
    {
        if (healthyManager != null)
        {
            healthyManager.MaxHealthy = archive.maxHealthy;
            healthyManager.healthy = archive.healthy;
            healthyManager.onChange += onHealthyChange;
            healthyManager.UpdateMaxHealthy();
        }
    }

    void onDeath()
    {
        Debug.Log("u are die.");
        player.death();
    }

    void onHealthyChange(int i)
    {
        if (i <= 0)
        {
            DeathEvent?.Invoke();
        }
    }
    void updateLevel(int i)
    {
        archive.level = i;
        if (i > archive.level)
        {
            //升级
            archive.maxHealthy++;
            archive.healthy = archive.maxHealthy;
            archive.score = scoreManager.score;
        }
        Save();
    }
    void Save()
    {
        archive.save(archives);
    }

    public void newArchive()
    {
        archive = archives.newArchive(defArchive, System.DateTime.Now.ToString());
        getLevelAntUpdate();
    }

    public void allArchives()
    {
        Debug.Log(archives.keysToString());
    }

    // Update is called once per frame
    void Update()
    {

    }


}
