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

        private Vector3 deckPos = new Vector3(2.86f, -10f, 4.2f);

        private GameObject deck;

		private void Start () {

            deck = Instantiate(deckPrefab, deckPos, Camera.main.transform.rotation);
            GameObject lCard;
            
            for (int i = 0; i < cardNumber; i++)
            {
                lCard = Instantiate(cardPrefab);
                deck.GetComponent<Deck>().AddCard(lCard);
            }
            
		}
		
		private void Update () {
		}
	}
}