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
        Toxicologue = 0,
        Expert_EnCybersecurite = 1,
        Technical_Artiste = 2,
        Ingenieur_Biomecanique = 3,
        IntegrateurD_Ia = 4,
        Biologiste = 5,
        Ingenieur_Automatique = 6
    }

	public class Job {

        static Dictionary<JobCode, Color> jobsColors;

        static public void InitJobColor()
        {
            jobsColors = new Dictionary<JobCode, Color>();

            jobsColors.Add(JobCode.Toxicologue, Color.red);
            jobsColors.Add(JobCode.Expert_EnCybersecurite, Color.blue);
            jobsColors.Add(JobCode.Technical_Artiste, Color.green);
            jobsColors.Add(JobCode.Ingenieur_Automatique, Color.cyan);
            jobsColors.Add(JobCode.IntegrateurD_Ia, Color.magenta);
            jobsColors.Add(JobCode.Biologiste, Color.yellow);
            jobsColors.Add(JobCode.Ingenieur_Biomecanique, Color.grey);
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