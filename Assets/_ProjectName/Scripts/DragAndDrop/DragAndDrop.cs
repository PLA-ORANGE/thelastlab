///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.Github.PLAORANGE.Thelastlab {
    public class DragAndDrop : MonoBehaviour, IDragHandler{
        private Vector3 vector = new Vector3();
        private Camera camera;
       

        public void OnDrag(PointerEventData eventData) {
            Debug.Log(eventData); 
        }

        private void Start() {
            camera = Camera.main;
        }

        Transform target;
        Card currentCardTaken;

        private void Update() {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D hitInfo = Physics2D.Raycast(ray.origin, ray.direction);

            // etape 1 : MouseButtonDown 
            // Récupérer l'élément qui a été touché, le stocker dans une variable
            // Stocker l'offset entre la position de la souris et la position de l'élement

            // etape 2 : 
            // si la variable n'est pas null
            // Placer l'élément stocker dans la variable en fonction de la position de la souris

            // etape 3 : MouseButtonUp
            // affecter null a la variable


            if (Input.GetMouseButtonDown(0) && hitInfo)
            {
                currentCardTaken = hitInfo.collider.GetComponent<Card>();
                currentCardTaken.StartDrag();

                target = hitInfo.transform;
                vector.Set(ray.GetPoint(1f).x - target.position.x, ray.GetPoint(1f).y - target.position.y, 0);
            }

            if (target)
            {
                target.position = ray.GetPoint(1f) - vector;
            }

            if (Input.GetMouseButtonUp(0))
            {
                currentCardTaken.StopDrag();
                target = null;
            }
                

        }


    }


}