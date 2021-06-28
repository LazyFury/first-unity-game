using UnityEngine;


[ExecuteInEditMode]
public class HealthyManager : MonoBehaviour
{
    public int MaxHealthy = 5;
    [SerializeField]
    private int Healthy = 5;
    //when use code set healthy, update ui
    public int healthy
    {
        get { return Healthy; }
        set
        {
            Healthy = value;
            _UpdateHealthy();
        }
    }


    [SerializeField]
    private GameObject heartPrefabs;
    public GameObject ParentTarget;
    private Trash trash = new Trash();

    // Start is called before the first frame update
    void Start()
    {
        UpdateMaxHealthy();
        UpdateHealthy();
    }


    // Update is called once per frame
    void Update()
    {
        UpdateHealthy();
    }

    public void HealthyUp(int _healthy)
    {
        MaxHealthy += _healthy;
        UpdateMaxHealthy();
        healthy = MaxHealthy;
    }

    private void UpdateHealthy()
    {
        healthy = Healthy;
    }


    private void _UpdateHealthy()
    {
        var len = ParentTarget.transform.childCount;
        for (int i = 0; i < len; i++)
        {
            HreatItem hreatItem = ParentTarget.transform.GetChild(i).gameObject.GetComponent<HreatItem>();
            if (hreatItem != null)
            {
                if (i < Healthy)
                {
                    hreatItem.status = HreatItem.Status.Live;
                }
                else
                {
                    hreatItem.status = HreatItem.Status.Death;
                }
            }

        }
    }

    void destoryChild(int i)
    {
        Transform child = ParentTarget.transform.GetChild(i);
        child.SetParent(trash.gameObject.transform);
        DestroyImmediate(child.gameObject, true);
    }
    public void UpdateMaxHealthy()
    {

        var len = ParentTarget.transform.childCount;
        if (MaxHealthy > len)
        {
            for (int i = 0; i < (MaxHealthy - len); i++)
            {
                Instantiate(heartPrefabs, ParentTarget.transform);
            }
        }
        else if (MaxHealthy < len)
        {
            for (int i = 0; i < (len - MaxHealthy); i++)
            {
                destoryChild(i);
            }
        }

        //MaxHealthy
        if (healthy > MaxHealthy)
        {
            healthy = MaxHealthy;
        }
        else
        {
            UpdateHealthy();
        }
    }
}
