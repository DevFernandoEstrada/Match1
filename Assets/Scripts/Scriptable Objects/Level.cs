using UnityEngine;

[CreateAssetMenu(menuName = "Level")]
public class Level : ScriptableObject
{
	public Vector2Int boardSize;
	public PlayableObject playableObject;
	public ItemData[] datas;
	public int moves;
	public float timeBetweenMoves;
}