﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTFollow : MonoBehaviour
{
    public GameObject Player;
    public float TargetDistance;
    public float AllowedDistance = 5;
    public GameObject Partner;
    public float FollowSpeed;
    public RaycastHit Shot;
    void Update()
    {
        if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),out Shot))
        {
            TargetDistance = Shot.distance;
            if(TargetDistance >= AllowedDistance)
            {
                FollowSpeed = 1f;
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, FollowSpeed);
            }
            else
            {FollowSpeed = 0;
            }
        }
    }
}
