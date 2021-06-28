using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{

    public LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


    public void loadMain()
    {
        levelManager.loadTo(Level.level1, true);
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
