///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using Pixelplacement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Github.PLAORANGE.Thelastlab.Popup
{
    public delegate void PopupInventionContainerEventHandler();
    public class PopupInventionContainer : MonoBehaviour
    {

        [SerializeField] protected InventionPopup invention1;
        [SerializeField] protected InventionPopup invention2;
        [SerializeField] protected InventionPopup invention3;

        [SerializeField] protected AnimationCurve animInvention;
        [SerializeField] protected AnimationCurve animButton;

        [SerializeField] private Button button;
        [SerializeField] private GameObject buttonContainer;
        [SerializeField] private Text titleCardText; 

        protected List<InventionPopup> inventionPopupList = new List<InventionPopup>();
        private Animator buttonContainerAnimator;

        private bool onClickTestAnim = false; 
        public static event PopupInventionContainerEventHandler OnSelectionPhaseFinish;
        private void Start() {
            inventionPopupList.Add(invention1);
            inventionPopupList.Add(invention2);
            inventionPopupList.Add(invention3);
            buttonContainerAnimator = buttonContainer.GetComponent<Animator>();
            GetComponent<Animator>().SetTrigger("Appear"); 
        }

        public void CheckPopup() {

            for(int i = 0; i < inventionPopupList.Count; i++) {
                if(inventionPopupList[i].onClick) {
                    inventionPopupList[i].NotOnClick();
                }
            }
        }

        public void CheckWichInvention(string inventionName) {
            if(invention1.gameObject.name == inventionName) {
                buttonContainerAnimator.SetTrigger("Appear");
                onClickTestAnim = true;
                titleCardText.gameObject.SetActive(false);

            }

            else {
                if(onClickTestAnim) {
                    buttonContainerAnimator.SetTrigger("Disappear");
                    onClickTestAnim = false;
                    titleCardText.gameObject.SetActive(false); 
                }
            }
        }

        public void ClearAll() {
            buttonContainerAnimator.SetTrigger("Disappear");
            GetComponent<Animator>().SetTrigger("Disappear");

            Tween.Position(button.transform, new Vector3(0, -20, 0), 0.8f, 0.2f, animButton);
            Tween.Position(invention2.transform, new Vector3(invention2.transform.position.x, 25, 0), 0.5f, 0.3f, Tween.EaseOutBack);
            Tween.Position(invention3.transform, new Vector3(invention3.transform.position.x, 25, 0), 0.5f, 0.5f, Tween.EaseOutBack);
            Tween.LocalScale(invention1.GetComponent<Image>().transform, Vector3.zero, 0.5f, 0.7f, animInvention, Tween.LoopType.None, null, EndPhase);
        }


        private void EndPhase() {
            gameObject.SetActive(false);
            OnSelectionPhaseFinish?.Invoke();
           
            //gameManager s'abonne a cet événement 
        }
    }
}