using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEvent : MonoBehaviour {

    private static TouchEvent instance;
    public static TouchEvent Instance
    {
        get
        {
            if (instance == null)
            {
                return GameObject.FindObjectOfType<TouchEvent>();
            }
            return TouchEvent.instance;
        }
    }

    public GameObject scavengerEffect;
    public GameObject dusterEffect;
    public GameObject ragEffect;

    private float[] clickTime;
    public GameObject[] currentEffect;
    public int currentWeaponCode;
    public int effectCount;
    private int MAX_EFFECT_COUNT = 10;

    private void Start()
    {
        currentEffect = new GameObject[MAX_EFFECT_COUNT];
        clickTime = new float[MAX_EFFECT_COUNT];
        currentWeaponCode = 1;
        for(int i = 0; i<MAX_EFFECT_COUNT; i++)
        {
            clickTime[i] = -1f;
        }
       
        effectCount = 0;
    }

    public void scavenger(Vector2 pos)
    {
        Collider2D[] detected = Physics2D.OverlapCircleAll(pos, 10f);//Physics.OverlapSphere(pos, 100f);
        //Physics2D.OverlapCircleAll
        Debug.Log("length : " + detected.Length);
        foreach (Collider2D col in detected)
        {

            GameObject obj = col.transform.gameObject;
            obj.transform.gameObject.name = "asdfsa";
           
            float x = MonsterMoveMent.xForce;
            float y = MonsterMoveMent.yForce;
            obj.GetComponent<Rigidbody2D>().AddRelativeForce(3000f * new Vector2(x, y));
                        //col.GetComponent<Rigidbody>().AddRelativeForce(new Vector2(x, y)
        }
    }
    void Update ()
    {
        if(effectCount == MAX_EFFECT_COUNT)
        {
            effectCount = 0;
        }

		if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)) //|| Input.GetMouseButton(0))
        {
          
            //Debug.Log("터치~");
            Plane objPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position);
            
            Ray mRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            float rayDistance;
            if( objPlane.Raycast(mRay, out rayDistance))
            {
                this.transform.position = mRay.GetPoint(rayDistance);
            }

            Debug.Log("Code : " + currentWeaponCode);

            switch (currentWeaponCode)
            {
                case 1:
                    currentEffect[effectCount] = Instantiate(dusterEffect);
                    currentEffect[effectCount].transform.position = this.transform.position;
                    currentEffect[effectCount].transform.parent = this.transform;

                    Destroy(currentEffect[effectCount], 0.5f);
                    effectCount++;
                    break;
               case 2:
                    currentEffect[effectCount] = Instantiate(ragEffect);
                    currentEffect[effectCount].transform.position = this.transform.position;
                    currentEffect[effectCount].transform.parent = this.transform;
                    Destroy(currentEffect[effectCount], 0.5f);
                    effectCount++;
                    break;
                case 3:
                    currentEffect[effectCount] = Instantiate(scavengerEffect);
                    currentEffect[effectCount].transform.position = this.transform.position;
                    currentEffect[effectCount].transform.parent = this.transform;

                    //scavenger(this.transform.position);
                    Destroy(currentEffect[effectCount], 0.5f);
                    effectCount++;
                    break;
                default:
                    break;
            }

        }

    }

}
