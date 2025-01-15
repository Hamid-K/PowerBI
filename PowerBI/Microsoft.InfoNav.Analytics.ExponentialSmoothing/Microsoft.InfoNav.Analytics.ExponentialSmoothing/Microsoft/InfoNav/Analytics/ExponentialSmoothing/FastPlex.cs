using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Analytics.ExponentialSmoothing
{
	// Token: 0x02000005 RID: 5
	internal class FastPlex<T> where T : class
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002067 File Offset: 0x00000267
		internal FastPlex(int maxSize)
		{
			this.buffer = new List<T>(maxSize);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000207B File Offset: 0x0000027B
		internal static FastPlex<T> PplNew(int ifooMaxNew = 0)
		{
			return new FastPlex<T>(ifooMaxNew);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002084 File Offset: 0x00000284
		internal static void FreePpl(FastPlex<T> pplfoo)
		{
			if (pplfoo != null)
			{
				List<T> list = pplfoo.buffer;
				for (int i = 0; i < pplfoo.buffer.Count; i++)
				{
					list[i] = default(T);
				}
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020C1 File Offset: 0x000002C1
		internal void DeleteLastItem()
		{
			if (this.buffer.Count >= 1)
			{
				this.buffer.RemoveAt(this.buffer.Count - 1);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020E9 File Offset: 0x000002E9
		internal int HrAddItem(T pfoo)
		{
			this.buffer.Add(pfoo);
			return 0;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020F8 File Offset: 0x000002F8
		internal void Sort(IComparer<T> pfnComp)
		{
			this.buffer.Sort(pfnComp);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002106 File Offset: 0x00000306
		internal int GetCount()
		{
			return this.buffer.Count;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002113 File Offset: 0x00000313
		internal T PFromI(int ifoo)
		{
			if (ifoo >= this.GetCount())
			{
				throw new ArgumentException("ifoo >= GetCount()");
			}
			return this.buffer[ifoo];
		}

		// Token: 0x04000029 RID: 41
		private List<T> buffer;
	}
}
