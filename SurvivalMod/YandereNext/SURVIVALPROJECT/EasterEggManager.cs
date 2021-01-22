using System;
using System.Collections;
using UnityEngine;

namespace YandereNext.SURVIVALPROJECT
{
	public class EasterEggManager : MonoBehaviour
	{
        private void Start()
        {
            this.CurrentReloadTimer = this.ReloadTimer;
            this.SurvivalManager.NotificationManager.Yandere = new YandereScript();
            this.SurvivalManager.NotificationManager.Yandere.Egg = false;
        }

        private void Update()
        {
            this.UpdateEasterEggs();
        }

        private void DisableEasterEgg()
        {
            switch (this.CurrentEasterEgg)
            {
                case EasterEggs.BadTime:
                    this.SurvivalManager.YandereChan.Sans = false;
                    break;
                case EasterEggs.Falcon:
                    this.SurvivalManager.YandereChan.FalconHelmet.SetActive(false);
                    break;
                case EasterEggs.OnePunch:
                    this.SurvivalManager.YandereChan.Cape.SetActive(false);
                    break;
                case EasterEggs.Cirno:
                    this.SurvivalManager.YandereChan.CirnoHair.SetActive(false);
                    this.SurvivalManager.YandereChan.Hairstyle = 1;
                    this.SurvivalManager.YandereChan.UpdateHair();
                    break;
                case EasterEggs.Nier:
                    this.SurvivalManager.YandereChan.Pod.SetActive(false);
                    break;
                case EasterEggs.Witch:
                    this.SurvivalManager.YandereChan.WitchMode = false;
                    break;
            }
            this.SurvivalManager.YandereChan.Egg = false;
            this.SurvivalManager.YandereChan.CanMove = true;
            this.CurrentEasterEgg = EasterEggs.None;
            this.EasterEggName = string.Empty;
            this.CurrentReloadTimer = this.ReloadTimer;
        }

        private void SetRandomEasterEgg()
        {
            bool flag = this.CurrentEasterEgg > EasterEggs.None;
            bool flag2 = flag;
            if (flag2)
            {
                this.DisableEasterEgg();
            }
            int currentEasterEgg = UnityEngine.Random.Range(1, Enum.GetNames(typeof(EasterEggs)).Length);
            switch (currentEasterEgg)
            {
                case 1:
                    this.SurvivalManager.YandereChan.Sans = true;
                    this.Ammo = 10f;
                    this.EasterEggName = "BAD TIME MODE";
                    break;
                case 2:
                    this.SurvivalManager.YandereChan.FalconHelmet.SetActive(true);
                    this.Ammo = 5f;
                    this.EasterEggName = "FALCON MODE";
                    break;
                case 3:
                    this.SurvivalManager.YandereChan.Cape.SetActive(true);
                    this.Ammo = 5f;
                    this.EasterEggName = "ONE PUNCH MODE";
                    break;
                case 4:
                    this.SurvivalManager.YandereChan.CirnoHair.SetActive(true);
                    this.SurvivalManager.YandereChan.Hairstyle = 0;
                    this.SurvivalManager.YandereChan.UpdateHair();
                    this.Ammo = 10f;
                    this.EasterEggName = "CIRNO MODE";
                    break;
                case 5:
                    this.SurvivalManager.YandereChan.Pod.SetActive(true);
                    this.Ammo = 250f;
                    this.EasterEggName = "NIER MODE";
                    break;
                case 6:
                    this.SurvivalManager.YandereChan.WitchMode = true;
                    this.Ammo = 4f;
                    this.EasterEggName = "WITCH MODE";
                    break;
            }
            this.CurrentEasterEgg = (EasterEggs)currentEasterEgg;
            this.SurvivalManager.YandereChan.Egg = true;
            this.SurvivalManager.Subtitles.Label.text = this.EasterEggName;
            this.UpdateAmmo();
        }

