///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.Github.PLAORANGE.Thelastlab {
    public class DragAndDrop : MonoBehaviour {

        private Camera camera;

        private void Start() {
            camera = Camera.main;

        }

        private void Update() {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            bool hitSomething = Physics.Raycast(ray, out RaycastHit hitInfo, 100f);

            if(hitSomething && Input.GetMouseButton(0)) {
                if(hitInfo.collider.CompareTag("carte")) hitInfo.transform.position = ray.GetPoint(1f);
                else Debug.Log("pas une carte");

            }



        }


    }


}