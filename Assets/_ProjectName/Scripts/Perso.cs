///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.Github.PLAORANGE.Thelastlab
{
	public class Perso : MonoBehaviour {

        [SerializeField] SpriteRenderer head = null;
        [SerializeField] SpriteRenderer bust = null;
        [SerializeField] public string job = "Mathématicien";

        private Color color;
        public Color Color {
            get {
                return color;
            }
            set {
                color = value;
                head.color = color;
                bust.color = color;
            }
        }

        private void Start () {
			
		}
		
		private void Update () {
			
		}
	}
}