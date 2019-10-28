///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Com.Github.PLAORANGE.Thelastlab
{
	public class SwipManager : MonoBehaviour {
        [SerializeField] GameObject cardPrefab;
        [SerializeField] Transform cardContainer;

        [SerializeField] float angleAmplitude;
        [SerializeField] AnimationCurve rotationCurve;
        [SerializeField] float rotationDuration;
        private float elapsedTime;
        private int rotationDirection;


        private Quaternion cardStartRotation;
        private Quaternion cardTargetRotation;
        private Vector3 startTouch;
        [SerializeField] private float touchTolerance;

        [SerializeField] private List<JobCode> startCards = new List<JobCode>();
        private List<GameObject> cardList = new List<GameObject>();

        private GameObject FrontCard { 
            get { 
                return (cardList.Count == 0)? null: cardList[0]; 
            } 
        }

        private void Start () {
            GameObject card = null;

            for (int i = 0; i < startCards.Count; i++)
            {
                card = Instantiate(cardPrefab, cardContainer);
                card.transform.localPosition = Vector3.forward * i;

                cardList.Add(card);
                card.GetComponent<Card>().setJob(startCards[i]);
            }
        }

        private void PushBack(GameObject card)
        {
            card.transform.localRotation = Quaternion.identity;

            cardList.Remove(card);
            cardList.Add(card);

            for (int i = 0; i < cardList.Count; i++)
            {
                cardList[i].transform.localPosition = Vector3.forward * i;
            }
        }

        public void SetRotation(int rotationDirection)
        {
            this.rotationDirection = rotationDirection;

            cardStartRotation = FrontCard.transform.localRotation;
            cardTargetRotation = Quaternion.AngleAxis(angleAmplitude * rotationDirection, Vector3.forward);
            elapsedTime = 0;
        }

        private void Update () {
            ////// swipControl
            if (Input.GetMouseButtonDown(0))
            {
                startTouch = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 currentTouch = Input.mousePosition;
                int rotationDirection;

                if (currentTouch.x > startTouch.x - touchTolerance && currentTouch.x < startTouch.x + touchTolerance) rotationDirection = 0;
                else rotationDirection = Mathf.RoundToInt((startTouch - currentTouch).normalized.x);

                if (rotationDirection != this.rotationDirection)
                    SetRotation(rotationDirection);
            }

            if (Input.GetMouseButtonUp(0))
            {
                if(rotationDirection == 1)
                {
                    PushBack(FrontCard);
                    SetRotation(0);
                }
                else if(rotationDirection == -1)
                {
                    Debug.Log("validé");
                }
            }

            //////
            elapsedTime += Time.deltaTime;

            float ratio = rotationCurve.Evaluate(elapsedTime / rotationDuration);
            FrontCard.transform.localRotation = Quaternion.Slerp(cardStartRotation, cardTargetRotation, ratio);
		}
	}
}