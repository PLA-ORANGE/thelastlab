///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace Com.Github.PLAORANGE.Thelastlab
{
    public class Deck : MonoBehaviour {
        
        private List<GameObject> cardList = new List<GameObject>();
        [SerializeField, Range(0,5)] private float maxHight = 4f;
        [SerializeField, Range(0,2)] private float cardSpace = 1f;
        [SerializeField, Range(0,90)] private float angleIntervalle = 35;


        private void Start()
        {
            Card.OnCardTaken += Card_OnCardTaken;
        }

        public void Card_OnCardTaken(Card card)
        {
            RemoveCard(card.gameObject);
        }

        public void AddCard(GameObject newCard)
        {
            cardList.Add(newCard);
            newCard.transform.parent = transform;
            OrderCards();
        }

        public GameObject GetCard(int index)
        {
            return cardList[index];
        }

        public GameObject GetCard(GameObject card)
        {
            int index = cardList.IndexOf(card);

            return (index < 0) ? null : cardList[index];
        }

        public void RemoveCard(GameObject card)
        {
            cardList.Remove(card);
            card.transform.parent = transform.parent;
            OrderCards();
        }

        public void RemoveCard(int index)
        {
            GameObject card = cardList[index];

            cardList.RemoveAt(index);
            card.transform.parent = transform.parent;
            OrderCards();
        }

        public void Clear()
        {
            GameObject card;

            while (cardList.Count != 0)
            {
                card = cardList[0];
                cardList.Remove(card);
                Destroy(this);
            }
        }

        private void OrderCards()
        {
            GameObject card;
            Vector3 pos = new Vector3();
            int cardListCount = cardList.Count - 1;

            float xRadius = cardListCount * cardSpace / 2;
            float yOffSet = 0;
            float angle;

            Quaternion lCameraRotation = Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up);

            for (int i = cardListCount; i >= 0; i--)
            {
                card = cardList[i];

                angle = (cardListCount == 0)? 0: Mathf.Lerp(angleIntervalle, -angleIntervalle, (float)i / cardListCount) * Mathf.Deg2Rad;
                //card.GetComponent<Card>().RotateInZ(angle);

                angle += Mathf.PI / 2;
                
                pos.x = xRadius * Mathf.Cos(angle);
                pos.y = maxHight * Mathf.Sin(angle);
                pos.z = -i;
                
                if (i == cardListCount) yOffSet = pos.y - 1;
                pos.y -= yOffSet;

                card.transform.position = transform.TransformPoint(pos);
            }
        }

        private void OnValidate()
        {
            OrderCards();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("carte") || GetCard(other.gameObject) != null) return;

            AddCard(other.gameObject);
        }
    }
}