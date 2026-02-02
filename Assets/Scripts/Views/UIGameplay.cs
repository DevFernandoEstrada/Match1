using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI scoreText, movesText;
	[SerializeField] private IntVariable score, moves;
	[SerializeField] private Button makeMoveButton;
	[SerializeField] private GameEvent onMakeMove;

	private void OnEnable()
	{
		score.OnValueChanged += ScoreOnValueChanged;
		moves.OnValueChanged += MovesOnValueChanged;
		makeMoveButton.onClick.AddListener(onMakeMove.Trigger);
	}

	private void OnDisable()
	{
		score.OnValueChanged -= ScoreOnValueChanged;
		moves.OnValueChanged -= MovesOnValueChanged;
		makeMoveButton.onClick.RemoveListener(onMakeMove.Trigger);
	}
	
	private void ScoreOnValueChanged(int value)
	{
		scoreText.text = value.ToString();
	}
	
	private void MovesOnValueChanged(int value)
	{
		movesText.text = value.ToString();
	}
}