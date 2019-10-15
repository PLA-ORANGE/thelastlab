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

        public void FalseAnswer()
        {
            Debug.Log("La reponse donnee a la requete est incorrecte");
            Disapear();
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
            Debug.Log("La reponse donnee a la requete est correct");
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
                 hit = Physics2D.Raycast(cardHolderRectTransform.position, cardHolderRectTransform.TransformDirection(-Vector3.forward)/*out hit*Mathf.Infinity*/, layerMask); 
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("carte"))
                    {
                        Debug.DrawRay(cardHolderRectTransform.position, transform.TransformDirection(-Vector3.forward) * hit.distance, Color.yellow);

                        if(hit.collider.GetComponent<Perso>().job == textPopup) {
                            Disapear();
                            cardHolderImage.color = Color.green;
                            GameObject.Destroy(hit.collider.gameObject);
                        }

                        else cardHolderImage.color = Color.red; 
                    }
                }
                else
                {
                    Debug.DrawRay(cardHolderRectTransform.position, transform.TransformDirection(-Vector3.forward) * 1000, Color.red);
                    cardHolderImage.color = Color.grey;
                }
            }
        }
    }
}