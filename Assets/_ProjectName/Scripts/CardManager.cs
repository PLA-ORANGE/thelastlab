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
        [SerializeField] private GameObject deckPrefab = null;
        [SerializeField] private int cardNumber = 7;
        [SerializeField] private Transform deckSpawn = null;

        private GameObject deck;

		private void Start () {
            deck = Instantiate(deckPrefab, deckSpawn.position, Camera.main.transform.rotation);
            GameObject lCard;
            
            for (int i = 0; i < cardNumber; i++)
            {
                lCard = Instantiate(cardPrefab);
                deck.GetComponent<Deck>().AddCard(lCard);
            }
            
		}

        public void GameFinish() {
            deck.GetComponent<Deck>().Clear(); 
        }
		
		private void Update () {
		}
	}
}