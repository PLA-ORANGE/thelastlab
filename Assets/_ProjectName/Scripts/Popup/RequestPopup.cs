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
        [SerializeField] private List<Sprite> spriteList = new List<Sprite>();
        [SerializeField] private Image image; 
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
                image.sprite = spriteList[(int)jobCode]; 
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
                Vector3 position = sender.transform.position;
                Vector3 position2 = sender.transform.position;
                position = cam.WorldToScreenPoint(position);
                Debug.Log("position" + position);
                Debug.Log("xMax :" + cardHolderRectTransform.rect.xMax + " xMin :" + cardHolderRectTransform.rect.xMin);
                Debug.Log("yMax :" + cardHolderRectTransform.rect.xMax + " yMin :" + cardHolderRectTransform.rect.yMin);
                if (position.x <= (cardHolderRectTransform.position.x + cardHolderRectTransform.rect.xMax) && position.x >= (cardHolderRectTransform.position.x + cardHolderRectTransform.rect.xMin))
                {
                    if (position.y <= (cardHolderRectTransform.position.y + cardHolderRectTransform.rect.yMax) && position.y >= (cardHolderRectTransform.position.y + cardHolderRectTransform.rect.yMin))
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
            float xMin = 0;
            float xMax = Screen.width/2 - rectTransform.rect.width ;
            float yMin = Screen.width/2 - rectTransform.rect.height;
            float yMax = Screen.width +rectTransform.rect.height;


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