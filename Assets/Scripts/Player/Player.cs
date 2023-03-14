using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1f;
    void Update()
    {
        var val = transform.forward * Input.GetAxis("Vertical") * speed;
    }
}
