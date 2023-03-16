using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;

    public CharacterController characterController;
    public float speed = 1f;
    public float turnSpeed = 300f;
    public float gravity = 9.8f;
    public float jumpSpeed = 15f;

    private float vSpeed = 0f;

    void Update()
    {
        // Allows for the rotation of gameobject when pressing Horizontal keys.
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed* Time.deltaTime, 0);

        // Allows for the forward movement of gameobject when pressing vertical keys.
        var InputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * Input.GetAxis("Vertical") * speed;

        // Jump if Key is pressed and character is grounded.
        if (characterController.isGrounded)
        {
            vSpeed = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                vSpeed = jumpSpeed;
            }
        }

        // Implements gravity for player character
        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed;

        characterController.Move(speedVector * Time.deltaTime);

        // Plays running animation if player character is moving forward or backwards
        animator.SetBool("Run", InputAxisVertical != 0);
    }
}
