///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 16/10/2019
///-----------------------------------------------------------------

using Com.Github.PLAORANGE.Thelastlab.Popup;
using Pixelplacement;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Github.PLAORANGE.Thelastlab.Hud
{
	public class ProgressBarProject : MonoBehaviour {
        protected RectTransform rectTransform;
        protected Slider slider;
        protected float slidervalue = 0;
        protected Vector2 initPosition;
        protected Color initColor;
        [SerializeField]
        protected Image fillImage;
        protected float height;
        protected PopupWin popupWin;
        protected GameManager gameManager;

        public float Slidervalue
        {
            get => slidervalue; set
            {
                slidervalue = Mathf.Clamp(value, 0, 100);
                UpdateSlider(slidervalue);
            }
        }

        private void Start () {
            rectTransform = GetComponent<RectTransform>();
            initPosition = rectTransform.anchoredPosition;
            slider = GetComponent<Slider>();
            UpdateSlider(slidervalue);
            height = rectTransform.rect.height;
            popupWin = FindObjectOfType<PopupWin>(); 
            gameManager = FindObjectOfType<GameManager>(); 
		}

        public void UpdateSlider(float value)
        {
            initColor = fillImage.GetComponentInChildren<Image>().color;
            fillImage.GetComponentInChildren<Image>().color = Color.green;
            Tween.Value(slider.value, value/100, SliderUpdate, 1, 0, Tween.EaseInOut, Tween.LoopType.None, null, EndTween);
        }

        private void EndTween() {
            gameManager.CheckScoreValue();
        }

        protected void SliderUpdate(float value)
        {
            slider.value = value;
            fillImage.enabled = (value != 0);
        }

        public void FalseAnswer()
        {
            initColor = fillImage.GetComponentInChildren<Image>().color;
            fillImage.GetComponentInChildren<Image>().color = Color.red;
            Tween.Shake(rectTransform, initPosition - (Vector2.up *height/2), (Vector3.up + Vector3.left) * 3, .75f, 0, Tween.LoopType.None, null, UpdatePosition);
        }

        protected void UpdatePosition()
        {
            fillImage.GetComponentInChildren<Image>().color = initColor;
            rectTransform.anchoredPosition = initPosition;
        }

        public void ClearBar() {
            Tween.LocalPosition(slider.transform, new Vector3(10, 10, 10), 0.3f, 0, Tween.EaseOut,Tween.LoopType.None,null,() => GameObject.Destroy(slider.gameObject)); 
           
        }
    }
}