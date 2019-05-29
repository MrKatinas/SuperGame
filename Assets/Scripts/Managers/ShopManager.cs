using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using CustomEventSystem;

public class ShopManager : MonoBehaviour
{
	[SerializeField] private List<TextMeshProUGUI> _upgradeTexts;
	[SerializeField] private List<TextMeshProUGUI> _upgradeButtonTexts;

	private Config shopSettings;
	private GameManager.Config gameSettings;

	private void Awake()
	{
		shopSettings = Settings.Get.ShopSettings;
		gameSettings = Settings.Get.GameSettings;

		UpdateButtonTexts();
	}

	public void Upgrade(int index)
	{
		var upgradeIndex = (UpgradeOrder) index;
		var upgrade = shopSettings.Upgrades[(int) upgradeIndex];

		if (gameSettings.PlayerMoney < upgrade.Price)
		{
			return;
		}

		gameSettings.PlayerMoney -= upgrade.Price;
		EventSystem.GetEvent<PlayerMoneyChangeEvent>().Publish(gameSettings.PlayerMoney);
		EventSystem.GetEvent<BuyEvent>().Publish();

		upgrade.IncreaseLevel();
		_upgradeTexts[(int)upgradeIndex].text = upgrade.ToString();
		_upgradeButtonTexts[(int)upgradeIndex].text = $"Costs - {upgrade.Price}";
	}

	private void UpdateButtonTexts()
	{
		for (var i = 0; i < _upgradeTexts.Count; i++)
		{
			var upgradeIndex = (UpgradeOrder)i;
			var upgrade = shopSettings.Upgrades[(int)upgradeIndex];

			_upgradeTexts[(int)upgradeIndex].text = upgrade.ToString();
			_upgradeButtonTexts[(int)upgradeIndex].text = $"Costs - {upgrade.Price}";
		}
	}

	[Serializable]
	public class Config
	{
		public List<Upgrade> Upgrades;

		[Serializable]
		public class Upgrade
		{
			public string Name;
			public int Value;
			public int IncrementalValue;
			public int Multiplier;
			public int Price;
			public float PriceMultiplier;

			public void IncreaseLevel()
			{
				if (IncrementalValue == ListOf.Constants.DefaultNegativeValue)
				{
					Value = (int) Mathf.Ceil(Value * Multiplier);
					Price = (int)(Price * PriceMultiplier);
					return;
				}

				Value += IncrementalValue;
				Price = (int)(Price * PriceMultiplier);
			}

			public override string ToString()
			{
				return $"{Name}\n{Value}";
			}

			public static Upgrade Clone(Upgrade upgrade)
			{
				return new Upgrade()
				{
					Name = upgrade.Name,
					Value = upgrade.Value,
					IncrementalValue = upgrade.IncrementalValue,
					Multiplier = upgrade.Multiplier,
					Price = upgrade.Price,
					PriceMultiplier = upgrade.PriceMultiplier
				};
			}
		}

		public static Config Clone(Config shopSettings)
		{
			var clone = new Config()
			{
				Upgrades = new List<Upgrade>()
			};

			foreach (var upgrade in shopSettings.Upgrades)
			{
				clone.Upgrades.Add(Upgrade.Clone(upgrade));
			}

			return clone;
		}
	}
}

public enum UpgradeOrder
{
	ClickingPower = 0,
	MoneyOnClick = 1,
	ClickingPowerMultiplier = 2
}
