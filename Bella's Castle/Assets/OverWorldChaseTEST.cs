using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldChaseTEST : MonoBehaviour
{
    public GameObject player;
    public float targetDistance;
    public float allowedDistance = 6;
    public GameObject follower;
    public float followSpeed;
    public Rigidbody theRB;
    //public Animator anim;


    private void OnTriggerEnter(Collider other)
    {
        targetDistance = Vector3.Distance(player.transform.position, follower.transform.position);
        if (targetDistance >= allowedDistance)
        {
            followSpeed = 0.1f;
            //maybe tell her to walk animate?
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, followSpeed);
        }
        else
        {
            followSpeed = 0f;
            //maybe tell her to idle animate?
        }
        //anim.SetFloat("followSpeed", theRB.velocity.magnitude);
    }
}
