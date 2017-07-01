using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_EventMaster : MonoBehaviour {

    public delegate void MoveEvent();

    public event MoveEvent GoToIngame;
    public event MoveEvent GoToSlotMachine;
    public event MoveEvent GoToShop;
    public event MoveEvent GoToMainMenu;

    public GameObject shopCanvas;
    public GameObject mainCanvas;
    public GameObject slotMachineCanvas;
    public CanvasGroup shopCanvasGroup;
    public CanvasGroup mainCanvasGroup;
    public CanvasGroup slotMachineCanvasGroup;

    public GameObject monsterCanvas;
    public CanvasGroup monsterCanvasGroup;

    public static GameManager_EventMaster instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        mainCanvasGroup.alpha = 1f;
        mainCanvas.SetActive(true);

        shopCanvasGroup.alpha = 0f;
        shopCanvas.SetActive(false);

        slotMachineCanvasGroup.alpha = 0f;
        slotMachineCanvas.SetActive(false);

        monsterCanvasGroup.alpha = 0f;
        monsterCanvas.SetActive(false);

    }
 

    public void goIngame()
    {
        if (GoToIngame != null)
        {
            //Debug.Log("인게임으로1");
            GoToIngame();
        }
        //Debug.Log("인게임으로2");
    }

    public void goSlot()
    {
        if (GoToSlotMachine != null)
            GoToSlotMachine();
        //Debug.Log("슬롯머신으로");
    }

    public void goShop()
    {
        if (GoToShop != null)
            GoToShop();
    }

    public void goMainMenu()
    {
        if (GoToMainMenu != null)
        {
            GoToMainMenu();
        }
        //Debug.Log("메인메뉴로");
    }

    public void ShopActive()
    {
        shopCanvasGroup.alpha = 1f;
        shopCanvasGroup.interactable = true;
        shopCanvas.SetActive(true);
        mainCanvas.SetActive(false);
        slotMachineCanvas.SetActive(false);
        //shopCanvas.enabled = true;
        //mainCanvas.enabled = false;

    }

    public void MainActive()
    {
        mainCanvasGroup.alpha = 1f;
        mainCanvasGroup.interactable = true;
        mainCanvas.SetActive(true);
        shopCanvas.SetActive(false);
        slotMachineCanvas.SetActive(false);
        monsterCanvas.SetActive(false);
        //mainCanvas.enabled = true;
        //shopCanvas.enabled = false;
    }

    public void SlotMachineActive()
    {
        slotMachineCanvasGroup.alpha = 1f;
        slotMachineCanvasGroup.interactable = true;
        slotMachineCanvas.SetActive(true);
        mainCanvas.SetActive(false);
        shopCanvas.SetActive(false);
        monsterCanvas.SetActive(false);
    }

    public void MonsterActive()
    {
        monsterCanvasGroup.alpha = 1f;
        monsterCanvasGroup.interactable = true;
        monsterCanvas.SetActive(true);
        mainCanvas.SetActive(false);
        shopCanvas.SetActive(false);
        slotMachineCanvas.SetActive(false);
        //mainCanvas.enabled = true;
        //shopCanvas.enabled = false;
    }
}
