using UnityEngine;
using System.Collections;

public class Trash
{
    private string name = "_(trash)___del_____";
    public GameObject gameObject
    {
        get
        {
            var trash = GameObject.Find(name);
            if (trash == null)
            {
                trash = new GameObject(name);
            }
            return trash;
        }
    }
}
