﻿///-----------------------------------------------------------------
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
        private JobCode jobCode = JobCode.Mathématicien;

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
        }
            
        
        public void CorrectAnswer()
        {
            gameManager.UpdateScore(100); 
            
            Debug.Log("La reponse donnee a la requete est correct");
            Disapear();
        }
        
        public void FalseAnswer()
        {
            ProgressBarProject progressBar = FindObjectOfType<ProgressBarProject>();
            progressBar.FalseAnswer();
            Debug.Log("La reponse donnee a la requete est incorrecte");
            Disapear();
        }

        protected void Update()
        {
            CheckCard();
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

        private void CheckCard() {
            RaycastHit hit;
            bool lHitSomething;

            if(exist) {
                lHitSomething = Physics.Raycast(cardHolderRectTransform.position, cardHolderRectTransform.TransformDirection(-Vector3.forward), out hit);

                if(lHitSomething && hit.collider.CompareTag("carte")) {

                    if(hit.collider.GetComponent<Card>().JobCode == jobCode && detectedCard is null) {
                        detectedCard = hit.collider.gameObject;

                        float delay = 0;

                        Vector3 tweenCardPosition = new Vector3(cardHolderRectTransform.position.x, cardHolderRectTransform.position.y - 1, detectedCard.transform.position.z);
                        Tween.Position(detectedCard.transform, tweenCardPosition, .25f, delay, Tween.EaseIn, Tween.LoopType.None);
                        delay += 0.5f;
                        //Tween.Position(detectedCard.transform, tweenCardPosition, .25f, delay, null, Tween.LoopType.None, CardDetected);
                        CardDetected();
                        cardHolderImage.color = Color.green;


                        CorrectAnswer();
                    }
                    else if(detectedCard is null) {
                        cardHolderImage.color = Color.red;
                        FalseAnswer();

                        hit.collider.gameObject.GetComponent<Card>().StopDrag();
                    }

                }
                else {
                    cardHolderImage.color = Color.grey;
                }
            }
        }

        protected void CardDetected()
        {
            Disapear();
            Destroy(detectedCard);
            detectedCard = null;
        }
    }
}