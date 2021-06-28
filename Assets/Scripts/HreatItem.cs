using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class HreatItem : MonoBehaviour
{
    public enum Status { Live, Death};
    public Status status;
    public Sprite liveImage;
    public Sprite deathImage;
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (status.Equals(Status.Live))
        {
            image.sprite = liveImage;
        }else if (status.Equals(Status.Death))
        {
            image.sprite = deathImage;
        }
    }
}
