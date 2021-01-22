using System;
using System.Collections;
using UnityEngine;

namespace YandereNext.SURVIVALPROJECT
{
	public class SurvivalManager : MonoBehaviour
	{

		private void Awake()
		{
			MissionModeGlobals.MissionMode = true;
			MissionModeGlobals.NemesisDifficulty = 4;
            this.StudentManager = GameObject.FindObjectOfType<StudentManagerScript>();
			Debug.Log(string.Format("StudentManager {0}", this.StudentManager != null));
            this.MissionModeScript = GameObject.FindObjectOfType<MissionModeScript>();
			Debug.Log(string.Format("MissionModeScript {0}", this.MissionModeScript != null));
            this.YandereChan = GameObject.FindObjectOfType<YandereScript>();
			Debug.Log(string.Format("YandereChan {0}", this.YandereChan != null));
            this.NotificationManager = GameObject.FindObjectOfType<NotificationManagerScript>();
			Debug.Log(string.Format("NotificationManager {0}", this.NotificationManager != null));
            this.Subtitles = GameObject.FindObjectOfType<SubtitleScript>();
			Debug.Log(string.Format("Subtitles {0}", this.Subtitles != null));
            this.Clock = GameObject.FindObjectOfType<ClockScript>();
			Debug.Log(string.Format("Clock {0}", this.Clock != null));
			this.Clock.enabled = false;
			base.StartCoroutine(this.RemoveStudents());
			base.StartCoroutine(this.LoadMusic());
		}

		private IEnumerator LoadMusic()
		{
			yield break;
		}

		private void OnDisable()
		{
            this.RichPresenceHelper.enabled = true;
            bool flag = this.MissionModeScript.Destination != 1;
            bool flag2 = flag;
            if (flag2)
            {
                SurvivalModeGlobals.Survival = false;
            }
		}

		private void Start()
		{
			this.NemesisManager = base.gameObject.AddComponent<NemesisManager>();
			this.NemesisManager.SurvivalManager = this;
			this.EasterEggManager = base.gameObject.AddComponent<EasterEggManager>();
			this.EasterEggManager.SurvivalManager = this;
			this.MissionModeScript.Watermark.text = "SURVIVAL MOD REVIVAL (BETA) BY SELEDREAMS (AKA Pikachuk)";
			this.Clock.DayLabel.gameObject.SetActive(true);
			this.Clock.DayLabel.text = "";
			this.Clock.TimeLabel.text = "TIME : 00:00";
			this.Clock.PeriodLabel.text = "0 Kills";
			this.MissionModeScript.MoneyLabel.enabled = false;
			this.MissionModeScript.ReputationLabel.text = "DIFFICULTY";
			this.MissionModeScript.ExitPortal.SetActive(false);
            GameObject.Destroy(this.MissionModeScript.Headmaster);
			this.MissionModeScript.HeartbeatCamera.SetActive(false);
			this.MissionModeScript.WoodChipper.VictimList = new int[0];
			this.MissionModeScript.WoodChipper.Victims = 0;
			this.MissionModeScript.WoodChipper.VictimID = 0;
			this.DifficultyLevel = 1;
			this.EasterEggManager.ReloadTimer = (float)this.DifficultyLevel;
			this.DifficultyIncreaseTime = this.SecondsToSurvive;
		}

		private void Update()
		{
			bool flag = !this.Paused && Time.timeScale > 0.3f && !this.YandereChan.Hiding;
			bool flag3 = flag;
			if (flag3)
			{
				this.SecondsSurvived += Time.deltaTime;
				this.UpdateDifficulty();
				this.UpdateRichPresence();
				this.UpdateClock();
			}
			else
			{
				bool flag2 = !this.GameOver && Time.timeScale < 0.3f && this.MissionModeScript.GameOverPhase > 0;
				bool flag4 = flag2;
				if (flag4)
				{
					UILabel gameOverHeader = this.MissionModeScript.GameOverHeader;
					switch (UnityEngine.Random.Range(1, 11))
					{
					case 1:
						gameOverHeader.text = "NOT A BIG SURPRISE...";
						break;
					case 2:
						gameOverHeader.text = "SERIOUSLY?";
						break;
					case 3:
						gameOverHeader.text = "AS I THOUGHT!";
						break;
					case 4:
						gameOverHeader.text = "ARE YOU GONNA CRY?";
						break;
					case 5:
						gameOverHeader.text = "OK.";
						break;
					case 6:
						gameOverHeader.text = "NICE!";
						break;
					case 7:
						gameOverHeader.text = "I LOVE DYING!";
						break;
					case 8:
						gameOverHeader.text = "I SAID 0 DEATHS!";
						break;
					case 9:
						gameOverHeader.text = "I BELIEVE IN YOUR FAILURE!";
						break;
					case 10:
						gameOverHeader.text = "NEMESIS : 1, YOU : 0";
						break;
					default:
						gameOverHeader.text = ";-;";
						break;
					}
					this.MissionModeScript.GameOverReason.text = "YOU DIED AFTER KILLING " + this.NemesisManager.Killed.ToString() + " NEMESIS.\n" + this.Clock.TimeLabel.text;
					this.GameOver = true;
				}
			}
		}

