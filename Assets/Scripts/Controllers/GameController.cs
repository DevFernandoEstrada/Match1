using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
	[SerializeField] private Level level;
	[SerializeField] private GameEvent onGameOver, onReplay, onMakeMove;
	[SerializeField] private IntVariable score, moves;
	
	private GameState _currentState = GameState.Close;

	private GameState SetGameState
	{
		get { return _currentState; }
		set
		{
			if (value == _currentState) return;
			_currentState = value;
			OnGameStateChanged(value);
		}
	}

	private void Start()
	{
		SetGameState = GameState.Setup;
	}

	private void OnEnable()
	{
		onMakeMove.OnEvent += OnMakeMoveOnEvent;
		onReplay.OnEvent += OnReplayOnEvent;
	}

	private void OnDisable()
	{
		onMakeMove.OnEvent -= OnMakeMoveOnEvent;
		onReplay.OnEvent -= OnReplayOnEvent;
	}

	private void OnMakeMoveOnEvent()
	{
		score.SetValue(score.Value + 10);
		moves.SetValue(moves.Value - 1);

		SetGameState = moves.Value <= 0 ? GameState.GameOver : GameState.WaitingForMovement;
	}
	
	private void OnReplayOnEvent()
	{
		SetGameState = GameState.Init;
	}

	private void OnGameStateChanged(GameState newState)
	{
		switch (newState)
		{
			case GameState.Setup:
				SetGameState = GameState.Init;
				break;
			
			case GameState.Init:
				score.SetValue(0);
				moves.SetValue(level.moves);
				SetGameState = GameState.WaitingForMovement;
				break;
			
			case GameState.WaitingForMovement:
				break;
			
			case GameState.OnMovement:
				break;
			
			case GameState.GameOver:
				onGameOver.Trigger();
				break;
			
			case GameState.Close:
				break;
		}
	}
}