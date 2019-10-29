///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------


using Com.Github.PLAORANGE.Thelastlab.Hud;
using Com.Github.PLAORANGE.Thelastlab.Popup;
using System;
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
        private RequestPopup requestPopup;
        protected float score = 0;  
        private GameObject deck;

        private bool isRequestPopUp;

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

        [SerializeField]
        private List<JobCode> popupRequests = new List<JobCode>() {
            JobCode.Mathématicien,
            JobCode.Ingénieur,
            JobCode.Développeur,
            JobCode.Chimiste,
            JobCode.Mathématicien
        };

        [SerializeField] private float spawnFrequencyRequest = 4f;
        private float elapseTime = 0;

        private void Start () {
            deck = Instantiate(deckPrefab, deckSpawn.position, Camera.main.transform.rotation);
            GameObject lCard;
            
            for (int i = 0; i < startCards.Count; i++)
            {
                lCard = Instantiate(cardPrefab);
                lCard.GetComponent<Card>().setJob(startCards[i]);

                deck.GetComponent<Deck>().AddCard(lCard, true);
            }

            popupWin = FindObjectOfType<PopupWin>();
            progressBar = FindObjectOfType<ProgressBarProject>();
            requestPopup = FindObjectOfType<RequestPopup>();

            RequestPopup.OnDisappear += RequestPopup_OnDisappear;

            elapseTime = spawnFrequencyRequest / 2;
        }

        private void RequestPopup_OnDisappear(RequestPopup sender)
        {
            isRequestPopUp = false;
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
            if (isRequestPopUp || popupRequests.Count == 0) return;

            elapseTime += Time.deltaTime;

            if (elapseTime < spawnFrequencyRequest) return;
            
            elapseTime -= spawnFrequencyRequest;

            JobCode jobCode = popupRequests[0];
            popupRequests.Remove(jobCode);

            requestPopup.JobCode = jobCode;
            requestPopup.Appear();

            isRequestPopUp = true;
            
		}
	}
}