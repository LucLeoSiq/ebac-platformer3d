using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;

public class Player : MonoBehaviour//, IDamageable
{
    public Animator animator;

    public CharacterController characterController;
    public float speed = 1f;
    public float turnSpeed = 300f;
    public float gravity = 9.8f;
    public float jumpSpeed = 1.5f;

    [Header("Run Setup")]
    public KeyCode keyrun = KeyCode.LeftShift;
    public float speedRun = 15f;

    private float vSpeed = 0f;

    [Header("Flash")]
    public List<FlashColor> flashColors;

    public HealthBase healthBase;

    private void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    private void Awake()
    {
        OnValidate();

        healthBase.OnDamage += Damage;
        healthBase.OnDamage += OnKill;

    }

    private void OnKill(HealthBase h)
    {
        animator.SetTrigger("Death");
    }

    public void Damage(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
    }

    public void Damage(float damage, Vector3 dir)
    {
        //Damage(damage);
    }

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

        // Increases player speed when the run key is pressed
        var isWalking = InputAxisVertical != 0;

        if (isWalking)
        {
            if (Input.GetKey(keyrun))
            {
                speedVector *= speedRun;
                animator.speed = speedRun;
            }
            else
            {
                animator.speed = 1;
            }
        }

        // Implements gravity for player character
        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed;

        characterController.Move(speedVector * Time.deltaTime);

        // Plays running animation if player character is moving forward or backwards
        animator.SetBool("Run", isWalking);

    }
}
