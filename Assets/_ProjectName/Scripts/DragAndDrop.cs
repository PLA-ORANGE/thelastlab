///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.Github.PLAORANGE.Thelastlab {
    public class DragAndDrop : MonoBehaviour,IDragHandler{
        private Vector3 vector = new Vector3();
        private Camera camera;
       

        public void OnDrag(PointerEventData eventData) {
            Debug.Log(eventData); 
        }

        private void Start() {
            camera = Camera.main;
        }

        private void Update() {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            bool hitSomething = Physics.Raycast(ray, out RaycastHit hitInfo, 100f);
           
            if(hitSomething && Input.GetMouseButton(0)) {

                if(hitInfo.collider.CompareTag("carte")) {

                    if(Input.GetMouseButtonDown(0)) {
                        
                        vector.Set(ray.GetPoint(1f).x - hitInfo.transform.position.x, ray.GetPoint(1f).y - hitInfo.transform.position.y,0);
                    }
                    hitInfo.transform.position = (ray.GetPoint(1f) - vector);

                   
                }
                   
                else Debug.Log("pas une carte");

            }

        }


    }


}