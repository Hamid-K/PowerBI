using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x0200015C RID: 348
	internal sealed class QueryItemCollection<T> : IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable where T : class
	{
		// Token: 0x06000CB1 RID: 3249 RVA: 0x00034745 File Offset: 0x00032945
		internal QueryItemCollection()
		{
			this.m_usedNames = new HashSet<string>(QueryNamingContext.NameComparer);
			this.m_items = new List<T>();
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x00034768 File Offset: 0x00032968
		public int Count
		{
			get
			{
				return this.m_items.Count;
			}
		}

		// Token: 0x170001F3 RID: 499
		public T this[int index]
		{
			get
			{
				return this.m_items[index];
			}
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00034783 File Offset: 0x00032983
		public void Add(string name, T item)
		{
			if (this.m_usedNames.Add(name))
			{
				this.m_items.Add(item);
			}
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0003479F File Offset: 0x0003299F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x000347A7 File Offset: 0x000329A7
		public IEnumerator<T> GetEnumerator()
		{
			return this.m_items.GetEnumerator();
		}

		// Token: 0x04000652 RID: 1618
		private readonly HashSet<string> m_usedNames;

		// Token: 0x04000653 RID: 1619
		private readonly List<T> m_items;
	}
}
