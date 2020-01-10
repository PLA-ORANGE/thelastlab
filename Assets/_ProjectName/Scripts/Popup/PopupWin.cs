///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using Pixelplacement;
using Pixelplacement.TweenSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Com.Github.PLAORANGE.Thelastlab.Popup 
{
    public class PopupWin : Popup {

        [SerializeField] private GameObject invention;
        [SerializeField] private Transform inventionPos;
        [SerializeField] private GameObject fondInventionPrefab;
        [SerializeField] private Button buttonRetry;
        
        [SerializeField] private AnimationCurve inventionCurve;

        public override void Appear() {
          
            exist = true;
            invention =  GameObject.Instantiate(invention,inventionPos);
           
            
            Tween.LocalScale(invention.transform, new Vector3(5, 5, 5), 1f, 0.1f, inventionCurve);
            Tween.LocalPosition(buttonRetry.transform, new Vector2(0, -550),1.5f,0.7f);
            GetComponent<Animator>().SetTrigger("Entry"); 
        }

        public void Retry() {
            Destroy(FindObjectOfType<TweenEngine>().gameObject);
            SceneManager.LoadScene("CopyMainWinScreen"); 
        }

    }
}