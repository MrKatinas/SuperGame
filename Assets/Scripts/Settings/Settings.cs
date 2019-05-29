using UnityEngine;

[CreateAssetMenu(fileName = "Settings.asset", menuName = "CreateSettings")]
public class Settings : ScriptableObject
{
	#region Singleton

	private const string settingsPath = "Settings";
	private const string defaultSettingsPath = "Default/Settings";

	private static Settings _settings;
	public static Settings Get
	{
		get
		{
			if (_settings != null)
				return _settings;

			_settings = Resources.Load<Settings>(settingsPath);
			return _settings;
		}
	}

	#endregion

	public GameManager.Config GameSettings;
	public ShopManager.Config ShopSettings;
	public SoundManager.Config SoundSettings;
	public AchievementEventController.Config AchievementSettings;

	public void ResetSettings()
	{
		var defaultSettings = Resources.Load<Settings>(defaultSettingsPath);

		_settings.GameSettings = GameManager.Config.Clone(defaultSettings.GameSettings);
		_settings.ShopSettings = ShopManager.Config.Clone(defaultSettings.ShopSettings);

		ListOf.ToDoLater.Improve();

		// Not working, unity problems with serialization 
		//_settings.GameSettings = defaultSettings.GameSettings.Clone();
		//_settings.ShopSettings = defaultSettings.ShopSettings.Clone();
	}
}
