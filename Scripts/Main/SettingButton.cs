using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SettingButton : MainButton
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TaskOnClick()
    {
        SceneManager.LoadScene("Setting");
        base.TaskOnClick();
    }
}