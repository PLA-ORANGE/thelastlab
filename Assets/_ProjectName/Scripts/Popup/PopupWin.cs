///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using Pixelplacement;
using UnityEngine;

namespace Com.Github.PLAORANGE.Thelastlab.Popup 
    {
        public class PopupWin : Popup {

        [SerializeField] private GameObject invention;


        public override void Start() {
            base.Start();
          
        }
        public override void Appear() {
            Tween.LocalScale(rectTransform, new Vector2(0.50f, 0.50f), 1f, 0.1f, Tween.EaseOutBack);
            exist = true;
            invention =  GameObject.Instantiate(invention);
            invention.transform.position = new Vector3(transform.position.x, transform.position.y -10,transform.position.z); 
            Tween.LocalScale(invention.transform, new Vector3(5, 5,5), 1f, 0.1f, Tween.EaseOutBack);


        }





    }
}