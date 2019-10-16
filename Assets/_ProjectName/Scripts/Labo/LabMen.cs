///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 16/10/2019
///-----------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.AI;

namespace Com.Github.PLAORANGE.Thelastlab.lab
{
	public class LabMen : MonoBehaviour {
        protected NavMeshAgent agent;
        private float walkRadius = 5f;
        Vector3 finalPosition;

        private void Start () {
            agent = GetComponent<NavMeshAgent>();
            GoToRandomPosition();
            

        }

        private void GoToRandomPosition()
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * walkRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
            finalPosition = hit.position;

            agent.SetDestination(finalPosition);
        }

        private void Update()
        {
            Debug.Log(Vector3.Distance(transform.position, finalPosition));
            if (Vector3.Distance(transform.position, finalPosition) <= 1f || Vector3.Distance(transform.position, finalPosition) == Mathf.Infinity)
            {
                GoToRandomPosition();
            }
        }
	}
}