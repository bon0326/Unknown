using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : Singleton<Touch>
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 터치한 좌표에 이 오브젝트 위치
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.player.Move(false);
        }
    }
}
