using System;
using System.Collections.Generic;
using UnityEngine;

namespace YandereNext.Debugging
{
	internal class LogManager
	{
		public static string Log
		{
			get
			{
				return LogManager.logContent;
			}
		}

		public static void Clear()
		{
			LogManager.logMessages.Clear();
			LogManager.logContent = "";
		}

		public static void SetupLog()
		{
			Application.logMessageReceived += new Application.LogCallback(LogManager.Register);
		}

		private static void Register(string message, string stackTrace, LogType logtype)
		{
			bool flag = LogManager.logMessages.Count == LogManager._maxLines;
			bool flag2 = flag;
			if (flag2)
			{
				LogManager.logMessages.RemoveAt(0);
			}
			LogManager.logMessages.Add(message);
			string[] value = LogManager.logMessages.ToArray();
			LogManager.logContent = string.Join("\n", value);
		}

		private static List<string> logMessages = new List<string>();

		private static int _maxLines = 20;

		private static string logContent = "";
	}
}
