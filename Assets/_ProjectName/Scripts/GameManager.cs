///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------


using Com.Github.PLAORANGE.Thelastlab.Hud;
using Com.Github.PLAORANGE.Thelastlab.Popup;
using Pixelplacement;
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

        [SerializeField] private ParticleSystem cardExplosion;
        
        protected float score = 0;  
        private GameObject deck;

        private bool isRequestPopUp;

        private Action GamePhase;

        [SerializeField]
        private List<JobCode> popupRequests = new List<JobCode>() {
            JobCode.Toxicologue,
            JobCode.IntégrateurD_Ia,
            JobCode.Expert_EnCybersécurité,
            JobCode.Ingénieur_Automatique
        };

        [SerializeField]
        private List<JobCode> startCards = new List<JobCode>() {
            JobCode.Toxicologue,
            JobCode.Expert_EnCybersécurité,
            JobCode.Technical_Artiste,
            JobCode.Ingénieur_Automatique,
            JobCode.IntégrateurD_Ia,
            JobCode.Bilogiste,
            JobCode.Ingénieur_Biomécanique
        };

        [SerializeField] private float spawnFrequencyRequest = 4f;
        private float elapseTime = 0;

        [SerializeField] private PopupInventionContainer inventionContainer;
        [SerializeField] private Button inventionValidateBtn;

        [SerializeField] private Text TouchTxt;

        private void Start () {

            RequestPopup.OnDisappear += RequestPopup_OnDisappear;
            PopupInventionContainer.OnSelectionPhaseFinish += PopupInventionContainer_OnSelectionPhase; 
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

        private void PopupInventionContainer_OnSelectionPhase() {
            SetCardSelectPhase(); 
        }

        private void RequestPopup_OnDisappear(RequestPopup sender)
        {
            isRequestPopUp = false;
        }

        public void SpawnInLab(Job job)
        {
            GameObject labPeople = Instantiate(labMenPrefab, labo.transform.position, Quaternion.identity, labo.transform);

            cardExplosion.GetComponent<ParticleSystem>().startColor = job.color;
            cardExplosion.Play();

            labPeople.GetComponent<MeshRenderer>().material.color = job.color;
        }

        public void ClearAll() {
            deck.GetComponent<Deck>().Clear();
            progressBar.ClearBar(); 
            
        }

        public void DisplayWinScreen() {
            popupWin.Appear();
            Tween.LocalScale(popupWin.transform, new Vector3(1.2f, 1.2f, 1.2f),0.7f,0,Tween.EaseIn); 
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

            //inventionValidateBtn.onClick.AddListener(SetCardSelectPhase);
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