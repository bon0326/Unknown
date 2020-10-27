using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTouch : MonoBehaviour
{
    public Player player;

    private void OnMouseUpAsButton()
    {
        Debug.Log("down");
        player.Move();
    }
}
