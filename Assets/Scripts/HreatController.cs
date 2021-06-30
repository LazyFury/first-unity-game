using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HreatController : MonoBehaviour
{

    public HealthyManager healthyManager;
    // Start is called before the first frame update
    void Start()
    {
        healthyManager = FindObjectOfType<HealthyManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(nameof(GameManager.Tag.Player)))
        {
            healthyManager.healthy += 1;
            Destroy(gameObject, .1f);
        }
    }
}
