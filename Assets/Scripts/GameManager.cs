using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public enum Tag
    {
        Player,
        Enemy
    }
    public static ArchivesList archives = new ArchivesList();
    public Archives defArchive;

    private Archives _archive;
    public Archives archive
    {
        get
        {
            if (_archive == null)
            {
                _archive = archives.archive(defArchive);
            }
            return _archive;
        }
    }
    public HealthyManager healthyManager;
    public LevelManager levelManager;
    public bool init = false;

    // Start is called before the first frame update
    private void Init()
    {

        getHealthyAndUpdate();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        levelManager.levels.currentLevel = archive.level;
        levelManager.levels.onLevelChange += updateLevel;

    }

    private void getHealthyAndUpdate()
    {
        healthyManager = GameObject.FindObjectOfType<HealthyManager>();
        if (healthyManager != null)
        {
            healthyManager.MaxHealthy = archive.maxHealthy;
            healthyManager.healthy = archive.healthy;
            healthyManager.UpdateMaxHealthy();
        }
    }

    void updateLevel(int i)
    {
        archive.level = i;
        Save();
    }
    void Save()
    {
        archive.save(archives);
    }

    public void newArchive()
    {
        _archive = archives.newArchive(defArchive, System.DateTime.Now.ToString());
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
