using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trophyRotate : MonoBehaviour
{
    [SerializeField] float degPs = 5f;
    
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, degPs * Time.deltaTime);
    }
}
