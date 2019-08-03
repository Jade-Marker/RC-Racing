using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceCountdown : MonoBehaviour
{
    [SerializeField] Text startText;

    float timeLeft;
    bool timerStarted = false;

    public void StartCountDown(float time)
    {
        if (!timerStarted)
        {
            timeLeft = time;
            timerStarted = true;
            StartCoroutine(Start3());
            StartCoroutine(Start2());
            StartCoroutine(Start1());
            StartCoroutine(RaceStart());
        }
    }

    IEnumerator Start3()
    {
        yield return new WaitForSeconds(3);
        startText.text = "3!";
    }

    IEnumerator Start2()
    {
        yield return new WaitForSeconds(4);
        startText.text = "2!";
    }

    IEnumerator Start1()
    {
        yield return new WaitForSeconds(5);
        startText.text = "1!";
    }

    IEnumerator RaceStart() {
        yield return new WaitForSeconds(6);
        startText.text = "Go!";
        yield return new WaitForSeconds(0.5f);
        startText.gameObject.SetActive(false);
    }
}
