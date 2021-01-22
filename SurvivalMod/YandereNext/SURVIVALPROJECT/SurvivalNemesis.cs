using System;
using System.Collections;
using UnityEngine;

namespace YandereNext.SURVIVALPROJECT
{
	public class SurvivalNemesis : MonoBehaviour
	{
		private void Awake()
		{
			this.Nemesis = base.GetComponent<NemesisScript>();
			this.Nemesis.Student = base.GetComponent<StudentScript>();
			this.Nemesis.Cosmetic = base.GetComponent<CosmeticScript>();
		}

		private void Start()
		{
			this.Nemesis.MissionMode.LastKnownPosition.position = this.Nemesis.Yandere.transform.position;
			this.Nemesis.Aggressive = true;
			base.StartCoroutine(this.RandomValues());
			bool paused = this.NemesisManager.SurvivalManager.Paused;
			bool flag2 = paused;
			if (flag2)
			{
				this.Nemesis.Student.Pathfinding.speed = 0f;
				this.Nemesis.Student.Pathfinding.canSearch = false;
				this.Nemesis.Student.Pathfinding.canMove = false;
				this.Nemesis.Student.CharacterAnimation.Stop();
				bool flag = this.Nemesis.Student.EventManager != null;
				bool flag3 = flag;
				if (flag3)
				{
					this.Nemesis.Student.EventManager.EndEvent();
				}
				this.Nemesis.enabled = false;
			}
			else
			{
				this.Nemesis.Student.StudentID = 2;
			}
		}

		private void LateUpdate()
		{
			bool chibi = this.Chibi;
			bool flag = chibi;
			if (flag)
			{
				this.Nemesis.Student.Head.localScale = new Vector3(3f, 3f, 3f);
				this.Nemesis.Student.Character.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
			}
			else
			{
				bool monster = this.Monster;
				bool flag2 = monster;
				if (flag2)
				{
					this.Nemesis.Student.LeftHand.localScale = new Vector3(5f, 1f, 3f);
					this.Nemesis.Student.Head.localScale = new Vector3(6f, 2f, 0.5f);
				}
				else
				{
					bool longNecc = this.LongNecc;
					bool flag3 = longNecc;
					if (flag3)
					{
						this.Nemesis.Student.Head.localPosition = new Vector3(0f, 3f, 0f);
					}
				}
			}
		}

		private IEnumerator AutoDestruction()
		{
			while (this.Nemesis.Student.MyRenderer.isVisible)
			{
				yield return new WaitForSeconds(5f);
			}
			Debug.Log(base.gameObject.name + " Got disabled for performance reasons");
			base.gameObject.SetActive(false);
			yield break;
		}

		private void Update()
		{
			bool enabled = this.Nemesis.Student.Ragdoll.enabled;
			bool flag2 = enabled;
			if (flag2)
			{
				bool attacking = this.Nemesis.Attacking;
				bool flag3 = attacking;
				if (flag3)
				{
					this.Nemesis.Yandere.StudentManager.YandereDying = false;
					this.Nemesis.Yandere.YandereVision = false;
					this.Nemesis.Yandere.FollowHips = true;
					this.Nemesis.Yandere.Laughing = false;
					this.Nemesis.Yandere.CanMove = true;
					this.Nemesis.Yandere.EyeShrink = 0f;
				}
				Debug.Log(base.gameObject.name + " has been killed");
				this.NemesisManager.Killed++;
				this.NemesisManager.SurvivalManager.NotificationManager.CustomText = this.NemesisManager.Killed.ToString() + " kills";
                this.NemesisManager.SurvivalManager.NotificationManager.DisplayNotification(NotificationType.Custom);
				this.Nemesis.enabled = false;
				base.enabled = false;
				base.StartCoroutine(this.AutoDestruction());
			}
			else
			{
				bool scarySpawn = this.ScarySpawn;
				bool flag4 = scarySpawn;
				if (flag4)
				{
					base.transform.position = this.Nemesis.Yandere.transform.position - this.Nemesis.Yandere.transform.forward * 3f;
					this.ScarySpawn = false;
				}
				this.Timer += Time.deltaTime;
				bool flag = this.Timer >= this.NemesisManager.ReactionTime;
				bool flag5 = flag;
				if (flag5)
				{
					bool inView = this.Nemesis.InView;
					bool flag6 = inView;
					if (flag6)
					{
						this.Nemesis.Chasing = true;
					}
					else
					{
						this.Nemesis.Chasing = false;
					}
					this.Timer = 0f;
				}
			}
		}

		private IEnumerator RandomValues()
		{
			int i = 0;
			while (i < 3)
			{
				int RandomNumber = UnityEngine.Random.Range(1000, 5000);
				int num = RandomNumber;
				int num2 = num;
				int num3 = num2;
				bool flag = num3 <= 3001;
				if (flag)
				{
					bool flag2 = num3 <= 1420;
					if (flag2)
					{
						bool flag3 = num3 == 1285;
						if (flag3)
						{
							goto IL_1FF;
						}
						bool flag4 = num3 == 1420;
						if (flag4)
						{
							goto IL_20E;
						}
					}
					else
					{
						bool flag5 = num3 == 1580;
						if (flag5)
						{
							goto IL_1F0;
						}
						bool flag6 = num3 == 2580;
						if (flag6)
						{
							goto IL_1FF;
						}
						bool flag7 = num3 == 3001;
						if (flag7)
						{
							goto IL_1F0;
						}
					}
				}
				else
				{
					bool flag8 = num3 <= 4650;
					if (flag8)
					{
						bool flag9 = num3 == 3658;
						if (flag9)
						{
							goto IL_20E;
						}
						bool flag10 = num3 == 4231 || num3 == 4650;
						if (flag10)
						{
							goto IL_1FF;
						}
					}
					else
					{
						bool flag11 = num3 == 4658;
						if (flag11)
						{
							goto IL_20E;
						}
						bool flag12 = num3 == 4820;
						if (flag12)
						{
							goto IL_1F0;
						}
						bool flag13 = num3 == 4852;
						if (flag13)
						{
							goto IL_20E;
						}
					}
				}
				IL_1BC:
				yield return null;
				num3 = i;
				i = num3 + 1;
				continue;
				IL_1F0:
				this.Chibi = true;
				goto IL_1BC;
				IL_1FF:
				this.Monster = true;
				goto IL_1BC;
				IL_20E:
				this.LongNecc = true;
				goto IL_1BC;
			}
			yield break;
		}

		public NemesisScript Nemesis;

		public NemesisManager NemesisManager;

		public bool ScarySpawn;

		private float Timer;

		private bool Chibi;

		private bool Monster;

		private bool LongNecc;
	}
}
