///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Com.Github.PLAORANGE.Thelastlab
{
    public delegate void CardEventHandler(Card sender);
    public class Card : MonoBehaviour {

        [SerializeField] private TextMeshPro titleText = null;
        [SerializeField] private SpriteRenderer logoSprite = null;
        [SerializeField] private Perso perso = null;
        [SerializeField] private SpriteRenderer backgroundSprite = null;

        private const float BACKGROUND_COLOR_COEFF = 0.5f;

        public static event CardEventHandler OnCardTaken;
        public static event CardEventHandler OnCardDrop;

        private Job job;

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

        private void Start () {
            setJob(Job.GetAleaJob());
        }

        public void setJob(Job job)
        {
            this.job = job;
            Title = this.job.code.ToString();

            Color lColor = this.job.color;

            perso.Color = lColor;
            logoSprite.color = lColor;

            lColor.g *= BACKGROUND_COLOR_COEFF;
            lColor.r *= BACKGROUND_COLOR_COEFF;
            lColor.b *= BACKGROUND_COLOR_COEFF;

            backgroundSprite.color = lColor;
        }
	}
}