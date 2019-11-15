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

        [SerializeField] private GameObject deckPrefab = null;
        [SerializeField] private GameObject cardPrefab = null;
        [SerializeField] private Transform deckSpawn = null;
        [SerializeField] private GameObject labMenPrefab = null;
        [SerializeField] private GameObject labo = null;

        [SerializeField] private GameObject hud = null;
        [SerializeField] private GameObject swipHud = null;
        [SerializeField] private RequestPopup requestPopup;

        protected PopupWin popupWin;
        [SerializeField] protected ProgressBarProject progressBar;
        
        protected float score = 0;  
        private GameObject deck;

        private bool isRequestPopUp;

        private Action GamePhase;

        [SerializeField]
        private List<JobCode> popupRequests = new List<JobCode>() {
            JobCode.Mathématicien,
            JobCode.Ingénieur,
            JobCode.Développeur,
            JobCode.Chimiste,
            JobCode.Mathématicien
        };

        [SerializeField]
        private List<JobCode> startCards = new List<JobCode>() {
            JobCode.Mathématicien,
            JobCode.Ingénieur,
            JobCode.Développeur,
            JobCode.Chimiste,
            JobCode.Mathématicien,
            JobCode.Mathématicien,
            JobCode.Mathématicien
        };

        [SerializeField] private float spawnFrequencyRequest = 4f;
        private float elapseTime = 0;

        private void Start () {
            popupWin = FindObjectOfType<PopupWin>();

            RequestPopup.OnDisappear += RequestPopup_OnDisappear;

            SetCardSelectPhase();

            //SetProjectPhase();

            //deck = Instantiate(deckPrefab, deckSpawn.position, Camera.main.transform.rotation);
            /*
            GameObject card;

            for (int i = 0; i < startCards.Count; i++)
            {
                card = Instantiate(cardPrefab);
                deck.GetComponent<Deck>().AddCard(card);
                card.GetComponent<Card>().setJob(startCards[i]);
            }*/

            
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
            GamePhase();
		}
        /*
        public void StartPhase()
        {
            if(Input.GetMouseButtonDown(0))
        }*/

        public void SelectProjectPhase

        public void SetCardSelectPhase()
        {
            GamePhase = VoidPhase;
            ///priorisation d'évenement
            requestPopup.InitEvent();
            ///

            SwipManager swipManager = gameObject.GetComponent<SwipManager>();

            deck = Instantiate(deckPrefab, deckSpawn.position, Camera.main.transform.rotation);

            swipManager.deck = deck.GetComponent<Deck>();
            swipManager.Init();

            swipHud.SetActive(true);
        }

        public void SetProjectPhase()
        {
            GamePhase = ProjectPhase;
            elapseTime = spawnFrequencyRequest / 2;

            swipHud.SetActive(false);
            hud.SetActive(true);

            gameObject.GetComponent<DragAndDrop>().active = true;
            gameObject.GetComponent<SwipManager>().active = false;
        }

        private void ProjectPhase()
        {
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

        protected void VoidPhase()
        {
        }
    }
}