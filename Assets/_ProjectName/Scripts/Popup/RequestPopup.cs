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
	public class RequestPopup : Popup {

        [SerializeField]
        protected RectTransform cardHolderRectTransform;
        protected GameObject detectedCard;
        [SerializeField]
        protected Image cardHolderImage;
        protected Camera cam;


        
       override public void Start() {
            base.Start();
            cam = Camera.main; 
        }
            
        
        public void CorrectAnswer()
        {
            ProgressBarProject progressBar = FindObjectOfType<ProgressBarProject>();
            progressBar.Slidervalue += 100;
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

        private void CheckCard() {
            RaycastHit hit;
            bool lHitSomething;

            if(exist) {
                lHitSomething = Physics.Raycast(cardHolderRectTransform.position, cardHolderRectTransform.TransformDirection(-Vector3.forward), out hit);

                if(lHitSomething && hit.collider.CompareTag("carte")) {

                    if(hit.collider.GetComponent<Card>().job == textPopup && detectedCard is null) {
                        CorrectAnswer();
                        float delay = 0;
                        detectedCard = hit.collider.gameObject;
                        Vector3 tweenCardPosition = new Vector3(cardHolderRectTransform.position.x, cardHolderRectTransform.position.y - 1, detectedCard.transform.position.z);
                        Tween.Position(detectedCard.transform, tweenCardPosition, .25f, delay, Tween.EaseIn, Tween.LoopType.None);
                        delay += 0.5f;
                        Tween.Position(detectedCard.transform, tweenCardPosition, .25f, delay, null, Tween.LoopType.None, CardDetected);
                        cardHolderImage.color = Color.green;

                    }
                    else if(detectedCard is null) {
                        cardHolderImage.color = Color.red;
                        FalseAnswer();
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
            GameObject.Destroy(detectedCard);
            detectedCard = null;
        }
    }
}