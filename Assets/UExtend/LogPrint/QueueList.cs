/*
 * A queueList which support get/remove/insert by position
 * 
 */ 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
public class QueueList<T> : IEnumerable<T>, IEnumerable
{
	protected List<T> m_list = new List<T>();
	public int Count
	{
		get
		{
			return this.m_list.Count;
		}
	}
	public T this[int index]
	{
		get
		{
			return this.m_list[index];
		}
		set
		{
			this.m_list.Insert(index, value);
		}
	}
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}
	public int Enqueue(T item)
	{
		int count = this.m_list.Count;
		this.m_list.Add(item);
		return count;
	}
	public T Dequeue()
	{
		T result = this.m_list[0];
		this.m_list.RemoveAt(0);
		return result;
	}
	public T Peek()
	{
		return this.m_list[0];
	}
	public int GetCount()
	{
		return this.m_list.Count;
	}
	public T GetItem(int index)
	{
		return this.m_list[index];
	}
	public void Clear()
	{
		this.m_list.Clear();
	}
	public T RemoveAt(int position)
	{
		if (this.m_list.Count <= position)
		{
			return default(T);
		}
		T result = this.m_list[position];
		this.m_list.RemoveAt(position);
		return result;
	}
	public bool Remove(T item)
	{
		return this.m_list.Remove(item);
	}
	public List<T> GetList()
	{
		return this.m_list;
	}
	public bool Contains(T item)
	{
		return this.m_list.Contains(item);
	}
	public IEnumerator<T> GetEnumerator()
	{
		return this.Enumerate().GetEnumerator();
	}
	public override string ToString()
	{
		return string.Format("Count={0}", this.Count);
	}
	[DebuggerHidden]
	protected IEnumerable<T> Enumerate()
	{
		for(int i = 0;i<m_list.Count;++i)
		{
			yield return m_list[i];
		}
	}
}
