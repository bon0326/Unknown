using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExitButton : MainButton {
    protected override void Start()
    {
        base.Start();
    }

    protected override void TaskOnClick()
    {
        Application.Quit();
        base.TaskOnClick();
    }
}