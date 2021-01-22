using System;
using UnityEngine;

namespace YandereNext.SURVIVALPROJECT
{
	internal class LoadingScreenManager : MonoBehaviour
	{
		private void Awake()
		{
			this.keyboard = GameObject.Find("Keyboard");
			this.gamepad = GameObject.Find("Gamepad");
		}

		private void Start()
		{
			GameObject.Destroy(this.keyboard);
            GameObject.Destroy(this.gamepad);
			this.SurvivalModLoadingText();
		}

		private void SurvivalModLoadingText()
		{
            GameObject gameObject = GameObject.Instantiate<GameObject>(GameObject.Find("CrashLabel"));
			UILabel component = gameObject.GetComponent<UILabel>();
			component.transform.position = gameObject.transform.position + gameObject.transform.up * 1.5f;
		}

		private GameObject keyboard = null;

		private GameObject gamepad = null;
	}
}