        // Token: 0x06000006 RID: 6 RVA: 0x000023E4 File Offset: 0x000005E4
        private void UpdateEasterEggs()
        {
            bool flag = this.CurrentEasterEgg != EasterEggs.None && this.CurrentEasterEgg != EasterEggs.Nier;
            bool flag2 = flag;
            if (flag2)
            {
                bool flag3 = !this.waiting && this.SurvivalManager.YandereChan.CanMove && Input.GetButtonDown("RB");
                bool flag4 = flag3;
                if (flag4)
                {
                    base.StartCoroutine(this.WaitForEnd());
                }
            }
            else
            {
                bool flag5 = this.CurrentEasterEgg == EasterEggs.Nier;
                bool flag6 = flag5;
                if (flag6)
                {
                    this.Nier();
                }
                else
                {
                    this.CurrentReloadTimer -= Time.deltaTime;
                    bool flag7 = this.previousValueTimer != (int)this.CurrentReloadTimer;
                    bool flag8 = flag7;
                    if (flag8)
                    {
                        this.SurvivalManager.Subtitles.Label.text = "RELOADING " + ((int)this.CurrentReloadTimer).ToString();
                        this.previousValueTimer = (int)this.CurrentReloadTimer;
                    }
                    bool flag9 = this.CurrentReloadTimer <= 0f;
                    bool flag10 = flag9;
                    if (flag10)
                    {
                        this.SurvivalManager.Subtitles.Label.text = string.Empty;
                        this.SetRandomEasterEgg();
                    }
                }
            }
        }

		private void Nier()
		{
			bool flag = this.SurvivalManager.YandereChan.CanMove && Input.GetButton("RB");
			bool flag3 = flag;
			if (flag3)
			{
				this.Ammo -= 1f;
				this.UpdateAmmo();
			}
			else
			{
				bool flag2 = Input.GetButtonDown("Y") || Input.GetButtonDown("X");
				bool flag4 = flag2;
				if (flag4)
				{
					this.Ammo -= 1f;
					this.UpdateAmmo();
				}
			}
		}

		private void WitchTime()
		{
			bool flag = !this.SurvivalManager.Paused;
			bool flag4 = flag;
			if (flag4)
			{
				this.SurvivalManager.Paused = true;
                NemesisScript[] array = GameObject.FindObjectsOfType<NemesisScript>();
				foreach (NemesisScript nemesisScript in array)
				{
					nemesisScript.Student.Pathfinding.speed = 0f;
					nemesisScript.Student.Pathfinding.canSearch = false;
					nemesisScript.Student.Pathfinding.canMove = false;
					nemesisScript.Student.CharacterAnimation.Stop();
					bool flag2 = nemesisScript.Student.EventManager != null;
					bool flag5 = flag2;
					if (flag5)
					{
						nemesisScript.Student.EventManager.EndEvent();
					}
					nemesisScript.enabled = false;
				}
			}
			else
			{
				this.SurvivalManager.Paused = false;
                NemesisScript[] array2 = GameObject.FindObjectsOfType<NemesisScript>();
				foreach (NemesisScript nemesisScript2 in array2)
				{
					nemesisScript2.Student.Pathfinding.speed = (float)(nemesisScript2.Chasing ? 5 : 1);
					nemesisScript2.Student.Pathfinding.canSearch = true;
					nemesisScript2.Student.Pathfinding.canMove = true;
					nemesisScript2.Student.CharacterAnimation.Play();
					nemesisScript2.enabled = true;
					bool flag3 = nemesisScript2.Student.StudentID < 2;
					bool flag6 = flag3;
					if (flag6)
					{
						nemesisScript2.Student.StudentID = 2;
					}
				}
			}
		}

		private void UpdateAmmo()
		{
			this.SurvivalManager.NotificationManager.CustomText = Math.Max(0f, this.Ammo).ToString() + " LEFT";
			this.SurvivalManager.NotificationManager.DisplayNotification(NotificationType.Custom);
			bool flag = this.Ammo <= 0f;
			bool flag2 = flag;
			if (flag2)
			{
				this.DisableEasterEgg();
			}
		}

		public IEnumerator WaitForEnd()
		{
			this.waiting = true;
			bool flag = this.CurrentEasterEgg == EasterEggs.Witch;
			bool flag2 = flag;
			if (flag2)
			{
				this.WitchTime();
			}
			while (this.SurvivalManager.YandereChan.CanMove)
			{
				yield return new WaitForEndOfFrame();
			}
			while (!this.SurvivalManager.YandereChan.CanMove)
			{
				yield return new WaitForEndOfFrame();
			}
			this.waiting = false;
			this.Ammo -= 1f;
			this.UpdateAmmo();
			yield break;
		}

		public EasterEggs CurrentEasterEgg = EasterEggs.None;

		public float ReloadTimer = 5f;

		public float CurrentReloadTimer = 0f;

		private int previousValueTimer = 0;

		public float Ammo = 0f;

		private string EasterEggName = string.Empty;

		public SurvivalManager SurvivalManager = null;

		private bool waiting;
	}
}
