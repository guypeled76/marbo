using System;
using UnityEngine;

public class Utils
{

    public static GameObject CloneObject(GameObject original, GameObject parent)
    {
        return GameObject.Instantiate(original, parent.transform.position, parent.transform.rotation, parent.transform);
    }


    public static T CloneComponent<T>(T original, Component parent) where T : Component
    {
        return CloneObject(original.gameObject, parent.gameObject).GetComponent<T>();
    }
}

