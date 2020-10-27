using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CreditButton : MainButton
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TaskOnClick()
    {
        SceneManager.LoadScene("Main_Credit");
        base.TaskOnClick();
    }
}   