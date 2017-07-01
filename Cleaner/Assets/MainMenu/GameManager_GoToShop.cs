using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_GoToShop : MonoBehaviour {

    private GameManager_EventMaster eventMaster;
    //public GameObject ShopUI;

    private void OnEnable()
    {
        eventMaster = GetComponent<GameManager_EventMaster>();
        eventMaster.GoToShop += GoToShop;
    }

    private void OnDisable()
    {
        eventMaster = GetComponent<GameManager_EventMaster>();
        eventMaster.GoToShop -= GoToShop;
    }

    public void GoToShop()
    {
        StartCoroutine(DoFade());
        Debug.Log("상점으로~");
    }

    IEnumerator DoFade()
    {
        CanvasGroup canvasGroup = GameManager_EventMaster.instance.mainCanvasGroup;

        while(canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * 1.5f;
            yield return null;
        }

        canvasGroup.interactable = false;
        yield return null;

        GameManager_EventMaster.instance.ShopActive();
    }
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
