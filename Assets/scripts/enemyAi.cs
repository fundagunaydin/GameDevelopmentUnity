using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAi : MonoBehaviour {
    NavMeshAgent navmeshAgent;
    Transform player;
    Animator fsm;
    public Transform rayOrigin;
    public Transform  p1 , p2 , p3;
    Vector3[] waypoints;
    int currentTarget;
    float maxCheckDistance= 10f;
	// Use this for initialization
	void Start () {
        navmeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("myplayer").transform;
        fsm = GetComponent<Animator>();
        waypoints = new Vector3[] { p1.position, p2.position, p3.position };
        navmeshAgent.SetDestination(waypoints[currentTarget]);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        float distance = Vector3.Distance(player.position, transform.position);
        fsm.SetFloat("distance", distance);
        Vector3 dir = player.position - rayOrigin.position;
        dir = dir.normalized;
        Debug.DrawRay(rayOrigin.position, dir*maxCheckDistance,Color.black);
        RaycastHit castHit;
        if (Physics.Raycast(rayOrigin.position, dir,out castHit, maxCheckDistance))
        {
            if (castHit.transform.tag == "myplayer")
            {
                fsm.SetBool("isVisible", true);
            }
            else 
           
                fsm.SetBool("isVisible", false);
           
        }
        else
        fsm.SetBool("isVisible", false);


        float distanceFromWaypoints = Vector3.Distance(transform.position, waypoints[currentTarget]);
        fsm.SetFloat("distanceFromWaypoint", distanceFromWaypoints);
	}
    public void setNextWaypoint(){
        switch (currentTarget)
        {
            case 0: currentTarget = 1; break;
            case 1: currentTarget = 2; break;
            case 2: currentTarget = 0; break;

        }

        navmeshAgent.SetDestination(waypoints[currentTarget]);
    }
    public void SetLookRotation()
    {
        Vector3 dir = (player.position - transform.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.2f);
    }


    public void Shoot()
    {
        float shootFreq = 5;
        GetComponent<ShootBehave>().Shoot(shootFreq);
    }

    public void chase()
    {
        navmeshAgent.SetDestination(player.position);
    }

    public void patrol()
    {
        navmeshAgent.SetDestination(waypoints[currentTarget]);
    }
}
