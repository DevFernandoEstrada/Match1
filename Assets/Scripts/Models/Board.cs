using UnityEngine;

public class Board<T>
{
	private T[,] _grid;

	public Board(Vector2Int size)
	{
		_grid = new T[size.x, size.y];
	}
	
	public int GetWidth { get { return _grid.GetLength(0); } }
	
	public int GetHeight { get { return _grid.GetLength(1); } }
	
	public int GetSize() { return GetWidth * GetHeight; }
	
	public bool InBounds(int x, int y) { return x >= 0 && x < GetWidth && y >= 0 && y < GetHeight; }
	public bool InBounds(Vector2Int position) { return InBounds(position.x, position.y); }
	
	public T GetItem(int x, int y) { return _grid[x, y]; }
	public T GetItem(Vector2Int position) { return GetItem(position.x, position.y); }
	
	public void SetItem(int x, int y, T item) { _grid[x, y] = item; }
	public void SetItem(Vector2Int position, T item) { SetItem(position.x, position.y, item); }
}