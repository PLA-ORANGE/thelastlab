///-----------------------------------------------------------------
/// Author : #DEVELOPER_NAME#
/// Date : #DATE#
///-----------------------------------------------------------------

using Com.Github.PLAORANGE.Thelastlab.Settings;
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

        public JobCode code;
        public Color color;
        public string name;
        public Sprite sprite;
        public Sprite backgroundSprite;

        public Job(JobCode jobCode )
        {
            
            JobConfig config = JobManager.Instance.GetJobConfig(jobCode);

            code = jobCode;
            color = config.Color;
            name = config.Name;
            sprite = config.Sprite;
            backgroundSprite = config.BackgroundSprite;
        }
		
	}
}