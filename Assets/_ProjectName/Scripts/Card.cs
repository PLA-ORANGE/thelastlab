﻿///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using Com.Github.PLAORANGE.Thelastlab.Popup;
using Pixelplacement;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Github.PLAORANGE.Thelastlab
{
    public delegate void CardEventHandler(Card sender);
    public class Card : MonoBehaviour {

        [SerializeField] private SpriteRenderer perso = null;
        public SpriteRenderer backgroundSprite = null;
        [SerializeField] protected GameObject cardHollow;
        private const float BACKGROUND_COLOR_COEFF = 0.5f;

        public static event CardEventHandler OnCardTaken;
        public static event CardEventHandler OnCardDrop;

        public Job job;

        public bool isDestroying;


        protected void Awake()
        {
            RequestPopup.OnAppear += RequestPopup_OnAppear;
        }

        protected void RequestPopup_OnAppear(RequestPopup sender)
        {
            if (RequestPopup.firstCard)
            {
                RequestPopup.firstCard = false;
                if(job.code == sender.JobCode)
                {
                    Transform hollow = Instantiate(cardHollow, backgroundSprite.transform).transform;
                    hollow.localPosition = Vector3.zero;
                    hollow.localScale = Vector3.one;
                    hollow.localRotation = Quaternion.Euler(Vector3.zero);
                    hollow.parent = null;
                }
            }
        }

        public JobCode JobCode { 
            get { return job.code;}
        }

        public Quaternion CameraRotation {
            get
            {
                return Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up);
            }
        }

        public void RotateInZ(float angle)
        {
            transform.rotation = CameraRotation * Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
        }

        public void RotateInZ(float angle,bool isTween = true)
        {
            Tween.Rotation(transform, CameraRotation * Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward), 0.2f, 0);
        }


        public void StartDrag()
        {
            OnCardTaken?.Invoke(this);
            transform.rotation = CameraRotation;

            GetComponent<Collider>().enabled = false;
        }

        public void StopDrag()
        {
            GetComponent<Collider>().enabled = true;
            OnCardDrop?.Invoke(this);
        }

        public void setJob(Job job)
        {
            
            this.job = job;
            //Title = this.job.name;

            Color lColor = this.job.color;
            perso.sprite = job.sprite;


            //lColor.b *= BACKGROUND_COLOR_COEFF;

            backgroundSprite.sprite = job.backgroundSprite;
        }

        public void setJob(JobCode jobCode)
        {
            setJob(new Job(jobCode));
        }

        public void Destroy()
        {
            isDestroying = true;
            Destroy(gameObject);
        }

        protected void OnDestroy()
        {
            RequestPopup.OnAppear -= RequestPopup_OnAppear;
        }
    }
}