using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration
{
	// Token: 0x0200004F RID: 79
	internal sealed class NestedQueryCollection
	{
		// Token: 0x0600039E RID: 926 RVA: 0x000111E1 File Offset: 0x0000F3E1
		internal int Add(SqlNestedQuery item)
		{
			this.m_list.Add(item);
			return this.m_list.Count - 1;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000111FC File Offset: 0x0000F3FC
		internal void ReplaceAt(int index, SqlNestedQuery replacementQuery)
		{
			this.m_list[index] = replacementQuery;
		}

		// Token: 0x17000085 RID: 133
		internal SqlNestedQuery this[int index]
		{
			get
			{
				return this.m_list[index];
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00011219 File Offset: 0x0000F419
		internal int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00011226 File Offset: 0x0000F426
		internal bool Contains(SqlNestedQuery query)
		{
			return this.m_list.Contains(query);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00011234 File Offset: 0x0000F434
		public IEnumerator<SqlNestedQuery> GetEnumerator()
		{
			return this.m_list.GetEnumerator();
		}

		// Token: 0x040001B5 RID: 437
		private readonly List<SqlNestedQuery> m_list = new List<SqlNestedQuery>();
	}
}
