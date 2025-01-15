using System;
using Microsoft.Reporting.QueryDesign.BatchQueries;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000159 RID: 345
	internal sealed class GeneratedTable
	{
		// Token: 0x06000CA4 RID: 3236 RVA: 0x000343FC File Offset: 0x000325FC
		internal GeneratedTable(QueryTable queryTable, GeneratedColumnMap columnMap)
		{
			this.m_queryTable = queryTable;
			this.m_columnMap = columnMap;
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x00034412 File Offset: 0x00032612
		public QueryTable QueryTable
		{
			get
			{
				return this.m_queryTable;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0003441A File Offset: 0x0003261A
		public GeneratedColumnMap ColumnMap
		{
			get
			{
				return this.m_columnMap;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x00034422 File Offset: 0x00032622
		// (set) Token: 0x06000CA8 RID: 3240 RVA: 0x0003442A File Offset: 0x0003262A
		internal bool ShouldCrossFilterGroupColumns
		{
			get
			{
				return this.m_shouldCrossFilterGroupColumns;
			}
			set
			{
				this.m_shouldCrossFilterGroupColumns = value;
			}
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00034433 File Offset: 0x00032633
		internal bool IsQueryExpressionEquivalent(GeneratedTable table)
		{
			return this.QueryTable.Expression.Equals(table.QueryTable.Expression);
		}

		// Token: 0x0400064B RID: 1611
		private readonly QueryTable m_queryTable;

		// Token: 0x0400064C RID: 1612
		private readonly GeneratedColumnMap m_columnMap;

		// Token: 0x0400064D RID: 1613
		private bool m_shouldCrossFilterGroupColumns;
	}
}
