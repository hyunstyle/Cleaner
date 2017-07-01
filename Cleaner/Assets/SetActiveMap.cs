using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveMap : MonoBehaviour {

    private static SetActiveMap instance;
    public static SetActiveMap Instance
    {
        get
        {
            if (instance == null)
            {
                return GameObject.FindObjectOfType<SetActiveMap>();
            }
            return SetActiveMap.instance;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("trigger name : " + collision.transform.name);
        if(collision.transform.name == "TopColider")
        {
            Debug.Log("Active");
        }
    }
}
