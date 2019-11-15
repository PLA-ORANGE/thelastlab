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
        protected Camera cam;

        public static event RequestPopupEventHandler OnAppear;
        public static event RequestPopupEventHandler OnDisappear;
        protected  GameManager gameManager; 
        private JobCode jobCode;
        private int givingScore = 20;
        private bool eventIsInit;

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
            if (!eventIsInit) InitEvent();
        }

        public void InitEvent()
        {
            eventIsInit = true;
            Card.OnCardDrop += Card_OnCardDrop;
            Card.OnCardTaken += Card_OnCardTaken;
        }

        private void Card_OnCardTaken(Card sender)
        {
            if (exist)
            {
                Deck deck = FindObjectOfType<Deck>();
                deck.colliderOnly = false;
            }
        }

        private void Card_OnCardDrop(Card sender)
        {
            Debug.Log("Request");

            if (exist)
            {
                Vector3 position = cardHolderRectTransform.position;
                position = cam.ScreenToWorldPoint(position);

                if (sender.transform.position.x >= (position.x - cardHolderRectTransform.rect.width/2) && sender.transform.position.x <= (position.x + cardHolderRectTransform.rect.width / 2))
                {
                    if (sender.transform.position.y >= (position.y - cardHolderRectTransform.rect.height / 2) && sender.transform.position.y <= (position.y + cardHolderRectTransform.rect.height / 2))
                    {
                        if (sender.JobCode == jobCode && detectedCard is null)
                        {
                            detectedCard = sender.gameObject;

                            sender.Destroy();
                            detectedCard = null;

                            gameManager.SpawnInLab(sender.job);
                            CorrectAnswer();
                        }
                        else if (detectedCard is null)
                        {
                            FalseAnswer();
                            Deck deck = FindObjectOfType<Deck>();
                            deck.colliderOnly = false;
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
            float xMin = -Screen.width/3 + rectTransform.rect.width/2;
            float xMax = Screen.width/3 - rectTransform.rect.width / 2;
            float yMin = Screen.height/4 - rectTransform.rect.height / 2;
            float yMax = -Screen.height/4 + rectTransform.rect.height / 2;
            
            Debug.Log(xMin);
            Debug.Log(xMax);
            Vector2 position = new Vector2(UnityEngine.Random.Range(xMin,xMax), UnityEngine.Random.Range(yMin, yMax));
            Debug.Log(position);
            rectTransform.anchoredPosition = position;
            base.Appear();
            OnAppear?.Invoke(this);
        }

        public override void Disapear()
        {
            base.Disapear();
            OnDisappear?.Invoke(this);
        }
    }
}