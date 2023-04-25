using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;

public class Player : MonoBehaviour//, IDamageable
{
    public List<Collider> colliders;
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

    [Header("Life")]
    public HealthBase healthBase;
    public UIFillUpdater iuGunUpdater;

    private bool _alive = true;

         void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    private void Awake()
    {
        OnValidate();

        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;

    }

    private void OnKill(HealthBase h)
    {
        if(_alive)
        {
            _alive = false;
            animator.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);
            Debug.Log("Colider Off"); 


            Invoke(nameof(Revive), 3f);
        }
    }

    private void Revive()
    {
        _alive = true; 
        healthBase.ResetLife();
        animator.SetTrigger("Revive");
        Respawn();
        Invoke(nameof(TurnOnColliders), .1f);
    }

    private void TurnOnColliders()
    {
        colliders.ForEach(i => i.enabled = true);
        Debug.Log("Colider On");
    }

    public void Damage(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignette();
    }

    public void Damage(float damage, Vector3 dir)
    {
        //Damage(damage);
    }

    void Update()
    {
        // Allows for the rotation of gameobject when pressing Horizontal keys.
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

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

    [NaughtyAttributes.Button]
    public void Respawn()
    {
        if(CheckpointManager.Instance.HasCheckpoint())
        {
            characterController.enabled = false;
            transform.position = CheckpointManager.Instance.GetPositionFromLasCheckPoint();
            characterController.enabled = true;
        }
    }

    
}
