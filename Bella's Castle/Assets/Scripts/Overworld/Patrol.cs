﻿using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float waitTime = 1f;
    float currentWaitTime = 0f;

    public float minX, maxX, minZ, maxZ;
    Vector3 moveSpot;

    // Start is called before the first frame update
    void Start()
    {
        GetGroundSize();//get the ground size
        moveSpot = GetNewPosition();
    }

    // Update is called once per frame
    void Update()
    {
        WatchYourStep();
        GetToStepping();
    }

    private void GetGroundSize()
    {
        GameObject ground = GameObject.FindWithTag("Ground");
        Renderer groundSize = ground.GetComponent<Renderer>();
        minX = (groundSize.bounds.center.x - groundSize.bounds.extents.x);
        maxX = (groundSize.bounds.center.x + groundSize.bounds.extents.x);
        minZ = (groundSize.bounds.center.z - groundSize.bounds.extents.z);
        maxZ = (groundSize.bounds.center.z + groundSize.bounds.extents.z);
    }

    Vector3 GetNewPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        Vector3 newPosition = new Vector3(randomX, transform.position.y, randomZ);
        return newPosition;
    }

    void GetToStepping()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpot, speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, moveSpot) <= 0.2f)
        {
            moveSpot = GetNewPosition();
            currentWaitTime = waitTime;
        }
        else
        {
            currentWaitTime -= Time.deltaTime;
        }
    }

    void WatchYourStep()
    {
        Vector3 targetDirection = moveSpot - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 0.3f, 0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
