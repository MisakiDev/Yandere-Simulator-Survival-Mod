using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YandereNext.SURVIVALPROJECT
{
	public class MainMenuManager : MonoBehaviour
	{
		public void Awake()
		{
            this.TitleMenuScript = GameObject.FindObjectOfType<TitleMenuScript>();
		}

		private void Start()
		{
			base.StartCoroutine(this.Initialize());
		}

		private void Update()
		{
			bool flag = this.TitleMenuScript.Selected == this.SurvivalModeID && Input.GetButtonDown("A");
			bool flag3 = flag;
			if (flag3)
			{
				this.TitleMenuScript.Darkness.color = new Color(0f, 0f, 0f, this.TitleMenuScript.Darkness.color.a);
				this.TitleMenuScript.InputTimer = -10f;
				this.TitleMenuScript.FadeOut = true;
				this.TitleMenuScript.Fading = true;
			}
			bool flag2 = this.TitleMenuScript.Selected == this.SurvivalModeID && this.TitleMenuScript.Fading && this.TitleMenuScript.FadeOut && this.TitleMenuScript.Darkness.color.a >= 1f;
			bool flag4 = flag2;
			if (flag4)
			{
				SurvivalModeGlobals.Survival = true;
				GameGlobals.LoveSick = true;
                SceneManager.LoadScene("SuicideScene");

			}
		}

		private IEnumerator Initialize()
		{
			yield return null;
			List<UILabel> labels = this.TitleMenuScript.ColoredLabels.ToList<UILabel>();
			UILabel missionModeLabel = (from m in labels
			where m.text.Contains("Exit")
			select m).FirstOrDefault<UILabel>();
			bool flag = missionModeLabel != null;
			bool flag2 = flag;
			if (flag2)
			{
				this.SurvivalModeID = labels.IndexOf(missionModeLabel) + 1;
                GameObject exitClone = GameObject.Instantiate<GameObject>(missionModeLabel.gameObject);
				UILabel SurvivalLabel = exitClone.GetComponent<UILabel>();
				SurvivalLabel.text = "Survival";
				SurvivalLabel.name = "9 Survival Mode";
				SurvivalLabel.transform.rotation = missionModeLabel.transform.rotation;
				SurvivalLabel.transform.position = missionModeLabel.transform.position - missionModeLabel.transform.up * 0.075f;
				SurvivalLabel.UpdateNGUIText();
				labels.Add(SurvivalLabel);
				this.TitleMenuScript.ColoredLabels = labels.ToArray();
				this.TitleMenuScript.SelectionCount++;
				exitClone = null;
				SurvivalLabel = null;
				exitClone = null;
				SurvivalLabel = null;
			}
			else
			{
				Debug.Log("It was found");
			}
			yield break;
		}

		private TitleMenuScript TitleMenuScript = null;

		private int SurvivalModeID = 0;
	}
}
