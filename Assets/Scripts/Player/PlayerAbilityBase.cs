using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityBase : MonoBehaviour
{
    protected Player Player;

    private void Start()
    {
        RemoveListeners();
        OnValidate();
        RegisterListeners();
    }

    private void OnValidate()
    {
        if (Player == null) Player = GetComponent<Player>();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    protected virtual void Init() { }

    protected virtual void RegisterListeners() { }

    protected virtual void RemoveListeners() { }
}
