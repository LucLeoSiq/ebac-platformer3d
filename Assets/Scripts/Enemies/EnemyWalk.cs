using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : EnemyBase
{
    [Header("Waypoints")]
    public GameObject[] waypoints;
    public float minDistance = 1f;
    public float speed = 1f;

    private int _index = 0;

    void Update()
    {
        if(Vector3.Distance(transform.position, waypoints[_index].transform.position) < minDistance)
        {
            _index++;
            if(_index >= waypoints.Length)
            {
                _index = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[_index].transform.position, Time.deltaTime * speed);
        transform.LookAt(waypoints[_index].transform.position);
    }
}
