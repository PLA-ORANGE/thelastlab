///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using Pixelplacement;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Github.PLAORANGE.Thelastlab
{
    public delegate void CardEventHandler(Card sender);
    public class Card : MonoBehaviour {

        [SerializeField] private TextMeshProUGUI titleText = null;
        [SerializeField] private SpriteRenderer perso = null;
        [SerializeField] private SpriteRenderer backgroundSprite = null;

        private const float BACKGROUND_COLOR_COEFF = 0.5f;

        public static event CardEventHandler OnCardTaken;
        public static event CardEventHandler OnCardDrop;

        public Job job;

        public bool isDestroying;

        public JobCode JobCode { 
            get { return job.code;}
        }

        public string Title {
            get{
                return titleText.text;
            }
            set{
                titleText.text = value;
            }
        }

        private Quaternion CameraRotation {
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
            Debug.Log(job);

            this.job = job;
            Title = this.job.name;

            Color lColor = this.job.color;
            perso.sprite = job.sprite;
            Debug.Log(lColor);

            //lColor.b *= BACKGROUND_COLOR_COEFF;

            backgroundSprite.color = lColor;
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
    }
}