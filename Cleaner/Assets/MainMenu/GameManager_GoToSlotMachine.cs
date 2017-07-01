using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_GoToSlotMachine : MonoBehaviour {

    private GameManager_EventMaster eventMaster;


    private void OnEnable()
    {
        eventMaster = GetComponent<GameManager_EventMaster>();
        eventMaster.GoToSlotMachine += GoToSlotMachine;
    }

    private void OnDisable()
    {
        eventMaster = GetComponent<GameManager_EventMaster>();
        eventMaster.GoToSlotMachine -= GoToSlotMachine;
    }

    public void GoToSlotMachine()
    {
        StartCoroutine(DoFade());
        Debug.Log("슬롯머신으로~");
    }

    IEnumerator DoFade()
    {
        CanvasGroup canvasGroup = GameManager_EventMaster.instance.mainCanvasGroup;

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * 1.5f;
            yield return null;
        }

        canvasGroup.interactable = false;
        yield return null;

        GameManager_EventMaster.instance.SlotMachineActive();
    }
}
