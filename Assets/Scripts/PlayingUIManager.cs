using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayingUIManager : MonoBehaviour
{
    public Animator animator;
    public LevelManager levelManager;
    public GameObject setting;

    public GameManager gameManager;
    public TMPro.TextMeshProUGUI levelName;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>();
        gameManager = FindObjectOfType<GameManager>();

        levelName.text = levelManager.levels.scenes[gameManager.archive.level].name;
    }

    public void showMenu()
    {
        animator.SetTrigger("show");
        Time.timeScale = 0;
    }
    public void hideMenu()
    {
        animator.SetTrigger("hide");
        Time.timeScale = 1;
    }

    public void showDie()
    {
        animator.SetTrigger("show_die");
    }
    public void hideDie()
    {
        animator.SetTrigger("hide_die");
    }

    public void reLaunch()
    {
        levelManager.loadTo(Level.launch);
    }

    public void openSetting()
    {
        setting.SetActive(true);
    }
    public void hideSetting()
    {
        setting.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
