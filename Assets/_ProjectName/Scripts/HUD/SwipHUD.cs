///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace Com.Github.PLAORANGE.Thelastlab.Hud
{
	public class SwipHUD : MonoBehaviour {

        [SerializeField] GameObject iventionModel;
        [SerializeField] float modelSize;
        
        [SerializeField] Text cartCount;
        [SerializeField] Text inventionName;

        [SerializeField] SwipManager swipManager;
	    
        public string CartCountTxt
        {
            set
            {
                cartCount.text = value;
            }
        }

        public string InventionNameTxt 
        {
            set
            {
                inventionName.text = value;
            }
        }

        private void Start () {
            //swipManager .OnValidateCard 
        }
		
		private void Update () {
            iventionModel.transform.localScale = Vector3.one * modelSize;
        }
	}
}