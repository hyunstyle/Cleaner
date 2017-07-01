using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_GoToMainMenu : MonoBehaviour {

    private GameManager_EventMaster eventMaster;

    private void OnEnable()
    {
        eventMaster = GetComponent<GameManager_EventMaster>();
        eventMaster.GoToMainMenu += GoToMainMenu;
    }

    private void OnDisable()
    {
        eventMaster = GetComponent<GameManager_EventMaster>();
        eventMaster.GoToMainMenu -= GoToMainMenu;
    }

    public void GoToMainMenu()
    {
        StartCoroutine(DoFade());
        Debug.Log("메인메뉴로~");
    }

    IEnumerator DoFade()
    {
        CanvasGroup canvasGroup;
        if (GameManager_EventMaster.instance.shopCanvas.activeSelf == true) // Main->shop 와있는 상태
            canvasGroup = GameManager_EventMaster.instance.shopCanvasGroup;
        else // Main->slotMachine 와있는 상태
            canvasGroup = GameManager_EventMaster.instance.slotMachineCanvasGroup;

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * 1.5f;
            yield return null;
        }

        canvasGroup.interactable = false;
        yield return null;

        GameManager_EventMaster.instance.MainActive();
    }
}
