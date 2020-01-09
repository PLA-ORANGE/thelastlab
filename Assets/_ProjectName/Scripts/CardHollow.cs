///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using Com.Github.PLAORANGE.Thelastlab.Popup;
using Pixelplacement;
using System;
using UnityEngine;

namespace Com.Github.PLAORANGE.Thelastlab
{
	public class CardHollow : MonoBehaviour {
		private static CardHollow instance;
		public static CardHollow Instance { get { return instance; } }
		private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}
			
			instance = this;
		}
		
		private void Start () {
            RequestPopup.OnDisappear += RequestPopup_OnDisappear;
            HollowAnim();
		}

        private void RequestPopup_OnDisappear(RequestPopup sender)
        {
            Destroy(gameObject);
        }

        private void HollowAnim()
        {
            Vector3 startPoint = new Vector3();
            Card[] cards = FindObjectsOfType<Card>();
            RequestPopup popup = FindObjectOfType<RequestPopup>();
            foreach(Card card in cards)
            {
                if(card.JobCode == popup.JobCode)
                {
                    startPoint = card.backgroundSprite.transform.position;
                    transform.rotation = card.transform.rotation;
                    transform.parent = card.backgroundSprite.transform;
                    transform.localScale = Vector3.one;
                    transform.parent = null;
                }
            }
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(popup.transform.position.x, popup.transform.position.y, Camera.main.nearClipPlane));
            Debug.Log("point : " + point);
            Tween.Position(transform,startPoint ,point, 1, 0, Tween.EaseInOut, Tween.LoopType.None, null, null,true);
            Tween.LocalRotation(transform,cards[0].CameraRotation, 0.5f, 0, Tween.EaseInOut, Tween.LoopType.None, null, null,true);
            Tween.LocalScale(transform, Vector3.zero, 0.5f, 1, Tween.EaseInBack, Tween.LoopType.None, null, HollowAnim,true);
        }
		
		private void OnDestroy(){
            RequestPopup.OnDisappear -= RequestPopup_OnDisappear;

            if (this == instance) instance = null;
		}
	}
}