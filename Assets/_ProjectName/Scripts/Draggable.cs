///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.Github.PLAORANGE.Thelastlab
{
    public class Draggable : MonoBehaviour {

        private Vector3 yOffset;
        private bool isDragging = false;
        private Transform target = null;
        private Camera camera = null;

        public Draggable(Transform target)
        {
            this.target = target;
        }

        public void StartDrag(Ray ray)
        {
            isDragging = true;

            yOffset.Set(ray.GetPoint(1f).x - target.position.x, ray.GetPoint(1f).y - target.position.y, 0);

            Debug.Log("start");
        }
	
		private void Start () {
            camera = Camera.main;
        }
		
		private void Update () {
            Debug.Log("déplacement");
            if (!isDragging) return;

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            target.position = (ray.GetPoint(1f) - yOffset);
        }
	}
}