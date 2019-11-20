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
           
            exist = true;
            invention =  GameObject.Instantiate(invention,inventionPos);
            
            SetText("Bravo voici ton Drone assanissant");
            Tween.LocalScale(invention.transform, new Vector3(3, 3, 3), 1f, 0.1f,Tween.EaseIn);
        }

    }
}