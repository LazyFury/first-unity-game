using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

[ExecuteInEditMode]
public class ButtonAutoSetting : MonoBehaviour
{

    public GameObject prefabs;
    Button prefabsButton;
    Image prefabsImage;
    public string label;
    public RectOffset padding;
    public bool autoWidth = true;
    public UnityEvent onClick;
    // Start is called before the first frame update
    TextMeshProUGUI textMesh;
    HorizontalLayoutGroup horizontalLayoutGroup;
    RectTransform prefsRectTransform;
    RectTransform rectTransform;

    private void OnEnable()
    {

        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            throw new System.Exception("need rectTransform");
        }
        if (prefabs == null)
        {
            prefabs = transform.GetChild(0).gameObject;
        }
        if (prefabs == null)
        {
            throw new System.Exception("need prefabd");
        }
        prefabsImage = prefabs.GetComponent<Image>();
        prefabsButton = prefabs.GetComponent<Button>();
        prefabsButton.onClick.AddListener(onClickListener);
        textMesh = prefabs.GetComponentInChildren<TextMeshProUGUI>();
        horizontalLayoutGroup = prefabs.GetComponent<HorizontalLayoutGroup>();
        prefsRectTransform = prefabs.GetComponent<RectTransform>();
    }
    void onClickListener()
    {
        onClick.Invoke();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = label;
        prefsRectTransform.sizeDelta = new Vector2(autoWidth ? textMesh.preferredWidth + padding.left + padding.right : prefsRectTransform.sizeDelta.x, textMesh.preferredHeight + padding.top + padding.bottom);

        horizontalLayoutGroup.padding = padding;
        // 修改父元素size 和 位置对齐
        rectTransform.sizeDelta = prefsRectTransform.sizeDelta;
        prefsRectTransform.transform.position = rectTransform.transform.position;
    }
}
