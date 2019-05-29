using System;
using CustomEventSystem;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	[SerializeField] private AudioSource _ambientAudioSource;
	[SerializeField] private AudioSource _sfxAudioSource;

	private Config soundSettings;

	private void Awake()
	{
		soundSettings = Settings.Get.SoundSettings;

		EventSystem.GetEvent<ClickEvent>().Subscribe(PlayClick);
		EventSystem.GetEvent<DestroyBlockEvent>().Subscribe(PlayDestroyBlockSound);
		EventSystem.GetEvent<ChangeLevelEvent>().Subscribe(PlayChangeLevelMusic);
	}

	private void OnDestroy()
	{
		EventSystem.GetEvent<ClickEvent>().UnSubscribe(PlayClick);
		EventSystem.GetEvent<DestroyBlockEvent>().UnSubscribe(PlayDestroyBlockSound);
		EventSystem.GetEvent<ChangeLevelEvent>().UnSubscribe(PlayChangeLevelMusic);
	}

	private void PlayClick(int stuff)
	{
		_sfxAudioSource.PlayOneShot(soundSettings.ClickSFX);
	}

	private void PlayChangeLevelMusic(ChangeLevelEventArgs stuff)
	{
		PlayClick(-1);
	}

	private void PlayDestroyBlockSound()
	{
		PlayClick(-1);
	}

	[Serializable]
	public class Config
	{
		public AudioClip MainMenuMusic;
		public AudioClip GameMusic;
		public AudioClip ClickSFX;
	}

}
