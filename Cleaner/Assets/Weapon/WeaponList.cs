using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponList : MonoBehaviour {

    private static WeaponList instance;
    public static WeaponList Instance
    {
        get
        {
            if (instance == null)
            {
                return GameObject.FindObjectOfType<WeaponList>();
            }
            return WeaponList.instance;
        }
    }

    
    public static int DUSTER = 1;
    public static int RAG = 2;
    public static int SCAVENGER = 3;
}
