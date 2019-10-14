///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace Com.Github.PLAORANGE.Thelastlab
{
	public class CardManager : MonoBehaviour {

        [SerializeField] private GameObject cardPrefab = null;
        [SerializeField] private Deck deck = null;
        [SerializeField] private int cardNumber = 7;

        [SerializeField] private List<string> jobList = new List<string>();

        private bool onSpaceDown = false;
	
		private void Start () {
            GameObject lCard;

            for (int i = 0; i < cardNumber; i++)
            {
                lCard = Instantiate(cardPrefab);
                deck.AddCard(lCard);
            }
		}
		
		private void Update () {
            if (!onSpaceDown && Input.GetKey(KeyCode.Space))
            {
                Debug.Log("teub");
                onSpaceDown = true;
                
                GameObject card = deck.GetCard(0);
                deck.RemoveCard(card);
                Destroy(card);
            }
            else if(onSpaceDown) onSpaceDown = false;
		}
	}
}