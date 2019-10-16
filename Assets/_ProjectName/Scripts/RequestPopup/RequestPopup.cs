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
	public class RequestPopup : MonoBehaviour {

        [SerializeField]
        protected TextMeshProUGUI popupTitle;
        [SerializeField]
        protected TextMeshProUGUI popupText;

        protected Vector2 initialPos;
        protected RectTransform rectTransform;
        [SerializeField]
        protected RectTransform cardHolderRectTransform;
        [SerializeField]
        protected Image cardHolderImage;

        [SerializeField]
        protected string titlePopup;
        [SerializeField]
        protected string textPopup;

        protected GameObject detectedCard;

        protected bool exist = false;
       

        protected Camera cam;


        public string TextPopup
        {
            get => textPopup; 
            set
            {
                textPopup = value;
                UpdateText();
            }
        }

        protected string TitlePopup
        {
            get => titlePopup; 
            set
            {
                titlePopup = value;
                UpdateText();
            }
        }

        public void SetText()
        {
            TitlePopup = "";
            TextPopup = "";

        }

        public void SetText(string title, string text)
        {
            TitlePopup = title;
            TextPopup = text;
        }

        public void SetText(string text)
        {
            TitlePopup = "";
            TextPopup = text;
        }
        

        protected void UpdateText()
        {
            popupTitle.text = TitlePopup;
            popupText.text = TextPopup;
        }

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            initialPos = rectTransform.anchoredPosition;
            cam = Camera.main;
            UpdateText();
            rectTransform.localScale = new Vector2(0, 0);
        }

        

        public void Appear()
        {
            Tween.LocalScale(rectTransform, new Vector2(1, 1), 1f, 0.1f, Tween.EaseOutBack);
            exist = true;
        }

        public void Disapear()
        {
            Tween.LocalScale(rectTransform, new Vector2(0, 0), 0.5f, 0.1f, Tween.EaseIn);
            exist = false;
        }

        public void CorrectAnswer()
        {
            ProgressBarProject progressBar = FindObjectOfType<ProgressBarProject>();
            progressBar.Slidervalue += 10;
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

        private void CheckCard()
        {
            int layerMask = 1 << 8;
            layerMask = ~layerMask;
            RaycastHit2D hit;

            if (exist)
            {
                 hit = Physics2D.Raycast(cardHolderRectTransform.position, cardHolderRectTransform.TransformDirection(-Vector3.forward), layerMask); 
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("carte"))
                    {
                        Debug.DrawRay(cardHolderRectTransform.position, transform.TransformDirection(-Vector3.forward) * hit.distance, Color.yellow);

                        if(hit.collider.GetComponent<Perso>().job == "Mathematicien" && detectedCard is null) {
                            float delay = 0;
                            detectedCard = hit.collider.gameObject;
                            Vector3 tweenCardPosition = new Vector3(cardHolderRectTransform.position.x, cardHolderRectTransform.position.y - 1, detectedCard.transform.position.z);
                            Tween.Position(detectedCard.transform, tweenCardPosition, .25f, delay, Tween.EaseIn, Tween.LoopType.None);
                            delay += 0.5f;
                            Tween.Position(detectedCard.transform, tweenCardPosition, .25f, delay, null, Tween.LoopType.None,CardDetected);
                            cardHolderImage.color = Color.green;
                            
                        }
                        else if(detectedCard is null)
                        {
                            cardHolderImage.color = Color.red;
                        }
                    }
                }
                else
                {
                    Debug.DrawRay(cardHolderRectTransform.position, transform.TransformDirection(-Vector3.forward) * 1000, Color.red);
                    cardHolderImage.color = Color.grey;
                }
            }
        }

        protected void CardDetected()
        {
            //Debug.Log("coucou");
            Disapear();
            GameObject.Destroy(detectedCard);
            detectedCard = null;
        }
    }
}