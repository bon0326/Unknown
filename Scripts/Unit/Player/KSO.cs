using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KSO : Player
{
    public Gun equipGun;
    public Gun[] possibleGuns;
    public ParticleSystem explosionParticle;
    public GameObject angle;
    public int bulletCount; //스테이지마다 주어지는 총알
    public Text bulletText; //총알량 표시
    private ParticleSystem orginalAttackParticle;
    //이동 시작할 때 필요한거, 이동 끝나면 멈춰야 하기 때문에 넣음
    private bool moveit = false;
    //이동할 지점 저장할 벡터
    private Vector3 targetpos;
    //총알이 이동할 방향
    private int usedBullet = 0; //사용한 총알; 공격을 수행하는 함수에서 사용
    private bool sniper;
    private Vector3 bulletpos;
    private Vector3 attackpos;
    private int attackcount = 2;

    protected override void Start()
    {
        base.Start();
        if (playerType == PlayerType.PLAYER_DEFAULT) playerType = PlayerType.ARCHER;
        originSpeed = speed;
        orginalAttackParticle = attackParticle;
        //bulletText = FindObjectOfType<Text>();
        usedBullet = bulletCount;
        bulletText.text = usedBullet + " / " + bulletCount;
        sniper = false;

        gameManager.player = this;
    }

    protected override void Update()
    {
        
        base.Update();
       // bulletStart.Set(transform.position.x + 0.5f, transform.position.y, transform.position.z);
        //땅 찍은 좌표 반환
        if (Input.GetMouseButtonUp(0))
        {
            targetpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moveit = true;
        }

        if (moveit == true)
        {
            Move();
        }
        
        if(gameManager.enemyTouch) // 적을 터치했다면
        {
            if (sniper)
                angle.SetActive(true);
            attackcount--;
            if (attackcount == 0)
            {
                Attack(); // 공격
            }
            gameManager.enemyTouch = false;
            // 터치 판단 원상 복구
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) // 터치하면
        {
            //Move(); // 목적지로 이동
        }

    }

    public override void Attack()
    {
        Vector3 temp;
        bulletpos.Set(angle.transform.position.x, angle.transform.position.y, -10);
        attackpos = transform.position;

        angle.SetActive(false);
        base.Attack();
        //animator.SetTrigger("Attack"); // 애니메이션의 공격 트리거 호출
        if (usedBullet == 0)
            Debug.Log("Dont Attack");
        else
        {
            Debug.Log("attack " + gameManager.enemyTouch);
            if (sniper)
            {
                Instantiate(equipGun, transform.position, Quaternion.LookRotation(attackpos - bulletpos, Vector3.forward)); // 화살 객체화
                usedBullet--;
            }
            else
            {
                if (gameManager.enemyTouch)
                {
                    temp = new Vector3(targetEnemy.transform.position.x, targetEnemy.transform.position.y, -10);
                    Debug.Log(targetEnemy.transform.position);
                    Instantiate(equipGun, transform.position, Quaternion.LookRotation(attackpos - temp, Vector3.forward)); // 화살 객체화
                    usedBullet--;
                }
            }
        }
        attackcount = 2;
        bulletText.text = usedBullet + " / " + bulletCount;
    }

    public override void Move()
    {
        // 부드럽지만 신속하게 이동
        //transform.position = Vector2.Lerp(transform.position, targetpos, 0.01f);

        // 딱딱한 이동
        animator.SetBool("Magnitude 0", true);
        animator.SetFloat("Horizontal", (targetpos.x - transform.position.x) * 0.1f);
        animator.SetFloat("Vertical", (targetpos.y - transform.position.y) * 0.1f);
        //animator.SetFloat("Magnitude", (targetpos.magnitude - transform.position.magnitude) * 0.1f);

        transform.position = Vector2.MoveTowards(transform.position, targetpos, Time.deltaTime * speed);
        
        // 벡터간 거리를 재어보고
        float dis = Vector2.Distance(transform.position, targetpos);

        // OK! 이동 완료! 멈추자.
        if (dis < 0.001f)
        {
            moveit = false;
            animator.SetBool("Magnitude 0", false);
        }
        base.Move();
    }

    /*public void ChangeArrow(int index)
    {
        equipArrow = possibleArrows[index];
    }*/

    public override void Init()
    {
        base.Init();
        attackParticle = orginalAttackParticle;
    }

    public void Reload() {
        usedBullet = bulletCount;
        bulletText.text = usedBullet + " / " + bulletCount;
    }

    public void ChangeMode() {
        Debug.Log("change");
        if (sniper)
            sniper = false;
        else
            sniper = true;
    }

    public bool GetCurrentMode() {
        return sniper;
    }
    
}
