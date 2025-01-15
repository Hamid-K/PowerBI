using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Internal.DotNet4;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Threading.DotNet4;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent.DotNet4
{
	// Token: 0x020000BF RID: 191
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(SystemThreadingCollections_ConcurrentStackDebugView<>))]
	[Serializable]
	public class ConcurrentStack<T> : IConcurrentCollection<T>, IEnumerable<T>, IEnumerable, ICollection, ISerializable, IDeserializationCallback
	{
		// Token: 0x06000847 RID: 2119 RVA: 0x0002B8A2 File Offset: 0x00029AA2
		public ConcurrentStack()
		{
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0002B8AC File Offset: 0x00029AAC
		public ConcurrentStack(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw Error.ArgumentNull("collection");
			}
			ConcurrentStack<T>.Node node = null;
			foreach (T t in collection)
			{
				node = new ConcurrentStack<T>.Node(t)
				{
					m_next = node
				};
			}
			this.m_head = node;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0002B918 File Offset: 0x00029B18
		protected ConcurrentStack(SerializationInfo info, StreamingContext context)
		{
			this.m_serializationInfo = info;
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x0002B927 File Offset: 0x00029B27
		public bool IsEmpty
		{
			get
			{
				return this.m_head == null;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x0002B934 File Offset: 0x00029B34
		public int Count
		{
			get
			{
				int num = 0;
				for (ConcurrentStack<T>.Node node = this.m_head; node != null; node = node.m_next)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x0002B95D File Offset: 0x00029B5D
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x0002B960 File Offset: 0x00029B60
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0002B963 File Offset: 0x00029B63
		public void Clear()
		{
			this.m_head = null;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0002B96E File Offset: 0x00029B6E
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw Error.ArgumentNull("array");
			}
			this.ToList().CopyTo(array, index);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0002B98B File Offset: 0x00029B8B
		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw Error.ArgumentNull("array");
			}
			this.ToList().CopyTo(array, arrayIndex);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0002B9A8 File Offset: 0x00029BA8
		[SecurityCritical]
		[SecurityPermission(6, Flags = 128)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw Error.ArgumentNull("info");
			}
			info.AddValue("Data", this.ToArray(), typeof(T[]));
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0002B9D4 File Offset: 0x00029BD4
		public virtual void OnDeserialization(object sender)
		{
			if (this.m_serializationInfo != null)
			{
				T[] array = this.m_serializationInfo.GetValue("Data", typeof(T[])) as T[];
				if (array == null)
				{
					throw new SerializationException("The serialization stream contains no stack data.");
				}
				ConcurrentStack<T>.Node node = null;
				for (int i = 0; i < array.Length; i++)
				{
					ConcurrentStack<T>.Node node2 = new ConcurrentStack<T>.Node(array[i]);
					if (node == null)
					{
						this.m_head = node2;
					}
					else
					{
						node.m_next = node2;
					}
					node = node2;
				}
			}
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0002BA4C File Offset: 0x00029C4C
		public void Push(T item)
		{
			SpinWait spinWait = default(SpinWait);
			ConcurrentStack<T>.Node node = new ConcurrentStack<T>.Node(item);
			for (;;)
			{
				ConcurrentStack<T>.Node head = this.m_head;
				node.m_next = head;
				if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this.m_head, node, head) == head)
				{
					break;
				}
				spinWait.SpinOnce();
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0002BA91 File Offset: 0x00029C91
		bool IConcurrentCollection<T>.Add(T item)
		{
			this.Push(item);
			return true;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0002BA9C File Offset: 0x00029C9C
		public bool TryPeek(out T result)
		{
			ConcurrentStack<T>.Node head = this.m_head;
			if (head == null)
			{
				result = default(T);
				return false;
			}
			result = head.m_value;
			return true;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0002BACC File Offset: 0x00029CCC
		public bool TryPop(out T result)
		{
			SpinWait spinWait = default(SpinWait);
			ConcurrentStack<T>.Node head;
			for (;;)
			{
				head = this.m_head;
				if (head == null)
				{
					break;
				}
				ConcurrentStack<T>.Node next = head.m_next;
				if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this.m_head, next, head) == head)
				{
					goto IL_003D;
				}
				spinWait.SpinOnce();
			}
			result = default(T);
			return false;
			IL_003D:
			result = head.m_value;
			return true;
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0002BB23 File Offset: 0x00029D23
		bool IConcurrentCollection<T>.Remove(out T item)
		{
			return this.TryPop(out item);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0002BB2C File Offset: 0x00029D2C
		public T[] ToArray()
		{
			return this.ToList().ToArray();
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0002BB3C File Offset: 0x00029D3C
		private List<T> ToList()
		{
			List<T> list = new List<T>();
			for (ConcurrentStack<T>.Node node = this.m_head; node != null; node = node.m_next)
			{
				list.Add(node.m_value);
			}
			return list;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0002BB71 File Offset: 0x00029D71
		public IEnumerator<T> GetEnumerator()
		{
			for (ConcurrentStack<T>.Node current = this.m_head; current != null; current = current.m_next)
			{
				yield return current.m_value;
			}
			yield break;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0002BB80 File Offset: 0x00029D80
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040001AE RID: 430
		private volatile ConcurrentStack<T>.Node m_head;

		// Token: 0x040001AF RID: 431
		private SerializationInfo m_serializationInfo;

		// Token: 0x02000154 RID: 340
		private class Node
		{
			// Token: 0x06000AB1 RID: 2737 RVA: 0x00030033 File Offset: 0x0002E233
			internal Node(T value)
			{
				this.m_value = value;
				this.m_next = null;
			}

			// Token: 0x0400039C RID: 924
			internal T m_value;

			// Token: 0x0400039D RID: 925
			internal ConcurrentStack<T>.Node m_next;
		}
	}
}
