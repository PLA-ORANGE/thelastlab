///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using Com.Github.PLAORANGE.Thelastlab.Settings;
using System.Collections.Generic;
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
            FeedDictionary();

        }

        [SerializeField] private List<JobConfig> list = new List<JobConfig>();

        private Dictionary<JobCode, JobConfig> jobDictionary;
        /*
        private void OnValidate()
        {
            FeedDictionary();
        }*/

        void FeedDictionary()
        {
            jobDictionary = new Dictionary<JobCode, JobConfig>();

            foreach (JobConfig jobConfig in list)
            {
                jobDictionary.Add(jobConfig.Code, jobConfig);
            }
        }

        public JobConfig GetJobConfig(JobCode jobCode)
        {
            return jobDictionary[jobCode];
        }

        private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}