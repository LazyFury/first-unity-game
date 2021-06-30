using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{

    public GameManager gameManager;
    public LevelManager levelManager;

    public GameObject continueBtn;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (GameManager.archives.hasCurrentArchive())
        {
            continueBtn.SetActive(true);
            continueBtn.GetComponent<ButtonAutoSetting>().label = "continue " + GameManager.archives.currentKey;
            continueBtn.GetComponentInChildren<Animator>().SetTrigger("fadeIn");
        }
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


    public void loadNew()
    {
        gameManager.newArchive();
        levelManager.loadLevel();
    }

    public void loadArchive()
    {
        levelManager.loadTo(Level.archive);
    }

    public void loadContinue()
    {
        if (!GameManager.archives.hasCurrentArchive())
        {
            gameManager.newArchive();
        }
        levelManager.loadLevel();
    }
    public void loadSetting()
    {
        levelManager.loadTo(Level.setting);
    }
    public void loadAbout()
    {
        levelManager.loadTo(Level.about);
    }
    public void loadbackUp()
    {
        levelManager.loadTo(Level.setting);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
