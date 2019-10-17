///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Github.PLAORANGE.Thelastlab
{
    public enum JobCode
    {
        Mathématicien = 0,
        Chimiste = 1,
        Ingénieur = 2,
        Développeur = 3
    }

	public class Job {

        static Dictionary<JobCode, Color> jobsColors;

        static public void InitJobColor()
        {
            Debug.Log(JobCode.Développeur == 0);
            
            jobsColors = new Dictionary<JobCode, Color>();

            jobsColors.Add(JobCode.Mathématicien, Color.red);
            jobsColors.Add(JobCode.Chimiste, Color.blue);
            jobsColors.Add(JobCode.Ingénieur, Color.green);
            jobsColors.Add(JobCode.Développeur, Color.cyan);
        }

        static public Job GetAleaJob()
        {
            return new Job(GetAleaCode());
        }

        static public JobCode GetAleaCode()
        {
            Array jobCodes = Enum.GetValues(typeof(JobCode));
            int index = UnityEngine.Random.Range(0, jobCodes.Length);

            return (JobCode)jobCodes.GetValue(index);
        }

        public Color color;
        public JobCode code;
	    
        public Job(JobCode jobCode )
        {
            if (jobsColors == null) InitJobColor();

            code = jobCode;
            color = jobsColors[jobCode];
        }
		
	}
}