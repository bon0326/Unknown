using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damageMultiple; // 화살의 공격력 (유닛 공격력의 배수)
    [Range(0, 10)] public float speed; // 화살 날아가는 속도

    protected bool fired; // 발사됐는가
    protected bool stopMoving; // 그만 움직여라
    protected KSO archer; // 부모 객체
    private Vector3 player;

    private ParticleSystem attackParticle;

    private void Start()
    {
        fired = false;
        stopMoving = false;
        archer = FindObjectOfType<KSO>();
        attackParticle = archer.attackParticle;
        player = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && !fired)
        {
            Debug.Log("발사!");
            fired = true;
        }

        if (!stopMoving) transform.Translate(0, speed * Time.deltaTime, 0); // 어딘가에 박히지 않으면 이동
        if (transform.position.x > player.x + 20 || transform.position.x < player.x + -20 || transform.position.y < player.y - 20 || transform.position.y > player.y + 20) // 화면 범위 한참 벗어난 경우
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 0.3f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") // 충돌체가 Enemy일 때
        {
            if (archer != null)
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.LossHp((int)(archer.damage * damageMultiple)); // 적의 HP 타격
                Instantiate(attackParticle, new Vector3(transform.position.x,transform.position.y,transform.position.z-5), Quaternion.identity);
            }
            Destroy(gameObject);
        }

        stopMoving = true; // 화살이 박힘 (어쨌거나 충돌이 일어났으므로)

        Destroy(gameObject, 0.3f);
    }
}
