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
        [SerializeField] private Transform inventionPos;

        public override void Appear() {
            Tween.LocalScale(rectTransform, new Vector2(0.50f, 0.50f), 1f, 0.1f, Tween.EaseOutBack);
            exist = true;
            invention =  GameObject.Instantiate(invention,inventionPos);
            //invention.transform.position =  
            Tween.LocalScale(invention.transform, new Vector3(3, 3, 3), 1f, 0.1f, Tween.EaseOutBack);
        }

    }
}