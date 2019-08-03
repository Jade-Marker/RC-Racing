using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceStartTimer : MonoBehaviour
{
    [SerializeField] float startTime = 5f;

    PlayerMovement player;
    EnemyMovement enemy;

    void Start()
    {
        RaceCountdown countdown = FindObjectOfType<RaceCountdown>();

        try
        {
            player = GetComponent<PlayerMovement>();
            player.enabled = false;
            StartCoroutine(WaitForStartOfRace());
            countdown.StartCountDown(startTime);
        }
        catch {
            enemy = GetComponent<EnemyMovement>();
            enemy.enabled = false;
            StartCoroutine(WaitForStartOfRace());
            countdown.StartCountDown(startTime);
        }
    }

    IEnumerator WaitForStartOfRace()
    {
        yield return new WaitForSeconds(startTime);
        try
        {
            player.enabled = true;
        }
        catch {
            enemy.enabled = true;
        }
    }
}
