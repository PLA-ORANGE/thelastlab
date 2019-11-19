///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace Com.Github.PLAORANGE.Thelastlab.Settings
{
    [CreateAssetMenu(fileName = "JobSettings", menuName = "Settings/JobSettings", order = 1)]
    public class JobSettings : ScriptableObject
    {

        [SerializeField] private List<JobConfig> list = new List<JobConfig>();

        private Dictionary<JobCode, JobConfig> jobDictionary;

        private void OnValidate()
        {
            jobDictionary = new Dictionary<JobCode, JobConfig>();

            foreach (JobConfig jobConfig in list) {
                jobDictionary.Add(jobConfig.Code, jobConfig);
            }
        }

        public JobConfig GetJobConfig(JobCode jobCode)
        {
            return jobDictionary[jobCode];
        }

    }
}