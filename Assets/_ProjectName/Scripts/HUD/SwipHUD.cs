///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Github.PLAORANGE.Thelastlab.Hud
{
	public class SwipHUD : MonoBehaviour {

        [SerializeField] GameObject iventionModel;
        [SerializeField] float modelSize;
        
        [SerializeField] Text cartCount;
        [SerializeField] Text inventionName;

        [SerializeField] SwipManager swipManager;
	    
        public string CartCountTxt
        {
            set
            {
                cartCount.text = value;
            }
        }

        public string InventionNameTxt 
        {
            set
            {
                inventionName.text = value;
            }
        }

        private void Start () {
            swipManager.OnValidateCard += SwipManager_OnValidateCard;
        }

        private void SwipManager_OnValidateCard(SwipManager sender)
        {
            Deck deck = sender.Deck;

            CartCountTxt = deck.Count + " / " + deck.MaxCard;
        }

        private void Update () {
            iventionModel.transform.localScale = Vector3.one * modelSize;
        }
	}
}