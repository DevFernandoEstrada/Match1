using System.Collections.Generic;
using UnityEngine;

public class PlayableObjectsPool : Pool<PlayableObject, ItemData>
{
	private readonly Queue<PlayableObject> _pool = new Queue<PlayableObject>();

	public PlayableObjectsPool(PlayableObject item, ItemData[] datas) : base(item, datas)
	{
	}

	public override void FillPool(int poolSize)
	{
		for (int i = 0; i < poolSize; i++) { AddToQueue(); }
	}
	
	public override PlayableObject Spawn()
	{
		if (_pool.Count == 0) AddToQueue();
		return _pool.Dequeue().SetData(GetRandomData()).SetActive(true);
	}
	
	public override void Despawn(PlayableObject item) => _pool.Enqueue(item.SetActive(false));

	private void AddToQueue() => _pool.Enqueue(Object.Instantiate(Item).SetActive(false));

	private ItemData GetRandomData() => Datas[Random.Range(0, Datas.Length)];
}