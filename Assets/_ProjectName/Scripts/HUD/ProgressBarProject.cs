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
		}

        protected void UpdateSlider(float value)
        {
            Tween.Value(slider.value, value/100, SliderUpdate, 1, 0, Tween.EaseInOut);
        }

        protected void SliderUpdate(float value)
        {
            slider.value = value;
        }
        public void FalseAnswer()
        {
            Tween.Shake(rectTransform, initPosition, (Vector3.up + Vector3.left) * 5, 1f, 0, Tween.LoopType.None, null, UpdatePosition);
        }

        protected void UpdatePosition()
        {
            Debug.Log("Position");
            rectTransform.anchoredPosition = initPosition;
        }
	}
}