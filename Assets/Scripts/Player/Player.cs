using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 1f;
    public float turnSpeed = 1f;

    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed* Time.deltaTime, 0);
        
        // Makes object which script is attached move vertically.
        var speedVector = transform.forward * Input.GetAxis("Vertical") * speed;

        characterController.Move(speedVector * Time.deltaTime);
    }
}
