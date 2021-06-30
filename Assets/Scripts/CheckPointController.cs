using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CheckPointController : MonoBehaviour
{

    public UnityEvent OnPlayerTriggerCheckPoint;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(nameof(GameManager.Tag.Player)))
        {
            OnPlayerTriggerCheckPoint?.Invoke();
        }
    }

}
