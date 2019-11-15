﻿///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------


using Com.Github.PLAORANGE.Thelastlab.Hud;
using Com.Github.PLAORANGE.Thelastlab.Popup;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Github.PLAORANGE.Thelastlab
{
	public class GameManager : MonoBehaviour {

        [SerializeField] private GameObject deckPrefab = null;
        [SerializeField] private Transform deckSpawn = null;
        [SerializeField] private GameObject labMenPrefab = null;
        [SerializeField] private GameObject labo = null;

        [SerializeField] private GameObject hud = null;
        [SerializeField] private GameObject swipHud = null;
        [SerializeField] private RequestPopup requestPopup;

        [SerializeField] protected PopupWin popupWin;
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

        [SerializeField] float spawnFrequencyRequest = 4f;
        [SerializeField, Range(1,10)] protected float spawnFrequencyRequestMax = 4f;
        [SerializeField, Range(1,10)] protected float spawnFrequencyRequestMin = 4f;
        private float elapseTime = 0;

        [SerializeField] private PopupInventionContainer inventionContainer;
        [SerializeField] private Button inventionValidateBtn;

        [SerializeField] private Text TouchTxt;

        private void Start () {

            RequestPopup.OnDisappear += RequestPopup_OnDisappear;
            spawnFrequencyRequest = UnityEngine.Random.Range(spawnFrequencyRequestMin, spawnFrequencyRequestMax);
            GamePhase = WaitToStart;
            //SetSelectProjectPhase();
            //SetCardSelectPhase();

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

            TouchTxt.gameObject.SetActive(true);
        }

        private void RequestPopup_OnDisappear(RequestPopup sender)
        {
            spawnFrequencyRequest = UnityEngine.Random.Range(spawnFrequencyRequestMin, spawnFrequencyRequestMax);
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
            Debug.Log(popupWin);

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
            GamePhase?.Invoke();
		}
        /*
        public void StartPhase()
        {
            if(Input.GetMouseButtonDown(0))
        }*/

        public void SetSelectProjectPhase()
        {
            TouchTxt.gameObject.SetActive(false);

            inventionContainer.gameObject.SetActive(true);
            inventionValidateBtn.gameObject.SetActive(true);

            inventionValidateBtn.onClick.AddListener(SetCardSelectPhase);
        }

        public void SetCardSelectPhase()
        {
            GamePhase = VoidPhase;
            ///priorisation d'évenement
            requestPopup.InitEvent();
            ///

            inventionContainer.gameObject.SetActive(false);
            inventionValidateBtn.gameObject.SetActive(false);

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

        private void WaitToStart()
        {
            if (Input.GetMouseButtonDown(0)) SetSelectProjectPhase();
        }

        protected void VoidPhase()
        {
        }
    }
}