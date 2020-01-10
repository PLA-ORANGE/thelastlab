///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using Pixelplacement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Github.PLAORANGE.Thelastlab.Popup
{
    public class InventionPopup : Popup
    {
        [HideInInspector] public bool onClick = false;
        protected PopupInventionContainer container;

        [SerializeField] RectTransform inventionPos;
        [SerializeField] private GameObject invention;
        [SerializeField] private RectTransform centerScreen;
        [SerializeField] private bool testPopup;
        [SerializeField] private float timingAppear;
        [SerializeField] private Color textColor;

        private RectTransform inventionRect;
        private Vector3 initalPosition;


        public override void Start() {
            base.Start();
            rectTransform.localScale = Vector2.zero;
            Appear();
            container = FindObjectOfType<PopupInventionContainer>();
            invention = GameObject.Instantiate(invention, inventionPos);
            inventionRect = invention.AddComponent<RectTransform>();
            inventionRect.localPosition = Vector3.zero;


            invention.GetComponent<Invention>().win = false;
            Tween.LocalScale(rectTransform, new Vector3(1.5f, 1.5f, 1.5f), timingAppear, 0.1f, Tween.EaseOutBack);

            // else Tween.LocalScale(rectTransform, new Vector3(0.4f, 0.4f, 0.4f), timingAppear, 0.1f, Tween.EaseOutBack);
            initalPosition = transform.position;
            //Tween.LocalScale(rectTransform, new Vector3(1.5f, 1.5f,1.5f), timingAppear, 0.1f, Tween.EaseOutBack);

        }


        public override void Appear() {
            //Tween.LocalScale(rectTransform, new Vector2(0.5f, 0.6f), 1f, 0.1f, Tween.EaseOutBack);
            exist = true;
        }

        public void OnClick() {
            container.CheckPopup();
            SetText("");
            Tween.LocalScale(rectTransform, new Vector3(2.2f, 2.2f, 2.2f), 0.3f, 0, Tween.EaseOutBack);
            Tween.Position(rectTransform, new Vector3(centerScreen.position.x, centerScreen.position.y, 0), 0.5f, 0, Tween.EaseInOutBack, Tween.LoopType.None, null, SetDescription);
            onClick = true;
            container.CheckWichInvention(gameObject.name);

        }

        public void NotOnClick() {
            onClick = false;
            SetText("");
            Tween.LocalScale(rectTransform, new Vector3(1.5f, 1.5f, 1.5f), 0.3f, 0, Tween.EaseOutBack);
            Tween.Position(rectTransform, initalPosition, 0.5f, 0, Tween.EaseInOutBack, Tween.LoopType.None, null, SetDescription);
            //transform.GetChild(0).gameObject.SetActive(false);
            
        }

        private void SetDescription() {
            if(testPopup && onClick) {
              

                transform.GetChild(0).gameObject.SetActive(true);
                popupText.color = textColor;
                popupText.fontSize = 11;
                popupText.rectTransform.anchoredPosition = new Vector2(popupText.rectTransform.anchoredPosition.x, 25.2f);
            }
            else if(testPopup && !onClick) {
                SetText("Drone assainissant");
                popupText.fontSize = 11;
                popupText.rectTransform.anchoredPosition = new Vector2(popupText.rectTransform.anchoredPosition.x, 30);
            }

            /*if(!testPopup && onClick) {
                popupText.rectTransform.anchoredPosition = new Vector2(popupText.rectTransform.anchoredPosition.x, 30);
                SetText("Bloqué");
            }*/
        }

        protected override void UpdateText() {
            
        }
    }
}