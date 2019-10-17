///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.Github.PLAORANGE.Thelastlab 
    {
	public class Invention : MonoBehaviour {

        private void Start() {
            transform.localScale = Vector3.zero;
            transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up);
        }


        private void Update () {
            transform.Rotate(Vector3.up, 20f * Time.deltaTime);
        }
	}
}