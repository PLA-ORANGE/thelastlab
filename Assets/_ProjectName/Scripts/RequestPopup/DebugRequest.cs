///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 14/10/2019
///-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace Com.Github.PLAORANGE.Thelastlab.Popup
{
	public class DebugRequest : MonoBehaviour {

        
        [SerializeField]
        protected Button correctAnswerButton;
        [SerializeField]
        protected Button falseAnswerButton;
        [SerializeField]
        protected RequestPopup requestPopup;
        [SerializeField]
        protected GameObject DebugPopupManager;
        protected RectTransform DebugPopupManagerRectTransform;
        

		protected void Start () {
            DebugPopupManagerRectTransform = Instantiate(DebugPopupManager, transform.parent.parent).GetComponent<RectTransform>();
            DebugPopupManagerRectTransform.anchoredPosition = new Vector2(20, -100);
            falseAnswerButton.onClick.AddListener(DebugFalseAnswer);
            correctAnswerButton.onClick.AddListener(DebugCorrectAnswer);
		}
		
        protected void DebugFalseAnswer()
        {
            requestPopup.FalseAnswer();
        }

        protected void DebugCorrectAnswer()
        {
            requestPopup.CorrectAnswer();
        }

        protected void Update () {
			
		}
	}
}