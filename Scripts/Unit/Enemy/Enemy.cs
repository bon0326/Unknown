using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public EnemyType enemyType;
    [HideInInspector] public enum EnemyType { ENEMY_DEFAULT = -1, NORMAL = 0, MIDDLE = 1, BOSS = 2 }
    [HideInInspector] public Player targetPlayer; // 어그로 끌린 플레이어
    [HideInInspector] public bool isStatic; // 고정몹인가?
    [HideInInspector] public enum Status { dead = -1, search = 0, battle = 1 }//상태표현
    protected Status status;
    public float SearchAngle;    //탐지범위의 각
    public float SeachDistance; //탐지범위의 거리
    public LayerMask PlayerMask;    //Player 레이어마스크 지정을 위한 변수
    public LayerMask ObstacleMask;  //Obstacle 레이어마스크 지정 위한 변수
    public GameObject WayPoint0;
    public GameObject WayPoint1;
    public GameObject Player;
    private Vector3 destPos;
    bool isWayPoint = false;
    
    protected override void Start()
    {
        base.Start();
        gameManager = FindObjectOfType<GameManager>();
        if (type == UnitType.DEFAULT) type = UnitType.ENEMY;
        status = Status.search;
    }

    protected virtual void Update()
    {
        //CheckDie();
        //CheckPattern();
        
        if (status == Status.search)
        {
            MoveToWayPoint();
            DrawView();
            SearchTarget();
        }

        if (status == Status.battle)
        {
            MoveToPlayer();
        }
    }
    public Vector3 AngleDirection(float angleDegrees)
    {
        angleDegrees += -(transform.eulerAngles.z);
        //경계 벡터값 반환
        return new Vector3(Mathf.Sin(angleDegrees * Mathf.Deg2Rad), Mathf.Cos(angleDegrees * Mathf.Deg2Rad),0);
    }
    protected void OnAggro()
    {
        status = Status.battle;
    }

    //디버깅용
     void DrawView()
    {
        Vector3 leftBoundary = AngleDirection(-SearchAngle / 2);
        Vector3 rightBoundary = AngleDirection(SearchAngle / 2);
        Debug.DrawLine(transform.position, transform.position + leftBoundary * SeachDistance, Color.red);
        Debug.DrawLine(transform.position, transform.position + rightBoundary * SeachDistance, Color.red);
    }
    protected void RotateToPlayer() // 객체가 플레이어를 바라보도록
    {
        if (isStatic || targetPlayer == null) return; // 고정 몹이거나 player 설정이 안되어 있으면 return
        Vector2 direction = targetPlayer.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0, 0, angle - 90);
    }
    public void SearchTarget()
    {
        //탐색 범위의 플레이어 컬라이더 탐색
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, SeachDistance, PlayerMask);

        for (int i = 0; i < targets.Length; i++)
        {
            // Debug.Log("doing");
            Transform target = targets[i].transform;
            Debug.DrawLine(transform.position, target.position, Color.green);
            //적 객체와 탐색된 객체까지의 단위벡터
            Vector3 TargetDirection = (target.position - transform.position).normalized;

            //transform.forward와 TargetDirection은 모두 단위벡터이므로 내적값은 두 벡터가 이루는 각의 Cos값과 같다.
            //내적값이 중간벡터의 Cos값보다 크면 시야에 들어온 것이다.

            //if (Vector3.Dot(transform.forward, TargetDirection) > Mathf.Cos((SearchAngle / 2) * Mathf.Deg2Rad))
            if (Vector3.Angle(transform.up, TargetDirection) < SearchAngle/2)
            {
                float TargetDistance = Vector3.Distance(transform.position, target.position);//탐색된 객체까지의 거리
                if (!Physics2D.Raycast(transform.position, TargetDirection, TargetDistance, ObstacleMask))
                {
                    Debug.Log("got it");
                    Debug.DrawLine(transform.position, target.position, Color.blue);
                    OnAggro();
                }
            }
        }
    }
    protected void MoveToPlayer() // 플레이어를 향해 움직이기
    {
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
    }

    protected void MoveToWayPoint()
    {
        destPos.Set(transform.position.x, transform.position.y, -10);
        if (isWayPoint == false)
        {
            //이동
            transform.position = Vector3.MoveTowards(transform.position, WayPoint1.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(WayPoint0.transform.position - destPos, Vector3.forward);
            //반전
            if (Vector3.Distance(transform.position, WayPoint1.transform.position) <= 0.5f)
                isWayPoint = true;
        }



        else
        {
            //이동
            transform.position = Vector3.MoveTowards(transform.position, WayPoint0.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(WayPoint1.transform.position - destPos, Vector3.forward);
            //반전
            if (Vector3.Distance(transform.position, WayPoint0.transform.position) <= 0.5f)
                isWayPoint = false;
        }
    }
    /* private void OnMouseDown()
     {
         if (gameManager.player != null)
         {
             if (gameManager.player.playerType == Player.PlayerType.WARRIOR)
             {
                 //gameManager.player.GetComponent<Warrior>().SpeedOn(false);
             }
             else gameManager.player.SpeedOn(false); // 클릭하면 바로 플레이어의 속도 0
         }
     }

     private void OnMouseUp()
     {
         if (gameManager.player != null)
         {
             if (gameManager.player.playerType == Player.PlayerType.WARRIOR)
             {
                 //gameManager.player.GetComponent<Warrior>().SpeedOn(true);
             }
             else gameManager.player.SpeedOn(true); //  버튼이 떼지는 순간 플레이어 속도 복구
         }
     }

     private void OnMouseUpAsButton() // 이 객체를 터치했을 때
     {
         if (gameManager.player != null)
         {
             switch (gameManager.player.playerType)
             {
                 case Player.PlayerType.PLAYER_DEFAULT:
                     break;
                 case Player.PlayerType.ARCHER:
                     gameManager.player.Move(false); // 원거리 타입이므로 이동 중지
                     gameManager.enemyTouch = true;
                     break;
                 case Player.PlayerType.WARRIOR:
                     gameManager.enemyTouch = true;
                     break;
             }

             gameManager.player.targetEnemy = this; // 플레이어의 타겟을 이 객체로 설정
         }
     }*/

    //private void OnMouseUpAsButton() {
    //    Debug.Log("touch Enemy");
    //    //gameManager.enemyTouch = true;
    //    gameManager.player.targetEnemy = this;
    //}

    private void OnMouseDown()
    {
        Debug.Log("touch Enemy");
        gameManager.enemyTouch = true;
        gameManager.player.targetEnemy = this;
    }

    protected override void CheckDie()
    {
        if (hp <= 0)
        {
            status = Status.dead;
            if (gameManager.player.targetEnemy == this)
            {
                gameManager.player.targetEnemy = null;
                gameManager.player.SpeedOn(true);
            }
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    protected virtual void CheckPattern()
    {
    }
}
