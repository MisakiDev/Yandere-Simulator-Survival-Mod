using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace YandereNext.SURVIVALPROJECT
{
	public class NemesisManager : MonoBehaviour
	{
		private void Awake()
		{
			base.StartCoroutine(this.InitializeNemesis());
		}

		private void Update()
		{
			bool flag = this.OriginalSurvivalNemesis != null && !this.SurvivalManager.Paused && Time.timeScale > 0.3f;
			bool flag4 = flag;
			if (flag4)
			{
				bool flag2 = this.Killed != this.PreviousKilled;
				bool flag5 = flag2;
				if (flag5)
				{
					this.PreviousKilled = this.Killed;
					this.SurvivalManager.Clock.PeriodLabel.text = this.Killed.ToString() + " Kills";
				}
				this.TimeBeforeNextSpawn -= Time.deltaTime;
				bool flag3 = this.TimeBeforeNextSpawn < 0f;
				bool flag6 = flag3;
				if (flag6)
				{
					this.TimeBeforeNextSpawn = 60f / (float)this.SurvivalManager.DifficultyLevel;
					this.Spawn();
				}
			}
		}

		public IEnumerator InitializeNemesis()
		{
			NemesisScript originalNemesis = Resources.FindObjectsOfTypeAll<NemesisScript>().FirstOrDefault<NemesisScript>();
			bool flag = originalNemesis != null;
			bool flag2 = flag;
			if (flag2)
			{
				originalNemesis.gameObject.SetActive(false);
                GameObject instance = GameObject.Instantiate<GameObject>(originalNemesis.gameObject);
				SurvivalNemesis survivalNemesis = instance.AddComponent<SurvivalNemesis>();
				survivalNemesis.NemesisManager = this;
				this.OriginalSurvivalNemesis = survivalNemesis;
				int num;
				for (int i = 0; i < this.BeginningNemesisCount; i = num + 1)
				{
					this.Spawn();
					yield return null;
					num = i;
				}
				instance = null;
				survivalNemesis = null;
				instance = null;
				survivalNemesis = null;
			}
			else
			{
				Debug.LogError("No Nemesis found, aborting.");
                GameObject.Destroy(base.gameObject);
			}
			yield break;
		}

		public void Spawn()
		{
			bool flag = this.OriginalSurvivalNemesis == null;
			bool flag3 = flag;
			if (flag3)
			{
				Debug.Log("yep there's a problem");
			}
			bool flag2 = this.ScarySpawnCounter >= this.NumberOfSpawnsBeforeScarySpawn;
			bool flag4 = flag2;
			if (flag4)
			{
                SurvivalNemesis survivalNemesis = GameObject.Instantiate<SurvivalNemesis>(this.OriginalSurvivalNemesis);
				survivalNemesis.gameObject.SetActive(true);
				survivalNemesis.ScarySpawn = true;
				this.ScarySpawnCounter = 0;
			}
			else
			{
				GameObject.Instantiate<SurvivalNemesis>(this.OriginalSurvivalNemesis).gameObject.SetActive(true);
				this.ScarySpawnCounter++;
			}
		}

		public SurvivalManager SurvivalManager = null;

		public SurvivalNemesis OriginalSurvivalNemesis = null;

		public float ReactionTime = 2f;

		public float TimeBeforeNextSpawn = 0f;

		public int BeginningNemesisCount = 15;

		public int Spawned = 0;

		public int Killed = 0;

		private int PreviousKilled = 0;

		private int NumberOfSpawnsBeforeScarySpawn = 20;

		private int ScarySpawnCounter = 0;
	}
}
