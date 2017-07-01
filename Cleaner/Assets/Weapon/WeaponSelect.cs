using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponSelect : MonoBehaviour
{
    private static WeaponSelect instance;
    public static WeaponSelect Instance
    {
        get
        {
            if (instance == null)
            {
                return GameObject.FindObjectOfType<WeaponSelect>();
            }
            return WeaponSelect.instance;
        }
    }

    public GameObject Weapon1;
    public GameObject Weapon2;
    public GameObject Weapon3;

    public Sprite selected;
    public Sprite notSelected;
    
    public GameObject currentWeapon;
    public int currentWeaponCode;

    private void Start()
    {
        currentWeapon = Weapon1;
        Weapon1.GetComponent<Image>().sprite = selected;
        currentWeaponCode = WeaponList.DUSTER;
        //Weapon1.guiTextu
    }

    private void OnMouseUp()
    {
        if(currentWeapon == Weapon1)
        {
            currentWeapon = Weapon2;
            Weapon1.GetComponent<Image>().sprite = notSelected;
            Weapon2.GetComponent<Image>().sprite = selected;

            currentWeaponCode = WeaponList.RAG;
            TouchEvent.Instance.currentWeaponCode = this.currentWeaponCode;
            Debug.Log("currentWeaponCode : " + currentWeaponCode);
            Debug.Log("1->2");
            
        }
        else if(currentWeapon == Weapon2)
        {
            currentWeapon = Weapon3;
            Weapon2.GetComponent<Image>().sprite = notSelected;
            Weapon3.GetComponent<Image>().sprite = selected;

            currentWeaponCode = WeaponList.SCAVENGER;
            TouchEvent.Instance.currentWeaponCode = this.currentWeaponCode;
            Debug.Log("currentWeaponCode : " + currentWeaponCode);
            Debug.Log("2->3");
        }
        else if(currentWeapon == Weapon3)
        {
            currentWeapon = Weapon1;
            Weapon3.GetComponent<Image>().sprite = notSelected;
            Weapon1.GetComponent<Image>().sprite = selected;

            currentWeaponCode = WeaponList.DUSTER;
            TouchEvent.Instance.currentWeaponCode = this.currentWeaponCode;
            Debug.Log("currentWeaponCode : " + currentWeaponCode);
            Debug.Log("3->1");
        }
        else
        {
            Debug.Log("not valid weapons!");
        }
        //Debug.Log("Up " + this.name);
        


    }
}
