///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.Github.PLAORANGE.Thelastlab {
    public class DragAndDrop : MonoBehaviour{
        private Vector3 dragOffset = new Vector3();
        private Camera cam;

        private void Start() {
            cam = Camera.main;
        }

        private Transform target;
        private Card currentCardTaken;

        private void Update() {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            bool isHitSomthing = Physics.Raycast(ray.origin, ray.direction, out hitInfo);

            // etape 1 : MouseButtonDown 
            // Récupérer l'élément qui a été touché, le stocker dans une variable
            // Stocker l'offset entre la position de la souris et la position de l'élement

            // etape 2 : 
            // si la variable n'est pas null
            // Placer l'élément stocker dans la variable en fonction de la position de la souris

            // etape 3 : MouseButtonUp
            // affecter null a la variable

            
            if (Input.GetMouseButtonDown(0) && isHitSomthing)
            {
                target = hitInfo.transform;
                dragOffset = ray.GetPoint(1f) - target.position;

                currentCardTaken = hitInfo.collider.GetComponent<Card>();
                currentCardTaken.StartDrag();
            }


            if (!target) return;

            target.position = ray.GetPoint(1f) - dragOffset;

            if (Input.GetMouseButtonUp(0))
            {
                currentCardTaken.StopDrag();
                target = null;
            }
            
        }


    }


}