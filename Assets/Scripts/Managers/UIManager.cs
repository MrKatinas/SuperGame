using CustomEventSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;

	[SerializeField] private Image _blockSprite;
	[SerializeField] private TextMeshProUGUI _blockTitleText;
	[SerializeField] private TextMeshProUGUI _blockHealthText;

    [SerializeField] private Button _previousButton;
    [SerializeField] private Image _previousButtonSprite;
	[SerializeField] private TextMeshProUGUI _previousButtonText;

	[SerializeField] private Button _nextButton;
    [SerializeField] private Image _nextButtonSprite;
	[SerializeField] private TextMeshProUGUI _nextButtonText;

	private int maxHealth;

	private void Awake()
	{
		_moneyText.text = string.Empty;
		EventSystem.GetEvent<PlayerMoneyChangeEvent>().Subscribe(ChangePlayerMoney);
		EventSystem.GetEvent<ClickEvent>().Subscribe(ShowClick);
		EventSystem.GetEvent<DestroyBlockEvent>().Subscribe(DestroyBlock);
		EventSystem.GetEvent<ChangeLevelEvent>().Subscribe(ChangeLevel);
	}

	private void OnDestroy()
	{
		EventSystem.GetEvent<PlayerMoneyChangeEvent>().UnSubscribe(ChangePlayerMoney);
		EventSystem.GetEvent<ClickEvent>().UnSubscribe(ShowClick);
		EventSystem.GetEvent<DestroyBlockEvent>().UnSubscribe(DestroyBlock);
		EventSystem.GetEvent<ChangeLevelEvent>().UnSubscribe(ChangeLevel);
	}

	private void ChangePlayerMoney(int newMoney)
	{
		_moneyText.text = $"{newMoney}";
	}

	private void ShowClick(int newHealth)
	{
		_blockHealthText.text = $"Heath: {newHealth}/{maxHealth}";
	}

	private void DestroyBlock()
	{
		_blockHealthText.text = $"Heath: {maxHealth}/{maxHealth}";

		// Need to Animate block destruction or just some sfx.
		ListOf.ToDoLater.Improve();
	}

	private void ChangeLevel(ChangeLevelEventArgs args)
	{
		maxHealth = args.Health;

		_blockSprite.sprite = args.Sprite;
		_blockTitleText.text = args.Title;
		_blockHealthText.text = $"Heath: {maxHealth}/{maxHealth}";

		_previousButton.interactable = true;
		_previousButtonSprite.color = Color.white;
		_previousButtonText.color = Color.white;

		_nextButton.interactable = true;
		_nextButtonSprite.color = Color.white;
		_nextButtonText.color = Color.white;


		if (args.IsFirstLevel)
		{
			_previousButton.interactable = false;
			_previousButtonSprite.color = Color.gray;
			_previousButtonText.color = Color.black;
			return;
		}

		if (args.IsLastLevel)
		{
			_nextButton.interactable = false;
			_nextButtonSprite.color = Color.gray;
			_nextButtonText.color = Color.black;
		}
	}
}
