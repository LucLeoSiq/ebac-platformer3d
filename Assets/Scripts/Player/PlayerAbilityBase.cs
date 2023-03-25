using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityBase : MonoBehaviour
{
    protected Player Player;
    protected Inputs inputs;

    private void Start()
    {
        inputs = new Inputs();
        inputs.Enable(); 

        Init();
        OnValidate();
        RegisterListeners();
    }

    private void OnValidate()
    {
        if (Player == null) Player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        if(inputs != null)   
            inputs.Disable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    protected virtual void Init() { }
    protected virtual void RegisterListeners() { }
    protected virtual void RemoveListeners() { }
}
