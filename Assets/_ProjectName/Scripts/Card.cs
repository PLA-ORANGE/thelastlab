﻿///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Com.Github.PLAORANGE.Thelastlab
{
	public class Card : MonoBehaviour {

        [SerializeField] private TextMeshPro persoText = null;
        [SerializeField] private SpriteRenderer logoSprite = null;
        [SerializeField] private Perso perso = null;
        [SerializeField] private SpriteRenderer backgroundSprite = null;

        [SerializeField] private string persoName = "Default";

        private const float BACKGROUND_COLOR_COEFF = 0.5f;
        private static List<Color> colorList = new List<Color>() { Color.red, Color.blue, Color.green};

        private static int lol = 0;

        private void Start () {

            setAleaColor();
        }

        private void setAleaColor()
        {
            persoText.text = persoName;
            persoText.color = Color.white;
            
            Color lColor = colorList[Random.Range(0, colorList.Count)];
            logoSprite.color = lColor;

            lColor.g *= BACKGROUND_COLOR_COEFF;
            lColor.r *= BACKGROUND_COLOR_COEFF;
            lColor.b *= BACKGROUND_COLOR_COEFF;

            backgroundSprite.color = lColor;
        }
		
		private void Update () {
			
		}
	}
}