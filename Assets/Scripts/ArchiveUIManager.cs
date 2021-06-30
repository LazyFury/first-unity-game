using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchiveUIManager : MonoBehaviour
{

    public ButtonAutoSetting button;
    public GameObject content;

    public Animator plane;
    public TMPro.TextMeshProUGUI text;
    public GameManager gameManager;
    public LevelManager levelManager;
    Trash trash = new Trash();
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        levelManager = FindObjectOfType<LevelManager>();
        generateBtns();
    }

    private void generateBtns()
    {
        RectTransform contentRect = content.GetComponent<RectTransform>();
        removeAllChilds();


        var list = GameManager.archives.keys;
        if (list.Count <= 0) return;
        list.Reverse();
        for (int i = 0; i < list.Count; i++)
        {
            var key = list[i];
            var btn = Instantiate(button, content.transform);
            btn.label = key;
            btn.gameObject.SetActive(true);
            void onClick()
            {
                showPlane(i, key);
            }
            btn.onClick.AddListener(onClick);
            contentRect.sizeDelta += new Vector2(0, btn.GetComponent<RectTransform>().sizeDelta.y + 20);
        }
    }

    public void load()
    {
        GameManager.archives.archive(gameManager.defArchive, index);
        levelManager.loadLevel();
    }

    private void removeAllChilds()
    {
        var childCount = content.transform.childCount;
        List<GameObject> childs = new List<GameObject>();
        for (int i = 0; i < childCount; i++)
        {
            var child = content.transform.GetChild(i);
            childs.Add(child.gameObject);
        }

        foreach (GameObject child in childs)
        {
            child.transform.SetParent(trash.gameObject.transform);
            Destroy(child);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    int index = -1;
    public void showPlane(int i, string s)
    {
        index = i;
        text.text = s;
        plane.SetTrigger("show");
    }

    public void hidePlane()
    {
        plane.SetTrigger("hide");
    }

    public void delArchive()
    {
        GameManager.archives.delKey(GameManager.archives.keys.Count - index);
        hidePlane();

        generateBtns();
    }

    public void clear()
    {
        GameManager.archives.cleanAll();
        generateBtns();
    }

}
