using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPos : MonoBehaviour
{

    [Header("속도, 반지름")]

    [SerializeField]
    [Range(0f, 10f)]
    private float speed = 1;
    [SerializeField] [Range(0f, 10f)] private float radius = 1;

    private float runningTime = 0;
    private Vector2 newPos = new Vector2();
    private float tmp = 0;
    private float angle;
    private Vector3 destPos;
    private Vector3 angleP;
    private Vector3 angleT;
    public GameObject player;
    float a;

    // Use this for initialization
    void Start()
    {
       
    }

    void Update()
    {
        Rotation();
       
        if (Input.GetMouseButtonUp(0))
        {
            angleP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            angleT = player.transform.position;

            a = Mathf.Atan2(Camera.main.ScreenToWorldPoint(Input.mousePosition).y - player.transform.position.y, Camera.main.ScreenToWorldPoint(Input.mousePosition).x - player.transform.position.x) * Mathf.Rad2Deg;
            if (a < 0)
            {
                a = a + 360;
            }
            angle = 6.4f * (a/360) ;
            Debug.Log(a);
            //Debug.Log(angleP);
        }

        if (runningTime <= -0.8f)
        {
            tmp = 0;
        }
        else if (runningTime >= 0.8f)
            tmp = 1;

        if(tmp == 0)
        {
            runningTime += Time.deltaTime * speed;
        }
        else
            runningTime -= Time.deltaTime * speed;

        /*
        if (Mathf.Abs(angleP.x-angleT.x) > Mathf.Abs(angleP.y - angleT.y)) 
        {
            if (angleP.x - angleT.x < 0)
            {
                angle = 3.2f;
            }
            else
            {
                angle = 0;
            }
        }
        else if (Mathf.Abs(angleP.x - angleT.x) < Mathf.Abs(angleP.y - angleT.y))
        {
            if (angleP.y - angleT.y < 0)
            {
                angle = 4.8f;
            }
            else
            {
                angle = 1.6f;
            }
        }
       */

        float x = player.transform.position.x + radius * Mathf.Cos(angle + runningTime);
        float y = player.transform.position.y + radius * Mathf.Sin(angle + runningTime);
        newPos = new Vector2(x, y);
        this.transform.position = newPos;

    }

    public void Rotation()
    {
        destPos.Set(transform.position.x, transform.position.y, -10);
        //destPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(player.transform.position - destPos, Vector3.forward);
    }
}
