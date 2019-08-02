using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed = 2f;
    [SerializeField] float rotationSpeed = 2f;

    bool moving = false;
    bool moveLeft = false;

    void Update()
    {
        if (moving)
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
            //gameObject.transform.Rotate(rotationVector,Space.Self);
            gameObject.transform.Translate(movementVector, Space.World);
        }
    }

    public void Left()
    {
        print("left");
        moving = true;
        moveLeft = true;
    }

    public void Right()
    {
        print("right");
        moving = true;
        moveLeft = false;
    }
}
