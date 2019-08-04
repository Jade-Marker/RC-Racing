using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceCountdown : MonoBehaviour
{
    [SerializeField] Text startText;
    [SerializeField] AudioClip count;
    [SerializeField] AudioClip start;

    float timeLeft;
    bool timerStarted = false;
    AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

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
        audioSource.PlayOneShot(count);
    }

    IEnumerator Start2()
    {
        yield return new WaitForSeconds(4);
        startText.text = "2!";
        audioSource.PlayOneShot(count);
    }

    IEnumerator Start1()
    {
        yield return new WaitForSeconds(5);
        startText.text = "1!";
        audioSource.PlayOneShot(count);
    }

    IEnumerator RaceStart() {
        yield return new WaitForSeconds(6);
        startText.text = "Go!";
        audioSource.PlayOneShot(start);
        yield return new WaitForSeconds(0.5f);
        startText.gameObject.SetActive(false);
    }
}
