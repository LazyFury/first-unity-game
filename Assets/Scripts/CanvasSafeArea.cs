using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasSafeArea : MonoBehaviour
{

    HorizontalLayoutGroup horizontalLayoutGroup;
    VerticalLayoutGroup verticalLayoutGroup;
    // Start is called before the first frame update
    void Start()
    {

        horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();

        if (horizontalLayoutGroup != null)
        {
            var left = horizontalLayoutGroup.padding.left;
            horizontalLayoutGroup.padding.left = Mathf.Max(Mathf.RoundToInt(Screen.safeArea.xMin), left);
        }


        verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();

        if (verticalLayoutGroup != null)
        {
            var left = verticalLayoutGroup.padding.left;
            verticalLayoutGroup.padding.left = Mathf.Max(Mathf.RoundToInt(Screen.safeArea.xMin), left);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
