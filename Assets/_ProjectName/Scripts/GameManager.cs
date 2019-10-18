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
        //[SerializeField] private int cardNumber = 7;
        [SerializeField] private Transform deckSpawn = null;
        [SerializeField] private GameObject labMenPrefab = null;
        [SerializeField] private GameObject labo = null;

        protected PopupWin popupWin;
        protected ProgressBarProject progressBar;
        protected float score = 0;  
        private GameObject deck;

        [SerializeField]
        private List<JobCode> startCards = new List<JobCode>() {
            JobCode.Mathématicien, 
            JobCode.Chimiste, 
            JobCode.Développeur, 
            JobCode.Mathématicien,
            JobCode.Ingénieur,
            JobCode.Chimiste,
            JobCode.Développeur
        };

		private void Start () {
            deck = Instantiate(deckPrefab, deckSpawn.position, Camera.main.transform.rotation);
            GameObject lCard;
            
            for (int i = 0; i < startCards.Count; i++)
            {
                lCard = Instantiate(cardPrefab);
                lCard.GetComponent<Card>().setJob(startCards[i]);

                deck.GetComponent<Deck>().AddCard(lCard);
            }

            popupWin = FindObjectOfType<PopupWin>();
            progressBar = FindObjectOfType<ProgressBarProject>();
        }

        public void SpawnInLab(Job job)
        {
            GameObject labPeople = Instantiate(labMenPrefab, labo.transform.position, Quaternion.identity, labo.transform);
            labPeople.GetComponent<MeshRenderer>().material.color = job.color;
        }

        public void ClearAll() {
            deck.GetComponent<Deck>().Clear();
            progressBar.ClearBar(); 
            
        }

        public void DisplayWinScreen() {
            popupWin.Appear();
            popupWin.SetText("Félicitations !!", "Voici votre invention");
        }

        public void  AddScore (float value) {
            score = Mathf.Clamp(score + value, 0, 100);
            progressBar.UpdateSlider(score);
        }

        public void CheckScoreValue() {
            if(score < 100) return;

            ClearAll();
            DisplayWinScreen();
        }

        private void Update () {
		}
	}
}