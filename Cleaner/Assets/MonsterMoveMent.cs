using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoveMent : MonoBehaviour
{
    public float MIN_SPEED = 1000f;
    public float MAX_SPEED = 2000f;
    public float directionChangeTime = 2f;

    private float PdirectionChangeTime;
    Rigidbody2D obj;

    public static float xForce;
    public static float yForce;

    private void Start()
    {
        obj = GetComponent<Rigidbody2D>();
        PdirectionChangeTime = directionChangeTime;
        Push();
    }

    private void FixedUpdate()
    {
        PdirectionChangeTime -= Time.deltaTime;
        if (PdirectionChangeTime < 0)
        {
            //Debug.Log("Call");
            Push();
            PdirectionChangeTime = directionChangeTime;
        }
    }
    
        
    

    public void Push()
    {
        float speed = Random.Range(MIN_SPEED, MAX_SPEED);
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);

        xForce = x;
        yForce = y;
        obj.AddRelativeForce(speed * new Vector2(x, y));
        
       
    }

    //public void 
}
