using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterSpawn : MonoBehaviour
{
    private static MonsterSpawn instance;
    public static MonsterSpawn Instance
    {
        get
        {
            if (instance == null)
            {
                return GameObject.FindObjectOfType<MonsterSpawn>();
            }
            return MonsterSpawn.instance;
        }
    }

    public GameObject warpMon;
    public GameObject tornadoMon;
    public GameObject steadyMon;
    public GameObject normalMon;
    public GameObject packMon;
    public GameObject goldMon;
    public GameObject cloneMon;

    private int attackCount = 0;
    private GameObject warp;
    private GameObject tornado;
    private GameObject steady;
    private GameObject normal;
    private GameObject pack;
    private GameObject gold;
    private GameObject clone;

    public GameObject top;

    // 몬스터 개수
    public int waMon;
    public int torMon;
    public int steMon;
    public int norMon;
    public int pacMon;
    public int golMon;
    public int cloMon;

    void Awake()
    {
        makeMonster(warpMon, "warpMon", ref waMon);
        makeMonster(tornadoMon, "tornadoMon", ref torMon);
        makeMonster(steadyMon, "steadyMon", ref steMon);
        makeMonster(normalMon, "normalMon", ref norMon);
        makeMonster(packMon, "packMon", ref pacMon);
        makeMonster(goldMon, "goldMon", ref golMon);
        makeMonster(cloneMon, "cloneMon", ref cloMon);

      
    }

    void makeMonster(GameObject mon, string monName, ref int num)
    {
        while (num > 0)
        {
            clone = Instantiate(mon, this.transform);
            float randomX = Random.Range(-Screen.width / 2, Screen.width / 2);
            float randomY = Random.Range(-100, Screen.height / 2 - 190);
            clone.transform.localPosition = new Vector3(randomX, randomY, 0);
            clone.transform.parent = this.transform;
            clone.name = monName;
            clone.SetActive(true);
            num--;
        }
    }

    /*public void makeMonsterSet()
    {
        warp = Instantiate(warpMon, this.transform);
        warp.transform.localPosition = new Vector3(this.transform.position.x, this.transform.position.y, -3973);
        warp.transform.parent = this.transform;
        warp.name = "warpMon";
        warp.SetActive(true);

        tornado = Instantiate(tornadoMon, this.transform);
        tornado.transform.localPosition = new Vector3(this.transform.position.x, this.transform.position.y, -3973);
        tornado.transform.parent = this.transform;
        tornado.name = "tornadoMon";
        tornado.SetActive(true);

        steady = Instantiate(steadyMon, this.transform);
        steady.transform.localPosition = new Vector3(this.transform.position.x, this.transform.position.y, -3973);
        steady.transform.parent = this.transform;
        steady.name = "steadyMon";
        steady.SetActive(true);

        
        normal = Instantiate(normalMon, this.transform);
        normal.transform.localPosition = new Vector3(this.transform.position.x, this.transform.position.y, -3973);
        normal.transform.parent = this.transform;
        normal.name = "normalMon";
        normal.SetActive(true);
    }*/

    /*public void monsterAttack()
    {
        Debug.Log("공격~");
        attackCount++;
        if (attackCount > 3)
        {
            Debug.Log("제거합니다");
            Destroy(warp);
        }
    }*/
}