using System;
using System.Collections.Generic;
using CustomEventSystem;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[SerializeField] private bool _isReset;
	[SerializeField] private GameObject MainScreen;
	[SerializeField] private GameObject EndScreen;

	private Config gameSettings;
	private Config.Level currentLevel;
	private ShopManager.Config shopSettings;

	private ShopManager.Config.Upgrade moneyMultiplier;
	private ShopManager.Config.Upgrade clickPower;
	private ShopManager.Config.Upgrade clickPowerMultiplier;

	private void Awake()
	{
		if (_isReset)
		{
			Debug.Log("as");
			
		}

		gameSettings = Settings.Get.GameSettings;
		shopSettings = Settings.Get.ShopSettings;
		currentLevel = gameSettings.Levels[gameSettings.CurrentLevelIndex];
		currentLevel.CurrentHealth = currentLevel.Health;

		moneyMultiplier = shopSettings.Upgrades[(int) UpgradeOrder.MoneyOnClick];
		clickPower = shopSettings.Upgrades[(int) UpgradeOrder.ClickingPower];
		clickPowerMultiplier = shopSettings.Upgrades[(int) UpgradeOrder.ClickingPowerMultiplier];
	}

	private void Start()
    {
	    if (PlayerPrefs.GetInt(ListOf.PlayerPrefs.ResetKey, ListOf.Constants.DefaultNegativeValue) == ListOf.Constants.DefaultPositiveValue)
	    {
		    PlayerPrefs.SetInt(ListOf.PlayerPrefs.ResetKey, ListOf.Constants.DefaultNegativeValue);
		    ResetGame();
		}

		EventSystem.GetEvent<PlayerMoneyChangeEvent>().Publish(gameSettings.PlayerMoney);
		PublishChangeLevelEvent(currentLevel);
	}

	public void Click()
	{
		var power = clickPower.Value * clickPowerMultiplier.Value;
		power = (power > currentLevel.Armor) ? power - currentLevel.Armor : 0;

		if (power < currentLevel.CurrentHealth)
		{
			currentLevel.CurrentHealth -= power;
			EventSystem.GetEvent<ClickEvent>().Publish(currentLevel.CurrentHealth);
			return;
		}

		currentLevel.CurrentHealth = currentLevel.Health;
		EventSystem.GetEvent<DestroyBlockEvent>().Publish();

		gameSettings.PlayerMoney += currentLevel.Money * moneyMultiplier.Value;
		EventSystem.GetEvent<PlayerMoneyChangeEvent>().Publish(gameSettings.PlayerMoney);

		var a = gameSettings.Levels[gameSettings.Levels.Count - 1];
		var b = gameSettings.Levels[0];

		if (currentLevel == b && power >= currentLevel.Health + currentLevel.Armor)
		{
			OpenEndScreen();
		}
	}

	public void OpenEndScreen()
	{
		MainScreen.SetActive(false);
		EndScreen.SetActive(true);
	}

	public void ResetGame()
	{
		Settings.Get.ResetSettings();
		EventSystem.GetEvent<ResetGameEvent>().Publish();

		Awake();

		EventSystem.GetEvent<PlayerMoneyChangeEvent>().Publish(gameSettings.PlayerMoney);
		PublishChangeLevelEvent(currentLevel);
	}

	public void ChangeLevel(bool isNextLevel)
	{
		if (isNextLevel)
		{
			gameSettings.CurrentLevelIndex++;
			currentLevel = gameSettings.Levels[gameSettings.CurrentLevelIndex];
			currentLevel.CurrentHealth = currentLevel.Health;
			PublishChangeLevelEvent(currentLevel);
			return;
		}

		gameSettings.CurrentLevelIndex--;
		currentLevel = gameSettings.Levels[gameSettings.CurrentLevelIndex];
		currentLevel.CurrentHealth = currentLevel.Health;
		PublishChangeLevelEvent(currentLevel);
	}

	private void PublishChangeLevelEvent(Config.Level level)
	{
		EventSystem.GetEvent<ChangeLevelEvent>().Publish(ChangeLevelEventArgs.Create(
			level.Sprite, 
			level.Name, 
			level.Health, 
			gameSettings.CurrentLevelIndex <= 0,
			gameSettings.CurrentLevelIndex >= gameSettings.Levels.Count - 1));
	}

	[Serializable]
	public class Config
	{
		public int PlayerMoney;
		public int CurrentLevelIndex;
		public List<Level> Levels;

		[Serializable]
		public class Level
		{
			public string Name;
			public int Money;
			public int Health;
			public int CurrentHealth;
			public int Armor;
			public Sprite Sprite;

			public static Level Clone(Level level)
			{
				return new Level()
				{
					Name = level.Name,
					Money = level.Money,
					Health = level.Health,
					CurrentHealth = level.CurrentHealth,
					Armor = level.Armor,
					Sprite = level.Sprite
				};
			}
		}

		public static Config Clone(Config gameSettings)
		{
			var clone = new Config()
			{
				PlayerMoney = gameSettings.PlayerMoney,
				CurrentLevelIndex = gameSettings.CurrentLevelIndex,
				Levels = new List<Level>()
			};

			foreach (var level in gameSettings.Levels)
			{
				clone.Levels.Add(Level.Clone(level));
			}

			return clone;
		}
	}
}
