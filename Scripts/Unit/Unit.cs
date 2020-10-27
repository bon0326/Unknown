using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [HideInInspector] public enum UnitType { DEFAULT = -1, PLAYER = 0, ENEMY = 1 };

    [HideInInspector] public int originalHp; // 최대 HP
    [Header("Status")]
    public int hp; // 현재 HP
    [Range(0, 10)] public float speed;
    public float damage;
    public float attackInterval; // 공격 간격
    public UnitType type; // 타입

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip attackSound;
    public AudioClip hitSound;

    [Header("Particle")]
    public ParticleSystem attackParticle;
    public ParticleSystem hitParticle;

    //[HideInInspector] public bool possibleAttack;
    [HideInInspector] public bool rangeAttackUnit; // 원거리 타입인가?
    public Animator animator;
    [HideInInspector] public float originSpeed; // 원래 이동 속도

    protected GameManager gameManager;
    protected bool isMove; // 움직이는가?
    protected bool isDead; // 죽었는가?
    protected bool isIdle; // 평소 상태인가?
    protected float lastAttackTime; // 마지막 공격 시간

    public virtual void Move()
    {
        isMove = true;
    }

    protected virtual void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        originalHp = hp;
        lastAttackTime = Time.fixedTime;
        audioSource.loop = false;
    }

    public virtual void Attack() // 유닛의 공격 모션
    {
        PlayAttackSound();
    }

    public virtual void LossHp(float damage) // 피격(체력 손실)
    {
        PlayHitSound();
        hp -= (int)damage;
        //gameManager.enemyHPUI.SetText();
    }

    protected virtual void CheckDie() // 죽었는가
    {
        if (hp <= 0)
        {
            isDead = true;
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void PlayAttackSound()
    {
        audioSource.clip = attackSound;
        audioSource.Play();
    }

    public void PlayHitSound()
    {
        audioSource.clip = hitSound;
        audioSource.Play();
    }

    public void PlayEffectSound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
