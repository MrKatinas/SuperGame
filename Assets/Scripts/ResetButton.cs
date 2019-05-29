using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
	public void ResetData()
	{
		ListOf.ToDoLater.Remove();
		PlayerPrefs.SetInt(ListOf.PlayerPrefs.ResetKey, ListOf.Constants.DefaultPositiveValue);
	}
}
