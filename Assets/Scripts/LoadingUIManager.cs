using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingUIManager : MonoBehaviour
{


    public LevelManager levelManager;
    public Text text;
    public float fixedSpeed = .05f;
    int progress;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "90%";
        StartCoroutine(loadScene());
    }

    IEnumerator loadScene()
    {
        yield return new WaitForSeconds(fixedSpeed);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(LoadLevel.currentLevel);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            int _progress = Mathf.CeilToInt(asyncOperation.progress * 100);
            while (_progress - progress > 0)
            {
                progress++;
                yield return new WaitForSeconds(fixedSpeed);
            }
            if (_progress >= 90)
            {
                while (progress < 99)
                {
                    progress++;
                    yield return new WaitForSeconds(fixedSpeed);
                }
                progress++;
                yield return new WaitForSeconds(.5f);
                levelManager.triggerfadeOut();
                yield return new WaitForSeconds(1f);
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        text.text = progress == 100 ? "Done." : progress + "%";
    }
}
