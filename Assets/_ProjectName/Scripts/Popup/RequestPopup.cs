///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 14/10/2019
///-----------------------------------------------------------------

using UnityEngine;
using TMPro;
using Pixelplacement;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using Com.Github.PLAORANGE.Thelastlab.Hud;

namespace Com.Github.PLAORANGE.Thelastlab.Popup
{
    public delegate void RequestPopupEventHandler(RequestPopup sender);

    public class RequestPopup : Popup {

        [SerializeField]
        protected RectTransform cardHolderRectTransform;
        protected GameObject detectedCard;
        [SerializeField]
        protected Image cardHolderImage;
        protected Camera cam;

        public static event RequestPopupEventHandler OnAppear;
        public static event RequestPopupEventHandler OnDisappear;
        protected  GameManager gameManager; 
        private JobCode jobCode;
        private int givingScore = 20;

        public JobCode JobCode {
            set
            {
                jobCode = value;
                SetText(jobCode.ToString());
            }
        }

        override public void Start() {
            base.Start();
            cam = Camera.main;
            gameManager = FindObjectOfType<GameManager>();
            Card.OnCardDrop += Card_OnCardDrop;
        }

        private void Card_OnCardDrop(Card sender)
        {
            if (exist)
            {
                if (sender.transform.position.x >= cardHolderRectTransform.rect.x && sender.transform.position.x <= cardHolderRectTransform.rect.xMax)
                {
                    if (sender.transform.position.y >= cardHolderRectTransform.rect.y && sender.transform.position.y <= cardHolderRectTransform.rect.yMax)
                    {
                        if (sender.JobCode == jobCode && detectedCard is null)
                        {
                            detectedCard = sender.gameObject;
                            Debug.Log("destroy card");

                            sender.Destroy();
                            detectedCard = null;

                            gameManager.SpawnInLab(sender.job);
                            CorrectAnswer();
                        }
                        else if (detectedCard is null)
                        {
                            FalseAnswer();
                        }
                    }
                }
            }
        }

        public void CorrectAnswer()
        {
            gameManager.AddScore(givingScore);
            
            //Debug.Log("La reponse donnee a la requete est correct");
            Disapear();
        }
        
        public void FalseAnswer()
        {
            ProgressBarProject progressBar = FindObjectOfType<ProgressBarProject>();
            progressBar.FalseAnswer();
            //Debug.Log("La reponse donnee a la requete est incorrecte");
            //Disapear();
        }

        protected void Update()
        {
            //CheckCard();
        }

        public override void Appear()
        {
            base.Appear();
            OnAppear?.Invoke(this);
        }

        public override void Disapear()
        {
            base.Disapear();
            OnDisappear?.Invoke(this);
        }

        protected void CreateRect()
        {
            Rect rect = cardHolderRectTransform.rect;
        }

        /*
        private void CheckCard() {
            RaycastHit hit;
            bool lHitSomething;

            if(exist) {
                lHitSomething = Physics.Raycast(GetComponent<RectTransform>().position, GetComponent<RectTransform>().TransformDirection(-Vector3.forward), out hit);
                Debug.DrawRay(GetComponent<RectTransform>().position, GetComponent<RectTransform>().TransformDirection(-Vector3.forward));
                if(lHitSomething && hit.collider.CompareTag("carte")) {

                    if(hit.collider.GetComponent<Card>().JobCode == jobCode && detectedCard is null) {
                        detectedCard = hit.collider.gameObject;

                        float delay = 0;

                        Vector3 tweenCardPosition = new Vector3(GetComponent<RectTransform>().position.x, GetComponent<RectTransform>().position.y - 1, GetComponent<RectTransform>().position.z);
                        Tween.Position(detectedCard.transform, tweenCardPosition, .25f, delay, Tween.EaseIn, Tween.LoopType.None);
                        delay += 0.5f;

                        CardDetected();
                        //cardHolderImage.color = Color.green;
                        gameManager.SpawnInLab(hit.collider.GetComponent<Card>().job);

                        CorrectAnswer();
                    }
                    else if(detectedCard is null) {
                        //cardHolderImage.color = Color.red;
                        FalseAnswer();

                        hit.collider.gameObject.GetComponent<Card>().StopDrag();
                    }

                }
                else {
                    //cardHolderImage.color = Color.grey;
                }
            }
        }*/

            /*
        protected void CardDetected()
        {
            Destroy(detectedCard);
            detectedCard = null;
        }
        */
    }
}