using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
	[SerializeField] private AudioMixer _mixer;

	public void SetAmbientLevel(float sliderValue)
	{
		_mixer.SetFloat(ListOf.Names.GroupAmbient, (float)Math.Log10(sliderValue) * 20); 
	}

	public void SetSfxLevel(float sliderValue)
	{
		_mixer.SetFloat(ListOf.Names.GroupSfx, (float)Math.Log10(sliderValue) * 20);
	}
}
