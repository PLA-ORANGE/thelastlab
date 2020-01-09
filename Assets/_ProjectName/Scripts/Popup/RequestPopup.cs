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

    public class RequestPopup : Popup
    {
        [SerializeField]
        protected RectTransform cardSpawner;
        [SerializeField]
        protected RectTransform cardHolderRectTransform;
        protected GameObject detectedCard;
        protected Camera cam;
        [SerializeField] private List<Sprite> spriteList = new List<Sprite>();
        [SerializeField] private Image image;
        public static event RequestPopupEventHandler OnAppear;
        public static event RequestPopupEventHandler OnDisappear;
        protected GameManager gameManager;
        private JobCode jobCode;
        private int givingScore = 20;
        private bool eventIsInit;
        static public bool firstCard = true;
        public JobCode JobCode
        {
            get => jobCode;
            set
            {
                jobCode = value;
                image.sprite = spriteList[(int)jobCode];
            }
        }

        override public void Start()
        {
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

            if (exist)
            {
                Vector3 cardPos = Vector3.ProjectOnPlane(sender.transform.position, Camera.main.transform.forward);
                //cardPos.z = Camera.main.nearClipPlane;
                //Vector2 cardPos = Camera.main.WorldToScreenPoint(sender.backgroundSprite.transform.position);
                //cardPos = Camera.main.ScreenToWorldPoint(new Vector3(cardPos.x, cardPos.y, Camera.main.nearClipPlane));
                Vector3 popupPos = Camera.main.ScreenToWorldPoint(new Vector3(transform.position.x, transform.position.y, Camera.main.nearClipPlane));
                popupPos = Vector3.ProjectOnPlane(popupPos, Camera.main.transform.forward);
                
                float distance = Vector3.Distance(cardPos, popupPos);

                Debug.Log("pos : " + popupPos);
                Debug.DrawLine(cardPos, popupPos, Color.red, 1000);

                if (distance <= 2)
                {
                    if(sender.JobCode == jobCode && detectedCard is null)
                    {
                        detectedCard = sender.gameObject;

                        //sender.Destroy();
                        detectedCard = null;

                        gameManager.SpawnInLab(sender.job, cardPos);
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
            float xMin = cardSpawner.rect.xMin + rectTransform.rect.width/2;
            float xMax = cardSpawner.rect.xMax - rectTransform.rect.width / 2;
            float yMin = cardSpawner.rect.yMin + rectTransform.rect.height/ 2;
            float yMax = cardSpawner.rect.yMax - rectTransform.rect.height / 2;

            Vector2 position =/* new Vector2(450, 450);*/ new Vector2(UnityEngine.Random.Range(xMin, xMax), UnityEngine.Random.Range(yMin, yMax));

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