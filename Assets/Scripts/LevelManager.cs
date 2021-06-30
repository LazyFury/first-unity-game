using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class Level : ScriptableObject
{
    public static string launch = "Launch";
    public static string loading = "Loading";
    public static string level1 = "Level1";
    public static string setting = "Setting";
    public static string about = "About";
    public static string archive = "Archive";
}

public static class LoadLevel
{
    public static string currentLevel = Level.launch;
    public static void loadTo(string level, bool widthLoading)
    {
        currentLevel = level;
        if (widthLoading)
        {
            SceneManager.LoadScene("Loading");
        }
        else
        {
            loadTo(level);
        }
    }
    public static void loadTo(string level)
    {
        currentLevel = level;
        SceneManager.LoadScene(currentLevel);
    }
}


public class LevelManager : MonoBehaviour
{

    public Levels.Levels levels;
    public UnityEvent onFadeIn;
    public UnityEvent onFadeOut;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        onFadeOut.AddListener(switchScene);

    }

    private void OnDestroy()
    {
        onFadeOut.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {

    }
    string currentLevel = Level.launch;
    bool needLoading = false;


    public void relaunch()
    {
        loadTo(Level.launch, false);
    }
    public void loadTo(string level)
    {
        loadTo(level, false);
    }
    public void loadTo(string level, bool withLoading)
    {
        currentLevel = level;
        needLoading = withLoading;
        animator.SetTrigger("fadeOut");
    }


    public void loadLevel()
    {
        loadLevel(levels.currentLevel);
    }
    public void loadLevel(int i)
    {
        levels.currentLevel = i;
        currentLevel = levels.scenes[i].sceneName;
        needLoading = true;
        animator.SetTrigger("fadeOut");
    }

    public void nextLevel()
    {
        var i = levels.currentLevel + 1;
        if (i > levels.scenes.Length - 1)
        {
            return;
        }
        loadLevel(i);
    }
    public void triggerfadeOut()
    {
        currentLevel = "";
        animator.SetTrigger("fadeOut");
    }

    void switchScene()
    {
        if (currentLevel != "") LoadLevel.loadTo(currentLevel, needLoading);
    }


    public void _onFadeIn()
    {
        onFadeIn.Invoke();
    }

    public void _onFadeOut()
    {
        onFadeOut.Invoke();
        onFadeOut.RemoveListener(switchScene);
    }
}
