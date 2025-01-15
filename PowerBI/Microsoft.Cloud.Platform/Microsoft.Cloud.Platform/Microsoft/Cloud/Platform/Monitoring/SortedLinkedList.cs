using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x02000092 RID: 146
	internal class SortedLinkedList<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000417 RID: 1047 RVA: 0x0000EDE3 File Offset: 0x0000CFE3
		public SortedLinkedList()
			: this(Comparer<T>.Default)
		{
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000EDF0 File Offset: 0x0000CFF0
		public SortedLinkedList([NotNull] IComparer<T> comparer)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IComparer<T>>(comparer, "comparer");
			this.m_linkedList = new LinkedList<T>();
			this.m_comparer = comparer;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000EE18 File Offset: 0x0000D018
		public void AddLast(T value)
		{
			LinkedListNode<T> linkedListNode = this.m_linkedList.Last;
			while (linkedListNode != null && this.m_comparer.Compare(linkedListNode.Value, value) > 0)
			{
				linkedListNode = linkedListNode.Previous;
			}
			if (linkedListNode == null)
			{
				this.m_linkedList.AddFirst(value);
				return;
			}
			this.m_linkedList.AddAfter(linkedListNode, value);
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000EE71 File Offset: 0x0000D071
		public int Count
		{
			get
			{
				return this.m_linkedList.Count;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000EE7E File Offset: 0x0000D07E
		public T First
		{
			get
			{
				ExtendedDiagnostics.EnsureOperation(this.m_linkedList.Count > 0, "Cannot get first item of an empty list.");
				return this.m_linkedList.First.Value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000EEA8 File Offset: 0x0000D0A8
		public T Last
		{
			get
			{
				ExtendedDiagnostics.EnsureOperation(this.m_linkedList.Count > 0, "Cannot get last item of an empty list.");
				return this.m_linkedList.Last.Value;
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000EED2 File Offset: 0x0000D0D2
		public void RemoveFirst()
		{
			ExtendedDiagnostics.EnsureOperation(this.m_linkedList.Count > 0, "Cannot remove first item from an empty list.");
			this.m_linkedList.RemoveFirst();
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000EEF7 File Offset: 0x0000D0F7
		public IEnumerator<T> GetEnumerator()
		{
			return this.m_linkedList.GetEnumerator();
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000EF09 File Offset: 0x0000D109
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400015E RID: 350
		private readonly LinkedList<T> m_linkedList;

		// Token: 0x0400015F RID: 351
		private readonly IComparer<T> m_comparer;
	}
}
