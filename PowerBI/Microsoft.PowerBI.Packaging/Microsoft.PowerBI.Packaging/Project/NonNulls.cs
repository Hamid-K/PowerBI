using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x02000068 RID: 104
	[JsonArray(AllowNullItems = false)]
	public class NonNulls<T> : IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable where T : class
	{
		// Token: 0x060002D2 RID: 722 RVA: 0x00008474 File Offset: 0x00006674
		public NonNulls(IEnumerable<T> values)
		{
			if (values == null)
			{
				return;
			}
			foreach (T t in values)
			{
				if (t == null)
				{
					throw new ArgumentException("Can't construct NonNullables. Values have null.");
				}
				this.values.Add(t);
			}
		}

		// Token: 0x170000D1 RID: 209
		public T this[int index]
		{
			get
			{
				return this.values[index];
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x000084FA File Offset: 0x000066FA
		public int Count
		{
			get
			{
				return this.values.Count;
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00008507 File Offset: 0x00006707
		public IEnumerator<T> GetEnumerator()
		{
			return this.values.GetEnumerator();
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00008519 File Offset: 0x00006719
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.values.GetEnumerator();
		}

		// Token: 0x0400016D RID: 365
		private readonly List<T> values = new List<T>();
	}
}
