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
        [SerializeField] private float maxHight = 0;

        public void AddCard(GameObject newCard)
        {
            cardList.Add(newCard);
            newCard.transform.parent = transform;

            GameObject card;
            Vector3 localPos = new Vector3();
            int cardListCount = cardList.Count - 1;
            float angle = 0;

            Vector3 right = Vector3.right * cardListCount / 2;
            Vector3 left = Vector3.left * cardListCount / 2;
            Vector3 up = Vector3.up * cardListCount / 2;

            for (int i = cardListCount; i >= 0; i--)
            {
                card = cardList[i];

                if (cardListCount > 0)
                {
                    localPos = Vector3.Slerp(left, right, (float)i / cardListCount);
                    localPos.z = -i;

                    

                    angle = Mathf.Lerp(45, -45, (float)i / cardListCount);
                    localPos.y = Mathf.Lerp(0, 1, 1 - ((float)Mathf.Abs(angle) / 90));
                }

                card.transform.position = transform.position + localPos;
                card.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }
}