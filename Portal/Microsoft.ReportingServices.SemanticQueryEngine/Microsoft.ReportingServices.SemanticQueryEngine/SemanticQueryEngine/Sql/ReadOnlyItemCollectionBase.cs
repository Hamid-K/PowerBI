using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql
{
	// Token: 0x02000018 RID: 24
	internal class ReadOnlyItemCollectionBase<T>
	{
		// Token: 0x1700002F RID: 47
		internal T this[int index]
		{
			get
			{
				return this.m_list[index];
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00006AF6 File Offset: 0x00004CF6
		internal int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00006B03 File Offset: 0x00004D03
		internal void Add(T item)
		{
			this.Insert(this.m_list.Count, item);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00006B17 File Offset: 0x00004D17
		internal virtual void Insert(int index, T item)
		{
			this.m_list.Insert(index, item);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006B26 File Offset: 0x00004D26
		internal bool Contains(T item)
		{
			return this.m_list.Contains(item);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00006B34 File Offset: 0x00004D34
		public IEnumerator<T> GetEnumerator()
		{
			return this.m_list.GetEnumerator();
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006B46 File Offset: 0x00004D46
		internal void Sort(IComparer<T> comparer)
		{
			this.m_list.Sort(comparer);
		}

		// Token: 0x04000066 RID: 102
		private readonly List<T> m_list = new List<T>();
	}
}
