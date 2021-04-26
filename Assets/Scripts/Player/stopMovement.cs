using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopMovement : MonoBehaviour
{
    public PlayerMain player;

    private void OnMouseOver()
    {
        player.canMove = false;
    }

    private void OnMouseExit()
    {
        player.canMove = true;
    }
}
