using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    
    private int attackCount;
    public Text text;
    private Animator Monster;
    private bool isHit = false;
    private float hitTime;

    private void Start()
    {
        Monster = gameObject.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if(isHit)
        {
            if(hitTime + 0.7f < Time.time)
            {
                isHit = false;
                Monster.SetBool("Hit", false);
            }
        }
    }
    private void OnMouseDown()
    {
        isHit = true;
        hitTime = Time.time;

        Monster.SetBool("Hit", true);
        ScoreBoard.Instance.score += 10;
        ScoreBoard.Instance.scoreText.text = "Score : " + ScoreBoard.Instance.score.ToString();
        Debug.Log("으악");
        
    }

    private void OnMouseUp()
    {
        
        Debug.Log("Up " + this.name);
        attackCount++;
        if(attackCount >= 3)
        {
            Destroy(this.gameObject);
        }
       
     
    }
}
