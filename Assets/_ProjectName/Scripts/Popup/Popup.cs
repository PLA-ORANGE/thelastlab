///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Pixelplacement;
namespace Com.Github.PLAORANGE.Thelastlab.Popup 
    {
	public class Popup : MonoBehaviour {
        [SerializeField]
        protected TextMeshProUGUI popupTitle;
        [SerializeField]
        protected TextMeshProUGUI popupText;
        protected RectTransform rectTransform;
        protected Vector2 initialPos;
        [SerializeField]
        protected string titlePopup;
        [SerializeField]
        protected string textPopup;
        protected bool exist = false;

        private void Update () {
			
		}



        public string TextPopup {
            get => textPopup;
            set {
                textPopup = value;
                UpdateText();
            }
        }

        protected string TitlePopup {
            get => titlePopup;
            set {
                titlePopup = value;
                UpdateText();
            }
        }

        public void SetText() {
            TitlePopup = "";
            TextPopup = "";

        }

        public void SetText(string title, string text) {
            TitlePopup = title;
            TextPopup = text;
        }

        public void SetText(string text) {
            TitlePopup = "";
            TextPopup = text;
        }


        protected void UpdateText() {
            popupTitle.text = TitlePopup;
            popupText.text = TextPopup;
        }

        virtual public void Start() {
            rectTransform = GetComponent<RectTransform>();
            initialPos = rectTransform.anchoredPosition;
            UpdateText();
            rectTransform.localScale = new Vector2(0, 0);
        }



        virtual public void Appear() {
            Tween.LocalScale(rectTransform, new Vector2(1, 1), 1f, 0.1f, Tween.EaseOutBack);
            exist = true;
        }

        virtual public void Disapear() {
            Tween.LocalScale(rectTransform, new Vector2(0, 0), 0.5f, 0.1f, Tween.EaseIn);
            exist = false;
        }
    }
}