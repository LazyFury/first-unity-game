using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayAnimation : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator[] targets;
    public float delay = .5f;
    public string trigger = "trigger";

    void Start()
    {
        StartCoroutine(StartAnimation());
    }


    IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(.5f);
        foreach (var item in targets)
        {
            yield return new WaitForSeconds(delay);
            if (item != null && item.isActiveAndEnabled)
            {
                item.SetTrigger(trigger);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
