using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartButton : MainButton
{
    private GameManager gameManager;
    public string LoadScene;

    protected override void Start()
    {
        base.Start();
        gameManager = FindObjectOfType<GameManager>();
    }

    protected override void TaskOnClick()
    {
        SceneManager.LoadScene(LoadScene);
        gameManager.soundManager.ChangeMusic(1);
        base.TaskOnClick();
    }
}