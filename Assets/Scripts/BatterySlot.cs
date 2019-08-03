using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterySlot : MonoBehaviour
{
    [SerializeField] bool leftSlot = false;

    Battery battery;
    PlayerMovement player;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (leftSlot)
        {
            player.Left();
        }
        else {
            player.Right();
        }
    }
}
