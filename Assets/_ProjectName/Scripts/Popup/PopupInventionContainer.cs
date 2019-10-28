///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace Com.Github.PLAORANGE.Thelastlab.Popup
{
	public class PopupInventionContainer : MonoBehaviour {

        [SerializeField] protected InventionPopup invention1; 
        [SerializeField] protected InventionPopup invention2; 
        [SerializeField] protected InventionPopup invention3;

        protected List<InventionPopup> inventionPopupList = new List<InventionPopup>(); 
		private void Start () {
            inventionPopupList.Add(invention1); 
            inventionPopupList.Add(invention2); 
            inventionPopupList.Add(invention3); 
		}

        public void CheckPopup() {
            for(int i = 0; i < inventionPopupList.Count; i++) {
                if(inventionPopupList[i].onClick) inventionPopupList[i].NotOnClick(); 
            }
        }
	}
}