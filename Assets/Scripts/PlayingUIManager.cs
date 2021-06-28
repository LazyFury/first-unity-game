using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingUIManager : MonoBehaviour
{
    public Animator animator;
    public LevelManager levelManager;
    public GameObject setting;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>();
    }

    public void showMenu()
    {
        animator.SetTrigger("show");
    }
    public void hideMenu()
    {
        animator.SetTrigger("hide");
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
