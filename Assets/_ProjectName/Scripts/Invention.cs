///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.Github.PLAORANGE.Thelastlab 
    {

    
	public class Invention : MonoBehaviour {
        public bool win = true; 
        private void Start() {
            transform.localScale = win ? Vector3.zero : new Vector3(40, 40, 40); 
            //transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up);
        }


        private void Update () {
            transform.Rotate(Vector3.up, 20f * Time.deltaTime);
        }
	}
}