using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Int Variable")]
public class IntVariable : ScriptableObject
{
	[SerializeField] private int value;

	public int Value { get { return value; } }

	public event Action<int> OnValueChanged;

	public void SetValue(int newValue)
	{
		value = newValue;
		OnValueChanged?.Invoke(value);
	}
}