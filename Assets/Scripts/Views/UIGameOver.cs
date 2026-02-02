	using UnityEngine;
	using UnityEngine.UI;

	public class UIGameOver : MonoBehaviour
	{
		[SerializeField] private GameEvent onGameOver, onReplay;
		[SerializeField] private Button replayButton;
		[SerializeField] private GameObject gameOverPanel;

		private void Start()
		{
			SetPanelActive(false);
		}

		private void OnEnable()
		{
			onGameOver.OnEvent += OnGameOverOnEvent;
			replayButton.onClick.AddListener(OnReplayOnEvent);
		}

		private void OnDisable()
		{
			onGameOver.OnEvent -= OnGameOverOnEvent;
			replayButton.onClick.RemoveListener(OnReplayOnEvent);
		}
		
		private void OnReplayOnEvent()
		{
			SetPanelActive(false);
			onReplay.Trigger();
		}

		private void OnGameOverOnEvent()
		{
			SetPanelActive(true);
		}

		private void SetPanelActive(bool active)
		{
			gameOverPanel.SetActive(active);
		}
	}
