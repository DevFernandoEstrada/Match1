using TMPro;
using UnityEngine;

public class UIGameplay : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI scoreText, movesText;
	[SerializeField] private IntVariable score, moves;

	private void OnEnable()
	{
		score.OnValueChanged += ScoreOnValueChanged;
		moves.OnValueChanged += MovesOnValueChanged;
	}

	private void OnDisable()
	{
		score.OnValueChanged -= ScoreOnValueChanged;
		moves.OnValueChanged -= MovesOnValueChanged;
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