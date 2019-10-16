///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Com.Github.PLAORANGE.Thelastlab
{
    public delegate void CardEventHandler(Card sender);
    public class Card : MonoBehaviour {

        [SerializeField] private TextMeshPro titleText = null;
        [SerializeField] private SpriteRenderer logoSprite = null;
        [SerializeField] private Perso perso = null;
        [SerializeField] private SpriteRenderer backgroundSprite = null;

        private const float BACKGROUND_COLOR_COEFF = 0.5f;
        private static List<Color> colorList = new List<Color>() { Color.red, Color.blue, Color.green};

        public static event CardEventHandler OnCardTaken;

        public string job = "Mathématicien";

        private Collider collider;

        public string Title {
            get{
                return titleText.text;
            }
            set{
                titleText.text = value;
            }
        }

        private Quaternion CameraRotation {
            get
            {
                return Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up);
            }
        }

        public void RotateInZ(float angle)
        {
            transform.rotation = CameraRotation * Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
        }

        public void StartDrag()
        {
            OnCardTaken?.Invoke(this);
            transform.rotation = CameraRotation;

            collider.enabled = false;
        }

        public void StopDrag()
        {
            collider.enabled = true;
        }

        private void Start () {
            setAleaColor();
            Title = perso.job;
            collider = GetComponent<Collider>();
        }

        private void setAleaColor()
        {
            Color lColor = colorList[Random.Range(0, colorList.Count)];
            logoSprite.color = lColor;

            lColor.g *= BACKGROUND_COLOR_COEFF;
            lColor.r *= BACKGROUND_COLOR_COEFF;
            lColor.b *= BACKGROUND_COLOR_COEFF;

            backgroundSprite.color = lColor;
        }
	}
}