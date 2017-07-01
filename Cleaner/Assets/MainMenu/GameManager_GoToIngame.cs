using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_GoToIngame : MonoBehaviour {

    private GameManager_EventMaster eventMaster;


    private void OnEnable()
    {
        eventMaster = GetComponent<GameManager_EventMaster>();
        eventMaster.GoToIngame += GoToIngameScene;
    }

    private void OnDisable()
    {
        eventMaster = GetComponent<GameManager_EventMaster>();
        eventMaster.GoToIngame -= GoToIngameScene;
    }

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GoToIngameScene()
    {
        Debug.Log("인게임으로~");
        StartCoroutine(DoFade());
        //SceneManager.LoadScene("ingame");
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

        GameManager_EventMaster.instance.MonsterActive();
    }
}
