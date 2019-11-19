///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using UnityEngine;
using System;

namespace Com.Github.PLAORANGE.Thelastlab.Settings
{
    [Serializable]
	public class JobConfig {

        [SerializeField] private JobCode code;
        [SerializeField] private Color color;
        [SerializeField] private string name;
        [SerializeField] private Sprite sprite;
        [SerializeField] private Sprite backgroundSprite;

        public JobCode Code { get => code; }
        public Color Color { get => color; }
        public string Name { get => name; }
        public Sprite Sprite { get => sprite; }
        public Sprite BackgroundSprite { get => backgroundSprite; }
    }
}