using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
	public event Action OnEvent;
	
	public void Trigger() { OnEvent?.Invoke(); }
}