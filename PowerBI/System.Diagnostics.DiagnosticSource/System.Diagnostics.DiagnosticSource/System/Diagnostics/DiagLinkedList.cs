using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Diagnostics
{
	// Token: 0x02000029 RID: 41
	internal sealed class DiagLinkedList<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600015E RID: 350 RVA: 0x00005C6F File Offset: 0x00003E6F
		public DiagLinkedList()
		{
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005C78 File Offset: 0x00003E78
		public DiagLinkedList(T firstValue)
		{
			this._last = (this._first = new DiagNode<T>(firstValue));
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00005CA0 File Offset: 0x00003EA0
		public DiagLinkedList(IEnumerator<T> e)
		{
			this._last = (this._first = new DiagNode<T>(e.Current));
			while (e.MoveNext())
			{
				T t = e.Current;
				this._last.Next = new DiagNode<T>(t);
				this._last = this._last.Next;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00005CFE File Offset: 0x00003EFE
		public DiagNode<T> First
		{
			get
			{
				return this._first;
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00005D08 File Offset: 0x00003F08
		public void Clear()
		{
			lock (this)
			{
				this._first = (this._last = null);
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00005D50 File Offset: 0x00003F50
		private void UnsafeAdd(DiagNode<T> newNode)
		{
			if (this._first == null)
			{
				this._last = newNode;
				this._first = newNode;
				return;
			}
			this._last.Next = newNode;
			this._last = newNode;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005D8C File Offset: 0x00003F8C
		public void Add(T value)
		{
			DiagNode<T> diagNode = new DiagNode<T>(value);
			lock (this)
			{
				this.UnsafeAdd(diagNode);
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00005DD0 File Offset: 0x00003FD0
		public bool AddIfNotExist(T value, Func<T, T, bool> compare)
		{
			bool flag2;
			lock (this)
			{
				for (DiagNode<T> diagNode = this._first; diagNode != null; diagNode = diagNode.Next)
				{
					if (compare(value, diagNode.Value))
					{
						return false;
					}
				}
				DiagNode<T> diagNode2 = new DiagNode<T>(value);
				this.UnsafeAdd(diagNode2);
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00005E40 File Offset: 0x00004040
		public T Remove(T value, Func<T, T, bool> compare)
		{
			T t;
			lock (this)
			{
				DiagNode<T> diagNode = this._first;
				if (diagNode == null)
				{
					t = default(T);
					t = t;
				}
				else if (compare(diagNode.Value, value))
				{
					this._first = diagNode.Next;
					if (this._first == null)
					{
						this._last = null;
					}
					t = diagNode.Value;
				}
				else
				{
					for (DiagNode<T> diagNode2 = diagNode.Next; diagNode2 != null; diagNode2 = diagNode2.Next)
					{
						if (compare(diagNode2.Value, value))
						{
							diagNode.Next = diagNode2.Next;
							if (this._last == diagNode2)
							{
								this._last = diagNode;
							}
							return diagNode2.Value;
						}
						diagNode = diagNode2;
					}
					t = default(T);
				}
			}
			return t;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00005F1C File Offset: 0x0000411C
		public void AddFront(T value)
		{
			DiagNode<T> diagNode = new DiagNode<T>(value);
			lock (this)
			{
				diagNode.Next = this._first;
				this._first = diagNode;
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005F6C File Offset: 0x0000416C
		public Enumerator<T> GetEnumerator()
		{
			return new Enumerator<T>(this._first);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005F79 File Offset: 0x00004179
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005F86 File Offset: 0x00004186
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000082 RID: 130
		private DiagNode<T> _first;

		// Token: 0x04000083 RID: 131
		private DiagNode<T> _last;
	}
}
