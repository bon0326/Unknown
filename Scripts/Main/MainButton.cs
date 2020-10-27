using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 교연

public class MainButton : MonoBehaviour
{
    public Button thisButton;

    protected virtual void Start()
    {

        thisButton.onClick.AddListener(TaskOnClick);
    }

    protected virtual void TaskOnClick()
    {
        Debug.Log(thisButton + "Click");
    }
}