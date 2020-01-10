﻿///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Github.PLAORANGE.Thelastlab.Hud
{
	public class SwipHUD : MonoBehaviour {

        [SerializeField] private GameObject inventionModel;
        [SerializeField] private float modelSize;
        
        [SerializeField] private Text cartCount;
        [SerializeField] private Text inventionName;

        [SerializeField] private SwipManager swipManager;


	    
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
            //inventionModel = Instantiate(iventionPrefab, inventionSpawn);
            GetComponent<Animator>().SetTrigger("appear");
        }

        private void SwipManager_OnValidateCard(SwipManager sender)
        {
            Deck deck = sender.deck;

            CartCountTxt = deck.Count + " / 4";
        }

        private void Update () {
            inventionModel.transform.localScale = Vector3.one * modelSize;
        }
	}
}