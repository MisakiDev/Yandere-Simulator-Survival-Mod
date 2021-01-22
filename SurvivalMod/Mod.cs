using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using YandereNext.SURVIVALPROJECT;

namespace SurvivalMod
{
	public static class Mod
	{
		public static void Start(string Path)
		{
			SurvivalModeGlobals.Survival = true;
			GameGlobals.LoveSick = true;
			SceneManager.sceneLoaded += delegate(Scene scene, LoadSceneMode mode)
			{
				bool flag = scene.name == "SchoolScene";
				if (flag)
				{
					GameObject obj = new GameObject("SurvivalMod");
					obj.AddComponent<SurvivalManager>();
				}
				else
				{
					bool flag2 = scene.name == "LoadingScene";
					if (flag2)
					{
						GameObject obj2 = new GameObject("LoadingMod");
						obj2.AddComponent<LoadingScreenManager>();
					}
				}
			};
			SceneManager.LoadScene("LoadingScene");
		}
	}
}
