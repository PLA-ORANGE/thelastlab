///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using Pixelplacement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Com.Github.PLAORANGE.Thelastlab
{
    public delegate void SwipManagerEventHandler(SwipManager sender);

    public class SwipManager : MonoBehaviour {

        public event SwipManagerEventHandler OnValidateCard;

        [SerializeField] protected float cardScale = 1.25f;
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

        public Deck deck;
        public bool active;

        [SerializeField] Transform rightArroy;
        [SerializeField] Transform leftArroy;

        [SerializeField] private GameObject tutoTxt;
        private bool isFirstCardChoose = true;

        private GameObject FrontCard 
        { 
            get 
            { 
                return (cardList.Count == 0)? null: cardList[0]; 
            } 
        }

        private void Start () {
            //Init();
        }

        public void Init()
        {
            GameObject card = null;

            for (int i = 0; i < startCards.Count; i++)
            {
                card = Instantiate(cardPrefab, cardContainer);
                card.transform.localPosition = Vector3.forward * i;

                cardList.Add(card);
                card.GetComponent<Card>().setJob(startCards[i]);
            }

            active = true;
        }

        private void PushBack(GameObject card)
        {
            cardList.Remove(card);
            cardList.Add(card);

            OrderRoulement();
        }
        
        private void OrderRoulement()
        {
            for (int i = 0; i < cardList.Count; i++)
            {
                cardList[i].transform.localPosition = Vector3.forward * i;
                cardList[i].transform.localRotation = Quaternion.identity;
            }
        }

        public void SetRotation(int rotationDirection)
        {
            this.rotationDirection = rotationDirection;

            cardStartRotation = FrontCard.transform.localRotation;
            cardTargetRotation = Quaternion.AngleAxis(angleAmplitude * rotationDirection, Vector3.forward);
            elapsedTime = 0;
        }

        private void Control()
        {
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

                

                if(rotationDirection > 0)
                {
                    Tween.LocalScale(leftArroy, new Vector3(1.5f, 1.5f, 1), 0.1f, 0);
                    Tween.LocalScale(rightArroy, new Vector3(0, 0, 0), 0.1f, 0);
                }
                else if(rotationDirection < 0)
                {
                    Tween.LocalScale(leftArroy, new Vector3(0, 0, 0), 0.1f, 0);
                    Tween.LocalScale(rightArroy, new Vector3(1.5f, 1.5f, 1), 0.1f, 0);
                }
                else
                {
                    Tween.LocalScale(leftArroy, new Vector3(0, 0, 0), 0.1f, 0);
                    Tween.LocalScale(rightArroy, new Vector3(0, 0, 0), 0.1f, 0);
                }

                if (rotationDirection != this.rotationDirection)
                    SetRotation(rotationDirection);
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (rotationDirection == 1)
                {
                    PushBack(FrontCard);
                    SetRotation(0);
                }
                else if (rotationDirection == -1)
                {
                    if (isFirstCardChoose)
                    {
                        isFirstCardChoose = false;
                        tutoTxt.SetActive(false);
                    }
                    GameObject frontCard = FrontCard;

                    cardList.Remove(frontCard);
                    deck.AddCard(frontCard);
                    frontCard.transform.localScale = Vector3.one * cardScale;
                    
                    OnValidateCard?.Invoke(this);


                    if (cardList.Count == 0)
                    {
                        Destroy(cardContainer.gameObject);
                        GetComponent<GameManager>().SetProjectPhase();
                    }
                    else
                    {
                        SetRotation(0);
                        OrderRoulement();
                    }
                }

                Tween.LocalScale(leftArroy, new Vector3(0, 0, 0), 0.1f, 0);
                Tween.LocalScale(rightArroy, new Vector3(0, 0, 0), 0.1f, 0);
            }
        }

        private void Update () {
            if (!active) return;

            elapsedTime += Time.deltaTime;

            float ratio = rotationCurve.Evaluate(elapsedTime / rotationDuration);
            FrontCard.transform.localRotation = Quaternion.Slerp(cardStartRotation, cardTargetRotation, ratio);

            Control();
        }
	}
}