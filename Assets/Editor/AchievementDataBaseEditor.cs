using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(AchievementDataBase))]
public class AchievementDataBaseEditor : Editor
{
	private AchievementDataBase database;

	private void OnEnable()
	{
		database = target as AchievementDataBase;
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if (GUILayout.Button("Generate Enum"))
		{
			GenerateEnum();
		}

		
	}

	private void GenerateEnum()
	{
		string filePath = Path.Combine(Application.dataPath, "Achievements.cs");
		string code = "public enum Achievements {";
		foreach (Achievement achievement in database.achievements)
		{
			//need to validate if id is proper
			code += achievement.id + ",";
		}

		code += "}";
		File.WriteAllText(filePath, code);
		AssetDatabase.ImportAsset("Assets/Achievements.cs");
	}
}
