///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 14/10/2019
///-----------------------------------------------------------------

using UnityEngine;
using TMPro;

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
        protected string titlePopup;
        [SerializeField]
        protected string textPopup;

        public string TextPopup
        {
            get => textPopup; 
            set
            {
                textPopup = value;
                
            }
        }

        protected string TitlePopup
        {
            get => titlePopup; 
            set
            {
                titlePopup = value;
            }
        }

        protected void SetText()
        {
            TitlePopup = "";
            TextPopup = "";

        }

        protected void SetText(string title, string text)
        {
            TitlePopup = title;
            TextPopup = text;
        }

        protected void SetText(string text)
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
            UpdateText();
        }

        public void FalseAnswer()
        {
            Debug.Log("La reponse donnee a la requete est incorrecte");
        }

        public void CorrectAnswer()
        {
            Debug.Log("La reponse donnee a la requete est correct");
        }
    }
}