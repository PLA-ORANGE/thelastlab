///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using Com.Github.PLAORANGE.Thelastlab.Settings;
using UnityEngine;

namespace Com.Github.PLAORANGE.Thelastlab
{
	public class JobManager : MonoBehaviour {

        public JobSettings jobSettings;
		private static JobManager instance;
		public static JobManager Instance { get { return instance; } }
		
		private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}
			
			instance = this;
		}
		
		private void Start () {
			
		}
		
		private void Update () {
			
		}
		
		private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}