using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
	[SerializeField] private Level level;
	[SerializeField] private GameEvent onGameOver, onReplay;
	[SerializeField] private IntVariable score, moves;

	[SerializeField] private CameraScaler cameraScaler;
	[SerializeField] private BoardClicker boardClicker;

	private Board<PlayableObject> _board;
	private PlayableObjectsPool _pool;
	private Transform _boardTransform;
	private Vector2Int _lastClickedCell;

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
		onReplay.OnEvent += OnReplayOnEvent;
		boardClicker.OnCellClick += BoardClickerOnCellClick;
	}

	private void OnDisable()
	{
		onReplay.OnEvent -= OnReplayOnEvent;
		boardClicker.OnCellClick -= BoardClickerOnCellClick;
	}

	private void BoardClickerOnCellClick(Vector2Int position)
	{
		if (SetGameState != GameState.WaitingForMovement) return;
		if (!_board.InBounds(position)) return;

		_lastClickedCell = position;
		SetGameState = GameState.OnMovement;
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
				cameraScaler.SetCameraSize(level.boardSize);
				boardClicker.SetupBoard(level.boardSize);
				_boardTransform = boardClicker.transform;
				_board = new Board<PlayableObject>(level.boardSize);
				_pool = new PlayableObjectsPool(level.playableObject, level.datas);
				_pool.FillPool(_board.GetSize());
				FillBoard();
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
				StartCoroutine(OnMovement());
				break;

			case GameState.GameOver:
				onGameOver.Trigger();
				break;

			case GameState.Close:
				break;
		}
	}

	private IEnumerator OnMovement()
	{
		Dictionary<Vector2Int, PlayableObject> affectedBlocks = new Dictionary<Vector2Int, PlayableObject>();
		ExecuteMovement(_lastClickedCell.x, _lastClickedCell.y, _board.GetItem(_lastClickedCell.x, _lastClickedCell.y), affectedBlocks);

		score.SetValue(score.Value + affectedBlocks.Count);
		moves.SetValue(moves.Value - 1);

		foreach (KeyValuePair<Vector2Int, PlayableObject> block in affectedBlocks)
		{
			_pool.Despawn(block.Value);
			_board.SetItem(block.Key, null);
		}

		yield return new WaitForSeconds(level.timeBetweenMoves);
		
		foreach (int column in affectedBlocks.Select(pair=>pair.Key.x).Distinct())
		{
			ApplyGravity(column);
			FillEmptyCells(column);
		}

		SetGameState = moves.Value <= 0 ? GameState.GameOver : GameState.WaitingForMovement;
	}

	private void ExecuteMovement(int x, int y, PlayableObject playableObject, Dictionary<Vector2Int, PlayableObject> affectedBlocks)
	{
		if (!_board.InBounds(x, y)) return;
		if (!playableObject.Compare(_board.GetItem(x, y))) return;
		if (affectedBlocks.ContainsKey(new Vector2Int(x, y))) return;

		affectedBlocks.Add(new Vector2Int(x, y), _board.GetItem(x, y));

		ExecuteMovement(x + 1, y, playableObject, affectedBlocks);
		ExecuteMovement(x - 1, y, playableObject, affectedBlocks);
		ExecuteMovement(x, y + 1, playableObject, affectedBlocks);
		ExecuteMovement(x, y - 1, playableObject, affectedBlocks);
	}

	private void ApplyGravity(int column)
	{
		int emptyY = -1;
		for (int y = 0; y < _board.GetHeight; y++)
		{
			if (_board.GetItem(column, y) == null)
			{
				if (emptyY == -1) emptyY = y;
			}
			else if (emptyY != -1)
			{
				_board.SetItem(column, emptyY, _board.GetItem(column, y).SetPosition(new Vector2Int(column, emptyY)));
				_board.SetItem(column, y, null);
				emptyY++;
			}
		}
	}

	private void FillEmptyCells(int column)
	{
		for (int y = 0; y < _board.GetHeight; y++)
		{
			if (_board.GetItem(column, y) == null)
			{
				_board.SetItem(column, y, _pool.Spawn().SetParent(_boardTransform).SetPosition(new Vector2Int(column, y)));
			}
		}
	}

	private void FillBoard()
	{
		for (int x = 0; x < _board.GetWidth; x++)
		for (int y = 0; y < _board.GetHeight; y++)
		{
			_board.SetItem(x, y, _pool.Spawn().SetParent(_boardTransform).SetPosition(new Vector2Int(x, y)));
		}
	}

	private void ClearBoard()
	{
		for (int x = 0; x < _board.GetWidth; x++)
		for (int y = 0; y < _board.GetHeight; y++)
		{
			_pool.Despawn(_board.GetItem(x, y));
			_board.SetItem(x, y, null);
		}
	}
}