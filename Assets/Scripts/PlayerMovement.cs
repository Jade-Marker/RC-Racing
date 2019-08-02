using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed = 2f;
    [SerializeField] float rotationSpeed = 2f;

    Track currentTrack;
    bool moving = false;
    bool moveLeft = false;
    bool alive = true;

    const float a = 1440f/13.332f;
    const float b = 108f;
    const float c = 960f;
    const float d = 540f;

    void Start() {
        currentTrack = FindObjectOfType<Track>();
    }

    void Update()
    {
        if (moving && alive)
        {
            Vector3 movementVector = new Vector3();
            float rotationAngle = 0f;

            if (moveLeft) {
                movementVector = gameObject.transform.up;
                rotationAngle = rotationSpeed * Time.deltaTime;
            }
            else {
                movementVector = gameObject.transform.up;
                rotationAngle = -rotationSpeed * Time.deltaTime;
            }
            movementVector *= playerSpeed / 1000f;

            gameObject.transform.Rotate(0, 0, rotationAngle, Space.Self);
            gameObject.transform.Translate(movementVector, Space.World);
        }

        Vector2 currPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        Vector2Int imgPos = new Vector2Int(Mathf.RoundToInt(a * currPos.x + c), Mathf.RoundToInt(b * currPos.y + d));
        Color posColor = currentTrack.pathImg.GetPixel(imgPos.x, imgPos.y);
        if (posColor == Color.black)
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        print("Dead");
        alive = false;
    }

    public void Left()
    {
        moving = true;
        moveLeft = true;
    }

    public void Right()
    {
        moving = true;
        moveLeft = false;
    }
}
