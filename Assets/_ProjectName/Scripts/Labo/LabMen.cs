///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 16/10/2019
///-----------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Com.Github.PLAORANGE.Thelastlab.lab
{
	public class LabMen : MonoBehaviour {
        protected NavMeshAgent agent;
        private float walkRadius = 5f;
        Vector3 finalPosition;

        public Image img;
        public Canvas canvas;

        Action DoAction;
        private Quaternion targetRotation;

        public void SetModeVoid()
        {
            DoAction = DoActionVoid;
        }

        private void DoActionVoid()
        {

        }

        public void SetModeNavigate()
        {
            agent = GetComponent<NavMeshAgent>();
            GoToRandomPosition();
            DoAction = DoActionNavigate;
        }

        public void DoActionNavigate()
        {
            //Debug.Log(Vector3.Distance(transform.position, finalPosition));
            if (Vector3.Distance(transform.position, finalPosition) <= 1f || Vector3.Distance(transform.position, finalPosition) == Mathf.Infinity)
            {
                GoToRandomPosition();
            }
            Vector3 vector3 = Camera.main.transform.position - transform.position;
            vector3 = Vector3.ProjectOnPlane(vector3, Vector3.up);

            targetRotation = Quaternion.LookRotation(vector3, Vector3.up);
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

        private void Start()
        {
            SetModeNavigate();
        }

        private void Update()
        {
            DoAction();



            canvas.transform.rotation = targetRotation;
        }
	}
}