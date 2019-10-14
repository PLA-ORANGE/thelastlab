///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 14/10/2019
///-----------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.UI;

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
            popupAppearButton.onClick.AddListener(OnClickEvent2);

        }

        

        protected void OnClickPopupAppearButton()
        {

        }

        protected void OnClickDisapearButton()
        {

        }

        protected void OnClickEvent1()
        {

        }
        private void OnClickEvent2()
        {

        }


        private void Update () {
			
		}
		
		private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}