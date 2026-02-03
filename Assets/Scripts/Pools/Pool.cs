public abstract class Pool<TItem, TData>
{
	protected readonly TItem Item;
	protected readonly TData[] Datas;

	protected Pool(TItem item, TData[] datas)
	{
		Item = item;
		Datas = datas;
	}

	public abstract void FillPool(int poolSize);
	
	public abstract TItem Spawn();
	
	public abstract void Despawn(TItem item);
}
