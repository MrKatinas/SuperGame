using System;
using UnityEngine;
using Malee;

[CreateAssetMenu()]
public class AchievementDataBase : ScriptableObject
{
	[Reorderable(sortable = false, paginate = false)]
	public AchievementsArray achievements;

	[Serializable]
	public class AchievementsArray : ReorderableArray<Achievement>{}
}
