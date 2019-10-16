///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 16/10/2019
///-----------------------------------------------------------------

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
        public float Slidervalue
        {
            get => slidervalue; set
            {
                if (value >= 100)
                {
                    slidervalue = 100;
                }
                else if(value <= 0)
                {
                    slidervalue = 0;
                } else
                {
                    slidervalue = value;
                }
                UpdateSlider(slidervalue);
            }
        }

        private void Start () {
            rectTransform = GetComponent<RectTransform>();
            initPosition = rectTransform.anchoredPosition;
            slider = GetComponent<Slider>();
            UpdateSlider(slidervalue);
            height = rectTransform.rect.height;
		}

        protected void UpdateSlider(float value)
        {
            Tween.Value(slider.value, value/100, SliderUpdate, 1, 0, Tween.EaseInOut);
        }

        protected void SliderUpdate(float value)
        {
            slider.value = value;
            if(slider.value == 0)
            {
                fillImage.enabled = false;
            }
            else
            {
                fillImage.enabled = true;
            }
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
            Debug.Log("Position");
            rectTransform.anchoredPosition = initPosition;
        }
	}
}