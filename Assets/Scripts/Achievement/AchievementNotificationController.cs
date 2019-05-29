using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]
public class AchievementNotificationController : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _achievementTitleLabel;

	private Animator m_animator;

	private void Awake()
	{
		m_animator = GetComponent<Animator>();
	}

	public void ShowNotification(Achievement achievement)
	{
		_achievementTitleLabel.text = achievement.title;
		m_animator.SetTrigger("Appear");
	}
}
