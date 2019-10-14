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
        [SerializeField, Range(0,5)] private float maxHight = 1.6f;
        [SerializeField, Range(0,2)] private float cardSpace = 0.45f;
        [SerializeField, Range(0,90)] private float angleIntervalle = 60;

            
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

            float angle = 0;
            float xRadius = cardListCount * cardSpace / 2;

            for (int i = cardListCount; i >= 0; i--)
            {
                card = cardList[i];

                if (cardListCount > 0)
                {
                    angle = Mathf.Lerp(Mathf.PI, 0, (float)i / cardListCount);

                    pos.x = Mathf.Cos(angle) * xRadius;
                    pos.y = Mathf.Sin(angle) * maxHight;
                    pos.z = -i;

                    angle = Mathf.Lerp(angleIntervalle, -angleIntervalle, (float)i / cardListCount) * Mathf.Deg2Rad;
                }

                card.transform.position = transform.position + pos;
                card.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
            }
        }

        private void OnValidate()
        {
            OrderCards();
        }
        
    }
}