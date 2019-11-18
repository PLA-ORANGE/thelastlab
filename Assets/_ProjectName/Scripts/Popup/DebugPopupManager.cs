///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 14/10/2019
///-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Com.Github.PLAORANGE.Thelastlab.Popup
{
	public class DebugPopupManager : MonoBehaviour {
		private static DebugPopupManager instance;
		public static DebugPopupManager Instance { get { return instance; } }

        [SerializeField]
        protected Button popupAppearButton;
        [SerializeField]
        protected Button popupDisapearButton;
        [SerializeField]
        protected Button Event1Button;
        [SerializeField]
        protected Button Event2Button;
        [SerializeField]
        protected RequestPopup requestPopup;
        public static Random rand = new Random(); 
        private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}
			
			instance = this;
		}
		
		private void Start () {
            popupAppearButton.onClick.AddListener(OnClickPopupAppearButton);
            popupDisapearButton.onClick.AddListener(OnClickDisapearButton);
            Event1Button.onClick.AddListener(OnClickEvent1);
            Event2Button.onClick.AddListener(OnClickEvent2);
            requestPopup = GameObject.FindObjectOfType<RequestPopup>();
        }

        

        protected void OnClickPopupAppearButton()
        {
            //requestPopup.JobCode = Job.GetAleaCode();
            requestPopup.Appear();
        }

        protected void OnClickDisapearButton()
        {
            requestPopup.Disapear();
        }

        protected void OnClickEvent1()
        {
            requestPopup.SetText("Event 1","Ceci est l'event 1");
            requestPopup.Appear();
        }
        private void OnClickEvent2()
        {
            requestPopup.SetText("Ceci est l'event 2");
            requestPopup.Appear();
        }
		
		private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}