using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000089 RID: 137
	[Serializable]
	public sealed class IndexableStack<T>
	{
		// Token: 0x060005F8 RID: 1528 RVA: 0x00021D4C File Offset: 0x0001FF4C
		public IndexableStack()
		{
			this.stack = new List<T>();
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00021D5F File Offset: 0x0001FF5F
		public int Count
		{
			get
			{
				return this.stack.Count;
			}
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00021D6C File Offset: 0x0001FF6C
		public void Clear()
		{
			this.stack.Clear();
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00021D79 File Offset: 0x0001FF79
		public void Push(T t)
		{
			this.stack.Add(t);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00021D88 File Offset: 0x0001FF88
		public T Pop()
		{
			int num = this.stack.Count - 1;
			T t = this.stack[num];
			this.stack.RemoveAt(num);
			return t;
		}

		// Token: 0x170000DE RID: 222
		public T this[int idx]
		{
			get
			{
				return this.stack[idx];
			}
		}

		// Token: 0x0400011E RID: 286
		private List<T> stack;
	}
}
