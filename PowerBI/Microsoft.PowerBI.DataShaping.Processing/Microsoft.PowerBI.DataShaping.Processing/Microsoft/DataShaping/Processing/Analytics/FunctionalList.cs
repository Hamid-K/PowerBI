using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.DataShaping.Processing.Analytics
{
	// Token: 0x020000B3 RID: 179
	[ImmutableObject(true)]
	internal sealed class FunctionalList<T>
	{
		// Token: 0x0600049A RID: 1178 RVA: 0x0000DF59 File Offset: 0x0000C159
		internal FunctionalList(IReadOnlyList<T> head)
			: this(head, null)
		{
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000DF63 File Offset: 0x0000C163
		private FunctionalList(IReadOnlyList<T> head, FunctionalList<T> tail)
		{
			this._head = head;
			this._tail = tail;
			this._startIndex = ((tail != null) ? tail.Count : 0);
			this._count = head.Count + this._startIndex;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000DF9E File Offset: 0x0000C19E
		internal T GetItem(int index)
		{
			if (index >= this._startIndex)
			{
				return this._head[index - this._startIndex];
			}
			return this._tail.GetItem(index);
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x0000DFC9 File Offset: 0x0000C1C9
		internal int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000DFD1 File Offset: 0x0000C1D1
		internal FunctionalList<T> Append(IReadOnlyList<T> items)
		{
			return new FunctionalList<T>(items, this);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000DFDA File Offset: 0x0000C1DA
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x04000259 RID: 601
		private readonly IReadOnlyList<T> _head;

		// Token: 0x0400025A RID: 602
		private readonly FunctionalList<T> _tail;

		// Token: 0x0400025B RID: 603
		private readonly int _count;

		// Token: 0x0400025C RID: 604
		private readonly int _startIndex;
	}
}
