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
        public bool onClick = false;
        protected PopupInventionContainer container;
        [SerializeField] RectTransform inventionPos;
        [SerializeField] private GameObject invention;
        [SerializeField] private RectTransform centerScreen;
        private RectTransform inventionRect;
        private Vector2 initalPosition;
        
        
        public override void Start() {
            base.Start();
            rectTransform.localScale = new Vector2(1, 1);
            Appear();
            container = FindObjectOfType<PopupInventionContainer>();
            invention = GameObject.Instantiate(invention, inventionPos);
            inventionRect = invention.AddComponent<RectTransform>(); 
            inventionRect.localPosition = Vector3.zero;
            invention.GetComponent<Invention>().win = false;
            SetText(name);
            initalPosition = transform.position;

        }


        public override void Appear() {
            //Tween.LocalScale(rectTransform, new Vector2(0.5f, 0.6f), 1f, 0.1f, Tween.EaseOutBack);
            exist = true;
        }
        
        protected void OnClick() {
            if(!onClick) {
              
                container.CheckPopup();
                SetText(""); 
                Tween.LocalScale(rectTransform, new Vector2(2.5f, 2.5f), 0.3f, 0, Tween.EaseOutBack);
                Tween.Position(rectTransform, new Vector2(centerScreen.position.x, centerScreen.position.y), 0.5f, 0, Tween.EaseInOutBack,Tween.LoopType.None,null, ()=> { SetText("inserer description du bail tu connais le delire boy"); });
                onClick = true;
                container.CheckWichInvention(gameObject.name); 
            }
           
        }

        public void NotOnClick() {
            onClick = false;
            SetText(""); 
            Tween.LocalScale(rectTransform, new Vector2(1, 1), 0.3f, 0, Tween.EaseOutBack);
            Tween.Position(rectTransform, initalPosition, 0.5f, 0, Tween.EaseInOutBack, Tween.LoopType.None, null, () => { SetText(name); });
        }
    }
}