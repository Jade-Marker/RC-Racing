using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    [SerializeField] GameObject title;
    [SerializeField] float initMoveSpeed = 25f;
    [SerializeField] float acceleration = 0.2f;
    [SerializeField] GameObject introText;
    [SerializeField] float initTextSpeed = 5f;
    [SerializeField] float textDeceleration = 1f;

    bool moving = false;
    bool movingText = false;
    float currentMoveSpeed;
    float textCurrMoveSpd;

    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void MoveUp() {
        moving = true;
        currentMoveSpeed = initMoveSpeed;
        textCurrMoveSpd = initTextSpeed;
        StartCoroutine(StopMovingTitleAndButton());
        StartCoroutine(StopMovingText());
    }

    void Update() {
        if (moving)
        {
            Vector3 movementVector = new Vector3(0, 1f, 0);
            movementVector *= Time.deltaTime * currentMoveSpeed;
            gameObject.transform.Translate(movementVector);
            title.transform.Translate(movementVector);
            currentMoveSpeed += acceleration;
        }

        if (movingText) {
            Vector3 movementVector = new Vector3(0, 1f, 0);
            movementVector *= Time.deltaTime * textCurrMoveSpd;
            introText.transform.Translate(movementVector);
            textCurrMoveSpd -= textDeceleration;
            if (textCurrMoveSpd < 0) { textCurrMoveSpd = 0; }
        }
    }

    IEnumerator StopMovingTitleAndButton() {
        yield return new WaitForSeconds(1.2f);
        moving = false;
    }

    IEnumerator StopMovingText()
    {
        yield return new WaitForSeconds(0.6f);
        movingText = true;
        yield return new WaitForSeconds(1.2f);
        movingText = false;
    }
}
