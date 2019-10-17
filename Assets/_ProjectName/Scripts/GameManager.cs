///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------


using Com.Github.PLAORANGE.Thelastlab.Hud;
using Com.Github.PLAORANGE.Thelastlab.Popup;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Github.PLAORANGE.Thelastlab
{
	public class GameManager : MonoBehaviour {

        [SerializeField] private GameObject cardPrefab = null;
        [SerializeField] private GameObject deckPrefab = null;
        [SerializeField] private int cardNumber = 7;
        [SerializeField] private Transform deckSpawn = null;
        protected PopupWin popupWin;
        protected ProgressBarProject progressBar;
        protected float scoreValue = 0;  
        private GameObject deck;

		private void Start () {
            deck = Instantiate(deckPrefab, deckSpawn.position, Camera.main.transform.rotation);
            GameObject lCard;
            
            for (int i = 0; i < cardNumber; i++)
            {
                lCard = Instantiate(cardPrefab);
                deck.GetComponent<Deck>().AddCard(lCard);
            }
            popupWin = FindObjectOfType<PopupWin>();
            progressBar = FindObjectOfType<ProgressBarProject>();
        }

        public void ClearAll() {
            deck.GetComponent<Deck>().Clear();
            progressBar.ClearBar(); 
            
        }

        public void DisplayWinScreen() {
            popupWin.Appear();
            popupWin.SetText("Félicitations !!", "Voici votre invention");
        }

        public void  UpdateScore (float value) {
            scoreValue += value;
            progressBar.UpdateSlider(scoreValue);
           
        }

        public void CheckScoreValue() {
            if(scoreValue < 100) return;

            ClearAll();
            DisplayWinScreen();
        }

        private void Update () {
		}
	}
}