        private void UpdateRichPresence()
        {
            int num;
            int num2;
            int num3;
            this.SecondsToTime((int)this.SecondsSurvived, out num, out num2, out num3);
            bool flag = (float)num2 != this.previousMinutes;
            bool flag2 = flag;
            if (flag2)
            {
            }
        }

        private void UpdateClock()
        {
            bool flag = this.SecondsSurvived > 0f && this.previousHour < 99f;
            bool flag2 = flag;
            if (flag2)
            {
                int num;
                int num2;
                int num3;
                this.SecondsToTime((int)this.SecondsSurvived, out num, out num2, out num3);
                bool flag3 = (float)num3 != this.previousSeconds || (float)num2 != this.previousMinutes || (float)num != this.previousHour;
                bool flag4 = flag3;
                if (flag4)
                {
                    this.previousSeconds = (float)num3;
                    this.previousMinutes = (float)num2;
                    this.previousHour = (float)num;
                    string text = string.Empty;
                    bool flag5 = num == 0;
                    bool flag6 = flag5;
                    if (flag6)
                    {
                        text = "TIME : " + num2.ToString("00") + ":" + num3.ToString("00");
                    }
                    else
                    {
                        text = string.Concat(new string[]
						{
							"TIME : ",
							num.ToString("00"),
							":",
							num2.ToString("00"),
							":",
							num3.ToString("00")
						});
                    }
                    this.Clock.TimeLabel.text = text;
                }
            }
        }

        private void SecondsToTime(int baseSeconds, out int hours, out int minutes, out int seconds)
        {
            seconds = baseSeconds % 60;
            minutes = baseSeconds / 60;
            hours = minutes / 60;
        }

        private void UpdateDifficulty()
        {
            bool flag = this.DifficultyLevel < 99 && this.SecondsSurvived >= this.DifficultyIncreaseTime;
            bool flag2 = flag;
            if (flag2)
            {
                this.DifficultyLevel++;
                this.EasterEggManager.ReloadTimer = (float)this.DifficultyLevel;
                bool flag3 = this.DifficultyLevel < 99;
                bool flag4 = flag3;
                if (flag4)
                {
                    this.MissionModeScript.Reputation.PendingRep = -(float)this.DifficultyLevel;
                    this.DifficultyIncreaseTime *= 2f;
                }
            }
        }

		private IEnumerator RemoveStudents()
		{
			while (this.StudentManager.SpawnID < this.StudentManager.NPCsTotal || this.StudentManager.NPCsTotal == 0)
			{
				yield return null;
			}
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
            StudentScript[] students = GameObject.FindObjectsOfType<StudentScript>();
			foreach (StudentScript student in students)
			{
				bool flag = student.name != "Nemesis";
				bool flag2 = flag;
				if (flag2)
				{
					student.gameObject.SetActive(false);
				}
				students = null;
			}
			StudentScript[] array = null;
			yield break;
		}

		public NemesisManager NemesisManager = null;

		public EasterEggManager EasterEggManager = null;

		public MissionModeScript MissionModeScript = null;

		public StudentManagerScript StudentManager = null;

		public YandereScript YandereChan = null;

		public ClockScript Clock = null;

		public NotificationManagerScript NotificationManager = null;

		public SubtitleScript Subtitles = null;

		private RichPresenceHelper RichPresenceHelper = null;

		public float SecondsSurvived = 0f;

		public float DifficultyIncreaseTime = 0f;

		public float SecondsToSurvive = 20f;

		public int DifficultyLevel = 1;

		public bool Paused = false;

		private float previousMinutes = 0f;

		private float previousHour = 0f;

		private float previousSeconds = 0f;

		private bool GameOver = false;
	}
}
