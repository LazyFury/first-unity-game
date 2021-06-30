using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollowController : MonoBehaviour
{

    public GameObject follow;
    Vector3 followLastPosition;
    public float speed = .2f;
    Vector3 target = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(calcFollow());
    }

    IEnumerator calcFollow()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (followLastPosition == null || followLastPosition == Vector3.zero)
            {
                yield return null;
            }
            target = (follow.transform.position - followLastPosition) * speed;
            followLastPosition = follow.transform.position;
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position -= target;
    }
}
