using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.Reporting.QueryDesign.BatchQueries;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000137 RID: 311
	internal sealed class BatchQueryExpressionReferenceContext
	{
		// Token: 0x06000B96 RID: 2966 RVA: 0x0002E7E8 File Offset: 0x0002C9E8
		internal BatchQueryExpressionReferenceContext()
		{
			this.m_referenceTables = new Stack<GeneratedTable>();
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0002E7FB File Offset: 0x0002C9FB
		public bool TryGetAvailableColumn(ExpressionId expressionId, out QueryTableColumn availableColumn)
		{
			availableColumn = null;
			return this.HasReferenceTable && this.ReferenceTable.ColumnMap.TryGetColumn(expressionId, out availableColumn);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0002E821 File Offset: 0x0002CA21
		public bool TryGetAvailableColumn(string planColumnName, out QueryTableColumn availableColumn)
		{
			availableColumn = null;
			return this.HasReferenceTable && this.ReferenceTable.ColumnMap.TryGetColumn(planColumnName, out availableColumn);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0002E847 File Offset: 0x0002CA47
		public void PushReferenceTable(GeneratedTable table)
		{
			this.m_referenceTables.Push(table);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0002E855 File Offset: 0x0002CA55
		public void PopReferenceTable()
		{
			this.m_referenceTables.Pop();
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0002E863 File Offset: 0x0002CA63
		private bool HasReferenceTable
		{
			get
			{
				return this.m_referenceTables.Count > 0;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x0002E873 File Offset: 0x0002CA73
		public GeneratedTable ReferenceTable
		{
			get
			{
				return this.m_referenceTables.Peek();
			}
		}

		// Token: 0x040005DA RID: 1498
		private readonly Stack<GeneratedTable> m_referenceTables;
	}
}
