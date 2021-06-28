using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AwlController : MonoBehaviour
{

    public Sprite newImage;
    SpriteRenderer image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(nameof(GameManager.Tag.Player)))
        {
            image.sprite = newImage;
        }
    }
}
