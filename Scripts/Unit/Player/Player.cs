using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    [HideInInspector] public enum PlayerType { PLAYER_DEFAULT = -1, ARCHER = 0, WARRIOR = 1 }
    [HideInInspector] public Enemy targetEnemy;
    [HideInInspector] public Vector3 targetPos;
    public PlayerType playerType;

    protected Touch touch;
    protected Vector3 destPos; // 터치 좌표 (목적지)

    protected override void Start()
    {
        base.Start();

        isMove = false;
        type = UnitType.PLAYER;

        gameManager = FindObjectOfType<GameManager>();
        touch = gameManager.touch;

        //DontDestroyOnLoad(gameObject);
    }

    protected virtual void Update()
    {
        CheckDie();

        if (isMove) // 움직이는 객체이면
        {
            MoveToDest(); // 목적지로 이동
        }
    }

    protected void MoveToDest()
    {
        
        //transform.Translate(0, speed * Time.deltaTime, 0); // 목적지로 이동
    }

    public void Move(bool option) // 이동해야하는가 여부 판단
    {
        if (option) isMove = true;
        else isMove = false;
    }

    //private void CheckTarget()
    //{
    //    if (targetEnemy.gameObject == null) targetEnemy = null;
    //}

    public override void LossHp(float damage)
    {
        base.LossHp(damage);
       // gameManager.ScreenHitEffect(true); // 피격당하면 화면에 피격 효과 보여주기
        Invoke("LossHpEffectOff", 0.5f); // 0.5초 후에 해당 효과 끄기
        gameManager.CheckGameOver();
    }

    private void LossHpEffectOff()
    {
        //gameManager.ScreenHitEffect(false);
    }

    public void SpeedOn(bool on)
    {
        if (on) speed = originSpeed;
        else speed = 0;
    }

    public virtual void Init()
    {
        hp = originalHp;
        transform.position = new Vector3(5.3f, -10.3f);
    }
}
