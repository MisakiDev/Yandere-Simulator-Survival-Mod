using System;
using UnityEngine;

namespace YandereNext.Debugging
{
	public class LogConsole : MonoBehaviour
	{
		private void Clear()
		{
			LogManager.Clear();
		}

		private void Start()
		{
			this.pos = new Vector2(2f, 2f);
			this.size = new Vector2(200f, 400f);
			this.logStyle = new GUIStyle();
			this.logStyle.normal.textColor = Color.blue;
		}

		private void OnGUI()
		{
		}

		public static bool log = true;

		private Vector2 pos;

		private Vector2 size;

		private GUIStyle logStyle;
	}
}
