using System;
using CustomEventSystem;
using UnityEngine;

public class AchievementEventController : MonoBehaviour
{
	private Config AchievementSettings;

	private void Awake()
	{
		AchievementSettings = Settings.Get.AchievementSettings;

		EventSystem.GetEvent<BuyEvent>().Subscribe(BuyClick);
		EventSystem.GetEvent<ClickEvent>().Subscribe(Click);
		EventSystem.GetEvent<DestroyBlockEvent>().Subscribe(DestroyClick);
	}

	private void OnDestroy()
	{
		EventSystem.GetEvent<BuyEvent>().UnSubscribe(BuyClick);
		EventSystem.GetEvent<ClickEvent>().UnSubscribe(Click);
		EventSystem.GetEvent<DestroyBlockEvent>().Subscribe(DestroyClick);
	}

	public void MineClick()
	{
		if (PlayerPrefs.GetInt(Achievements.GoToMines.ToString()) == ListOf.Constants.DefaultPositiveValue)
			return;

		EventSystem.GetEvent<AchievementEvent>().Publish(Achievements.GoToMines);
	}

	public void BuyClick()
	{
		AchievementSettings.UpgradeCount++;

		switch (AchievementSettings.UpgradeCount)
		{
			case 1:
				EventSystem.GetEvent<AchievementEvent>().Publish(Achievements.Buy1);
				break;
			case 10:
				EventSystem.GetEvent<AchievementEvent>().Publish(Achievements.Break10);
				break;
			case 100:
				EventSystem.GetEvent<AchievementEvent>().Publish(Achievements.Buy100);
				break;
			default:
				break;
		}

	}

	public void Click()
	{
		AchievementSettings.ClickCount++;

		switch (AchievementSettings.ClickCount)
		{
			case 20:
				EventSystem.GetEvent<AchievementEvent>().Publish(Achievements.Click20);
				break;
			case 300:
				EventSystem.GetEvent<AchievementEvent>().Publish(Achievements.Click300);
				break;
			case 5000:
				EventSystem.GetEvent<AchievementEvent>().Publish(Achievements.Click5000);
				break;
			case 1000000:
				EventSystem.GetEvent<AchievementEvent>().Publish(Achievements.Click1000000);
				break;
			default:
				break;
		}
	}

	public void DestroyClick()
	{
		AchievementSettings.DestroyedBlockCount++;

		switch (AchievementSettings.DestroyedBlockCount)
		{
			case 1:
				EventSystem.GetEvent<AchievementEvent>().Publish(Achievements.Break1);
				break;
			case 10:
				EventSystem.GetEvent<AchievementEvent>().Publish(Achievements.Break10);
				break;
			case 50:
				EventSystem.GetEvent<AchievementEvent>().Publish(Achievements.Break50);
				break;
			case 100:
				EventSystem.GetEvent<AchievementEvent>().Publish(Achievements.Break100);
				break;
			default:
				break;
		}
	}

	[Serializable]
	public class Config
	{
		public int UpgradeCount;
		public int ClickCount;
		public int DestroyedBlockCount;

		public void Reset()
		{
			UpgradeCount = 0;
			ClickCount = 0;
			DestroyedBlockCount = 0;
		}
	}
}
