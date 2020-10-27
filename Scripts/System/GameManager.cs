using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public Player[] canPlayers;
    //public GameObject screenHitEffect;  피격 이펙트 (화면에 전체적으로 붉은색) 일단 주석
    public GameObject touchObject;
    public GameObject canvasObject;
    public SoundManager soundManager;
    [HideInInspector] public Touch touch; // 플레이어가 터치한 곳
    [HideInInspector] public bool enemyTouch; // 적을 터치했는가
    [HideInInspector] public Vector3 mousePos;
    [HideInInspector] public Vector3 targetPos;

    private bool loadingScene;
    private float waitTime = 5.0f;

    private void Start()
    {
        touch = FindObjectOfType<Touch>();
        soundManager = FindObjectOfType<SoundManager>();
        enemyTouch = false;
        //screenHitEffect.SetActive(false);
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // if (SceneManager.GetActiveScene().name != "Main Menu") CheckStage();
    }

   /* public void ScreenHitEffect(bool on)
    {
        if (on) screenHitEffect.SetActive(true);
        else screenHitEffect.SetActive(false);
    }

    private void CheckStage()
    {
        if (FindObjectsOfType<Enemy>().Length == 0)
        {
            if (!loadingScene) Invoke("LoadScene", waitTime);
            loadingScene = true;
        }
    }*/

    public void LoadScene()
    {
        player.Init();
        //ChangeMusic(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        Unit[] units = FindObjectsOfType<Unit>();
        foreach (Unit unit in units)
        {
            unit.speed = 0;
        }
        //stageExplain.stageText.text = SceneManager.GetActiveScene().name + '\n';
        Invoke("OffStageExplain", 3f);
        loadingScene = false;
    }

    private void OffStageExplain()
    {

        Unit[] units = FindObjectsOfType<Unit>();
        foreach (Unit unit in units)
        {
            unit.speed = unit.originSpeed;
        }
    }

    public void CheckGameOver()
    {
        if (player.hp <= 0)
        {
            SceneManager.LoadScene("GameOver");
            BoundCamera mainCamera = FindObjectOfType<BoundCamera>();
            mainCamera.minBounds = new Vector2(0, 0);
            mainCamera.transform.position = new Vector3(0, 0, 0);
            DestroyAll();
        }
    }

    public void DestroyAll()
    {
    }
}