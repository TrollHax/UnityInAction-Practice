using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public LayerMask clickable;

    private NavMeshAgent myAgent;
    private Animator animator;
    private Rigidbody rb;
    private Vector3 prevPos;
    private float speed;

    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(myRay, out hitInfo, 100, clickable))
            {
                myAgent.SetDestination(hitInfo.point);
                return;
            }
        }
        speed = ((transform.position - prevPos).magnitude) / Time.deltaTime;
        prevPos = transform.position;

        animator.SetFloat("speed", speed);
        Debug.Log(speed);
    }
}
