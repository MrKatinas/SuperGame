using System.Collections;
using System.Collections.Generic;
using CustomEventSystem;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
	public AchievementDataBase database;

	//for notifications
	public AchievementNotificationController achievementNotificationController;
	public Achievements achievementToShow;

	//for achievement table
	public GameObject achievementItemPrefab;
	public Transform content;

	//for loading achievements in editor
	[SerializeField][HideInInspector]
	public List<AchievementItemController> achievementItems; //for achievement status (un/locked)

	private void Awake()
	{
		EventSystem.GetEvent<AchievementEvent>().Subscribe(UnlockAchievement);
		EventSystem.GetEvent<ResetGameEvent>().Subscribe(LockAllAchievements);
	}

	private void Start()
	{
		LoadAchievementTable();
	}

	private void OnDestroy()
	{
		EventSystem.GetEvent<AchievementEvent>().UnSubscribe(UnlockAchievement);
		EventSystem.GetEvent<ResetGameEvent>().UnSubscribe(LockAllAchievements);
	}

	public void ShowNotification()
	{
		Achievement achievement = database.achievements[(int)achievementToShow];
		achievementNotificationController.ShowNotification(achievement);
	}

	public void ChangeAchievementToShow(Achievements achievement)
	{
		ListOf.Revision.Add();
		achievementToShow = achievement;
	}

	[ContextMenu("LoadAchievementTable")] //for loading achievements in editor
	private void LoadAchievementTable()
	{
		//for loading achievements in editor
		foreach (AchievementItemController controller in achievementItems)
		{
			DestroyImmediate(controller.gameObject);
		}

		achievementItems.Clear();

		foreach (Achievement achievement in database.achievements)
		{
			GameObject obj = Instantiate(achievementItemPrefab, content);
			AchievementItemController controller = obj.GetComponent<AchievementItemController>();
			bool unlocked = PlayerPrefs.GetInt(achievement.id, ListOf.Constants.DefaultNegativeValue) == 1; //from UnlockAchievement()
			controller.unlocked = unlocked;
			controller.achievement = achievement;
			controller.RefreshView();

			//for loading achievements in editor
			achievementItems.Add(controller);
		}
	}

	public void UnlockAchievement(Achievements achievement)
	{
		AchievementItemController item = achievementItems[(int) achievement];

		if (item.unlocked)
			return;

		ListOf.Revision.Add();
		PlayerPrefs.SetInt(item.achievement.id, ListOf.Constants.DefaultPositiveValue);
		ChangeAchievementToShow(achievement);
		ShowNotification();

		item.unlocked = true;
		item.RefreshView();
	}

	[ContextMenu("LockAllAchievements")]
	private void LockAllAchievements()
	{
		Settings.Get.AchievementSettings.Reset();

		foreach (Achievement achievement in database.achievements)
		{
			PlayerPrefs.DeleteKey(achievement.id);
		}

		foreach (AchievementItemController controller in achievementItems)
		{
			controller.unlocked = false;
			controller.RefreshView();
		}
	}
}
