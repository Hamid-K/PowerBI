using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform
{
	// Token: 0x0200000F RID: 15
	public sealed class DoublyLinkedList<TNode> : IEnumerable<TNode>, IEnumerable
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002E49 File Offset: 0x00001049
		public DoublyLinkedList(int maxLength)
		{
			this.m_maxLength = maxLength;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002E58 File Offset: 0x00001058
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002E60 File Offset: 0x00001060
		public DoublyLinkedList<TNode>.Node Head { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002E69 File Offset: 0x00001069
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002E71 File Offset: 0x00001071
		public DoublyLinkedList<TNode>.Node Tail { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002E7A File Offset: 0x0000107A
		public int Length
		{
			get
			{
				return this.m_length;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002E84 File Offset: 0x00001084
		public DoublyLinkedList<TNode>.Node AddToHead(DoublyLinkedList<TNode>.Node n)
		{
			DoublyLinkedList<TNode>.Node head = this.Head;
			this.Head = n;
			this.Head.Previous = null;
			if (head == null)
			{
				this.Tail = this.Head;
				this.Head.Next = null;
				this.m_length++;
				return null;
			}
			head.Previous = n;
			this.Head.Next = head;
			if (this.m_length == this.m_maxLength)
			{
				DoublyLinkedList<TNode>.Node tail = this.Tail;
				this.Tail = this.Tail.Previous;
				this.Tail.Next = null;
				tail.Previous = null;
				return tail;
			}
			this.m_length++;
			return null;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002F34 File Offset: 0x00001134
		public void MoveToHead(DoublyLinkedList<TNode>.Node n)
		{
			if (this.Head == n)
			{
				return;
			}
			if (this.Tail == n)
			{
				this.Tail = n.Previous;
				this.Tail.Next = null;
				this.Head.Previous = n;
				n.Next = this.Head;
				this.Head = n;
				this.Head.Previous = null;
				return;
			}
			n.Next.Previous = n.Previous;
			n.Previous.Next = n.Next;
			this.Head.Previous = n;
			n.Next = this.Head;
			this.Head = n;
			this.Head.Previous = null;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002FE8 File Offset: 0x000011E8
		public void Remove(DoublyLinkedList<TNode>.Node n)
		{
			if (this.Tail == n)
			{
				if (this.Head == n)
				{
					this.Head = null;
					this.Tail = null;
					n.Previous = null;
					n.Next = null;
					this.m_length--;
					return;
				}
				this.Tail = this.Tail.Previous;
				this.Tail.Next = null;
				n.Previous = null;
				n.Next = null;
				this.m_length--;
				return;
			}
			else
			{
				if (this.Head == n)
				{
					this.Head = n.Next;
					this.Head.Previous = null;
					n.Previous = null;
					n.Next = null;
					this.m_length--;
					return;
				}
				n.Previous.Next = n.Next;
				n.Next.Previous = n.Previous;
				n.Previous = null;
				n.Next = null;
				this.m_length--;
				return;
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000030E8 File Offset: 0x000012E8
		public IEnumerator<TNode> GetEnumerator()
		{
			for (DoublyLinkedList<TNode>.Node current = this.Head; current != null; current = current.Next)
			{
				yield return current.Value;
			}
			yield break;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000030F7 File Offset: 0x000012F7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400003F RID: 63
		private readonly int m_maxLength;

		// Token: 0x04000040 RID: 64
		private int m_length;

		// Token: 0x02000578 RID: 1400
		public sealed class Node
		{
			// Token: 0x06002A5E RID: 10846 RVA: 0x000983A4 File Offset: 0x000965A4
			public Node(TNode value)
			{
				this.Value = value;
			}

			// Token: 0x170006D1 RID: 1745
			// (get) Token: 0x06002A5F RID: 10847 RVA: 0x000983B3 File Offset: 0x000965B3
			// (set) Token: 0x06002A60 RID: 10848 RVA: 0x000983BB File Offset: 0x000965BB
			public DoublyLinkedList<TNode>.Node Previous { get; internal set; }

			// Token: 0x170006D2 RID: 1746
			// (get) Token: 0x06002A61 RID: 10849 RVA: 0x000983C4 File Offset: 0x000965C4
			// (set) Token: 0x06002A62 RID: 10850 RVA: 0x000983CC File Offset: 0x000965CC
			public DoublyLinkedList<TNode>.Node Next { get; internal set; }

			// Token: 0x170006D3 RID: 1747
			// (get) Token: 0x06002A63 RID: 10851 RVA: 0x000983D5 File Offset: 0x000965D5
			// (set) Token: 0x06002A64 RID: 10852 RVA: 0x000983DD File Offset: 0x000965DD
			public TNode Value { get; internal set; }
		}
	}
}
