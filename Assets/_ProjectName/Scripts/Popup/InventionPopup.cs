///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using Pixelplacement;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Github.PLAORANGE.Thelastlab.Popup
{
    public class InventionPopup : Popup
    {
        [HideInInspector]public bool onClick = false;
        protected PopupInventionContainer container;
        [SerializeField] RectTransform inventionPos;
        [SerializeField] private GameObject invention;
        [SerializeField] private RectTransform centerScreen;
        [SerializeField] private bool testPopup; 
        private RectTransform inventionRect;
        private Vector2 initalPosition;
        
        
        public override void Start() {
            base.Start();
            rectTransform.localScale = new Vector2(1.5f, 1.5f);
            Appear();
            container = FindObjectOfType<PopupInventionContainer>();
            invention = GameObject.Instantiate(invention, inventionPos);
            inventionRect = invention.AddComponent<RectTransform>(); 
            inventionRect.localPosition = Vector3.zero;
            invention.GetComponent<Invention>().win = false;
            initalPosition = transform.position;


        }


        public override void Appear() {
            //Tween.LocalScale(rectTransform, new Vector2(0.5f, 0.6f), 1f, 0.1f, Tween.EaseOutBack);
            exist = true;
        }
        
        public void OnClick() {
            container.CheckPopup();
            SetText("");
            Tween.LocalScale(rectTransform, new Vector2(2.2f, 2.2f), 0.3f, 0, Tween.EaseOutBack);
            Tween.Position(rectTransform, new Vector2(centerScreen.position.x, centerScreen.position.y), 0.5f, 0, Tween.EaseInOutBack, Tween.LoopType.None, null, SetDescription);
            onClick = true;
            container.CheckWichInvention(gameObject.name);

        }

        public void NotOnClick() {
            onClick = false;
            SetText(""); 
            Tween.LocalScale(rectTransform, new Vector2(1, 1), 0.3f, 0, Tween.EaseOutBack);
            Tween.Position(rectTransform, initalPosition, 0.5f, 0, Tween.EaseInOutBack, Tween.LoopType.None, null, SetDescription);
        }

        private void SetDescription() {
            if(testPopup && onClick) {
                SetText("Séticyksapu était une ville prospère jusqu’à ce que des gaz toxiques y soient détectés, et que la population ait dû être évacuée.");
                popupText.fontSize = 15;
                popupText.rectTransform.anchoredPosition = new Vector2(popupText.rectTransform.anchoredPosition.x, -17); 
            }
            else if(testPopup && !onClick) {
                SetText("Drone assainissant");
                popupText.fontSize = 20;
                popupText.rectTransform.anchoredPosition = new Vector2(popupText.rectTransform.anchoredPosition.x, -1.5f);
            }

            if(!testPopup) SetText("Bloqué"); 

        }
    }
}